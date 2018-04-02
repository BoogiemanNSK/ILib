using I2P_Project.Classes.UserSystem;
using I2P_Project.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace I2P_Project.Classes
{
    /// <summary>
    /// Class for managing library.
    /// Here are methods that are acessed by the system mostly, not by any users.
    /// </summary>
    class Library
    {
        private LMSDataBase db;
        // Map from docID to it's Priority queue for docs
        private Dictionary<int, PriorityQueue<int>> queueMap;

        /// <summary> Initializing DB </summary>
        public Library()
        {            
            db = new LMSDataBase(SDM.Strings.CONNECTION_STRING);
            queueMap = new Dictionary<int, PriorityQueue<int>>();
            ConnectToDB(db);
        }

        /// <summary> Connecting to Data Base </summary>
        /// <param name="db"></param>
        public void ConnectToDB(LMSDataBase db)
        {
            // Trying to connect to Azure cloud database
            try
            {
                db.SubmitChanges();
            }
            // If connection failed, establishing a local DB
            catch
            {
                string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string path = (Path.GetDirectoryName(executable));
                string connString = path + SDM.Strings.DB_RELATIVE_PATH;

                db = new LMSDataBase(connString);
            
                Directory.CreateDirectory(SDM.Strings.DB_DIRECTORY_NAME);
                if (!File.Exists(connString))
                {
                    db.CreateDatabase();
                    GenerateTestDB();
                }

                db.SubmitChanges();
            }
        }

        #region DB Addition

        /// <summary> Registers new user in data base </summary>
        public bool RegisterUser(string login, string password, string name, string adress, string phone, bool isLibrarian)
        {
            if (CheckLogin(login)) return false;

            using (System.Security.Cryptography.MD5 md5_hash = System.Security.Cryptography.MD5.Create())
            {
                Cryptography cpt = new Cryptography();
                password = cpt.GetHash(md5_hash, password);  // hashing password string by MD5
            }

            Users newUser = new Users
            {
                Login = login,
                Password = password,
                Name = name,
                Address = adress,
                PhoneNumber = phone,
                UserType = (isLibrarian ? 5 : 0) // TODO Замнить на enum
            };
            db.Users.InsertOnSubmit(newUser);
            db.SubmitChanges();
            return true;
        }

        /// <summary> Registers new document in the system </summary>
        public void AddDoc(string title, string description, int docType, int price, bool isBestseller)
        {
            bool isReference = !CheckReference(title);
            DataBase.Document newDoc = new DataBase.Document();
            newDoc.Title = title;
            newDoc.Description = description;
            newDoc.Price = price;
            newDoc.DocType = docType;
            newDoc.IsReference = isReference;
            newDoc.IsBestseller = isBestseller;
            db.Documents.InsertOnSubmit(newDoc);
            db.SubmitChanges();
        }

        public void AddBook(string title, string Autors, string Publisher, int PublishYear, string Edition, string description, int docType, int price, bool isBestseller)
        {
            bool isReference = !CheckReference(title);
            DataBase.Document newDoc = new DataBase.Document();
            newDoc.Title = title;
            newDoc.Autors = Autors;
            newDoc.Publisher = Publisher;
            newDoc.PublishYear = PublishYear;
            newDoc.Edition = Edition;
            newDoc.Description = description;
            newDoc.Price = price;
            newDoc.DocType = 0;
            newDoc.IsReference = isReference;
            newDoc.IsBestseller = isBestseller;
            db.Documents.InsertOnSubmit(newDoc);
            db.SubmitChanges();
        }

        public void AddAV(string title, string Autors,string description, int price)
        {
            DataBase.Document newDoc = new DataBase.Document();
            newDoc.Title = title;
            newDoc.Autors = Autors;
            newDoc.Description = description;
            newDoc.Price = price;
            newDoc.DocType = 2;
            newDoc.IsReference = false;
            newDoc.IsBestseller = false;
            db.Documents.InsertOnSubmit(newDoc);
            db.SubmitChanges();
        }

        /// <summary>
        /// First generate for show functionality
        /// </summary>
        private void GenerateTestDB()
        {
            //Not referense books
            for (int i = 0; i < 2; i++)
            {
                AddBook
                    (
                        "Introduction to Algorithms",
                        "Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest and Clifford Stein",
                        "MIT Press",
                        2009,
                        "Third Edition",
                        "Alghorithm techniques and design",
                        0,
                        1800,
                        false
                    );
                AddBook
                    (
                        "Design Patterns: Elements of Reusable Object-Oriented Software",
                        "Erich Gamma, Ralph Johnson, John Vlissides, Richard Helm",
                        "Addison-Wesley Professional",
                        2003,
                        "First Edition",
                        "Programm patterns, how to programm well w/o headache",
                        0,
                        2000,
                        true
                    );
            }
            //Reference book
            AddBook("The Mythical Man-month", "Brooks,Jr., Frederick P", 
                "Addison-Wesley Longman Publishing Co., Inc.", 1995,
                "Second edition", "How to do everything and live better", 
                0, 800,false);
            AddAV("Null References: The Billion Dollar Mistake", "Tony Hoare", "Some AV", 400);
            AddAV("Information Entropy", "Claude Shannon", "Another AV", 700);
            RegisterUser("p1", "p1", "Sergey Afonso", "Via Margutta, 3", "30001", false);
            RegisterUser("p2", "p2", "Nadia Teixeira", "Via Sacra, 13", "30002", false);
            RegisterUser("p3", "p3", "Elvira Espindola", "Via del Corso, 22", "30003", false);
            //Special for me
            RegisterUser("zhychek1@yandex.ru", "lolcore", "Toha", "1-312", "+79648350370", true);
            
        }

        #endregion

        #region DB Deletion

        /// <summary> Deletes registered user from the system </summary>
        public void RemoveUser(int patronID)
        {
            // Deleting user
            var user_to_remove = (from d in db.Users
                                    where d.Id == patronID
                                    select d).Single();
            db.Users.DeleteOnSubmit(user_to_remove);

            // Deleting user`s checkouts
            var checkouts_to_remove = (from c in db.Checkouts
                                       where c.UserID == patronID
                                       select c);
            db.Checkouts.DeleteAllOnSubmit(checkouts_to_remove);

            db.SubmitChanges();
        }

        /// <summary> Deletes registered doc from the system by ID </summary>
        internal bool RemoveDocument(int doc_id)
        {
            var record_to_remove = (from d in db.Documents
                                    where (d.Id == doc_id)
                                    select d).Single();
            if (record_to_remove.IsReference)
            {
                var check_copy = (from d in db.Documents
                                  where (d.Id != record_to_remove.Id && d.Title.Equals(record_to_remove.Title))
                                  select d);
                if (check_copy.Any())
                    return false;
            }
            db.Documents.DeleteOnSubmit(record_to_remove);
            db.SubmitChanges();
            return true;

        }

        /// <summary> Deletes registered doc from the system by Title </summary>
        internal void RemoveDocument(string Title)
        {
            var record_to_remove = (from d in db.Documents
                                    where (d.Title.Equals(Title) && d.IsReference == false)
                                    select d).FirstOrDefault();
            if (record_to_remove == null)
                record_to_remove = (from d in db.Documents where d.Title.Equals(Title) && d.IsReference select d).Single();
            db.Documents.DeleteOnSubmit(record_to_remove);
            db.SubmitChanges();
        }

        /// <summary> Clears DB (for test cases only) </summary>
        public void ClearDB()
        {
            db.ExecuteCommand("DELETE FROM documents");
            db.ExecuteCommand("DELETE FROM users");
            db.ExecuteCommand("DELETE FROM checkouts");
        }

        #endregion

        #region DB Updating

        /// <summary> Updates document info </summary>
        public void ModifyDoc(int DocID, string Title, string Description, string Price, bool IsBestseller, int DocType)
        {
            var doc = (from d in db.Documents
                       where d.Id == DocID
                       select d).Single();
            var copy = (from d in db.Documents
                        where d.Title == doc.Title
                        select d);

            foreach (DataBase.Document docs in copy)
            {
                docs.Title = Title;
                docs.Description = Description;
                docs.Price = Convert.ToInt32(Price);
                docs.IsBestseller = IsBestseller;
                docs.DocType = DocType;
                db.SubmitChanges();
            }
        }

        /// <summary> Updates user info </summary>
        public void UpdateUser(int userId, string userName, string userAdress, string userPhoneNumber, int userType)
        {
            Users user = GetUser(userId);
            user.Name = userName;
            user.Address = userAdress;
            user.PhoneNumber = userPhoneNumber;
            user.UserType = userType;
            db.SubmitChanges();
        }

        #endregion

        #region DB Output

        /// <summary>
        /// Returns collection of current logged user books
        /// Used in MyBooks page, where user can view and return his docs 
        /// </summary>
        public ObservableCollection<Pages.MyBooksTable> GetUserBooks()
        {
            ObservableCollection<Pages.MyBooksTable> temp_table = new ObservableCollection<Pages.MyBooksTable>();
            var load_user_books = from c in db.Checkouts
                                  join b in db.Documents on c.BookID equals b.Id
                                  where c.UserID == SDM.CurrentUser.PersonID && c.IsReturned == false
                                  select new
                                  {
                                      c.CheckID,
                                      c.BookID,
                                      b.Title,
                                      c.DateTaked,
                                      c.TimeToBack
                                  };
            foreach (var element in load_user_books)
            {
                Pages.MyBooksTable row = new Pages.MyBooksTable
                {
                    checkID = element.CheckID,
                    bookID = element.BookID,
                    b_title = element.Title,
                    c_dateTaked = (DateTime)element.DateTaked,
                    c_timeToBack = element.TimeToBack
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        /// <summary>
        /// Return overdued docs for particular user
        /// Used in OverdueInfo page, where librarian can view some patron`s overdued books
        /// </summary>
        public ObservableCollection<Pages.OverdueInfoTable> OverdueInfo(int userID)
        {
            ObservableCollection<Pages.OverdueInfoTable> temp_table = new ObservableCollection<Pages.OverdueInfoTable>();
            var load_user_books = from c in db.Checkouts
                                  join b in db.Documents on c.BookID equals b.Id
                                  where c.UserID == userID
                                  select new
                                  {
                                      b.Id,
                                      b.Title,
                                      b.IsReference,
                                      b.DocType,
                                      c.DateTaked,
                                      c.TimeToBack,
                                      b.Price
                                  };
            foreach (var element in load_user_books)
            {
                int passedDays = (int)DateTime.Now.Subtract(element.TimeToBack).TotalDays;
                if (passedDays > 0)
                {
                    Pages.OverdueInfoTable row = new Pages.OverdueInfoTable
                    {
                        docID = element.Id,
                        docTitle = element.Title,
                        isReference = element.IsReference,
                        docType = SDM.Strings.DOC_TYPES[element.DocType],
                        dateTaked = (DateTime)element.DateTaked,
                        timeToBack = element.TimeToBack,
                        fine = (passedDays * 50 > element.Price ?
                            element.Price : passedDays * 50)
                    };
                    temp_table.Add(row);
                }
            }
            return temp_table;
        }

        /// <summary>
        /// Returns collection of all patrons only
        /// Used in UserManagementPage so that librarian could see list of all patrons
        /// </summary>
        public ObservableCollection<Pages.LibrarianUserView> LibrarianViewUserTable()
        {
            ObservableCollection<Pages.LibrarianUserView> temp_table = new ObservableCollection<Pages.LibrarianUserView>();
            var load_users = from p in db.Users
                                  where p.UserType != 5 // TODO Заменить на enum
                                  select new
                                  {
                                      p.Id,
                                      p.Login
                                  };
            foreach (var element in load_users)
            {
                Pages.LibrarianUserView row = new Pages.LibrarianUserView
                {
                    userID = element.Id,
                    userLogin = element.Login,
                    docsNumber = GetUserBooksNumber(element.Id),
                    userFine = GetUserFine(element.Id)
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        // [FOR TEST]
        /// <summary> Returns a collection of books checked by particular user </summary>
        public ObservableCollection<Pages.DocumentsTable> TestDocsTableUsersBooks(int user_id)
        {
            ObservableCollection<Pages.DocumentsTable> temp_table = new ObservableCollection<Pages.DocumentsTable>();
            var load_user_docs = from c in db.Checkouts
                                 join b in db.Documents on c.BookID equals b.Id
                                 where c.UserID == user_id
                                 select new
                                 {
                                     c.BookID,
                                     b.Title,
                                     b.DocType,
                                     c.DateTaked,
                                     c.TimeToBack
                                 };
            foreach (var element in load_user_docs)
            {
                Pages.DocumentsTable row = new Pages.DocumentsTable
                {
                    docID = element.BookID,
                    docOwnerID = user_id,
                    docTitle = element.Title,
                    docType = SDM.Strings.DOC_TYPES[element.DocType],
                    dateTaked = (DateTime)element.DateTaked,
                    timeToBack = element.TimeToBack
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        // [FOR TEST]
        /// <summary> Returns a collection of all users registered in system </summary>
        public ObservableCollection<Pages.UserTable> TestUsersTable()
        {
            ObservableCollection<Pages.UserTable> temp_table = new ObservableCollection<Pages.UserTable>();
            var load_users = from u in db.Users
                             select new
                             {
                                 u.Id,
                                 u.Name,
                                 u.Address,
                                 u.PhoneNumber,
                                 u.UserType
                             };
            foreach (var element in load_users)
            {
                Pages.UserTable row = new Pages.UserTable
                {
                    userID = element.Id,
                    userName = element.Name,
                    userAddress = element.Address,
                    userPhoneNumber = element.PhoneNumber,
                    userType = SDM.Strings.USER_TYPES[element.UserType]
                };
                temp_table.Add(row);
            }
            return temp_table;
        }
        
        /// <summary>
        /// Return a list of all docs registered in system
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Pages.DocumentsTable> GetDocsTableForLibrarian()
        {
            ObservableCollection<Pages.DocumentsTable> temp_table = new ObservableCollection<Pages.DocumentsTable>();
            var load_user_docs = from b in db.Documents
                                 select new
                                 {
                                     b.Id,
                                     b.Title,
                                     b.DocType,
                                     b.IsReference
                                 };
            foreach (var element in load_user_docs)
            {
                Checkouts checkoutInfo = GetOwnerInfo(element.Id);
                Pages.DocumentsTable row = new Pages.DocumentsTable
                {
                    docID = element.Id,
                    docTitle = element.Title,
                    docType = SDM.Strings.DOC_TYPES[element.DocType],
                    docOwnerID = checkoutInfo == null ? -1 : checkoutInfo.UserID,
                    dateTaked = checkoutInfo == null ? DateTime.Now : (System.DateTime)checkoutInfo.DateTaked,
                    timeToBack = checkoutInfo == null ? DateTime.Now : (System.DateTime)checkoutInfo.TimeToBack,
                    isReference = element.IsReference
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        public ObservableCollection<Pages.UserDocsTable> GetUserDocsFromLibrarian(int patronID)
        {
            ObservableCollection<Pages.UserDocsTable> temp_table = new ObservableCollection<Pages.UserDocsTable>();
            var load_user_books = from c in db.Checkouts where c.UserID == patronID
                                  join b in db.Documents on c.BookID equals b.Id
                                  select new
                                  {
                                      b.Title,
                                      b.DocType,
                                      c.DateTaked,
                                      c.TimeToBack
                                  };
            foreach (var element in load_user_books)
            {
                Pages.UserDocsTable row = new Pages.UserDocsTable
                {
                    DocTitle = element.Title,
                    DocType = SDM.Strings.DOC_TYPES[element.DocType],
                    DateTaked = (DateTime)element.DateTaked,
                    DeadLine = element.TimeToBack
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        /// <summary> Returns all non-reference docs </summary>
        public ObservableCollection<Pages.LibraryTable> GetAllDocs() // сюда можно засунуть вывод по userType
        {
            var test = (from p in db.Documents select p);
            ObservableCollection<Pages.LibraryTable> temp_table = new ObservableCollection<Pages.LibraryTable>();
            var load_user_books = from d in db.Documents
                                  where (!d.IsReference) && (!d.IsBestseller)
                                  select new
                                  {
                                      d.Id,
                                      d.Title,
                                      d.Autors,
                                      d.Publisher,
                                      d.PublishYear,
                                      d.Price
                                  };
            foreach (var element in load_user_books)
            {
                Pages.LibraryTable row = new Pages.LibraryTable
                {
                    bookID = element.Id,
                    book_image = Directory.GetCurrentDirectory() + @"\media\source_images\book_default.png",
                    title = element.Title,
                    author = element.Autors,
                    publisher = element.Publisher,
                    publish_year = element.PublishYear,
                    price = element.Price
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        #endregion

        #region DB Existence Check

        /// <summary> Checks if there exist a user with given login </summary>
        public bool CheckLogin(string login)
        {
            var test = (from p in db.Users
                        where p.Login == login
                        select p);
            return test.Any();
        }

        /// <summary> Checks if a user with given e-mail has given password </summary>
        public bool CheckPassword(string login, string password)
        {
            using (System.Security.Cryptography.MD5 md5_hash = System.Security.Cryptography.MD5.Create())
            {
                Cryptography cpt = new Cryptography();
                password = cpt.GetHash(md5_hash, password);  // hashing password string by MD5
            }

            var test = (from p in db.Users
                        where (p.Login == login && p.Password == password)
                        select p);
            return test.Any();
        }

        /// <summary> Checks if a book with given title exists in the system </summary>
        private bool CheckReference(string title)
        {
            var test = (from p in db.Documents
                        where (p.Title == title)
                        select p);
            return test.Any();
        }

        #endregion

        #region DB Getters

        /// <summary> Returns document object from given ID </summary>
        public Document GetDocByID(int docID)
        {
            var test = from doc in db.Documents where doc.Id == docID select doc;
            return test.Single();
        }
        
        public Users GetUser(string Name)
        {
            var test = from u in db.Users where u.Name == Name select u;
            return test.Single();
        }

        /// <summary> Returns user row from given ID </summary>
        public Users GetUser(int userID)
        {
            var test = from u in db.Users where u.Id == userID select u;
            return test.Single();
        }
        
        public List<CheckedOut> GetCheckout(string Name)
        {
            Users user = GetUser(Name);
            int userID = user.Id;
            List<CheckedOut> res = new List<CheckedOut>();
            var load_user_books = from c in db.Checkouts
                                  where c.UserID == userID
                                  join b in db.Documents on c.BookID equals b.Id
                                  select new
                                  {
                                      b.Title,
                                      c.TimeToBack
                                  };
            foreach (var element in load_user_books)
            {
                CheckedOut pair = new CheckedOut();
                pair.CheckOutTime = element.TimeToBack.Day;
                pair.DocumentCheckedOut = element.Title;
                res.Insert(0, pair);
            }
            return res;
        }

        public List<OverdueInfo> GetOverdues(string Name)
        {
            Users user = GetUser(Name);
            int userID = user.Id;
            List<OverdueInfo> res = new List<OverdueInfo>();
            var load_user_books = from c in db.Checkouts
                                  where c.UserID == userID
                                  join b in db.Documents on c.BookID equals b.Id
                                  select new
                                  {
                                      b.Title,
                                      c.TimeToBack
                                  };
            foreach (var element in load_user_books)
            {
                int passedDays = (int)DateTime.Now.Subtract(element.TimeToBack).TotalDays;
                if (passedDays > 0)
                { 
                    OverdueInfo pair = new OverdueInfo();
                    pair.overdue = passedDays;
                    pair.DocumentChekedOut = element.Title;
                    res.Add(pair);
                }
            }
            return res;
        }

        /// <summary> Returns a checkout info of particular document </summary>
        private Checkouts GetOwnerInfo(int docID)
        {
            var test = from c in db.Checkouts
                       where c.BookID == docID
                       select c;
            if (test.Any()) return test.Single();
            else return null;
        }
      
        /// <summary> Gets patron row in UI table by his name </summary>
        public Pages.UserTable GetPatronByName(string name)
        {
            var table = SDM.LMS.TestUsersTable();

            var patron = (from p in table where p.userName.Equals(name) select p).FirstOrDefault();

            return patron;
        }

        /// <summary> Counts number of user`s docs from his ID </summary>
        private int GetUserBooksNumber(int userID)
        {
            var test = from c in db.Checkouts
                       where c.UserID == userID
                       select c;
            if (test.Any()) return test.Count();
            else return 0;
        }

        /// <summary> Counts overall user`s fine for overdued books </summary>
        public int GetUserFine(int userID)
        {
            int fine = 0;
            var test = from c in db.Checkouts
                       where c.UserID == userID
                       select c;
            if (test.Any())
            {
                foreach (Checkouts c in test)
                {
                    int overduedTime = (int)DateTime.Now.Subtract(c.TimeToBack).TotalDays;
                    if (overduedTime > 0)
                    {
                        int docPrice = GetDocPrice(c.BookID);
                        fine += (overduedTime * 50 > docPrice ? docPrice : overduedTime * 50);
                    }
                }
            }
            return fine;
        }

        /// <summary> Gets price of doc by its ID </summary>
        private int GetDocPrice(int docID)
        {
            var test = from c in db.Documents
                       where c.Id == docID
                       select c;
            return test.Single().Price;
        }

        #endregion

        #region DB Testers
        public bool DocExists(string Title)
        {
            var test = from d in db.Documents
                       where d.Title.Equals(Title)
                       select d;
            return test.Any();
        }

        public bool UserExists(string Name)
        {
            var test = from u in db.Users
                       where u.Name.Equals(Name)
                       select u;
            return test.Any();
        }

        public bool AmountOfDocs(string Title, int n)
        {
            var test = from d in db.Documents
                       where d.Title.Equals(Title)
                       select d;
            return test.Count()==n;
        }

        public bool CheckUserInfo(string Name, string Adress, string Phone, int UserType, List<CheckedOut> checkout)
        {
            Users user = GetUser(Name);
            List<CheckedOut> checkover = GetCheckout(Name);

            return user.Address.Equals(Adress) && user.PhoneNumber.Equals(Phone)
                && user.UserType == UserType && EqualCheckouts(checkout, checkover);
        }

        public bool CheckUserInfo(string Name, string Adress, string Phone, int UserType, List<OverdueInfo> overdues)
        {
            Users user = GetUser(Name);
            List<OverdueInfo> checkoverdues = GetOverdues(Name);

            return user.Address.Equals(Adress) && user.PhoneNumber.Equals(Phone)
                && user.UserType == UserType && EqualOverdues(overdues, checkoverdues);
        }

        private bool EqualOverdues(List<OverdueInfo> overdue, List<OverdueInfo> neededInfo)
        {
            return new HashSet<OverdueInfo>(overdue).SetEquals(neededInfo);
        }
        
        private bool EqualCheckouts(List<CheckedOut> checkedOuts, List<CheckedOut> neededInfo)
        {
            return new HashSet<CheckedOut>(checkedOuts).SetEquals(neededInfo);
        }

        #endregion

        #region PQ Operations

        public void PushInPQ(int docID, int personID)
        {
            var test = from doc in queueMap
                       where doc.Key == docID
                       select doc.Value;
            if (test.Any())
                test.Single().Push(personID, CheckPriority(personID));
            else
            {
                PriorityQueue<int> newPQ = new PriorityQueue<int>();
                newPQ.Push(personID, CheckPriority(personID));
                queueMap.Add(docID, newPQ);
            }
        }

        public bool ExistQueueForDoc(int docID)
        {
            return queueMap.ContainsKey(docID);
        }

        private int CheckPriority(int personID)
        {
            throw new NotImplementedException();
        }

        public bool SendNotificationToUser(string To, string Title, string Text)
        {
            //try {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(SDM.Strings.MAIL_SERVER_LOGIN, SDM.Strings.MAIL_SERVER_PASSWORD);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage msg = new MailMessage(SDM.Strings.MAIL_SERVER_LOGIN + "@gmail.com", To);
                msg.Subject = Title;
                msg.Body = Text;

                smtp.Send(msg);
                return true;
            /*}
            catch {
                return false;
            }*/
        }

        #endregion

    }
}

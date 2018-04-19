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

        /// <summary> Initializing DB </summary>
        public Library()
        {
            db = new LMSDataBase(SDM.Strings.CONNECTION_STRING);
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
            // TODO Проверять правильность вводимого мейла (adress)

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
                UserType = (isLibrarian ? 5 : 0) // TODO Заменить на enum
            };
            db.Users.InsertOnSubmit(newUser);
            db.SubmitChanges();
            return true;
        }

        // TODO Слудующие три методы схожие, может их можно как-то упростить?

        public void AddBook(string title, string autors, string publisher, int publishYear, string edition, string description, int price, bool isBestseller, int quantity)
        {
            // TODO Название метода и переменной не соответствуют их функционалу
            bool isReference = !CheckReference(title);
            if (isReference)
            {
                Document newDoc = new Document
                {
                    Title = title,
                    Autors = autors,
                    Publisher = publisher,
                    PublishYear = publishYear,
                    Edition = edition,
                    Description = description,
                    Price = price,
                    DocType = 0,
                    IsBestseller = isBestseller,
                    Quantity = quantity,
                    Queue = ""
                };
                db.Documents.InsertOnSubmit(newDoc);
            }
            else
            {
                var test = (from p in db.Documents
                            where (p.Title == title)
                            select p);
                Document newDoc = test.Single();
                newDoc.Quantity += quantity;
                NotifyNextUser(newDoc.Id);
            }
            db.SubmitChanges();
        }

        public void AddJournal(string title, string autors, string publishedIn, string issueTitle, string issueEditor, int price, int quantity)
        {
            // TODO Название метода и переменной не соответствуют их функционалу
            bool isReference = !CheckReference(title);
            if (isReference)
            {
                Document newDoc = new Document
                {
                    Title = title,
                    Autors = autors,
                    PublishedIn = publishedIn,
                    IssueTitle = issueTitle,
                    IssueEditor = issueEditor,
                    Price = price,
                    DocType = 1,
                    Quantity = quantity,
                    Queue = ""
                };
                db.Documents.InsertOnSubmit(newDoc);
            }
            else
            {
                var test = (from p in db.Documents
                            where (p.Title == title)
                            select p);
                Document newDoc = test.Single();
                newDoc.Quantity += quantity;
                NotifyNextUser(newDoc.Id);
            }
            db.SubmitChanges();
        }

        public void AddAV(string title, string autors, int price, int quantity)
        {
            // TODO Название метода и переменной не соответствуют их функционалу
            bool isReference = !CheckReference(title);
            if (isReference)
            {
                Document newDoc = new Document
                {
                    Title = title,
                    Autors = autors,
                    Price = price,
                    DocType = 2,
                    Quantity = quantity,
                    Queue = ""
                };
                db.Documents.InsertOnSubmit(newDoc);
            }
            else
            {
                var test = (from p in db.Documents
                            where (p.Title == title)
                            select p);
                Document newDoc = test.Single();
                newDoc.Quantity += quantity;
                NotifyNextUser(newDoc.Id);
            }
            db.SubmitChanges();
        }

        // [TEST]
        /// <summary> First generate for show functionality </summary>
        private void GenerateTestDB()
        {
            // Adding two books and their copies
            AddBook
                (
                    "Introduction to Algorithms",
                    "Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest and Clifford Stein",
                    "MIT Press",
                    2009,
                    "Third Edition",
                    "Alghorithm techniques and design",
                    1800,
                    false,
                    1
                );
            AddBook
                (
                    "Design Patterns: Elements of Reusable Object-Oriented Software",
                    "Erich Gamma, Ralph Johnson, John Vlissides, Richard Helm",
                    "Addison-Wesley Professional",
                    2003,
                    "First Edition",
                    "Programm patterns, how to programm well w/o headache",
                    2000,
                    true,
                    1
                );

            // Adding reference book
            AddBook
                (
                    "The Mythical Man-month",
                    "Brooks,Jr., Frederick P",
                    "Addison-Wesley Longman Publishing Co., Inc.",
                    1995,
                    "Second edition",
                    "How to do everything and live better",
                    800,
                    false,
                    0
                );

            // Adding two AV's
            AddAV("Null References: The Billion Dollar Mistake", "Tony Hoare", 400, 0);
            AddAV("Information Entropy", "Claude Shannon", 700, 0);

            // Registering 3 users
            RegisterUser("p1", "p1", "Sergey Afonso", "Via Margutta, 3", "30001", false);
            RegisterUser("p2", "p2", "Nadia Teixeira", "Via Sacra, 13", "30002", false);
            RegisterUser("p3", "p3", "Elvira Espindola", "Via del Corso, 22", "30003", false);

            // Special for me
            RegisterUser("zhychek1", "lolcore", "Toha", "zhychek1@yandex.ru", "+79648350370", true);
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

            // TODO Нужно ещё как-то удалить юзера из очередей за книгами

            db.SubmitChanges();
        }

        /// <summary> Deletes registered doc from the system by ID </summary>
        internal void RemoveDocument(int doc_id)
        {
            var record_to_remove = (from d in db.Documents
                                    where (d.Id == doc_id)
                                    select d).Single();
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

        public string RenewDoc(int docID, params int[] DateCheat)
        {
            DateTime time;
            if (DateCheat.Length == 0)
                time = DateTime.Now;
            else
                time = new DateTime(DateCheat[2], DateCheat[1], DateCheat[0]);

            var doc = (from book in db.Checkouts
                       where book.BookID == docID && SDM.CurrentUser.PersonID == book.UserID
                       select book).Single();
            if (doc.IsRenewed && SDM.CurrentUser.UserType != 3)
                return SDM.Strings.DOC_ALREADY_RENEWED;
            else if (ExistQueueForDoc(docID))
                return SDM.Strings.DOC_IN_QUEUE;
            else if (GetUserFineForDoc(SDM.CurrentUser.PersonID, docID) > 0)
                return SDM.Strings.USER_HAVE_FINE;
            else
            {
                doc.TimeToBack = time.Add(doc.TimeToBack.Subtract((DateTime)doc.DateTaked));
                doc.DateTaked = time;
                doc.IsRenewed = true;
                db.SubmitChanges();
                return SDM.Strings.SUCCESSFUL_RENEW;
            }
        }

        // TODO Сделать Update для разных типов доков, как при создании
        /// <summary> Updates document info </summary>
        public void ModifyDoc(int DocID, string Title, string Description, string Price, bool IsBestseller, int DocType)
        {
            Document doc = GetDocByID(DocID);
            doc.Title = Title;
            doc.Description = Description;
            doc.Price = Convert.ToInt32(Price);
            doc.IsBestseller = IsBestseller;
            doc.DocType = DocType;
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, doc);
            db.SubmitChanges();
        }

        /// <summary> Updates user info </summary>
        public void UpdateUser(int userId, string userName, string userAdress, string userPhoneNumber, int userType)
        {
            Users user = GetUser(userId);
            user.Name = userName;
            user.Address = userAdress;
            user.PhoneNumber = userPhoneNumber;
            user.UserType = userType;
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, user);
            db.SubmitChanges();
        }


        public void SetOutstandingRequest(int docID)
        {
            var doc = (from d in db.Documents
                       where d.Id == docID
                       select d).Single();

            NotifyNextUser(docID);
            while (doc.Queue.Length > 0) {
                PopFromPQ(docID);
            }

            var test = from c in db.Checkouts
                       where c.BookID == docID
                       select c;
            
            foreach (Checkouts c in test)
            {
                if (c.TimeToBack.CompareTo(DateTime.Now) > 0)
                    c.TimeToBack = DateTime.Now;
            }
            doc.Queue = "";

            // TODO Чёт сомнительный костыль, от него проблем не будет?
            PushInPQ(docID, GetUserID("lb"), 5);

            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, doc);
            db.SubmitChanges();
        }

        #endregion

        #region DB Output

        /// <summary>
        /// Returns collection of current logged user books
        /// Usage: MyBooks.xaml 
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
                                      b.Autors,
                                      b.Price,
                                      c.DateTaked,
                                      c.TimeToBack
                                  };
            foreach (var element in load_user_books)
            {
                Pages.MyBooksTable row = new Pages.MyBooksTable
                {
                    checkID = element.CheckID,
                    docID = element.BookID,
                    docTitle = element.Title,
                    docAutors = element.Autors,
                    docPrice = element.Price,
                    docFine = GetUserFineForDoc(SDM.CurrentUser.PersonID, element.BookID),
                    checkDateTaked = (DateTime)element.DateTaked,
                    checkTimeToBack = element.TimeToBack
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        /// <summary>
        /// Return overdued docs for particular user
        /// Usage: OverdueInfo.xaml
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
                                      b.Quantity,
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
                        quantity = element.Quantity,
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
        /// Usage: UserManagementPage.xaml
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

        // [FOR TEST] (TestingTool.xaml)
        /// <summary> Returns a collection of books checked by particular user </summary>
        public ObservableCollection<Pages.DocumentsTable> TestDocsTableUsersBooks(int user_id)
        {
            ObservableCollection<Pages.DocumentsTable> temp_table = new ObservableCollection<Pages.DocumentsTable>();
            var load_user_docs = from c in db.Checkouts
                                 join b in db.Documents on c.BookID equals b.Id
                                 where c.UserID == user_id
                                 select new
                                 {
                                     b.Id,
                                     b.Title,
                                     b.Autors,
                                     b.DocType,
                                     b.Price,
                                     b.Quantity
                                 };
            foreach (var element in load_user_docs)
            {
                Pages.DocumentsTable row = new Pages.DocumentsTable
                {
                    docID = element.Id,
                    docAutors = element.Autors,
                    docTitle = element.Title,
                    docType = SDM.Strings.DOC_TYPES[element.DocType],
                    docPrice = element.Price,
                    docQuantity = element.Quantity
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        // [FOR TEST] (TestingTool.xaml)
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
        /// Usage: UserHomePage.xaml, DocumentsManagementPage.xaml, TestingTool.xaml
        /// </summary>
        public ObservableCollection<Pages.DocumentsTable> GetDocsTable()
        {
            ObservableCollection<Pages.DocumentsTable> temp_table = new ObservableCollection<Pages.DocumentsTable>();
            var load_user_docs = from b in db.Documents
                                 select new
                                 {
                                     b.Id,
                                     b.Title,
                                     b.Autors,
                                     b.DocType,
                                     b.Price,
                                     b.Quantity
                                 };
            foreach (var element in load_user_docs)
            {
                Pages.DocumentsTable row = new Pages.DocumentsTable
                {
                    docID = element.Id,
                    docAutors = element.Autors,
                    docTitle = element.Title,
                    docType = SDM.Strings.DOC_TYPES[element.DocType],
                    docPrice = element.Price,
                    docQuantity = element.Quantity
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        /// <summary>
        /// Gets particular user docs for UserCard
        /// Usage: UserCard.xaml
        /// </summary>
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

        // TODO Переименовать
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

        // TODO Больно уж сомнительные все эти геттеры, я считаю, что многие не нужны
        // Ладно, потом обсудим вместе

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

        // TODO Объясните мне необходимость следующих двух методов

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

        // TODO Я человек простой, вижу 0 референсов - удаляю
        /// <summary> Returns a checkout info of particular document </summary>
        private Checkouts GetOwnerInfo(int docID)
        {
            var test = from c in db.Checkouts
                       where c.BookID == docID
                       select c;
            if (test.Any()) return test.Single();
            else return null;
        }

        // TODO Ну это для тестов, я так понимаю? Неправильно понимаешь
        /// <summary> Gets patron row in UI table by his name </summary>
        public Pages.UserTable GetPatronByName(string name)
        {
            var table = SDM.LMS.TestUsersTable();
            var patron = (from p in table where p.userName.Equals(name) select p).FirstOrDefault();
            return patron;
        }

        /// <summary> Counts overall user`s fine for overdued docs </summary>
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
                    fine += GetUserFineForDoc(userID, c.BookID);
                }
            }

            return fine;
        }

        /// <summary> Counts user`s fine for some doc </summary>
        public int GetUserFineForDoc(int userID, int docID)
        {
            var test = from c in db.Checkouts
                       where c.BookID == docID && c.UserID == userID
                       select c;
            Checkouts testCheck = test.Single();

            int overduedTime = (int)DateTime.Now.Subtract(testCheck.TimeToBack).TotalDays;
            if (overduedTime > 0)
            {
                int docPrice = GetDocByID(docID).Price;
                return (overduedTime * 100 > docPrice ? docPrice : overduedTime * 100);
            }

            return 0;
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

		#endregion

		#region DB Testers

		// TODO В общем методы для тестов надо вынести в другой класс, либо создать отдельный регион
		public DateTime CheckoutTimeToBack(int patronID, int docID)
		{
			var test = from c in db.Checkouts
					   where c.BookID == docID && c.UserID == patronID
					   select c;
			DateTime dt = test.Single().TimeToBack;
			return dt;
		}

		public int OverdueTime(int userID, int docID) { 
            var test = from c in db.Checkouts
                       where c.BookID == docID && c.UserID == userID
                       select c;
            Checkouts testCheck = test.Single();

            return (int)DateTime.Now.Subtract(testCheck.TimeToBack).TotalDays;
        }

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
            var test = (from d in db.Documents
                       where d.Title.Equals(Title)
                       select d).Single();
            return test.Quantity==n;
        }

        // TODO Я человек простой, вижу 0 референсов - удаляю//Сам завалил тесты а потом на 0 референсов жалобы
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

        public void UpgradeUser(string Name, int ut)
        {
            Users user = GetUser(Name);
            if (ut < 5)
                user.UserType = ut;
            db.SubmitChanges();
        }

        public int GetDocID(string Title)
        {
            var document = (from doc in db.Documents
                       where doc.Title.Equals(Title)
                       select doc).Single();
            return document.Id;
        }

        public int GetUserID(string Name)
        {
            var test = (from user in db.Users
                        where user.Name.Equals(Name)
                        select user).Single();
            return test.Id;
        }
        private bool EqualCheckouts(List<CheckedOut> checkedOuts, List<CheckedOut> neededInfo)
        {
            return new HashSet<CheckedOut>(checkedOuts).SetEquals(neededInfo);
        }

        #endregion

        #region PQ Operations

        /// <summary> Pushes personID with given priority to queue of given ID doc </summary>
        public void PushInPQ(int docID, int personID, int priority)
        {
            PriorityQueue<int> PQ = LoadPQ(docID);
            PQ.Push(personID, priority);
            SavePQ(PQ, docID);
        }

        /// <summary> Pops personID with given priority from queue of given ID doc </summary>
        public void PopFromPQ(int docID)
        {
            PriorityQueue<int> PQ = LoadPQ(docID);
            PQ.Pop();
            if (PQ.FirstElement != null)
            { 
                Users next = GetUser(Convert.ToInt32(PQ.FirstElement.Element));
                Document doc = GetDocByID(docID);
                SendNotificationToUser(next.Address, SDM.Strings.MAIL_TITLE, SDM.Strings.MAIL_TEXT(doc.Title, SDM.Strings.DOC_TYPES[doc.DocType]));
            }
            SavePQ(PQ, docID);
        }

        /// <summary> Checks if queue for doc with given ID exists </summary>
        public bool ExistQueueForDoc(int docID)
        {
            var test = from doc in db.Documents
                       where doc.Id == docID
                       select doc.Queue;
            if (test.Single().Length > 0) return true;
            return false;
        }

        /// <summary> Checks if person is in queue for given doc </summary>
        public bool IsPersonInQueue(int patronID, int bookID)
        {
            bool inQueue = false;
            var test = from doc in db.Documents
                       where doc.Id == bookID
                       select doc.Queue;

            string queue_string = test.Single();
            string[] queue_pairs = queue_string.Split('-');
            foreach (string pair in queue_pairs)
            {
                int id = Convert.ToInt32(pair.Split('|')[0]);
                if (id == patronID) inQueue = true;
            }

            return inQueue;
        }

        /// <summary> Send mail for next user in queue if it is not empty </summary>
        public void NotifyNextUser(int docID)
        {
            PriorityQueue<int> PQ = LoadPQ(docID);
            if (PQ.Length > 0)
            {
                Users next = GetUser(Convert.ToInt32(PQ.FirstElement.Element));
                Document doc = GetDocByID(docID);
                SendNotificationToUser(next.Address, SDM.Strings.MAIL_TITLE, SDM.Strings.MAIL_TEXT(doc.Title, SDM.Strings.DOC_TYPES[doc.DocType]));
                SavePQ(PQ, docID);
            }
        }

        /// <summary> Sends e-mail to given address with given title and text </summary>
        public bool SendNotificationToUser(string To, string Title, string Text)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(SDM.Strings.MAIL_SERVER_LOGIN, SDM.Strings.MAIL_SERVER_PASSWORD),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                MailMessage msg = new MailMessage(SDM.Strings.MAIL_SERVER_LOGIN + "@gmail.com", To)
                {
                    Subject = Title,
                    Body = Text
                };

                smtp.Send(msg);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary> Converts PQ into string and saves it in DB </summary>
        private void SavePQ(PriorityQueue<int> pq, int bookID)
        {
            string queue_string = "";

            while (pq.Length > 1)
            {
                queue_string += pq.FirstElement.Element;
                queue_string += '|';
                queue_string += pq.FirstElement.PriorityLevel;
                queue_string += '-';
                pq.Pop();
            }
            if (pq.FirstElement == null)
                queue_string = "";
            else
            {
                queue_string += pq.FirstElement.Element;
                queue_string += '|';
                queue_string += pq.FirstElement.PriorityLevel;
            }

            var test = from doc in db.Documents
                       where doc.Id == bookID
                       select doc;
            Document d = test.Single();
            d.Queue = queue_string;
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, d);
            db.SubmitChanges();
        }

        /// <summary> Parse PQ string from DB and push entries to PQ </summary>
        public PriorityQueue<int> LoadPQ(int bookID)
        {
            PriorityQueue<int> localQueue = new PriorityQueue<int>();
            var test = from doc in db.Documents
                       where doc.Id == bookID
                       select doc.Queue;

            string queue_string = test.Single();
            if (queue_string.Length == 0) return localQueue;
            
            string[] queue_pairs = queue_string.Split('-');
            foreach (string pair in queue_pairs)
            {
                int id = Convert.ToInt32(pair.Split('|')[0]);
                int priority = Convert.ToInt32(pair.Split('|')[1]);
                localQueue.Push(id, priority);
            }

            return localQueue;
        }

        #endregion

    }
}

using I2P_Project.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));

            System.IO.Directory.CreateDirectory(SDM.Strings.DB_DIRECTORY_NAME);

            string connString = path + SDM.Strings.DB_RELATIVE_PATH;
            db = new LMSDataBase(connString);

            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
                GenerateUserTypesDB();                
            }

            db.SubmitChanges(); // DB Preload
        }

        /// <summary> Registers new user in data base </summary>
        public bool RegisterUser(string login, string password, string name, string adress, string phone, bool isLibrarian)
        {
            if (CheckLogin(login)) return false;

            Users newUser = new Users
            {
                Login = login,
                Password = password,
                Name = name,
                Address = adress,
                PhoneNumber = phone,
                UserType = (isLibrarian ? 3 : 1)
            };
            db.Users.InsertOnSubmit(newUser);
            db.SubmitChanges();
            return true;
        }

        #region Output for viewing in tables

        /// <summary> Returns a collection of current user docs </summary>
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
                if (TimePassedDays(element.TimeToBack) > 0)
                {
                    Pages.OverdueInfoTable row = new Pages.OverdueInfoTable
                    {
                        docID = element.Id,
                        docTitle = element.Title,
                        isReference = element.IsReference,
                        docType = DocTypeString(element.DocType),
                        dateTaked = (DateTime)element.DateTaked,
                        timeToBack = element.TimeToBack,
                        fine = CountedFine(TimePassedDays(element.TimeToBack), element.Price)
                    };
                    temp_table.Add(row);
                }
            }
            return temp_table;
        }

        public ObservableCollection<Pages.LibrarianUserView> LibrarianViewUserTable()
        {
            ObservableCollection<Pages.LibrarianUserView> temp_table = new ObservableCollection<Pages.LibrarianUserView>();
            var load_users = from p in db.Users
                                  where p.UserType != 3
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
                    docsNumber = UserBooksNumber(element.Id),
                    userFine = CountUserFine(element.Id)
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        internal void RemoveUser(int patronID)
        {
            var record_to_remove = (from d in db.Users
                                    where d.Id == patronID
                                    select d).Single();
            db.Users.DeleteOnSubmit(record_to_remove);
            db.SubmitChanges();
        }

        internal void RemoveDocument(int doc_id)
        {
            var record_to_remove = (from d in db.Documents
                                    where (d.Id == doc_id)
                                    select d).Single();
            db.Documents.DeleteOnSubmit(record_to_remove);
            db.SubmitChanges();
        }

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

        public void ModifyDoc(int doc_id, string Title, string Description, string Price, string IsBestseller,
            string DocType)
        {
            var doc = (from d in db.Documents
                                 where d.Id == doc_id
                                 select d).Single();
            doc.Title = Title;
            doc.Description = Description;
            doc.Price = Convert.ToInt32(Price);
            doc.IsBestseller = IsBestseller.ToLower().Equals("yes") ? true : false;
            switch (DocType.ToLower())
            {
                case "book":
                    doc.DocType = 0;
                    break;
                case "journal":
                    doc.DocType = 1;
                    break;
                case "AV":
                    doc.DocType = 2;
                    break;
                default:
                    new Exception();
                    break;
            }
            db.SubmitChanges();
        }

        // [FOR TEST]
        /// <summary> Returns a collection of all docs </summary>
        public ObservableCollection<Pages.DocsTable> TestDocsTableOnlyBooks()
        {
            ObservableCollection<Pages.DocsTable> temp_table = new ObservableCollection<Pages.DocsTable>();
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
                Pages.DocsTable row = new Pages.DocsTable
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

        // [FOR TEST]
        /// <summary> Returns a collection of books of particular user </summary>
        public ObservableCollection<Pages.DocsTable> TestDocsTableUsersBooks(int user_id)
        {
            ObservableCollection<Pages.DocsTable> temp_table = new ObservableCollection<Pages.DocsTable>();
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
                Pages.DocsTable row = new Pages.DocsTable
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
        /// <summary> Returns a collection of all users </summary>
        public ObservableCollection<Pages.UserTable> TestUsersTable()
        {
            ObservableCollection<Pages.UserTable> temp_table = new ObservableCollection<Pages.UserTable>();
            var load_users = from u in db.Users
                             join ut in db.UserTypes on u.UserType equals ut.TypeID
                             select new
                             {
                                 u.Id,
                                 u.Name,
                                 u.Address,
                                 u.PhoneNumber,
                                 ut.TypeName
                             };
            foreach (var element in load_users)
            {
                Pages.UserTable row = new Pages.UserTable
                {
                    userID = element.Id,
                    userName = element.Name,
                    userAddress = element.Address,
                    userPhoneNumber = element.PhoneNumber,
                    userType = element.TypeName
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        // TODO Replace with Observable collection
        /// <summary> Returns all non-reference docs </summary>
        public List<DataBase.Document> GetAllDocs()
        {
            var test = (from p in db.Documents select p);
            return test.ToList();
        }

        
        public Document GetDoc(int docID)
        {
            var test = (from doc in db.Documents where doc.Id == docID select doc);
            Document res = new Document();
            DataBase.Document d;
            if (test.Any())
            {
                d = test.Single();
                res.descriptiion = d.Description;
                res.docTitle = d.Title;
                res.isBestseller = d.IsBestseller;
                res.isReference = d.IsReference;
                switch (d.DocType)
                {
                    case 0:
                        res.docType = "book";
                        break;
                    case 1:
                        res.docType = "journal";
                        break;
                    case 2:
                        res.docType = "AV";
                        break;
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

        #endregion

        #region Existence checking

        /// <summary> Checks if there exist a user with given e-mail </summary>
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
            var test = (from p in db.Users
                        where (p.Login == login && p.Password == password)
                        select p);
            return test.Any();
        }

        #endregion

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

        /// <summary> Clears DB (for test cases only) </summary>
        public void ClearDB()
        {
            db.ExecuteCommand("DELETE FROM documents");
            db.ExecuteCommand("DELETE FROM users");
            db.ExecuteCommand("DELETE FROM checkouts");
        }

        private string DocTypeString(int index)
        {
            switch (index)
            {
                case 0:
                    return "Book";
                case 1:
                    return "Journal";
                case 2:
                    return "Audio";
                case 3:
                    return "Video";
                default:
                    throw new Exception("Unknown type index");
            }
        }

        private int UserBooksNumber(int userID)
        {
            var test = from c in db.Checkouts
                       where c.UserID == userID
                       select c;
            if (test.Any()) return test.Count();
            else return 0;
        }

        private int CountUserFine(int userID)
        {
            int fine = 0;
            var test = from c in db.Checkouts
                       where c.UserID == userID
                       select c;
            if (test.Any())
            {
                foreach (Checkouts c in test)
                {
                    int overduedTime = TimePassedDays(c.TimeToBack);
                    if (overduedTime > 0)
                    {
                        int docPrice = DocPrice(c.BookID);
                        fine += (overduedTime * 50 > docPrice ? docPrice : overduedTime * 50);
                    }
                }
            }
            return fine;
        }

        private int CountedFine(int daysPassed, int bookPrice)
        {
            return (daysPassed * 50 > bookPrice ? bookPrice : daysPassed * 50);
        }

        public void UpdateUser(int userId, string userName, string userAdress, string userPhoneNumber, int userType)
        {
            Users user = GetUser(userId);
            user.Name = userName;
            user.Address = userAdress;
            user.PhoneNumber = userPhoneNumber;
            user.UserType = userType;
            db.SubmitChanges();
        }



        public Users GetUser(int userID)
        {
            var test = from u in db.Users where u.Id == userID select u;
            return test.Single();
        }

        private int TimePassedDays(DateTime from)
        {
            TimeSpan t = DateTime.Now.Subtract(from);
            return (int)t.TotalDays;
        }

        private int DocPrice(int docID)
        {
            var test = from c in db.Documents
                       where c.Id == docID 
                       select c;
            return test.Single().Price;
        }

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

        private bool CheckReference(string title)
        {
            var test = (from p in db.Documents
                        where (p.Title == title)
                        select p);
            return test.Any();
        }

        public Pages.UserTable PatronbyName(string name)
        {
            var table = SDM.LMS.TestUsersTable();

            var patron = (from p in table where p.userName.Equals(name) select p).FirstOrDefault();

            return patron;
        }

        private void GenerateUserTypesDB()
        {
            UserTypes studentType = new UserTypes { TypeName = "Student" };
            UserTypes facultyType = new UserTypes { TypeName = "Faculty" };
            UserTypes librarianType = new UserTypes { TypeName = "Librarian" };

            db.UserTypes.InsertOnSubmit(studentType);
            db.UserTypes.InsertOnSubmit(facultyType);
            db.UserTypes.InsertOnSubmit(librarianType);
        }

    }
}

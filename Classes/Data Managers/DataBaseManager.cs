using I2P_Project.DataBases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace I2P_Project.Classes.Data_Managers
{
    /// <summary>
    /// Static class for fast and easy acess to db.
    /// All required quries are incapsulated in methods to provide information hiding.
    /// </summary>
    static class DataBaseManager
    {
        /// <summary> LINQ to database, used for easier interaction </summary>
        private static LINQtoUserDBDataContext db;

        /// <summary> Initializing DB </summary>
        public static void Initialize()
        {
            db = new LINQtoUserDBDataContext();
        }

        /// <summary> Checks if there exist a user with given e-mail </summary>
        public static bool CheckLogin(string login)
        {
            var test = (from p in db.users
                        where p.login == login
                        select p);
            return test.Any();
        }

        /// <summary> Checks if a user with given e-mail has given password </summary>
        public static bool CheckPassword(string login, string password)
        {
            var test = (from p in db.users
                        where (p.login == login && p.password == password)
                        select p);
            return test.Any();
        }

        /// <summary> Registers new user in data base </summary>
        public static bool RegisterUser(string login, string password, string name, string adress, string phone, bool isLibrarian)
        {
            if (CheckLogin(login)) return false;

            users newUser = new users();
            newUser.login = login;
            newUser.password = password;
            newUser.name = name;
            newUser.address = adress;
            newUser.phoneNumber = phone;
            newUser.userType = isLibrarian ? 2 : 1;
            newUser.icNumber = NextLCNumber();
            db.users.InsertOnSubmit(newUser);
            db.SubmitChanges();
            return true;
        }

        public static void AddDocToDB(string title, string description, int docType, int price, bool isBestseller)
        {
            if (CheckDoc(title))
            {
                var test = (from p in db.documents
                            where (p.Title == title)
                            select p);
                documents doc = test.Single();
                doc.Count++;
            }
            else
            {
                documents newDoc = new documents();
                newDoc.Title = title;
                newDoc.Description = description;
                newDoc.Price = price;
                newDoc.DocType = docType;
                newDoc.IsBestseller = isBestseller;
                newDoc.Count = 0;
                db.documents.InsertOnSubmit(newDoc);
                db.SubmitChanges();
            }
        }

       
        
        /// <summary>
        /// Returns numerator of user type:
        /// 0 - Student
        /// 1 - Faculty
        /// 2 - Librarian
        /// </summary>
        public static int GetUserType(string login)
        {
            var test = (from p in db.users
                        where (p.login == login)
                        select p);
            if (test.Any())
                return test.Single().userType;
            return -1;
        }
        /// <summary>
        /// Change fields in DB when some user check out docs.
        /// Start timer for check out and get reference for book
        /// on it's owner.
        /// </summary>
        /// <param name="docID"></param>
        public static void SetCheckOut(int docID, int user_id)
        {
            System.DateTime time = System.DateTime.Now;

            checkouts chk = new checkouts();
            chk.userID = user_id;
            chk.bookID = docID;
            chk.isReturned = false;
            chk.dateTaked = time;
            chk.timeToBack = time.AddDays(10); // user can set the date himself;
            db.checkouts.InsertOnSubmit(chk);
            db.SubmitChanges();
        }

        public static ObservableCollection<Pages.MyBooksTable> GetUserBooks()
        {
            ObservableCollection<Pages.MyBooksTable> temp_table = new ObservableCollection<Pages.MyBooksTable>();
            var load_user_books = from c in db.checkouts
                                  join b in db.documents on c.bookID equals b.Id
                                  where c.userID == SystemDataManager.CurrentUser.PersonID
                                  select new
                                  {
                                      c.checkID,
                                      c.bookID,
                                      b.Title,
                                      c.dateTaked,
                                      c.timeToBack
                                  };
            foreach (var element in load_user_books)
            {
                Pages.MyBooksTable row = new Pages.MyBooksTable
                {
                    checkID = element.checkID,
                    bookID = element.bookID,
                    b_title = element.Title,
                    c_dateTaked = (System.DateTime)element.dateTaked,
                    c_timeToBack = (System.DateTime)element.timeToBack                    
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        public static ObservableCollection<Pages.DocsTable> TestDocsTable()
        {
            ObservableCollection<Pages.DocsTable> temp_table = new ObservableCollection<Pages.DocsTable>();
            var load_user_docs = from c in db.checkouts
                                 join b in db.documents on c.bookID equals b.Id
                                 select new
                                 {
                                     c.userID,
                                     c.bookID,
                                     b.Title,
                                     b.DocType,
                                     c.dateTaked,
                                     c.timeToBack
                                 };
            foreach (var element in load_user_docs)
            {
                Pages.DocsTable row = new Pages.DocsTable
                {
                    docID = element.bookID,
                    docOwnerID = element.userID,
                    docTitle = element.Title,
                    docType = TypeString(element.DocType),
                    dateTaked = (System.DateTime)element.dateTaked,
                    timeToBack = element.timeToBack
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        private static string TypeString(int num)
        {
            switch (num)
            {
                case 0:
                    return "Book";
                    break;
                case 1:
                    return "Journal";
                    break;
                case 2:
                    return "Audio";
                    break;
                case 3:
                    return "Video";
                    break;
                default:
                    throw new Exception("Something went wrong");
            }
        }


        /*public static ObservableCollection<Pages.UserTable> TestUsersTable()
        {
            ObservableCollection<Pages.UserTable> temp_table = new ObservableCollection<Pages.UserTable>();
            var load_user_books = from c in db.checkouts
                                  join b in db.documents on c.bookID equals b.Id
                                  select new
                                  {
                                      c.userID,
                                      c.bookID,
                                      b.Title,
                                      c.dateTaked,
                                      c.timeToBack
                                  };
            foreach (var element in load_user_books)
            {
                Pages.UserTable row = new Pages.UserTable
                {
                    userID = element.userID
                    b_title = element.Title,
                    c_dateTaked = (System.DateTime)element.dateTaked,
                    c_timeToBack = (System.DateTime)element.timeToBack
                };
                temp_table.Add(row);
            }
            return temp_table;
        }*/

        public static List<documents> GetAllDocs()
        {
            var test = (from p in db.documents select p);
            return test.ToList();
        }

        public static documents GetDoc(int docID)
        {
            var test = (from p in db.documents
                        where (p.Id == docID)
                        select p);
            return test.Single();
        }

        public static int GetIDByTitle(string title)
        {
            var test = (from p in db.documents
                        where (p.Title == title)
                        select p);
            return test.Single().Id;
        }

        private static bool CheckDoc(string title)
        {
            var test = (from p in db.documents
                where (p.Title == title)
                select p);
            return test.Any();
        }

        public static users GetUser(int userID)
        {
            var test = (from p in db.users
                        where (p.id == userID)
                        select p);
            if (test.Any())
            {
                return test.Single();
            }
            return null;
        }

        public static void UpgradeUser(int userID)
        {
            users user = GetUser(userID);
            if (user.userType < 2)
            {
                user.userType++;
            }
        }

        public static void DowngradeUser(int userID)
        {
            users user = GetUser(userID);
            if (user.userType > 0)
            {
                user.userType--;
            }
        }

        /// <summary> Increment library card number so that everyone had different Library Card number </summary>
        private static int NextLCNumber()
        {
            // TODO Implement query to find largest LC number and return next one
            return 100;
        }


        public static void ClearDB()
        {

            //TODO
        }

    }
}

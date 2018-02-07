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
            db.SubmitChanges(); // DB Preload
        }

        #region Input to DB

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

        /// <summary> Adding new doc to DB with given parameters </summary>
        public static void AddDocToDB(string title, string description, int docType, int price, bool isBestseller)
        {
            bool isReference = !CheckReference(title);
            document newDoc = new document();
            newDoc.Title = title;
            newDoc.Description = description;
            newDoc.Price = price;
            newDoc.DocType = docType;
            newDoc.IsReference = isReference;
            newDoc.IsBestseller = isBestseller;
            db.documents.InsertOnSubmit(newDoc);
            db.SubmitChanges();
        }

        /// <summary>
        /// Change fields in DB when some user check out docs.
        /// Start timer for check out and get reference for book
        /// on it's owner.
        /// </summary>
        /// <param name="docID"></param>
        public static void SetCheckOut(int docID, int user_id, int weeks)
        {
            System.DateTime time = System.DateTime.Now;

            checkouts chk = new checkouts();
            chk.userID = user_id;
            chk.bookID = docID;
            chk.isReturned = false;
            chk.dateTaked = time;
            chk.timeToBack = time.AddDays(weeks * 7);
            db.checkouts.InsertOnSubmit(chk);
            db.SubmitChanges();
        }

        #endregion

        #region Output from DB

        public static ObservableCollection<Pages.MyBooksTable> GetUserBooks()
        {
            ObservableCollection<Pages.MyBooksTable> temp_table = new ObservableCollection<Pages.MyBooksTable>();
            var load_user_books = from c in db.checkouts
                                  join b in db.documents on c.bookID equals b.Id
                                  where c.userID == SystemDataManager.CurrentUser.PersonID && c.isReturned == false
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

        public static ObservableCollection<Pages.UserTable> TestUsersTable()
        {
            ObservableCollection<Pages.UserTable> temp_table = new ObservableCollection<Pages.UserTable>();
            var load_users = from u in db.users
                                 join ut in db.userTypes on u.userType equals ut.typeID
                                 select new
                                 {
                                    u.id,
                                    u.name,
                                    u.address,
                                    u.phoneNumber,
                                    u.icNumber,
                                    ut.typeName
                                 };
            foreach (var element in load_users)
            {
                Pages.UserTable row = new Pages.UserTable
                {
                    userID = element.id,
                    userName = element.name,
                    userAddress = element.address,
                    userPhoneNumber = element.phoneNumber,
                    userICNumber = element.icNumber,
                    userType = element.typeName
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        // TODO Replace with Observable collection
        public static List<document> GetAllDocs()
        {
            var test = (from p in db.documents select p);
            return test.ToList();
        }

        #endregion

        #region Existence checking in DB

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

        /// <summary> Checks if there exist a reference doc with given title </summary>
        public static bool CheckReference(string title)
        {
            var test = (from p in db.documents
                        where (p.Title == title)
                        select p);
            return test.Any();
        }

        #endregion

        /// <summary> Returns numerator of user type </summary>
        /// <returns>
        /// 0 - Student
        /// 1 - Faculty
        /// 2 - Librarian
        /// </returns>
        public static int GetUserType(string login)
        {
            var test = (from p in db.users
                        where (p.login == login)
                        select p);
            if (test.Any())
                return test.Single().userType;
            return -1;
        }

        /// <summary> Returns document object from db searched by ID </summary>
        public static document GetFreeCopy(int docID)
        {
            var test = from b in db.documents
                       where b.Id == docID && !b.IsReference
                       select b;
            if (test.Any()) // Check if any copies of doc exists
            {
                foreach (document selected in test.ToArray()) // Checks if any of them are free
                {
                    var test2 = from c in db.checkouts
                                where c.bookID == selected.Id
                                select c;
                    if (!test2.Any()) return selected;
                }
                return null;
            }
            else
                return null;
        }

        /// <summary> Returns document object from db searched by author </summary>
        public static document GetFreeCopy(string author)
        {
            var test = from b in db.documents
                       where b.Title.ToLower().Contains(author.ToLower()) && !b.IsReference
                       select b;
            if (test.Any()) // Check if any copies of doc exists
            {
                foreach (document selected in test.ToArray()) // Checks if any of them are free
                {
                    var test2 = from c in db.checkouts
                                where c.bookID == selected.Id
                                select c;
                    if (!test2.Any()) return selected;
                }
                return null;
            }
            else
                return null;
        }

        /// <summary> Converts document title to its ID </summary>
        public static int GetIDByTitle(string title)
        {
            var test = (from p in db.documents
                        where (p.Title == title)
                        select p);
            return test.Single().Id;
        }

        /// <summary> User becomes faculty if they were student and vice-versa </summary>
        public static void SwapUserType(int userID)
        {
            var test = (from p in db.users
                        where (p.id == userID)
                        select p);
            if (test.Any())
            {
                users user = test.Single();
                user.userType = user.userType == 0 ? 1 : 0;
            }
        }
        
        /// <summary> Clears DB (for test cases only) </summary>
        public static void ClearDB()
        {
            db.ExecuteCommand("DELETE FROM documents");
            db.ExecuteCommand("DELETE FROM users");
            db.ExecuteCommand("DELETE FROM checkouts");
        }

        /// <summary> Increment library card number so that everyone had different Library Card number </summary>
        private static int NextLCNumber()
        {
            int maxLC = 100;
            var test = (from p in db.users select p);
            if (test.Any())
            {
                List<users> list = test.ToList();
                foreach (users user in list)
                {
                    if (user.icNumber > maxLC) maxLC = user.icNumber;
                }
            }
            return maxLC;
        }

        private static string TypeString(int num)
        {
            switch (num)
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
                    throw new Exception("Something went wrong");
            }
        }

    }
}

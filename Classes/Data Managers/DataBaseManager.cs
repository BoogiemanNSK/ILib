using I2P_Project.DataBases;
using System.Collections.Generic;
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
        public static bool CheckEmail(string email)
        {
            var test = (from p in db.users
                        where p.email == email
                        select p);
            if (test.Any())
                return true;
            return false;
        }

        /// <summary> Checks if a user with given e-mail has given password </summary>
        public static bool CheckPassword(string email, string password)
        {
            var test = (from p in db.users
                        where (p.email == email && p.password == password)
                        select p);
            if (test.Any())
                return true;
            return false;
        }

        /// <summary> Registers new user in data base </summary>
        public static void RegisterUser(string email, string password, string name, string adress, string phone, bool isLibrarian)
        {
            users newUser = new users();
            newUser.email = email;
            newUser.password = password;
            newUser.name = name;
            newUser.address = adress;
            newUser.phoneNumber = phone;
            newUser.userType = isLibrarian ? 2 : 0;
            newUser.icNumber = NextLCNumber();
            db.users.InsertOnSubmit(newUser);
            db.SubmitChanges();
        }
        
        /// <summary>
        /// Returns numerator of user type:
        /// 0 - Student
        /// 1 - Faculty
        /// 2 - Librarian
        /// </summary>
        public static int GetUserType(string email)
        {
            var test = (from p in db.users
                        where (p.email == email)
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
        public static void SetReferAndStartTimer(int docID)
        {
            var test = (from p in db.documents
                        where (p.Id == docID)
                        select p);
            document _current = test.Single();
            _current.OwnerID = SystemDataManager.CurrentUser.PersonID;
            System.DateTime time = new System.DateTime();
            _current.CheckOutTime = time;
        }

        public static List<document> GetAllDocs()
        {
            var test = (from p in db.documents select p);
            return test.ToList();
        }

        public static document GetDoc(int docID)
        {
            var test = (from p in db.documents
                        where (p.Id == docID)
                        select p);
            return test.Single();
        }

        /// <summary> Increment library card number so that everyone had different Library Card number </summary>
        private static int NextLCNumber()
        {
            // TODO Implement query to find largest LC number and return next one
            return 100;
        }

    }
}

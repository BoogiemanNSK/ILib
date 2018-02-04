using I2P_Project.DataBases;
using System.Linq;

namespace I2P_Project.Classes.Data_Managers
{
    static class DataBaseManager
    {
        private static LINQtoUserDBDataContext db;

        public static void Initialize()
        {
            db = new LINQtoUserDBDataContext();
        }

        public static bool CheckEmail(string email)
        {
            var test = (from p in db.users
                        where p.email == email
                        select p);
            if (test.Any())
                return true;
            return false;
        }

        public static bool CheckPassword(string email, string password)
        {
            var test = (from p in db.users
                        where (p.email == email && p.password == password)
                        select p);
            if (test.Any())
                return true;
            return false;
        }

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

        public static int GetUserType(string email)
        {
            var test = (from p in db.users
                        where (p.email == email)
                        select p);
            if (test.Any())
                return test.Single().userType;
            return -1;
        }

        private static int NextLCNumber()
        {
            // TODO Implement query to find largest LC number and return next one
            return 100;
        }

    }
}

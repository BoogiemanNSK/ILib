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

    }
}

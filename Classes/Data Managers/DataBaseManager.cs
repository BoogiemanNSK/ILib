using I2P_Project.DataBases;
using System.Data.Linq;

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
                        select p).Single();
            if (test != null)
                return true;
            return false;
        }

    }
}

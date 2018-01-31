using I2P_Project.DataBases;
using System.Data.Linq;

namespace I2P_Project.Classes.Data_Managers
{
    static class DataBaseManager
    {
        private static LINQtoUserDBDataContext userDBContext;
        private static Table<UserDB> users;

        public static void Initialize()
        {
            userDBContext = new LINQtoUserDBDataContext();
            users = userDBContext.GetTable<UserDB>();
        }

        public static bool UserExist(string inputMail)
        {
            var query =
                from user in users
                where user.n == inputMail
                select user;
            if (query) return true;
            return false;
        }

        public static bool RightPassword(string inputMail, string inputPassword)
        {

        }

    }
}

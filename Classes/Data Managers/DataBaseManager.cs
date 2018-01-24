using System.Data.Linq;
using System.Data.SqlClient;

namespace I2P_Project.Classes.Data_Managers
{
    static class DataBaseManager
    {

        private static DataContext _usersDB;

        public static void Initialize()
        {
            var connString = new SqlConnection();
            connString.ConnectionString =
                "Server=(localdb)\\v11.0;" +
                "Integrated Security=true;";
            connString.Open();
            _usersDB = new DataContext(connString);
        }

    }
}

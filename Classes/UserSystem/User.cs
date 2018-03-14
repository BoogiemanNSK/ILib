using I2P_Project.DataBase;
using System.Linq;

namespace I2P_Project.Classes.UserSystem
{
    /// <summary> User abstract class </summary>
    abstract class User
    {
        /// <summary> Link to DB for methods usage </summary>
        protected LMSDataBase uDB;

        /// <summary> First that we know about user is his login, all other info is taken from DB using it </summary>
        private string _login;
        /// <summary> Row from Users table, for access in constant time </summary>
        private Users _current;
                
        public User(string login)
        {
            _login = login;

            /*string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));*/
            string connString = "Server=tcp:ilibserver.database.windows.net,1433;Initial Catalog=iLibDB;Persist Security Info=False;User ID=iLibAdmin;Password=Faraday28;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            uDB = new LMSDataBase(connString);

            var getUser = (from p in uDB.Users
                           where (p.Login == login)
                           select p);
            _current = getUser.Single();
        }

        /// <summary> Getters from DB </summary>
        public string Name { get { return _current.Name; } }
        public string Adress { get { return _current.Address; } }
        public string PhoneNumber { get { return _current.PhoneNumber; } }
        public int PersonID { get { return _current.Id;  } }
        public int UserType { get { return _current.UserType; } }
    }
}

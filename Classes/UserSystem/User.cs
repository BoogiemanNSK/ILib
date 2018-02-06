using I2P_Project.DataBases;
using System.Linq;

namespace I2P_Project.Classes.UserSystem
{
    /// <summary> User abstract class </summary>
    abstract class User
    {
        /// <summary> First that we know about user is his login, all ather info is taken from DB using it </summary>
        private string _login;
        /// <summary> Row from Users table, for access in constant time </summary>
        private users _current;

        /// <summary> Method to find user`s row given an login </summary>
        protected void SetCurrent(string login)
        {
            _login = login;
            LINQtoUserDBDataContext uDB = new LINQtoUserDBDataContext();
            var getUser = (from p in uDB.users
                           where (p.login == login)
                           select p);
            _current = getUser.Single();
        }

        /// <summary> Getters from DB </summary>
        public string Name { get { return _current.name; } }
        public string Adress { get { return _current.address; } }
        public string PhoneNumber { get { return _current.phoneNumber; } }
        public int LibraryCardNumber { get { return _current.icNumber; } }
        public int PersonID { get { return _current.id;  } }
        public bool IsLibrarian { get { return _current.userType == 2; } }
    }
}

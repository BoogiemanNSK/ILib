using I2P_Project.DataBases;
using System.Linq;

namespace I2P_Project.Classes.UserSystem
{
    abstract class User
    {
        private string _eMail;
        private users _current;

        protected void SetCurrent(string eMail)
        {
            _eMail = eMail;
            LINQtoUserDBDataContext uDB = new LINQtoUserDBDataContext();
            var getUser = (from p in uDB.users
                           where (p.email == eMail)
                           select p);
            _current = getUser.Single();
        }
      
        public string Name { get { return _current.name; } }
        public string Adress { get { return _current.address; } }
        public string PhoneNumber { get { return _current.phoneNumber; } }
        public int LibraryCardNumber { get { return _current.icNumber; } }
    }
}

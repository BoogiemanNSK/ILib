using I2P_Project.DataBase;

namespace I2P_Project.Classes.UserSystem
{
    /// <summary> User abstract class </summary>
    abstract class User
    {
        /// <summary> Row from Users table, for access in constant time </summary>
        protected Users _current;
                
        public User(string login)
        {
            _current = SDM.LMS.GetUserByLogin(login);
        }

        /// <summary> Getters from DB </summary>
        public string Name { get { return _current.Name; } }
        public string Adress { get { return _current.Address; } }
        public string PhoneNumber { get { return _current.PhoneNumber; } }
        public int PersonID { get { return _current.Id;  } }
        public int UserType { get { return _current.UserType; } }
    }
	public enum UserType
	{
		Student,
		Instructor,
		TA,
		VisitingProfessor,
		Professor,
		Librarian,
		Admin
	}
}

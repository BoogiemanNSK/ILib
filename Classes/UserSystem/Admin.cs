namespace I2P_Project.Classes.UserSystem
{
    class Admin : User
    {
        public Admin(string login) : base(login) { }

        /// <summary> Registers a new user in the system </summary>
        public bool RegisterLibrarian(string login, string password, string name, string address, string phone)
        {
            return SDM.LMS.RegisterUser(login, password, name, address, phone, true);
        }

        /// <summary> Updates user info </summary>
        public void ModifyLibrarian(int librarianID, string name, string address, string phoneNumber, int librarianType)
        {
            SDM.LMS.UpdateUser(librarianID, name, address, phoneNumber, librarianType);
        }

        /// <summary> Deletes patron from DB </summary>
        public void DeleteLibrarian(int librarianID)
        {
            SDM.LMS.RemoveUser(librarianID);
        }
    }
}

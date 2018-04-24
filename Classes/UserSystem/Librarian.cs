namespace I2P_Project.Classes.UserSystem
{
    class Librarian : User
    {
        public Librarian(string login) : base(login) {}

        public int LibrarianType { get { return _current.LibrarianType; } }

        #region User Management

        /// <summary> Registers a new user in the system </summary>
        public bool RegisterUser(string login, string password, string name, string adress, string phone, bool isLibrarian)
        {
            if (LibrarianType > 0) {
                return SDM.LMS.RegisterUser(login, password, name, adress, phone, isLibrarian);
            } else {
                return false;
            }
        }

        /// <summary> Updates user info </summary>
        public void ModifyUser(int patronID, string name, string address, string phoneNumber, int userType)
        {
            SDM.LMS.UpdateUser(patronID, name, address, phoneNumber, userType);
        }

        /// <summary> Deletes patron from DB </summary>
        public void DeleteUser(int patronID)
        {
            if (LibrarianType > 1) {
                SDM.LMS.RemoveUser(patronID);
            }
        }

        #endregion

        #region Documents Management

        /// <summary> Adding new book to DB with given parameters </summary>
        public void AddBook(string title, string autors, string publisher, int publishYear, string edition, string description, int price, bool isBestseller, int quantity)
        {
            if (LibrarianType > 0) {
                SDM.LMS.AddBook(title, autors, publisher, publishYear, edition, description, price, isBestseller, quantity);
            }
        }

        /// <summary> Adding new journal to DB with given parameters </summary>
        public void AddJournal(string title, string autors, string publishedIn, string issueTitle, string issueEditor, int price, int quantity)
        {
            if (LibrarianType > 0) {
                SDM.LMS.AddJournal(title, autors, publishedIn, issueTitle, issueEditor, price, quantity);
            }
        }

        /// <summary> Adding new AV to DB with given parameters </summary>
        public void AddAV(string title, string autors, int price, int quantity)
        {
            if (LibrarianType > 0) {
                SDM.LMS.AddAV(title, autors, price, quantity);
            }
        }

        /// <summary> Modifies a book with new parameters </summary>
        public void ModifyBook(int bookID, string title, string autors, string publisher, int publishYear, string edition, string description, int price, bool isBestseller, int quantity)
        {
            SDM.LMS.ModifyBook(bookID, title, autors, publisher, publishYear, edition, description, price, isBestseller, quantity);
        }

        /// <summary> Modifies a journal with new parameters </summary>
        public void ModifyJournal(int journalID, string title, string autors, string publishedIn, string issueTitle, string issueEditor, int price, int quantity)
        {
            SDM.LMS.ModifyJournal(journalID, title, autors, publishedIn, issueTitle, issueEditor, price, quantity);
        }

        /// <summary> Modifies an AV with new parameters </summary>
        public void ModifyAV(int AVID, string title, string autors, int price, int quantity)
        {
            SDM.LMS.ModifyAV(AVID, title, autors, price, quantity);
        }

        /// <summary> Deletes a doc from the system </summary>
        public void DeleteDoc(int docID)
        {
            if (LibrarianType > 1) {
                SDM.LMS.RemoveDocument(docID);
            }
        }

        /// <summary> Sets an outstanding request for the document (deletes queue) </summary>
        public void OutstandingRequest(int docID)
        {
            if (LibrarianType > 1) {
                SDM.LMS.SetOutstandingRequest(docID);
            }
        }

        #endregion
    }
    /// <summary> Structure to take information about users checkouts/// </summary>
    public struct CheckedOut
    {
        public string DocumentCheckedOut { get; set; }   
        public int CheckOutTime { get; set; }
    }
	/// <summary> Structure to take information about users overdue /// </summary>
	public struct OverdueInfo
    {
        public string DocumentChekedOut { get; set; }
        public int Overdue { get; set; }
    }
}

using System.Collections.Generic;

namespace I2P_Project.Classes.UserSystem
{

    class Librarian : User
    {
        public Librarian(string login) : base(login) {}

        #region User Management

        /// <summary> Registers a new user in the system </summary>
        public bool RegisterUser(string login, string password, string name, string adress, string phone, bool isLibrarian)
        {
            return SDM.LMS.RegisterUser(login, password, name, adress, phone, isLibrarian);
        }

        /// <summary> Updates user info </summary>
        public void ModifyUser(int patronID, string name, string address, string phoneNumber, int userType)
        {
            SDM.LMS.UpdateUser(patronID, name, address, phoneNumber, userType);
        }

        /// <summary> Deletes patron from DB </summary>
        public void DeleteUser(int patronID)
        {
            SDM.LMS.RemoveUser(patronID);
        }

        #endregion

        #region Documents Management

        /// <summary> Adding new book to DB with given parameters </summary>
        public void AddBook(string title, string autors, string publisher, int publishYear, string edition, string description, int price, bool isBestseller, int quantity)
        {
            SDM.LMS.AddBook(title, autors, publisher, publishYear, edition, description, price, isBestseller, quantity);
        }

        /// <summary> Adding new journal to DB with given parameters </summary>
        public void AddJournal(string title, string autors, string publishedIn, string issueTitle, string issueEditor, int price, int quantity)
        {
            SDM.LMS.AddJournal(title, autors, publishedIn, issueTitle, issueEditor, price, quantity);
        }

        /// <summary> Adding new AV to DB with given parameters </summary>
        public void AddAV(string title, string autors, int price, int quantity)
        {
            SDM.LMS.AddAV(title, autors, price, quantity);
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
            SDM.LMS.RemoveDocument(docID);
        }

        /// <summary> Sets an outstanding request for the document (deletes queue) </summary>
        public void OutstandingRequest(int docID)
        {
            SDM.LMS.SetOutstandingRequest(docID);
        }

        #endregion

        #region TESTING

        public void UpgradeUser(string Name, int ut)
        {
            SDM.LMS.UpgradeUser(Name, ut);
        }

        public List<CheckedOut> CheckCheckouts(string Name)
        {
            return SDM.LMS.GetCheckout(Name);
        }

        public string ShowUserCard(string Name)
        {
            string output = "";
            var patron = SDM.LMS.GetPatronByName(Name);
            if (patron == null)
                output = SDM.Strings.USER_DOES_NOT_EXIST_TEXT;
            else
            {
                Pages.UserCard card = new Pages.UserCard(patron.userID);
                card.Show();
                output = SDM.Strings.USER_CARD_OBTAINING_TEXT;
            }

            return output;
        }

        public string ShowOverdue(string Name)
        {
            string output = "";
            var patron = SDM.LMS.GetPatronByName(Name);
            if (patron == null)
                output = SDM.Strings.USER_DOES_NOT_EXIST_TEXT;
            else
            {
                Pages.OverdueInfo doc = new Pages.OverdueInfo(patron.userID);
                doc.Show();
                output = SDM.Strings.OVERDUE_INFO_TEXT;
            }

            return output;
        }

        #endregion

    }

    // TODO Нужны ли эти структуры?

    public struct CheckedOut
    {
        public string DocumentCheckedOut { get; set; }   
        public int CheckOutTime { get; set; }
    }

    public struct OverdueInfo
    {
        public string DocumentChekedOut { get; set; }
        public int DocID { get; set; }
        public int Overdue { get; set; }
    }

}

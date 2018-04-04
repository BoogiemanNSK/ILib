using System;
using System.Collections.Generic;
using System.Linq;

namespace I2P_Project.Classes.UserSystem
{

    class Librarian : User
    {
        public Librarian(string login) : base(login) {}

        public List<CheckedOut> CheckCheckouts(string Name)
        {
            return SDM.LMS.GetCheckout(Name);            
        }

        /// <summary> Deletes patron from DB </summary>
        public void DeleteUser(int patronID)
        {
            SDM.LMS.RemoveUser(patronID);
        }
        public void UpgradeUser(string Name, int ut)
        {
            SDM.LMS.UpgradeUser(Name, ut);
        }
        public void ModifyUser(int patronID, string Name, string Adress, string PhoneNumber, int userType)
        {
            SDM.LMS.UpdateUser(patronID, Name, Adress, PhoneNumber, userType);
        }

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
        
        public void DeleteDoc(int docID)
        {
            SDM.LMS.RemoveDocument(docID);
        }

        public void DeleteDoc(string Title)
        {
            SDM.LMS.RemoveDocument(Title);
        }

        public void ModifyDoc(int DocID, string Title, string Description, string Price, bool IsBestseller,
            int DocType)
        {
            SDM.LMS.ModifyDoc(DocID, Title, Description, Price, IsBestseller, DocType);
        }

        public bool RegisterUser(string login, string password, string name, string adress, string phone, bool isLibrarian)
        {
            return SDM.LMS.RegisterUser(login, password, name, adress, phone, isLibrarian);
        }

        public string ShowUserCard (string Name)
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

    }

    public struct CheckedOut
    {
        public string DocumentCheckedOut { get; set; }   
        public int CheckOutTime { get; set; }
    }

    public struct OverdueInfo
    {
        public string DocumentChekedOut { get; set; }
        public int DocID { get; set; }
        public int overdue { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;

namespace I2P_Project.Classes.UserSystem
{

    class Librarian : User
    {
        public Librarian(string login) : base(login) {}

        public List<OverdueInfo> CheckOverdue()
        {
            List<OverdueInfo> ovList = new List<OverdueInfo>();
            // TODO
            return ovList;
        }

        /// <summary> Deletes patron from DB </summary>
        public void DeleteUser(int patronID)
        {
            SDM.LMS.RemoveUser(patronID);
        }

        public void ModifyUser(int patronID, string Name, string Adress, string PhoneNumber, int userType)
        {
            SDM.LMS.UpdateUser(patronID, Name, Adress, PhoneNumber, userType);
        }

        /// <summary> Adding new doc to DB with given parameters </summary>
        public void AddDoc(string title, string description, int docType, int price, bool isBestseller)
        {
            SDM.LMS.AddDoc(title, description, docType, price, isBestseller);
        }

        public void DeleteDoc(int docID)
        {
            SDM.LMS.RemoveDocument(docID);
        }

        public void DeleteDoc(string Title)
        {
            SDM.LMS.RemoveDocument(Title);
        }

        public void ModifyDoc(int doc_id, string Title, string Description, string Price, string IsBestseller,
            string DocType)
        {
            SDM.LMS.ModifyDoc(doc_id, Title, Description, Price, IsBestseller, DocType);
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
            var patron = SDM.LMS.PatronbyName(Name);
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

    public struct OverdueInfo
    {
        Patron OverduedPatron { get; }   
        DateTime CheckOutTime { get; }
    }

}

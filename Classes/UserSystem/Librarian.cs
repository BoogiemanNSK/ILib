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

        public Pages.UserTable PatronbyName (string name)
        {
            var table = SDM.LMS.TestUsersTable();

            var patron = (from p in table where p.userName.Equals(name) select p).FirstOrDefault();

            return patron;
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

    }

    public struct OverdueInfo
    {
        Patron OverduedPatron { get; }   
        DateTime CheckOutTime { get; }
    }

}

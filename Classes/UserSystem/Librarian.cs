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
            // TODO for JIObCTEP

            // Query to get row of user with patron ID
            // Get row from query -> query.Single();
            // Delete row from db
            // Submit changes
        }

        public void ModifyUser(int patronID, string Name, string Adress, string PhoneNumber, int userType)
        {
            SDM.LMS.UpdateUser(patronID, Name, Adress, PhoneNumber, userType);
        }

        /// <summary> Adding new doc to DB with given parameters </summary>
        public void AddDoc(string title, string description, int docType, int price, bool isBestseller)
        {
            bool isReference = !CheckReference(title);
            DataBase.Document newDoc = new DataBase.Document();
            newDoc.Title = title;
            newDoc.Description = description;
            newDoc.Price = price;
            newDoc.DocType = docType;
            newDoc.IsReference = isReference;
            newDoc.IsBestseller = isBestseller;
            uDB.Documents.InsertOnSubmit(newDoc);
            uDB.SubmitChanges();
        }

        public void DeleteDoc(int docID)
        {
            SDM.LMS.RemoveDocument(docID);
        }

        public void ModifyDoc(int doc_id, string Title, string Description, string Price, string IsBestseller,
            string DocType)
        {
            SDM.LMS.ModifyDoc(doc_id, Title, Description, Price, IsBestseller, DocType);
        }

        /// <summary> Checks if there exist a reference doc with given title </summary>
        private bool CheckReference(string title)
        {
            var test = (from p in uDB.Documents
                        where (p.Title == title)
                        select p);
            return test.Any();
        }
    }

    public struct OverdueInfo
    {
        Patron OverduedPatron { get; }   
        DateTime CheckOutTime { get; }
    }

}

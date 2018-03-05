using System;
using System.Collections.Generic;
using System.Linq;
using I2P_Project.DataBases;

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

        /// <summary> User becomes faculty if they were student and vice-versa </summary>
        public void SwapUserType(int patronID)
        {
            var test = (from p in uDB.users
                        where (p.id == patronID)
                        select p);
            if (test.Any())
            {
                users user = test.Single();
                user.userType = user.userType == 0 ? 1 : 0;
            }
        }

        /// <summary> User becomes faculty if they were student and vice-versa </summary>
        public void SwapUserType(string patronName)
        {
            var test = (from p in uDB.users
                        where (p.name == patronName)
                        select p);
            if (test.Any())
            {
                users user = test.Single();
                user.userType = user.userType == 0 ? 1 : 0;
            }
        }

        /// <summary> Deletes patron from DB </summary>
        public void DeleteUser(int patronID)
        {
            SDM.LMS.RemoveUser(patronID);
        }

        /// <summary> Adding new doc to DB with given parameters </summary>
        public void AddDoc(string title, string description, int docType, int price, bool isBestseller)
        {
            bool isReference = !CheckReference(title);
            document newDoc = new document();
            newDoc.Title = title;
            newDoc.Description = description;
            newDoc.Price = price;
            newDoc.DocType = docType;
            newDoc.IsReference = isReference;
            newDoc.IsBestseller = isBestseller;
            uDB.documents.InsertOnSubmit(newDoc);
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
            var test = (from p in uDB.documents
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

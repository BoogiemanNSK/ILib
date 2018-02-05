using I2P_Project.DataBases;
using I2P_Project.Classes.Data_Managers;
using System.Collections.Generic;

namespace I2P_Project.Classes.UserSystem
{

    class Faculty : Patron
    {
        public Faculty(string eMail)
        {
            CheckedDocs = new List<int>();
            SetCurrent(eMail);
        }

        public override string CheckOut(int docID)
        {
            document doc = DataBaseManager.GetDoc(docID);
            if (doc.Count == 0) return "Book is not availible for now, please come back later";
            if (CheckedDocs.Contains(docID)) return "You already have this book on your account";
            doc.Count--;
            CheckedDocs.Add(docID);
            DataBaseManager.SetReferAndStartTimer(docID);
            return "Checked out " + doc.Title + " successfully!";
        }
        
    }

}

using I2P_Project.Classes.Data_Managers;
using I2P_Project.DataBases;
using System.Collections.Generic;

namespace I2P_Project.Classes.UserSystem
{

    class Student : Patron
    {
        public Student(string eMail)
        {
            CheckedDocs = new List<int>();
            SetCurrent(eMail);
        }
        
        public override string CheckOut(int docID)
        {
            documents doc = DataBaseManager.GetDoc(docID);
            if (doc.Count == 0) return "Book is not availible for now, please come back later";
            if (CheckedDocs.Contains(docID)) return "You already have this book on your account";
            doc.Count--;
            //CheckedDocs.Add(docID);
            int user_id = SystemDataManager.CurrentUser.PersonID;
            DataBaseManager.SetCheckOut(docID, user_id);
            return "Checked out " + doc.Title + " successfully!";
        }

        public override string CheckOut(string author)
        {

            documents doc = DataBaseManager.GetDoc(author);
            if (doc.Count == 0) return "Book is not availible for now, please come back later";
            if (CheckedDocs.Contains(doc.Id)) return "You already have this book on your account";
            doc.Count--;
            //CheckedDocs.Add(docID);
            int user_id = SystemDataManager.CurrentUser.PersonID;
            DataBaseManager.SetCheckOut(doc.Id, user_id);
            return "Checked out " + doc.Title + " successfully!";
        }
    }

}

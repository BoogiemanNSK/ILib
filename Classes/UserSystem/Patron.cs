using I2P_Project.Classes.Data_Managers;
using I2P_Project.DataBases;
using System.Collections.Generic;

namespace I2P_Project.Classes.UserSystem
{

    abstract class Patron : User
    {
        public List<int> CheckedDocs;

        public abstract string CheckOut(int docID);

        public abstract string CheckOut(string author);

        public string ReturnDoc(int docID)
        {
            documents doc = DataBaseManager.GetDoc(docID);
            doc.Count++;
            CheckedDocs.Remove(docID);
            // TODO Remove deadline
            return doc.Title + " returned!";
        }
    }

}

using I2P_Project.Classes.Data_Managers;
using I2P_Project.DataBases;
using System.Collections.Generic;

namespace I2P_Project.Classes.UserSystem
{

    abstract class Patron : User
    {
        public List<int> CheckedDocs;

        public string CheckOut(int docID)
        {
            return CheckOut(DataBaseManager.GetTitleByID(docID));
        }

        public abstract string CheckOut(string author);

        // TODO Всё переделать 
        public string ReturnDoc(int docID)
        {
            document doc = DataBaseManager.GetFreeCopy(DataBaseManager.GetTitleByID(docID));
            CheckedDocs.Remove(docID);
            // TODO Remove deadline
            return doc.Title + " returned!";
        }
    }

}

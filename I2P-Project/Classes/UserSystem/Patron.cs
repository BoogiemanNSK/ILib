using System.Collections.Generic;

namespace I2P_Project.Classes.UserSystem
{

    abstract class Patron : User
    {

        public List<int> CheckedDocs;

        public abstract void CheckOut(int docID);

        public void ReturnDoc(int docID)
        {
            CheckedDocs.Remove(docID);
            // TODO 
        }

    }

}

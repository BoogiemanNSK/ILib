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
            CheckedDocs.Add(docID);
            DataBaseManager.SetReferAndStartTimer(docID);
        }

        

    }

}

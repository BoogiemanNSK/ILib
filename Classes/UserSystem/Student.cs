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

        public override string CheckOut(string title)
        {
            document doc = DataBaseManager.GetFreeCopy(title);
            if (doc == null) return "Book is not availible for now, please come back later";
            int user_id = SystemDataManager.CurrentUser.PersonID;

            if (doc.IsBestseller || doc.DocType != 0)
                DataBaseManager.SetCheckOut(doc.Id, user_id, 2);
            else
                DataBaseManager.SetCheckOut(doc.Id, user_id, 3);

            return "Checked out " + doc.Title + " successfully!";
        }
    }

}

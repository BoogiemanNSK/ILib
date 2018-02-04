using I2P_Project.Classes.Data_Managers;
namespace I2P_Project.Classes.UserSystem
{

    class Student : Patron
    {
        public Student(string eMail)
        {
            SetCurrent(eMail);
        }
        
        public override void CheckOut(int docID)
        {
            CheckedDocs.Add(docID);
            DataBaseManager.SetReferAndStartTimer(docID);
        }
    }

}

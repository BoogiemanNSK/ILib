using I2P_Project.DataBases;
namespace I2P_Project.Classes.UserSystem
{

    class Faculty : Patron
    {
        public Faculty(string eMail)
        {
            SetCurrent(eMail);
        }

        public override void CheckOut(int docID)
        {
            CheckedDocs.Add(docID);
        }

        

    }

}

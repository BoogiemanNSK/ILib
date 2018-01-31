namespace I2P_Project.Classes.UserSystem
{

    class Student : Patron
    {

        public override void CheckOut(int docID)
        {
            CheckedDocs.Add(docID);
            // TODO
        }

    }

}

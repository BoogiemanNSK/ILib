namespace I2P_Project.Classes.UserSystem
{

    class Faculty : Patron
    {

        public override void CheckOut(int docID)
        {
            CheckedDocs.Add(docID);
            // TODO
        }

    }

}

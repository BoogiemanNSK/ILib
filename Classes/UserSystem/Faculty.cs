namespace I2P_Project.Classes.UserSystem
{

    class Faculty : Patron
    {
        public Faculty(string login) : base(login) {} 

        /// <summary> Checks out a book for a current faculty user </summary>
        /// <returns> Result of check out as message </returns>
        public override string CheckOut(string title, params int[] DateCheat)
        {
            DataBase.Document doc = null;
            string result = CheckAvailibility(title);

            if (result != "") return result;

            doc = GetDocumentForCheckOut(title);

            if (doc.DocType != 0)
                SetCheckOut(doc.Id, 2, DateCheat);
            else
                SetCheckOut(doc.Id, 4, DateCheat);

            return SDM.Strings.SUCCESS_CHECK_OUT_TEXT + " " + doc.Title + " !";
        }

    }

}

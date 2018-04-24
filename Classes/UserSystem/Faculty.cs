namespace I2P_Project.Classes.UserSystem
{
    class Faculty : Patron
    {
        public Faculty(string login) : base(login) {} 

        /// <summary> Checks out a book for a current faculty user </summary>
        /// <returns> Result of check out as message </returns>
        public override string CheckOut(int DocID, params int[] DateCheat)
        {
            DataBase.Document doc = SDM.LMS.GetDoc(DocID);

            if (doc == null) return SDM.Strings.DOC_DOES_NOT_EXIST;
            string result = CheckAvailibility(doc);
            
            if (result == SDM.Strings.PERSON_NOT_IN_QUEUE_TEXT)
            {
                SDM.LMS.PushInPQ(doc.Id, PersonID, UserType);
                return result;
            }
            else if (result == SDM.Strings.PERSON_FIRST_IN_QUEUE_TEXT)
            {
                SDM.LMS.PopFromPQ(doc.Id);
                if (doc.Quantity > 1)
                    SDM.LMS.NotifyNextUser(DocID, SDM.Strings.MAIL_BOOK_AVAILIBLE_TITLE, SDM.Strings.MAIL_BOOK_AVAILIBLE_TEXT(doc.Title, SDM.Strings.DOC_TYPES[doc.DocType]));
            }
            else if (result != "") return result;

            SDM.LMS.ModifyAV(DocID, doc.Title, doc.Autors, doc.Price, doc.Quantity - 1, doc.Tags);

            if (doc.DocType != 0)
                SDM.LMS.SetCheckOut(PersonID, doc.Id, 2, DateCheat);
            else
                SDM.LMS.SetCheckOut(PersonID, doc.Id, 4, DateCheat);

            return SDM.Strings.SUCCESS_CHECK_OUT_TEXT + " " + doc.Title + " !";
        }
    }
}

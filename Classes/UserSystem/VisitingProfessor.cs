namespace I2P_Project.Classes.UserSystem
{
    class VisitingProfessor : Patron
    {
        public VisitingProfessor(string login) : base(login) { }

        /// <summary> Checks out a book for a current student user </summary>
        /// <returns> Result of check out as message </returns>
        public override string CheckOut(int DocID, params int[] DateCheat)
        {
            DataBase.Document doc = SDM.LMS.GetDoc(DocID);
            string result = CheckAvailibility(doc);

            if (result == SDM.Strings.PERSON_NOT_IN_QUEUE_TEXT)
            {
                SDM.LMS.PushInPQ(doc.Id, PersonID, UserType);
                return result;
            }
            else if (result == SDM.Strings.PERSON_FIRST_IN_QUEUE_TEXT)
            {
                SDM.LMS.PopFromPQ(doc.Id);
            }
            else if (result != "") return result;

            SDM.LMS.ModifyAV(DocID, doc.Title, doc.Autors, doc.Price, doc.Quantity - 1);

            SDM.LMS.SetCheckOut(PersonID, doc.Id, 1, DateCheat);

            return SDM.Strings.SUCCESS_CHECK_OUT_TEXT + " " + doc.Title + " !";
        }
    }
}

using I2P_Project.DataBase;
using System;

namespace I2P_Project.Classes.UserSystem
{
    abstract class Patron : User
    {
        public Patron(string login) : base(login) {}

        /// <summary> Check out by ID </summary>
        /// <returns> Result of check out as message </returns>
        public abstract string CheckOut(int docID, params int[] DateCheat);

        /// <summary> Renews a book (can use a datecheat for testing) </summary>
        public string RenewDoc(int docID, params int[] DateCheat)
        {
            return SDM.LMS.RenewDoc(PersonID, docID, DateCheat);
        }

        /// <summary> Returns a document from a user to the LMS </summary>
        /// <returns> Result of returning doc as message </returns>
        public string ReturnDoc(int docID)
        {
            Checkouts checkout = SDM.LMS.GetCheckout(PersonID, docID);
            Document doc = SDM.LMS.GetDoc(docID);

            // User has fine
            if (SDM.LMS.GetUserFineForDoc(PersonID, docID) > 0)
                return SDM.Strings.USER_HAVE_FINE;

            SDM.LMS.RemoveCheckout(docID, PersonID);
            SDM.LMS.ModifyAV(docID, doc.Title, doc.Autors, doc.Price, doc.Quantity + 1);
            SDM.LMS.NotifyNextUser(docID, SDM.Strings.MAIL_BOOK_AVAILIBLE_TITLE, SDM.Strings.MAIL_BOOK_AVAILIBLE_TEXT(doc.Title, SDM.Strings.DOC_TYPES[doc.DocType]));

            return SDM.Strings.SUCCESSFUL_RETURN + " " + doc.Title + "!";
        }

        public void PayFine(int docID)
        {
            SDM.LMS.UpdateDeadline(PersonID, docID, DateTime.Now);
        }
        
        protected string CheckAvailibility(Document doc)
        {
            if (doc.IsRequested)
                return SDM.Strings.DOC_IS_REQUESTED;
            else if (SDM.LMS.GetCheckout(PersonID, doc.Id) != null)
                return SDM.Strings.ALREADY_HAVE_TEXT;
            else if (doc.Queue.Length > 0) {
                if (doc.Queue.Split('|')[0].Equals(PersonID.ToString()))
                    return SDM.Strings.PERSON_FIRST_IN_QUEUE_TEXT;
                else if (PersonInQueue(doc.Queue))
                    return SDM.Strings.PERSON_IN_QUEUE_TEXT;
                else
                    return SDM.Strings.PERSON_NOT_IN_QUEUE_TEXT;
            } else if (doc.Quantity == 0)
                return SDM.Strings.PERSON_NOT_IN_QUEUE_TEXT;
            return "";
        }

        /// <summary> Checks if person is in queue for given doc </summary>
        private bool PersonInQueue(string queue_string)
        {
            bool inQueue = false;
            
            string[] queue_pairs = queue_string.Split('-');
            foreach (string pair in queue_pairs)
            {
                int id = Convert.ToInt32(pair.Split('|')[0]);
                if (id == PersonID) inQueue = true;
            }

            return inQueue;
        }
    }
}

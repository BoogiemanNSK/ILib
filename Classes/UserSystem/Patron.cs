using I2P_Project.DataBase;
using System.Linq;

namespace I2P_Project.Classes.UserSystem
{

    abstract class Patron : User
    {
        public Patron(string login) : base(login) {}

        /// <summary> Check out by ID </summary>
        /// <returns> Result of check out as message </returns>
        public string CheckOut(int docID, params int[] DateCheat)
        {
            return CheckOut(GetTitleByID(docID), DateCheat);
        }

        /// <summary> Check out by Title </summary>
        /// <returns> Result of check out as message </returns>
        public abstract string CheckOut(string title, params int[] DateCheat);

        public string RenewDoc(int docID, params int[] DateCheat)
        {
            return SDM.LMS.RenewDoc(docID, DateCheat);
        }

        /// <summary> Returns a document from a user to the LMS </summary>
        /// <returns> Result of returning doc as message </returns>
        public string ReturnDoc(int docID)
        {
            var test = from c in uDB.Checkouts
                       where (c.BookID == docID & c.UserID == PersonID)
                       select c;

            Checkouts checkout = test.Single();
            Document doc = SDM.LMS.GetDocByID(docID);

            // User has fine
            if (SDM.LMS.GetUserFineForDoc(PersonID, docID) > 0)
                return SDM.Strings.USER_HAVE_FINE;

            doc.Quantity++;
            uDB.Checkouts.DeleteOnSubmit(checkout);
            uDB.SubmitChanges();

            SDM.LMS.NotifyNextUser(docID);

            return SDM.Strings.SUCCESSFUL_RETURN + " " + GetTitleByID(docID) + "!";
        }

        public void PayFine(int docID)
        {
            var test = from c in uDB.Checkouts
                       where (c.BookID == docID & c.UserID == PersonID)
                       select c;
            Checkouts checkout = test.Single();
            checkout.TimeToBack = System.DateTime.Now;
            uDB.Refresh(System.Data.Linq.RefreshMode.KeepChanges, checkout);
            uDB.SubmitChanges();
        }
        
        protected string CheckAvailibility(string title)
        {
            Document selected = GetDocumentForCheckOut(title);

            if (DocBelongsToUser(selected.Id))
                return SDM.Strings.ALREADY_HAVE_TEXT;
            else if (selected.Queue.Length > 0)
            {
                if (selected.Queue.Split('|')[0].Equals(PersonID.ToString()))
                    return SDM.Strings.PERSON_FIRST_IN_QUEUE_TEXT;
                else if (SDM.LMS.IsPersonInQueue(PersonID, selected.Id))
                    return SDM.Strings.PERSON_IN_QUEUE_TEXT;
                else
                    return SDM.Strings.PERSON_NOT_IN_QUEUE_TEXT;
            }
            else if (selected.Quantity == 0)
                return SDM.Strings.PERSON_NOT_IN_QUEUE_TEXT;
            return "";
        }

        protected Document GetDocumentForCheckOut(string title)
        {
            var test = from b in uDB.Documents
                       where b.Title.Equals(title)
                       select b;
            return test.Single();
        }

        /// <summary>
        /// Change fields in DB when some user check out docs.
        /// Start timer for check out and get reference for book
        /// on it's owner.
        /// DateCheat format - dd mm yyyy
        /// </summary>
        protected void SetCheckOut(int docID, int weeks, params int[] DateCheat)
        {
            System.DateTime time;
            if (DateCheat.Length == 0)
                time = System.DateTime.Now;
            else
                time = new System.DateTime(DateCheat[2], DateCheat[1], DateCheat[0]);

            Checkouts chk = new Checkouts
            {
                UserID = PersonID,
                BookID = docID,
                IsReturned = false,
                DateTaked = time,
                TimeToBack = time.AddDays(weeks * 7)
            };

            uDB.Checkouts.InsertOnSubmit(chk);
            uDB.SubmitChanges();
        }

        /// <summary> Checks if current user has book with given ID </summary>
        protected bool DocBelongsToUser(int docID)
        {
            var test = from c in uDB.Checkouts
                       where (c.BookID == docID & c.UserID == PersonID)
                       select c;
            return test.Any();
        }

        /// <summary> Return book title from ID </summary>
        private string GetTitleByID(int docID)
        {
            var test = (from p in uDB.Documents
                        where (p.Id == docID)
                        select p);
            return test.First().Title;
        }
    }

}

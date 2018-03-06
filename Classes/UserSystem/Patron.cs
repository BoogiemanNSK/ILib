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

        /// <summary> Returns a document from a user to the LMS </summary>
        /// <returns> Result of returning doc as message </returns>
        public string ReturnDoc(int docID)
        {
            var test = from c in uDB.Checkouts
                       where (c.BookID == docID & c.UserID == PersonID)
                       select c;
            Checkouts checkout = test.Single();

            uDB.Checkouts.DeleteOnSubmit(checkout);
            uDB.SubmitChanges();

            return SDM.Strings.SUCCESSFUL_RETURN + " " + GetTitleByID(docID) + "!";
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

            Checkouts chk = new Checkouts();
            chk.UserID = PersonID;
            chk.BookID = docID;
            chk.IsReturned = false;
            chk.DateTaked = time;
            chk.TimeToBack = time.AddDays(weeks * 7);
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

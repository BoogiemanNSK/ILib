using I2P_Project.DataBases;
using System.Linq;

namespace I2P_Project.Classes.UserSystem
{

    abstract class Patron : User
    {
        public Patron(string login) : base(login) {}

        public string CheckOut(int docID)
        {
            return CheckOut(GetTitleByID(docID));
        }

        public abstract string CheckOut(string title);

        public string ReturnDoc(int docID)
        {
            // TODO Implement return doc
            return "";
        }

        /// <summary>
        /// Change fields in DB when some user check out docs.
        /// Start timer for check out and get reference for book
        /// on it's owner.
        /// </summary>
        /// <param name="docID"></param>
        protected void SetCheckOut(int docID, int user_id, int weeks)
        {
            System.DateTime time = System.DateTime.Now;

            checkouts chk = new checkouts();
            chk.userID = user_id;
            chk.bookID = docID;
            chk.isReturned = false;
            chk.dateTaked = time;
            chk.timeToBack = time.AddDays(weeks * 7);
            uDB.checkouts.InsertOnSubmit(chk);
            uDB.SubmitChanges();
        }

        protected bool DocBelongsToUser(int userID, int docID)
        {
            var test = from c in uDB.checkouts
                       where (c.bookID == docID & c.userID == userID)
                       select c;
            return test.Any();
        }

        private string GetTitleByID(int docID)
        {
            var test = (from p in uDB.documents
                        where (p.Id == docID)
                        select p);
            return test.First().Title;
        }
    }

}

using I2P_Project.DataBase;
using System.Collections.Generic;
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

        public string RenewDoc(int docID)
        {
            Patron patron = (Patron)SDM.CurrentUser;
            var doc = (from b in uDB.Checkouts
                       where b.BookID == docID
                       select b).Single();
            if (doc.IsRenewed)
                return SDM.Strings.DOC_ALREADY_RENEWED;
            else if (SDM.LMS.ExistQueueForDoc(docID))
                 return SDM.Strings.DOC_IN_QUEUE;
            else if (SDM.LMS.GetUserFine(patron.PersonID)>0)
            {
                return SDM.Strings.USER_HAVE_FINE;
            }
            else
            {
                 doc.TimeToBack = System.DateTime.Now.Add(doc.TimeToBack.Subtract((System.DateTime)doc.DateTaked));
                 doc.DateTaked = System.DateTime.Now;
                 doc.IsRenewed = true;
                 uDB.SubmitChanges();
                 return SDM.Strings.SUCCESSFUL_RENEW;
            }
        }

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
        
        protected string CheckAvailibility(string title)
        {
            var test = from b in uDB.Documents
                       where b.Title.ToLower().Contains(title.ToLower()) && !b.IsReference
                       select b;

            if (test.Any()) // Check if any copies of doc exists
            {
                foreach (DataBase.Document selected in test.ToArray()) // Checks that book doesnt`t belong to user already
                    if (DocBelongsToUser(selected.Id))
                        return SDM.Strings.ALREADY_HAVE_TEXT;
                foreach (DataBase.Document selected in test.ToArray()) // Checks if any of them are free
                {
                    var test2 = from c in uDB.Checkouts
                                where c.BookID == selected.Id
                                select c;
                    if (!test2.Any()) return "";
                }
                return SDM.Strings.NO_FREE_COPIES_TEXT;
            }
            else
                return SDM.Strings.NO_FREE_COPIES_TEXT;
        }

        protected DataBase.Document GetDocumentForCheckOut(string title)
        {
            var test = from b in uDB.Documents
                       where b.Title.ToLower().Contains(title.ToLower()) && !b.IsReference
                       select b;

            foreach (DataBase.Document selected in test.ToArray())
            {
                var test2 = from c in uDB.Checkouts
                            where c.BookID == selected.Id
                            select c;
                if (!test2.Any()) return selected;
            }

            return null;
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

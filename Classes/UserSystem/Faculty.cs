using System.Linq;

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
                    if (!test2.Any()) doc = selected;
                    else return SDM.Strings.NO_FREE_COPIES_TEXT;
                }
            }
            else
                return SDM.Strings.NO_FREE_COPIES_TEXT;

            if (doc.DocType != 0)
                SetCheckOut(doc.Id, 2, DateCheat);
            else
                SetCheckOut(doc.Id, 4, DateCheat);

            return SDM.Strings.SUCCESS_CHECK_OUT_TEXT + " " + doc.Title + " !";
        }

    }

}

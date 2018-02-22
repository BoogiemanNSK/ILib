﻿using I2P_Project.DataBases;
using System.Linq;

namespace I2P_Project.Classes.UserSystem
{

    class Faculty : Patron
    {
        public Faculty(string login) : base(login) {} 

        public override string CheckOut(string title)
        {
            document doc = null;
            var test = from b in uDB.documents
                       where b.Title.ToLower().Contains(title.ToLower()) && !b.IsReference
                       select b;
            if (test.Any()) // Check if any copies of doc exists
            {
                foreach (document selected in test.ToArray()) // Checks that book doesnt`t belong to user already
                    if (DocBelongsToUser(SDM.CurrentUser.PersonID, selected.Id))
                        return SDM.Strings.ALREADY_HAVE_TEXT;
                foreach (document selected in test.ToArray()) // Checks if any of them are free
                {
                    var test2 = from c in uDB.checkouts
                                where c.bookID == selected.Id
                                select c;
                    if (!test2.Any()) doc = selected;
                    else return SDM.Strings.NO_FREE_COPIES_TEXT;
                }
            }
            else
                return SDM.Strings.NO_FREE_COPIES_TEXT;

            int user_id = SDM.CurrentUser.PersonID;

            if (doc.DocType != 0)
                SetCheckOut(doc.Id, user_id, 2);
            else
                SetCheckOut(doc.Id, user_id, 4);

            return SDM.Strings.SUCCESS_CHECK_OUT_TEXT + " " + doc.Title + " !";
        }

    }

}

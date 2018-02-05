using System;
using System.Linq;
using I2P_Project.DataBases;

namespace I2P_Project.Classes.Documents
{
    /// <summary>
    /// Main class for Documents.
    /// Contain all fields.
    /// </summary>
    abstract class Document
    {
        private int _docID;
        private document _current;
        
        protected void setCurrent(int docID)
        {
            _docID = docID;
            LINQtoUserDBDataContext dDB = new LINQtoUserDBDataContext();
            var getDoc = (from p in dDB.documents
                          where (p.Id == docID)
                          select p);
            _current = getDoc.Single();
        }

        public string Title { get { return _current.Title; } }

        public string Description { get { return _current.Description;  } }
        
        public bool IsBestseller { get { return _current.IsBestseller; } }

        public DateTime TimeOfCheckOut { get { return _current.CheckOutTime;  } }

        public int PersonID { get { return _current.OwnerID;  } }
        
        public int DocType {  get { return _current.DocType;  } }
        
    }
}

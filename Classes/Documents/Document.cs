
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private DocumentsDB _current;
        
        protected void setCurrent(int docID)
        {
            _docID = docID;
            LINQtoDocumentsDBDataContext dDB = new LINQtoDocumentsDBDataContext();
            var getDoc = (from p in dDB.DocumentsDB
                          where (p.docID == docID)
                          select p);
            _current = getDoc.Single();
        }
        public string Title { get {  return _current.title } }

        public string Description { get { return _current.description;  } }

        public int Price {  get { return _current.price;  } }
        
        public bool IsBestseller { get { return _current.isBesteller == 1; } }

        public DateTime TimeOfCheckOut { get {  return _current.timeOfCheckOut} }

        public int PersonID { get { return _current.personID;  } }
        
        public int DocType {  get { return _current.docType;  } }
        
    }
}

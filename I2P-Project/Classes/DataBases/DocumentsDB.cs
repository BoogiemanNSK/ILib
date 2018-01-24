using System.Data.Linq.Mapping;
using System;
namespace I2P_Project.Classes.DataBases
{
    class DocumentsDB
    {
        private string _Title;
        private string _Description;
        private int _Price; //in rubles
        private bool _IsBesteller; //2 weeks for check out
        private int _TimeOfCheckOut;
        private int _PersonID;


        [Column(Storage = "_title", DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public string Title { get; }

        [Column(Storage = "_description", DbType = "NChar(16)")]
        public string Description { get; set; }

        [Column(Storage = "_Adress", DbType = "NChar(64)")]
        public string Price{ get; set; }

        [Column(Storage = "_PhoneNumber", DbType = "NChar(16)")]
        public string IsBesteller{ get; set; }

        [Column(Storage = "_LCNumber", DbType = "Int")]
        public int TimeOfChekOut{ get; set; }

        [Column(Storage = "_CheckedBookID", DbType = "Int")]
        public int PersonID { get; set; }


    }
}

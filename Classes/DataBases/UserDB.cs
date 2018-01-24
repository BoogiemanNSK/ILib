using System.Data.Linq.Mapping;

namespace I2P_Project.Classes.DataBases
{
    [Table(Name = "Users.DB")]
    class UserDB
    {
        /// <summary> Столбцы базы данных </summary>
        private int _ID;
        private string _Name;
        private string _Adress;
        private string _PhoneNumber;
        private int _LCNumber;
        private int _CheckedBookID;

        [Column(Storage = "_ID", DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; }

        [Column(Storage = "_Name", DbType = "NChar(16)")]
        public string Name { get; set; }

        [Column(Storage = "_Adress", DbType = "NChar(64)")]
        public string Adress { get; set; }

        [Column(Storage = "_PhoneNumber", DbType = "NChar(16)")]
        public string PhoneNumber { get; set; }

        [Column(Storage = "_LCNumber", DbType = "Int")]
        public int LCNumber { get; set; }

        [Column(Storage = "_CheckedBookID", DbType = "Int")]
        public int CheckedBookID { get; set; }

    }
}

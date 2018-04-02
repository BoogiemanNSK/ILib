namespace I2P_Project.DataBase
{
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.ComponentModel;
    using System;

    [Database(Name = "MainDB")]
    public partial class LMSDataBase : DataContext
    {
        private static MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        partial void Insertcheckouts(Checkouts instance);
        partial void Updatecheckouts(Checkouts instance);
        partial void Deletecheckouts(Checkouts instance);
        partial void Insertusers(Users instance);
        partial void Updateusers(Users instance);
        partial void Deleteusers(Users instance);
        partial void Insertdocument(Document instance);
        partial void Updatedocument(Document instance);
        partial void Deletedocument(Document instance);
        #endregion

        public LMSDataBase(string connection) :
                base(connection, MappingSource)
        {
            OnCreated();
        }
        
        public Table<Checkouts> Checkouts => GetTable<Checkouts>();
        public Table<Users> Users => GetTable<Users>();
        public Table<Document> Documents => GetTable<Document>();
        public static MappingSource MappingSource { get => mappingSource; set => mappingSource = value; }
    }

    [Table(Name = "dbo.checkouts")]
    public partial class Checkouts : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        private int _checkID;
        private int _userID;
        private int _bookID;
        private DateTime? _dateTaked;
        private DateTime _timeToBack;
        private bool _isReturned;
        private bool _isRenewed;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OncheckIDChanging(int value);
        partial void OncheckIDChanged();
        partial void OnuserIDChanging(int value);
        partial void OnuserIDChanged();
        partial void OnbookIDChanging(int value);
        partial void OnbookIDChanged();
        partial void OndateTakedChanging(System.Nullable<System.DateTime> value);
        partial void OndateTakedChanged();
        partial void OntimeToBackChanging(System.DateTime value);
        partial void OntimeToBackChanged();
        partial void OnisReturnedChanging(bool value);
        partial void OnisReturnedChanged();
        partial void OnisRenewedChanged();
        partial void OnisRenewedChanging(bool value);
        #endregion

        public Checkouts()
        {
            OnCreated();
        }

        [Column(Storage = "_checkID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int CheckID
        {
            get => _checkID;
            set
            {
                if ((_checkID != value))
                {
                    OncheckIDChanging(value);
                    SendPropertyChanging();
                    _checkID = value;
                    SendPropertyChanged("checkID");
                    OncheckIDChanged();
                }
            }
        }

        [Column(Storage = "_userID", DbType = "Int NOT NULL")]
        public int UserID
        {
            get => _userID;
            set
            {
                if ((_userID != value))
                {
                    OnuserIDChanging(value);
                    SendPropertyChanging();
                    _userID = value;
                    SendPropertyChanged("userID");
                    OnuserIDChanged();
                }
            }
        }

        [Column(Storage = "_bookID", DbType = "Int NOT NULL")]
        public int BookID
        {
            get => _bookID;
            set
            {
                if ((_bookID != value))
                {
                    OnbookIDChanging(value);
                    SendPropertyChanging();
                    _bookID = value;
                    SendPropertyChanged("bookID");
                    OnbookIDChanged();
                }
            }
        }

        [Column(Storage = "_dateTaked", DbType = "DateTime")]
        public DateTime? DateTaked
        {
            get => _dateTaked;
            set
            {
                if ((_dateTaked != value))
                {
                    OndateTakedChanging(value);
                    SendPropertyChanging();
                    _dateTaked = value;
                    SendPropertyChanged("dateTaked");
                    OndateTakedChanged();
                }
            }
        }

        [Column(Storage = "_timeToBack", DbType = "DateTime NOT NULL")]
        public DateTime TimeToBack
        {
            get => _timeToBack;
            set
            {
                if ((_timeToBack != value))
                {
                    OntimeToBackChanging(value);
                    SendPropertyChanging();
                    _timeToBack = value;
                    SendPropertyChanged("timeToBack");
                    OntimeToBackChanged();
                }
            }
        }

        [Column(Storage = "_isReturned", DbType = "Bit NOT NULL")]
        public bool IsReturned
        {
            get => _isReturned;
            set
            {
                if ((_isReturned != value))
                {
                    OnisReturnedChanging(value);
                    SendPropertyChanging();
                    _isReturned = value;
                    SendPropertyChanged("isReturned");
                    OnisReturnedChanged();
                }
            }
        }

        [Column(Storage = "_isRenewed", DbType = "Bit NOT NULL")]
        public bool IsRenewed
        {
            get => _isRenewed;
            set
            {
                if ((_isRenewed != value))
                {
                    OnisRenewedChanging(value);
                    SendPropertyChanging();
                    _isRenewed = value;
                    SendPropertyChanged("isRenewed");
                    OnisRenewedChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((PropertyChanging != null))
            {
                PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [Table(Name = "dbo.users")]
    public partial class Users : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        private int _id;
        private int _userType;
        private string _name;
        private string _address;
        private string _phoneNumber;
        private string _login;
        private string _password;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnuserTypeChanging(int value);
        partial void OnuserTypeChanged();
        partial void OnnameChanging(string value);
        partial void OnnameChanged();
        partial void OnaddressChanging(string value);
        partial void OnaddressChanged();
        partial void OnphoneNumberChanging(string value);
        partial void OnphoneNumberChanged();
        partial void OnloginChanging(string value);
        partial void OnloginChanged();
        partial void OnpasswordChanging(string value);
        partial void OnpasswordChanged();
        #endregion

        public Users()
        {
            OnCreated();
        }

        [Column(Storage = "_id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get => _id;
            set
            {
                if ((_id != value))
                {
                    OnidChanging(value);
                    SendPropertyChanging();
                    _id = value;
                    SendPropertyChanged("id");
                    OnidChanged();
                }
            }
        }

        [Column(Storage = "_userType", DbType = "Int NOT NULL")]
        public int UserType
        {
            get => _userType;
            set
            {
                if ((_userType != value))
                {
                    OnuserTypeChanging(value);
                    SendPropertyChanging();
                    _userType = value;
                    SendPropertyChanged("userType");
                    OnuserTypeChanged();
                }
            }
        }

        [Column(Storage = "_name", DbType = "VarChar(MAX) NOT NULL", CanBeNull = false)]
        public string Name
        {
            get => _name;
            set
            {
                if ((_name != value))
                {
                    OnnameChanging(value);
                    SendPropertyChanging();
                    _name = value;
                    SendPropertyChanged("name");
                    OnnameChanged();
                }
            }
        }

        [Column(Storage = "_address", DbType = "VarChar(MAX) NOT NULL", CanBeNull = false)]
        public string Address
        {
            get => _address;
            set
            {
                if ((_address != value))
                {
                    OnaddressChanging(value);
                    SendPropertyChanging();
                    _address = value;
                    SendPropertyChanged("address");
                    OnaddressChanged();
                }
            }
        }

        [Column(Storage = "_phoneNumber", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if ((_phoneNumber != value))
                {
                    OnphoneNumberChanging(value);
                    SendPropertyChanging();
                    _phoneNumber = value;
                    SendPropertyChanged("phoneNumber");
                    OnphoneNumberChanged();
                }
            }
        }

        [Column(Storage = "_login", DbType = "VarChar(MAX)")]
        public string Login
        {
            get => _login;
            set
            {
                if ((_login != value))
                {
                    OnloginChanging(value);
                    SendPropertyChanging();
                    _login = value;
                    SendPropertyChanged("login");
                    OnloginChanged();
                }
            }
        }

        [Column(Storage = "_password", DbType = "VarChar(100) NOT NULL", CanBeNull = false)]
        public string Password
        {
            get => _password;
            set
            {
                if ((_password != value))
                {
                    OnpasswordChanging(value);
                    SendPropertyChanging();
                    _password = value;
                    SendPropertyChanged("password");
                    OnpasswordChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((PropertyChanging != null))
            {
                PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [Table(Name = "dbo.documents")]
    public partial class Document : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        private int _Id;
        private string _Autors;
        private string _Publisher;
        private string _Edition;
        private int _PublishYear;
        private string _Title;
        private string _Description;
        private int _DocType;
        private bool _IsReference;
        private bool _IsBestseller;
        private int _Price;
        private int _Quantity;
        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnQuantityChanged();
        partial void OnQuantityChanging(int value);
        partial void OnIdChanged();
        partial void OnTitleChanging(string value);
        partial void OnTitleChanged();
        partial void OnAutorsChanging(string value);
        partial void OnAutorsChanged();
        partial void OnPublisherChanging(string value);
        partial void OnPublisherChanged();
        partial void OnPublishYearChanging(int value);
        partial void OnPublishYearChanged();
    
        partial void OnEditionChanging(string value);
        partial void OnEditionChanged();

        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        partial void OnDocTypeChanging(int value);
        partial void OnDocTypeChanged();
        partial void OnIsReferenceChanging(bool value);
        partial void OnIsReferenceChanged();
        partial void OnIsBestsellerChanging(bool value);
        partial void OnIsBestsellerChanged();
        partial void OnPriceChanging(int value);
        partial void OnPriceChanged();
        #endregion

        public Document()
        {
            OnCreated();
        }
        [Column(Storage = "_Quantity", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Quantity
        {
            get => _Quantity;
            set
            {
                if ((_Quantity != value))
                {
                    OnQuantityChanging(value);
                    SendPropertyChanging();
                    _Quantity = value;
                    SendPropertyChanged("Id");
                    OnQuantityChanged();
                }
            }
        }
        [Column(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get => _Id;
            set
            {
                if ((_Id != value))
                {
                    OnIdChanging(value);
                    SendPropertyChanging();
                    _Id = value;
                    SendPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }

        [Column(Storage = "_Autors", DbType = "NVarChar(MAX) NULL")]
        public string Autors
        {
            get => _Autors;
            set
            {
                if ((_Autors != value))
                {
                    OnAutorsChanging(value);
                    SendPropertyChanging();
                    _Autors = value;
                    SendPropertyChanged("Title");
                    OnAutorsChanged();
                }
            }
        }

        [Column(Storage = "_Title", DbType = "NVarChar(MAX) NULL")]
        public string Title
        {
            get => _Title;
            set
            {
                if ((_Title != value))
                {
                    OnTitleChanging(value);
                    SendPropertyChanging();
                    _Title = value;
                    SendPropertyChanged("Title");
                    OnTitleChanged();
                }
            }
        }


        [Column(Storage = "_Publisher", DbType = "NVarChar(MAX) NULL")]
        public string Publisher
        {
            get => _Publisher;
            set
            {
                if ((_Publisher != value))
                {
                    OnPublisherChanging(value);
                    SendPropertyChanging();
                    _Publisher = value;
                    SendPropertyChanged("Title");
                    OnPublisherChanged();
                }
            }
        }


        [Column(Storage = "_Edition", DbType = "NVarChar(MAX) NULL")]
        public string Edition
        {
            get => _Edition;
            set
            {
                if ((_Edition != value))
                {
                    OnEditionChanging(value);
                    SendPropertyChanging();
                    _Edition = value;
                    SendPropertyChanged("Edition");
                    OnTitleChanged();
                }
            }
        }


        [Column(Storage = "_PublishYear", DbType = "Int NOT NULL DEFAULT 0")]
        public int PublishYear
        {
            get => _PublishYear;
            set
            {
                if ((_PublishYear != value))
                {
                    OnPublishYearChanging(value);
                    SendPropertyChanging();
                    _PublishYear = value;
                    SendPropertyChanged("PublishYear");
                    OnPublishYearChanged();
                }
            }
        }

        [Column(Storage = "_Description", DbType = "NVarChar(MAX) NULL")]
        public string Description   
        {
            get => _Description;
            set
            {
                if ((_Description != value))
                {
                    OnDescriptionChanging(value);
                    SendPropertyChanging();
                    _Description = value;
                    SendPropertyChanged("Description");
                    OnDescriptionChanged();
                }
            }
        }

        [Column(Storage = "_DocType", DbType = "Int NOT NULL")]
        public int DocType
        {
            get => _DocType;
            set
            {
                if ((_DocType != value))
                {
                    OnDocTypeChanging(value);
                    SendPropertyChanging();
                    _DocType = value;
                    SendPropertyChanged("DocType");
                    OnDocTypeChanged();
                }
            }
        }

        [Column(Storage = "_IsReference", DbType = "Bit NOT NULL")]
        public bool IsReference
        {
            get => _IsReference;
            set
            {
                if ((_IsReference != value))
                {
                    OnIsReferenceChanging(value);
                    SendPropertyChanging();
                    _IsReference = value;
                    SendPropertyChanged("IsReference");
                    OnIsReferenceChanged();
                }
            }
        }

        [Column(Storage = "_IsBestseller", DbType = "Bit")]
        public bool IsBestseller
        {
            get => _IsBestseller;
            set
            {
                if ((_IsBestseller != value))
                {
                    OnIsBestsellerChanging(value);
                    SendPropertyChanging();
                    _IsBestseller = value;
                    SendPropertyChanged("IsBestseller");
                    OnIsBestsellerChanged();
                }
            }
        }

        [Column(Storage = "_Price", DbType = "Int NOT NULL")]
        public int Price
        {
            get => _Price;
            set
            {
                if ((_Price != value))
                {
                    OnPriceChanging(value);
                    SendPropertyChanging();
                    _Price = value;
                    SendPropertyChanged("Price");
                    OnPriceChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((PropertyChanging != null))
            {
                PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
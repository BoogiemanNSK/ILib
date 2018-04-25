using I2P_Project.Classes.UserSystem;
using I2P_Project.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace I2P_Project.Classes
{
    /// <summary>
    /// Class for managing library.
    /// Here are methods that are acessed by the system mostly, not by any users.
    /// </summary>
    class Library
    {
        private Logger log;
        private LMSDataBase db;

        /// <summary> Initializing DB </summary>
        public Library()
        {
            log = new Logger();
            db = new LMSDataBase(SDM.Strings.CONNECTION_STRING);
            ConnectToDB(db);
        }

        /// <summary> Connecting to Data Base </summary>
        public void ConnectToDB(LMSDataBase db)
        {
            // Trying to connect to Azure cloud database
            try
            {
                db.SubmitChanges();
                log.Write("System connected to Azure DB");
            }
            // If connection failed, establishing a local DB
            catch
            {
                string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string path = (Path.GetDirectoryName(executable));
                string connString = path + SDM.Strings.DB_RELATIVE_PATH;

                db = new LMSDataBase(connString);

                Directory.CreateDirectory(SDM.Strings.DB_DIRECTORY_NAME);
                if (!File.Exists(connString))
                {
                    db.CreateDatabase();
                    GenerateTestDB();
                }

                db.SubmitChanges();
                log.Write("System created local DB and connected to it");
            }
        }
        
        #region DB Addition

        /// <summary> Registers new user in data base </summary>
        public bool RegisterUser(string login, string password, string name, string adress, string phone, bool isLibrarian)
        {
            if (CheckLogin(login)) return false;

            using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                Cryptography cpt = new Cryptography();
                password = cpt.GetHash(md5Hash, password);  // Hashing password string by MD5
            }

            Users newUser = new Users {
                Login = login,
                Password = password,
                Name = name,
                Address = adress,
                PhoneNumber = phone,
                IsDeleted = false,
                UserType = (int) (isLibrarian ? UserType.Librarian : UserType.Student), 
                LibrarianType = 0
            };
            db.Users.InsertOnSubmit(newUser);
            db.SubmitChanges();
			if (!isLibrarian)
				log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " created patron " + newUser.Login);
			else
				log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " created librarian " + newUser.Login);
			return true;
        }

        /// <summary> Adds new book to the system or increments quantity of existing </summary>
        public void AddBook(string title, string autors, string publisher, int publishYear, string edition, string description, int price, bool isBestseller, int quantity, string tags)
        {
            bool notExist = (GetDocByTitle(title) == null);
            Document newDoc;
            if (notExist)
            {
                newDoc = new Document {
                    Title = title,
                    Autors = autors,
                    Publisher = publisher,
                    PublishYear = publishYear,
                    Edition = edition,
                    Description = description,
                    Price = price,
                    DocType = (int)DocType.Book,
                    IsBestseller = isBestseller,
                    IsRequested = false,
                    Quantity = quantity,
                    Queue = "",
                    Tags = tags
                };
                db.Documents.InsertOnSubmit(newDoc);
            }
            else
            {
                var test = (from p in db.Documents
                            where (p.Title == title)
                            select p);
                newDoc = test.Single();
                if (newDoc.Quantity == 0) {
                    NotifyNextUser(newDoc.Id, SDM.Strings.MAIL_BOOK_AVAILIBLE_TITLE, SDM.Strings.MAIL_BOOK_AVAILIBLE_TEXT(newDoc.Title, SDM.Strings.DOC_TYPES[newDoc.DocType]));
                }
                newDoc.Quantity += quantity;
            }
            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " created " + quantity + " copies of " + newDoc.Title);
        }

        /// <summary> Adds new journal to the system or increments quantity of existing </summary>
        public void AddJournal(string title, string autors, string publishedIn, string issueTitle, string issueEditor, int price, int quantity, string tags)
        {
            bool notExist = (GetDocByTitle(title) == null);
            Document newDoc;
            if (notExist)
            {
                newDoc = new Document
                {
                    Title = title,
                    Autors = autors,
                    PublishedIn = publishedIn,
                    IssueTitle = issueTitle,
                    IssueEditor = issueEditor,
                    Price = price,
                    DocType = (int) DocType.Journal,
                    IsRequested = false,
                    Quantity = quantity,
                    Queue = "",
                    Tags = tags
                };
                db.Documents.InsertOnSubmit(newDoc);
            }
            else
            {
                var test = (from p in db.Documents
                            where (p.Title == title)
                            select p);
                newDoc = test.Single();
                if (newDoc.Quantity == 0) {
                    NotifyNextUser(newDoc.Id, SDM.Strings.MAIL_BOOK_AVAILIBLE_TITLE, SDM.Strings.MAIL_BOOK_AVAILIBLE_TEXT(newDoc.Title, SDM.Strings.DOC_TYPES[newDoc.DocType]));
                }
                newDoc.Quantity += quantity;
            }
            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " created " + quantity + " copies of " + newDoc.Title);
        }

        /// <summary> Adds new AV to the system or increments quantity of existing </summary>
        public void AddAV(string title, string autors, int price, int quantity, string tags)
        {
            bool notExist = (GetDocByTitle(title) == null);
            Document newDoc;
            if (notExist)
            {
                newDoc = new Document
                {
                    Title = title,
                    Autors = autors,
                    Price = price,
                    DocType = (int) DocType.AV,
                    IsRequested = false,
                    Quantity = quantity,
                    Queue = "",
                    Tags = tags
                };
                db.Documents.InsertOnSubmit(newDoc);
            }
            else
            {
                var test = (from p in db.Documents
                            where (p.Title == title)
                            select p);
                newDoc = test.Single();
                if (newDoc.Quantity == 0) {
                    NotifyNextUser(newDoc.Id, SDM.Strings.MAIL_BOOK_AVAILIBLE_TITLE, SDM.Strings.MAIL_BOOK_AVAILIBLE_TEXT(newDoc.Title, SDM.Strings.DOC_TYPES[newDoc.DocType]));
                }
                newDoc.Quantity += quantity;
            }
            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " created " + quantity + " copies of " + newDoc.Title);
        }

        /// <summary>
        /// Change fields in DB when some user check out docs.
        /// Start timer for check out and get reference for book on it's owner.
        /// DateCheat format - dd mm yyyy
        /// </summary>
        public void SetCheckOut(int patronID, int docID, int weeks, params int[] DateCheat)
        {
            DateTime time;
            if (DateCheat.Length == 0)
                time = DateTime.Now;
            else
                time = new DateTime(DateCheat[2], DateCheat[1], DateCheat[0]);

            Checkouts chk = new Checkouts
            {
                UserID = patronID,
                BookID = docID,
                IsReturned = false,
                DateTaked = time,
                TimeToBack = time.AddDays(weeks * 7)
            };

            db.Checkouts.InsertOnSubmit(chk);
            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " checked out " + GetDoc(docID).Title);
        }

        #endregion

        #region DB Deletion

        /// <summary> Deletes registered user from the system </summary>
        public void RemoveUser(int patronID)
        {
            // Deleting user
            var userToRemove = GetUser(patronID);
            userToRemove.IsDeleted = true;

            // Deleting user`s checkouts
            var checkoutsToRemove = (from c in db.Checkouts
                                       where c.UserID == patronID
                                       select c);
            db.Checkouts.DeleteAllOnSubmit(checkoutsToRemove);

            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " removed user " + userToRemove.Login);
        }

        /// <summary> Deletes registered doc from the system by ID </summary>
        internal void RemoveDocument(int docID)
        {
            var recordToRemove = GetDoc(docID);
            db.Documents.DeleteOnSubmit(recordToRemove);
            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " removed doc " + recordToRemove.Title);
        }

        /// <summary> Deletes checkout from the system by user and doc ID </summary>
        public void RemoveCheckout(int docID, int userID)
        {
            var recordToRemove = GetCheckout(userID, docID);
            db.Checkouts.DeleteOnSubmit(recordToRemove);
            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " removed checkout for " + GetDoc(docID).Title);
        }

        #endregion
        
        #region DB Updating

        /// <summary> Updates user info </summary>
        public void UpdateUser(int userId, string userName, string userAdress, string userPhoneNumber, int userType)
        {
            Users user = GetUser(userId);
            user.Name = userName;
            user.Address = userAdress;
            user.PhoneNumber = userPhoneNumber;
            if (user.UserType == (int) UserType.Librarian) {
                user.LibrarianType = userType;
            } else {
                user.UserType = userType;
            }
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, user);
            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " updated info about user " + user.Login);
        }
        
        /// <summary> Updates book info </summary>
        public void ModifyBook(int docID, string title, string autors, string publisher, int publishYear, string edition, string description, int price, bool isBestseller, int quantity, string tags)
        {
            Document book = GetDoc(docID);
            book.Title = title;
            book.Autors = autors;
            book.Publisher = publisher;
            book.PublishYear = publishYear;
            book.Edition = edition;
            book.Description = description;
            book.Price = price;
            book.IsBestseller = isBestseller;
            book.Quantity = quantity;
            book.Tags = tags;
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, book);
            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " updated info about book " + title);
        }
        
        /// <summary> Updates journal info </summary>
        public void ModifyJournal(int docID, string title, string autors, string publishedIn, string issueTitle, string issueEditor, int price, int quantity, string tags)
        {
            Document journal = GetDoc(docID);
            journal.Title = title;
            journal.Autors = autors;
            journal.PublishedIn = publishedIn;
            journal.IssueTitle = issueTitle;
            journal.IssueEditor = issueEditor;
            journal.Price = price;
            journal.Quantity = quantity;
            journal.Tags = tags;
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, journal);
            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " updated info about journal " + title);
        }
        
        /// <summary> Updates AV info </summary>
        public void ModifyAV(int docID, string title, string autors, int price, int quantity, string tags)
        {
            Document AV = GetDoc(docID);
            AV.Title = title;
            AV.Autors = autors;
            AV.Price = price;
            AV.Quantity = quantity;
            AV.Tags = tags;
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, AV);
            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " updated info about AV " + title);
        }

        /// <summary> Renews doc according to defined rules </summary>
        public string RenewDoc(int userID, int docID, params int[] DateCheat)
        {
            DateTime time;
            Users currentUser = GetUser(userID);
            Checkouts c = GetCheckout(userID, docID);
            Document doc = GetDoc(docID);

            if (DateCheat.Length == 0)
                time = DateTime.Now;
            else
                time = new DateTime(DateCheat[2], DateCheat[1], DateCheat[0]);

            if (doc.IsRequested)
                return SDM.Strings.DOC_IS_REQUESTED;
            else if (c.IsRenewed && currentUser.UserType != 3)
                return SDM.Strings.DOC_ALREADY_RENEWED;
            else if (ExistQueueForDoc(docID))
                return SDM.Strings.DOC_IN_QUEUE;
            else if (GetUserFineForDoc(currentUser.Id, docID) > 0)
                return SDM.Strings.USER_HAVE_FINE;
            else {
                c.TimeToBack = time.Add(c.TimeToBack.Subtract((DateTime)c.DateTaked));
                c.DateTaked = time;
                c.IsRenewed = true;
                db.SubmitChanges();
                log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " renewed " + doc.Title);
                return SDM.Strings.SUCCESSFUL_RENEW;
            }
        }

        /// <summary> Sets an outstanding request for a doc </summary>
        public void SetOutstandingRequest(int docID)
        {
            var doc = GetDoc(docID);

            NotifyNextUser(docID, SDM.Strings.MAIL_BOOK_REQUESTED_TITLE, SDM.Strings.MAIL_BOOK_REQUESTED_TEXT(doc.Title, SDM.Strings.DOC_TYPES[doc.DocType]));
            while (doc.Queue.Length > 0) {
                log.Write(GetUser(Convert.ToInt32(doc.Queue.Split('|')[0])).Name + " was notifed that document " +
                    GetDoc(docID).Title + " is not longer available and he's removed from the waiting list");
                PopFromPQ(docID);
                NotifyNextUser(docID, SDM.Strings.MAIL_BOOK_REQUESTED_TITLE, SDM.Strings.MAIL_BOOK_REQUESTED_TEXT(doc.Title, SDM.Strings.DOC_TYPES[doc.DocType]));
            }

            var test = from c in db.Checkouts
                       where c.BookID == docID
                       select c;
            
            foreach (Checkouts c in test)
            {
                if (c.TimeToBack.CompareTo(DateTime.Now) > 0)
                    c.TimeToBack = DateTime.Now;
                SendNotificationToUser(GetUser(c.UserID).Address, SDM.Strings.MAIL_RETURN_BOOK_TITLE, SDM.Strings.MAIL_RETURN_BOOK_TEXT(doc.Title, SDM.Strings.DOC_TYPES[doc.DocType]));
				log.Write(GetUser(c.UserID).Name + " was notifed to return the respective books");
			}

            doc.Queue = "";
            doc.IsRequested = true;

            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, doc);
            db.SubmitChanges();
			log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " placed an outstanding request on document " + doc.Title);
			log.Write("Waiting list for document " + doc.Title + " was deleted" );
		}

		/// <summary> Updates deadline (fine payed) </summary>
		public void UpdateDeadline(int userID, int docID, DateTime newDeadline)
        {
            GetCheckout(userID, docID).TimeToBack = newDeadline;
            db.SubmitChanges();
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " updated deadline for " + GetDoc(docID).Title);
        }

        #endregion

        #region DB Output

        /// <summary>
        /// Returns collection of current logged user books with searched keyword and flag filters
        /// Usage: MyBooks.xaml 
        /// </summary>
        public ObservableCollection<Pages.MyBooksTable> GetUserBooks(int userID, int flags, string keyword)
        {
            ObservableCollection<Pages.MyBooksTable> tempTable = new ObservableCollection<Pages.MyBooksTable>();
            var loadUserDocs = from c in db.Checkouts
                                  join b in db.Documents on c.BookID equals b.Id
                                  where c.UserID == userID && c.IsReturned == false
                                  select new
                                  {
                                      c.CheckID,
                                      c.BookID,
                                      b.Title,
                                      b.Autors,
                                      b.Price,
                                      b.Tags,
                                      c.DateTaked,
                                      c.TimeToBack
                                  };

            string[] ANDSplit = keyword.Split(new[] { " AND " }, StringSplitOptions.None);
            string[] ORSplit = keyword.Split(new[] { " OR " }, StringSplitOptions.None);

            if (ANDSplit.Length == 2) {
                foreach (var element in loadUserDocs) {
                    if ((Match((flags >> 2) % 2, element.Title, ANDSplit[0]) && Match((flags >> 2) % 2, element.Title, ANDSplit[1])) ||
                        (Match((flags >> 1) % 2, element.Autors, ANDSplit[0]) && Match((flags >> 1) % 2, element.Autors, ANDSplit[1])) ||
                        (Match(flags % 2, element.Tags, ANDSplit[0]) && Match(flags % 2, element.Tags, ANDSplit[1]))) {

                        Pages.MyBooksTable row = new Pages.MyBooksTable {
                            checkID = element.CheckID,
                            docID = element.BookID,
                            docTitle = element.Title,
                            docAutors = element.Autors,
                            docPrice = element.Price,
                            docFine = GetUserFineForDoc(userID, element.BookID),
                            checkDateTaked = (DateTime)element.DateTaked,
                            checkTimeToBack = element.TimeToBack
                        };
                        tempTable.Add(row);

                    }
                }
            } else if (ORSplit.Length == 2) {
                foreach (var element in loadUserDocs) {
                    if ((Match((flags >> 2) % 2, element.Title, ORSplit[0]) || Match((flags >> 2) % 2, element.Title, ORSplit[1])) ||
                        (Match((flags >> 1) % 2, element.Autors, ORSplit[0]) || Match((flags >> 1) % 2, element.Autors, ORSplit[1])) ||
                        (Match(flags % 2, element.Tags, ORSplit[0]) || Match(flags % 2, element.Tags, ORSplit[1]))) {

                        Pages.MyBooksTable row = new Pages.MyBooksTable {
                            checkID = element.CheckID,
                            docID = element.BookID,
                            docTitle = element.Title,
                            docAutors = element.Autors,
                            docPrice = element.Price,
                            docFine = GetUserFineForDoc(userID, element.BookID),
                            checkDateTaked = (DateTime)element.DateTaked,
                            checkTimeToBack = element.TimeToBack
                        };
                        tempTable.Add(row);

                    }
                }
            } else {
                foreach (var element in loadUserDocs) {
                    if (Match((flags >> 2) % 2, element.Title, keyword) ||
                        Match((flags >> 1) % 2, element.Autors, keyword) ||
                        Match(flags % 2, element.Tags, keyword)) {

                        Pages.MyBooksTable row = new Pages.MyBooksTable {
                            checkID = element.CheckID,
                            docID = element.BookID,
                            docTitle = element.Title,
                            docAutors = element.Autors,
                            docPrice = element.Price,
                            docFine = GetUserFineForDoc(userID, element.BookID),
                            checkDateTaked = (DateTime)element.DateTaked,
                            checkTimeToBack = element.TimeToBack
                        };
                        tempTable.Add(row);

                    }
                }
            }

            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " uploaded table of " + GetUser(userID) + " docs");
            return tempTable;
        }

        /// <summary>
        /// Return overdued docs for particular user
        /// Usage: OverdueInfo.xaml
        /// </summary>
        public ObservableCollection<Pages.OverdueInfoTable> OverdueInfo(int userID)
        {
            ObservableCollection<Pages.OverdueInfoTable> tempTable = new ObservableCollection<Pages.OverdueInfoTable>();
            var loadUserBooks = from c in db.Checkouts
                                  join b in db.Documents on c.BookID equals b.Id
                                  where c.UserID == userID
                                  select new
                                  {
                                      b.Id,
                                      b.Title,
                                      b.DocType,
                                      c.DateTaked,
                                      c.TimeToBack,
                                      b.Price
                                  };
            foreach (var element in loadUserBooks)
            {
                int passedDays = (int)DateTime.Now.Subtract(element.TimeToBack).TotalDays;
                if (passedDays > 0)
                {
                    Pages.OverdueInfoTable row = new Pages.OverdueInfoTable
                    {
                        docID = element.Id,
                        docTitle = element.Title,
                        docType = SDM.Strings.DOC_TYPES[element.DocType],
                        dateTaked = (DateTime)element.DateTaked,
                        timeToBack = element.TimeToBack,
                        fine = (passedDays * 100 > element.Price ?
                            element.Price : passedDays * 100)
                    };
                    tempTable.Add(row);
                }
            }
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " uploaded overdue info about " + GetUser(userID));
            return tempTable;
        }

        /// <summary>
        /// Returns collection of all patrons with given keyword and flag filters
        /// Usage: UserManagementPage.xaml
        /// </summary>
        public ObservableCollection<Pages.LibrarianUserView> LibrarianViewUserTable(int flags, string keyword)
        {
            ObservableCollection<Pages.LibrarianUserView> tempTable = new ObservableCollection<Pages.LibrarianUserView>();
            var loadUsers = from p in db.Users
                             where p.UserType < (int) UserType.Librarian && !p.IsDeleted
                             select new
                             {
                                 p.Id,
                                 p.Login,
                                 p.Address
                             };

            string[] ANDSplit = keyword.Split(new[] { " AND " }, StringSplitOptions.None);
            string[] ORSplit = keyword.Split(new[] { " OR " }, StringSplitOptions.None);

            if (ANDSplit.Length == 2) {
                foreach (var element in loadUsers) {
                    if ((Match((flags >> 1) % 2, element.Login, ANDSplit[0]) && Match((flags >> 1) % 2, element.Login, ANDSplit[1])) ||
                        (Match(flags % 2, element.Address, ANDSplit[0]) && Match(flags % 2, element.Address, ANDSplit[1]))) {

                        Pages.LibrarianUserView row = new Pages.LibrarianUserView {
                            userID = element.Id,
                            userLogin = element.Login,
                            userMail = element.Address,
                            docsNumber = GetUserBooksNumber(element.Id),
                            userFine = GetUserFine(element.Id)
                        };
                        tempTable.Add(row);

                    }
                }
            } else if (ORSplit.Length == 2) {
                foreach (var element in loadUsers) {
                    if ((Match((flags >> 1) % 2, element.Login, ORSplit[0]) || Match((flags >> 1) % 2, element.Login, ORSplit[1])) ||
                        (Match(flags % 2, element.Address, ORSplit[0]) || Match(flags % 2, element.Address, ORSplit[1]))) {

                        Pages.LibrarianUserView row = new Pages.LibrarianUserView {
                            userID = element.Id,
                            userLogin = element.Login,
                            userMail = element.Address,
                            docsNumber = GetUserBooksNumber(element.Id),
                            userFine = GetUserFine(element.Id)
                        };
                        tempTable.Add(row);

                    }
                }
            } else {
                foreach (var element in loadUsers) {
                    if (Match((flags >> 1) % 2, element.Login, keyword) ||
                        Match(flags % 2, element.Address, keyword)) {

                        Pages.LibrarianUserView row = new Pages.LibrarianUserView {
                            userID = element.Id,
                            userLogin = element.Login,
                            userMail = element.Address,
                            docsNumber = GetUserBooksNumber(element.Id),
                            userFine = GetUserFine(element.Id)
                        };
                        tempTable.Add(row);

                    }
                }
            }

            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " uploaded users table");
            return tempTable;
        }

        /// <summary>
        /// Returns collection of all librarians with given keyword and flag filters
        /// Usage: LibrariansManagementPage.xaml
        /// </summary>
        public ObservableCollection<Pages.AdminUserView> AdminViewUserTable(int flags, string keyword)
        {
            ObservableCollection<Pages.AdminUserView> tempTable = new ObservableCollection<Pages.AdminUserView>();
            var loadUsers = from p in db.Users
                             where p.UserType == (int)UserType.Librarian && !p.IsDeleted
                            select new {
                                 p.Id,
                                 p.Login,
                                 p.Name,
                                 p.Address,
                                 p.LibrarianType
                             };

            string[] ANDSplit = keyword.Split(new[] { " AND " }, StringSplitOptions.None);
            string[] ORSplit = keyword.Split(new[] { " OR " }, StringSplitOptions.None);

            if (ANDSplit.Length == 2) {
                foreach (var element in loadUsers) {
                    if ((Match((flags >> 2) % 2, element.Login, ANDSplit[0]) && Match((flags >> 2) % 2, element.Login, ANDSplit[1])) ||
                        (Match((flags >> 1) % 2, element.Name, ANDSplit[0]) && Match((flags >> 1) % 2, element.Name, ANDSplit[1])) ||
                        (Match(flags % 2, element.Address, ANDSplit[0]) && Match(flags % 2, element.Address, ANDSplit[1]))) {

                        Pages.AdminUserView row = new Pages.AdminUserView {
                            LibrarianID = element.Id,
                            LibrarianLogin = element.Login,
                            LibrarianName = element.Name,
                            LibrarianMail = element.Address,
                            LibrarianType = "Priv" + (element.LibrarianType + 1)
                        };
                        tempTable.Add(row);

                    }
                }
            } else if (ORSplit.Length == 2) {
                foreach (var element in loadUsers) {
                    if ((Match((flags >> 2) % 2, element.Login, ORSplit[0]) || Match((flags >> 2) % 2, element.Login, ORSplit[1])) ||
                        (Match((flags >> 1) % 2, element.Name, ORSplit[0]) || Match((flags >> 1) % 2, element.Name, ORSplit[1])) ||
                        (Match(flags % 2, element.Address, ORSplit[0]) || Match(flags % 2, element.Address, ORSplit[1]))) {

                        Pages.AdminUserView row = new Pages.AdminUserView {
                            LibrarianID = element.Id,
                            LibrarianLogin = element.Login,
                            LibrarianName = element.Name,
                            LibrarianMail = element.Address,
                            LibrarianType = "Priv" + (element.LibrarianType + 1)
                        };
                        tempTable.Add(row);

                    }
                }
            } else {
                foreach (var element in loadUsers) {
                    if (Match((flags >> 2) % 2, element.Login, keyword) ||
                        Match((flags >> 1) % 2, element.Name, keyword) ||
                        Match(flags % 2, element.Address, keyword)) {

                        Pages.AdminUserView row = new Pages.AdminUserView {
                            LibrarianID = element.Id,
                            LibrarianLogin = element.Login,
                            LibrarianName = element.Name,
                            LibrarianMail = element.Address,
                            LibrarianType = "Priv" + (element.LibrarianType + 1)
                        };
                        tempTable.Add(row);

                    }
                }
            }

            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " uploaded librarians table");
            return tempTable;
        }

        /// <summary>
        /// Return a list of all docs registered in system with given keyword and flag filters
        /// Usage: UserHomePage.xaml, DocumentsManagementPage.xaml, TestingTool.xaml
        /// </summary>
        public ObservableCollection<Pages.DocumentsTable> GetDocsTable(int flags, string keyword)
        {
            ObservableCollection<Pages.DocumentsTable> tempTable = new ObservableCollection<Pages.DocumentsTable>();
            var loadUserDocs = from b in db.Documents
                               select new
                                 {
                                     b.Id,
                                     b.Title,
                                     b.Autors,
                                     b.DocType,
                                     b.Price,
                                     b.Tags,
                                     b.Quantity
                                 };

            string[] ANDSplit = keyword.Split(new[] {" AND "}, StringSplitOptions.None);
            string[] ORSplit = keyword.Split(new[] {" OR "}, StringSplitOptions.None);

            if (ANDSplit.Length == 2) {
                foreach (var element in loadUserDocs) {
                    if ((Match((flags >> 2) % 2, element.Title, ANDSplit[0]) && Match((flags >> 2) % 2, element.Title, ANDSplit[1])) ||
                        (Match((flags >> 1) % 2, element.Autors, ANDSplit[0]) && Match((flags >> 1) % 2, element.Autors, ANDSplit[1])) ||
                        (Match(flags % 2, element.Tags, ANDSplit[0]) && Match(flags % 2, element.Tags, ANDSplit[1]))) {

                        Pages.DocumentsTable row = new Pages.DocumentsTable {
                            docID = element.Id,
                            docAutors = element.Autors,
                            docTitle = element.Title,
                            docType = SDM.Strings.DOC_TYPES[element.DocType],
                            docPrice = element.Price,
                            docQuantity = element.Quantity
                        };
                        tempTable.Add(row);

                    }
                }
            } else if (ORSplit.Length == 2) {
                foreach (var element in loadUserDocs) {
                    if ((Match((flags >> 2) % 2, element.Title, ORSplit[0]) || Match((flags >> 2) % 2, element.Title, ORSplit[1])) ||
                        (Match((flags >> 1) % 2, element.Autors, ORSplit[0]) || Match((flags >> 1) % 2, element.Autors, ORSplit[1])) ||
                        (Match(flags % 2, element.Tags, ORSplit[0]) || Match(flags % 2, element.Tags, ORSplit[1]))) {

                        Pages.DocumentsTable row = new Pages.DocumentsTable {
                            docID = element.Id,
                            docAutors = element.Autors,
                            docTitle = element.Title,
                            docType = SDM.Strings.DOC_TYPES[element.DocType],
                            docPrice = element.Price,
                            docQuantity = element.Quantity
                        };
                        tempTable.Add(row);

                    }
                }
            } else {
                foreach (var element in loadUserDocs) {
                    if (Match((flags >> 2) % 2, element.Title, keyword) ||
                        Match((flags >> 1) % 2, element.Autors, keyword) ||
                        Match(flags % 2, element.Tags, keyword)) {

                        Pages.DocumentsTable row = new Pages.DocumentsTable {
                            docID = element.Id,
                            docAutors = element.Autors,
                            docTitle = element.Title,
                            docType = SDM.Strings.DOC_TYPES[element.DocType],
                            docPrice = element.Price,
                            docQuantity = element.Quantity
                        };
                        tempTable.Add(row);

                    }
                }
            }

            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " uploaded documents table");
            return tempTable;
        }

        /// <summary>
        /// Gets particular user docs for UserCard
        /// Usage: UserCard.xaml
        /// </summary>
        public ObservableCollection<Pages.UserDocsTable> GetUserDocsFromLibrarian(int patronID)
        {
            ObservableCollection<Pages.UserDocsTable> tempTable = new ObservableCollection<Pages.UserDocsTable>();
            var loadUserBooks = from c in db.Checkouts where c.UserID == patronID
                                  join b in db.Documents on c.BookID equals b.Id
                                  select new
                                  {
                                      b.Title,
                                      b.DocType,
                                      c.DateTaked,
                                      c.TimeToBack
                                  };
            foreach (var element in loadUserBooks)
            {
                Pages.UserDocsTable row = new Pages.UserDocsTable
                {
                    DocTitle = element.Title,
                    DocType = SDM.Strings.DOC_TYPES[element.DocType],
                    DateTaked = (DateTime)element.DateTaked,
                    DeadLine = element.TimeToBack
                };
                tempTable.Add(row);
            }
            log.Write(SDM.Strings.USER_TYPES[SDM.CurrentUser.UserType] + " " + SDM.CurrentUser.Login + " uploaded docs of " + GetUser(patronID).Login);
            return tempTable;
        }

        /// <summary>
        /// Loads all books into autocomplete box to make searching more simpler
        /// Usage: UserHomePage.xaml.cs
        /// </summary>
        public List<String> GetSearchBooks()
        {
            List<String> tempList = new List<String>();
            var loadUserDocs = from b in db.Documents
                                 select new
                                 {
                                     b.Title,
                                     b.Autors,
                                     b.Publisher
                                 };

            foreach (var element in loadUserDocs)
            {
                string tempRow = element.Title.ToString() + "\n" + Convert.ToString(element.Autors) + ", " + Convert.ToString(element.Publisher);
                tempList.Add(tempRow);
            }
            return tempList;
        }

        /// <summary>
        /// Loads all books into autocomplete box to make searching more simpler
        /// Usage: MyBooks.xaml.cs
        /// </summary>
        public List<String> GetSearchUserBooks()
        {
            List<String> tempList = new List<String>();
            var loadUserBooks = from c in db.Checkouts
                                  join b in db.Documents on c.BookID equals b.Id
                                  where c.UserID == SDM.CurrentUser.PersonID && c.IsReturned == false
                                  select new
                                  {
                                      b.Title,
                                      b.Autors,
                                      b.Publisher
                                  };

            foreach (var element in loadUserBooks)
            {
                string tempRow = element.Title.ToString() + "\n" + Convert.ToString(element.Autors) + ", " + Convert.ToString(element.Publisher);
                tempList.Add(tempRow);
            }
            return tempList;
        }

        /// <summary>
        /// Loads all users into autocomplete box to make searching more simpler
        /// Usage: UsersManagementPage.xaml.cs
        /// </summary>
        public List<String> GetSearchUser()
        {
            List<String> temp_list = new List<String>();
            var load_users = from p in db.Users
                             where p.UserType < (int)UserType.Librarian
                             select new
                             {
                                 p.Login,
                                 p.Name,
                                 p.PhoneNumber
                             };

            foreach (var element in load_users)
            {
                string temp_row = element.Login.ToString() + "\n" + Convert.ToString(element.Name) + ", " + Convert.ToString(element.PhoneNumber);
                temp_list.Add(temp_row);
            }
            return temp_list;
        }

        /// <summary>
        /// Returns collection of all librarians only for AutoCompleteBox
        /// Usage: LibrariansManagementPage.xaml
        /// </summary>
        public List<String> GetSearchLibrarian()
        {
            List<String> temp_list = new List<String>();
            var load_users = from p in db.Users
                             where p.UserType == (int)UserType.Librarian && !p.IsDeleted
                             select new
                             {
                                 p.Id,
                                 p.Login,
                                 p.Name,
                                 p.LibrarianType,
                                 p.PhoneNumber
                             };

            foreach (var element in load_users)
            {
                string temp_row = element.Login.ToString() + "\n" + Convert.ToString(element.Name) + ", " + Convert.ToString(element.PhoneNumber);
                temp_list.Add(temp_row);
            }
            return temp_list;
        }

        private bool Match(int flag, string s, string part)
        {
            if (s != null && flag == 1 && s.Contains(part)) return true;
            return false;
        }

        #endregion

        #region DB Existence Check

        /// <summary> Checks if there exist a user with given login </summary>
        public bool CheckLogin(string login)
        {
            return GetUserByLogin(login) != null;
        }

        /// <summary> Checks if a user with given login has given password </summary>
        public bool CheckPassword(string login, string password)
        {
            using (System.Security.Cryptography.MD5 md5_hash = System.Security.Cryptography.MD5.Create())
            {
                Cryptography cpt = new Cryptography();
                password = cpt.GetHash(md5_hash, password);  // Hashing password string by MD5
            }

            var test = (from p in db.Users
                        where (p.Login == login && p.Password == password)
                        select p);
            return test.Any();
        }

        #endregion
        
        #region DB Getters

        /// <summary> Returns document object from given ID </summary>
        public Document GetDoc(int docID)
        {
            var test = from doc in db.Documents where doc.Id == docID select doc;
            if (!test.Any()) return null;
            return test.Single();
        }
        
        /// <summary> Returns user row from given ID </summary>
        public Users GetUser(int userID)
        {
            var test = from u in db.Users where u.Id == userID && !u.IsDeleted select u;
            if (!test.Any()) return null;
            return test.Single();
        }

        /// <summary> Returns a particular checkout </summary>
        public Checkouts GetCheckout(int userID, int docID)
        {
            var test = from c in db.Checkouts where (c.UserID == userID && c.BookID == docID) select c;
            if (!test.Any()) return null;
            return test.Single();
        }

        /// <summary> Returns user row from given login </summary>
        public Users GetUserByLogin(string Login)
        {
            var test = from u in db.Users where u.Login == Login && !u.IsDeleted select u;
            if (!test.Any()) return null;
            return test.Single();
        }

        /// <summary> Returns document row from given title </summary>
        public Document GetDocByTitle(string Title)
        {
            var test = from d in db.Documents where d.Title == Title select d;
            if (!test.Any()) return null;
            return test.Single();
        }

        /// <summary>
        /// Usage: Shown to librarian in UserManagementPage.xaml
        /// Counts overall user`s fine for overdued docs
        /// </summary>
        public int GetUserFine(int userID)
        {
            int fine = 0;
            var test = from c in db.Checkouts
                       where c.UserID == userID
                       select c;

            if (test.Any())
            {
                foreach (Checkouts c in test)
                {
                    fine += GetUserFineForDoc(userID, c.BookID);
                }
            }

            return fine;
        }

        /// <summary> Counts user`s fine for some doc </summary>
        public int GetUserFineForDoc(int userID, int docID)
        {
            Checkouts testCheck = GetCheckout(userID, docID);

            int overduedTime = (int)DateTime.Now.Subtract(testCheck.TimeToBack).TotalDays;
            if (overduedTime > 0)
            {
                int docPrice = GetDoc(docID).Price;
                return (overduedTime * 100 > docPrice ? docPrice : overduedTime * 100);
            }

            return 0;
        }

        /// <summary>
        /// Usage: Shown to librarian in UserManagementPage.xaml
        /// Counts number of user`s docs from his ID
        /// </summary>
        private int GetUserBooksNumber(int userID)
        {
            var test = from c in db.Checkouts
                       where c.UserID == userID
                       select c;
            if (test.Any()) return test.Count();
            else return 0;
        }

		#endregion

        #region PQ Operations

        /// <summary> Pushes personID with given priority to queue of given ID doc </summary>
        public void PushInPQ(int docID, int personID, int priority)
        {
            PriorityQueue<int> PQ = LoadPQ(docID);
            PQ.Push(personID, priority);
            SavePQ(PQ, docID);
        }

        /// <summary> Pops personID with given priority from queue of given ID doc </summary>
        public void PopFromPQ(int docID)
        {
            PriorityQueue<int> PQ = LoadPQ(docID);
            PQ.Pop();
            while (PQ.Length > 0 && GetUser(PQ.FirstElement.Element).IsDeleted) { PQ.Pop(); }
            SavePQ(PQ, docID);
        }

        /// <summary> Checks if queue for doc with given ID exists </summary>
        public bool ExistQueueForDoc(int docID)
        {
            string queue = GetDoc(docID).Queue;
            if (queue.Length > 0) return true;
            return false;
        }

        /// <summary> Send mail for next user in queue if it is not empty </summary>
        public void NotifyNextUser(int docID, string mailTitle, string mailText)
        {
            PriorityQueue<int> PQ = LoadPQ(docID);
            if (PQ.Length > 0)
            {
                Users next = GetUser(Convert.ToInt32(PQ.FirstElement.Element));
                SendNotificationToUser(next.Address, mailTitle, mailText);
                SavePQ(PQ, docID);
            }
        }

        /// <summary> Sends e-mail to given address with given title and text </summary>
        public bool SendNotificationToUser(string To, string Title, string Text)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(SDM.Strings.MAIL_SERVER_LOGIN, SDM.Strings.MAIL_SERVER_PASSWORD),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                MailMessage msg = new MailMessage(SDM.Strings.MAIL_SERVER_LOGIN + "@gmail.com", To)
                {
                    Subject = Title,
                    Body = Text
                };

                smtp.Send(msg);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary> Converts PQ into string and saves it in DB </summary>
        private void SavePQ(PriorityQueue<int> pq, int bookID)
        {
            string queue_string = "";

            while (pq.Length > 1)
            {
                queue_string += pq.FirstElement.Element;
                queue_string += '|';
                queue_string += pq.FirstElement.PriorityLevel;
                queue_string += '-';
                pq.Pop();
            }
            if (pq.FirstElement == null)
                queue_string = "";
            else
            {
                queue_string += pq.FirstElement.Element;
                queue_string += '|';
                queue_string += pq.FirstElement.PriorityLevel;
            }

            var test = from doc in db.Documents
                       where doc.Id == bookID
                       select doc;
            Document d = test.Single();
            d.Queue = queue_string;
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, d);
            db.SubmitChanges();
        }

        /// <summary> Parse PQ string from DB and push entries to PQ </summary>
        public PriorityQueue<int> LoadPQ(int bookID)
        {
            PriorityQueue<int> localQueue = new PriorityQueue<int>();
            var test = from doc in db.Documents
                       where doc.Id == bookID
                       select doc.Queue;

            string queue_string = test.Single();
            if (queue_string.Length == 0) return localQueue;
            
            string[] queue_pairs = queue_string.Split('-');
            foreach (string pair in queue_pairs)
            {
                int id = Convert.ToInt32(pair.Split('|')[0]);
                int priority = Convert.ToInt32(pair.Split('|')[1]);
                localQueue.Push(id, priority);
            }

            return localQueue;
        }

        #endregion

        #region TESTING

        /// <summary> Clears DB (for test cases only) </summary>
        public void ClearDB()
        {
            db.ExecuteCommand("DELETE FROM documents");
            db.ExecuteCommand("DELETE FROM users");
            db.ExecuteCommand("DELETE FROM checkouts");
            GenerateAdmin();
        }

        /// <summary> Generates admin in the system, if DB was cleared </summary>
        public void GenerateAdmin()
        {
            if (GetUserByLogin("admin") != null)
                return;
            Users admin = new Users {
                Login = "admin",
                Password = "admin",
                Name = "Administrator",
                Address = "Asministration",
                PhoneNumber = ":)",
                IsDeleted = false,
                UserType = 6,
                LibrarianType = 0
            };
            db.Users.InsertOnSubmit(admin);
            db.SubmitChanges();
        }

        // [TEST]
        /// <summary> First generate for show functionality </summary>
        private void GenerateTestDB()
        {
            // Adding two books and their copies
            AddBook
                (
                    "Introduction to Algorithms",
                    "Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest and Clifford Stein",
                    "MIT Press",
                    2009,
                    "Third Edition",
                    "Alghorithm techniques and design",
                    1800,
                    false,
                    1,
                    ""
                );
            AddBook
                (
                    "Design Patterns: Elements of Reusable Object-Oriented Software",
                    "Erich Gamma, Ralph Johnson, John Vlissides, Richard Helm",
                    "Addison-Wesley Professional",
                    2003,
                    "First Edition",
                    "Programm patterns, how to programm well w/o headache",
                    2000,
                    true,
                    1,
                    ""
                );

            // Adding reference book
            AddBook
                (
                    "The Mythical Man-month",
                    "Brooks,Jr., Frederick P",
                    "Addison-Wesley Longman Publishing Co., Inc.",
                    1995,
                    "Second edition",
                    "How to do everything and live better",
                    800,
                    false,
                    0,
                    ""
                );

            // Adding two AV's
            AddAV("Null References: The Billion Dollar Mistake", "Tony Hoare", 400, 0, "");
            AddAV("Information Entropy", "Claude Shannon", 700, 0, "");

            // Registering 3 users
            RegisterUser("p1", "p1", "Sergey Afonso", "Via Margutta, 3", "30001", false);
            RegisterUser("p2", "p2", "Nadia Teixeira", "Via Sacra, 13", "30002", false);
            RegisterUser("p3", "p3", "Elvira Espindola", "Via del Corso, 22", "30003", false);

            // Special for me
            RegisterUser("zhychek1", "lolcore", "Toha", "zhychek1@yandex.ru", "+79648350370", true);
        }
		/// <summary>
		/// Get user by his name(test function)
		/// </summary>
		/// <param name="Name"></param>
		/// <returns></returns>
        public Users GetUser(string Name)
        {
            var test = from u in db.Users where u.Name == Name && !u.IsDeleted select u;
            return test.Single();
        }

        public bool CheckAdmin()
        {
            var test = from u in db.Users where u.UserType == (int)UserType.Admin select u;
            return test.Count() == 1;
        }
        /// <summary> Return how many days the user has overdued /// </summary>
        public int OverdueTime(int userID, int docID, DateTime Now)
        {
            Checkouts testCheck = GetCheckout(userID, docID);
            int days = (int) Math.Round(Now.Subtract(testCheck.TimeToBack).TotalDays);
            return days;
        }
		/// <summary> Method for assertion. Return equality of checkouts /// </summary>
		private bool EqualCheckouts(List<CheckedOut> checkedOuts, List<CheckedOut> neededInfo)
        {
            return new HashSet<CheckedOut>(checkedOuts).SetEquals(neededInfo);
        }
		/// <summary> Method for assertion. Return equality of overdues /// </summary>
		private bool EqualOverdues(List<OverdueInfo> overdue, List<OverdueInfo> neededInfo)
        {
            return new HashSet<OverdueInfo>(overdue).SetEquals(neededInfo);
        }
		/// <summary> Method for assertion. Return list of books, which were taken by user./// </summary>
		public List<CheckedOut> GetCheckoutsList(string Name)
        {
            Users user = GetUser(Name);
            List<CheckedOut> res = new List<CheckedOut>();
            var load_user_books = from c in db.Checkouts
                                  where c.UserID == user.Id
                                  join b in db.Documents on c.BookID equals b.Id
                                  select new
                                  {
                                      b.Title,
                                      c.TimeToBack
                                  };
            foreach (var element in load_user_books)
            {
                CheckedOut pair = new CheckedOut
                {
                    CheckOutTime = element.TimeToBack.Day,
                    DocumentCheckedOut = element.Title
                };
                res.Insert(0, pair);
            }
            return res;
        }
		/// <summary> Method for assertion. Return list of overdueinfo,(books which were overdue by user)./// </summary>
		public List<OverdueInfo> GetOverdues(string Name, DateTime Now)
        {
            Users user = GetUser(Name);
            List<OverdueInfo> res = new List<OverdueInfo>();

            var load_user_books = from c in db.Checkouts
                                  where c.UserID == user.Id
                                  join b in db.Documents on c.BookID equals b.Id
                                  select new
                                  {
                                      b.Title,
                                      c.TimeToBack
                                  };
            foreach (var element in load_user_books)
            {
                int passedDays = (int)Now.Subtract(element.TimeToBack).TotalDays;
                if (passedDays > 0)
                {
                    OverdueInfo pair = new OverdueInfo
                    {
                        Overdue = passedDays,
                        DocumentChekedOut = element.Title
                    };
                    res.Add(pair);
                }
            }
            return res;
        }
		/// <summary> Method for assertion. Return amount of rubles,
		/// which user must give for this book(docID)./// </summary>
		public int GetUserFineForDoc(int userID, int docID, DateTime Now)
        {
            Checkouts testCheck = GetCheckout(userID, docID);

            int overduedTime = OverdueTime(userID, docID, Now);
            if (overduedTime > 0) {
                int docPrice = GetDoc(docID).Price;
                return (overduedTime * 100 > docPrice ? docPrice : overduedTime * 100);
            }

            return 0;
        }

		/// <summary> Method for assertion. Return equality of params in BD and given params./// </summary>
		public bool CheckUserInfo(string Name, string Adress, string Phone, int UserType, List<CheckedOut> checkout)
        {
            Users user = GetUser(Name);
            List<CheckedOut> checkover = GetCheckoutsList(Name);

            return user.Address.Equals(Adress) && user.PhoneNumber.Equals(Phone)
                && user.UserType == UserType && EqualCheckouts(checkout, checkover);
        }
		/// <summary> Method for assertion. Return equality of params in BD and given params./// </summary>
		public bool CheckUserInfo(string Name, string Adress, string Phone, int UserType, List<OverdueInfo> overdues, DateTime DateCheat)
        {
            Users user = GetUser(Name);
            List<OverdueInfo> checkoverdues = GetOverdues(Name, DateCheat);

            return user.Address.Equals(Adress) && user.PhoneNumber.Equals(Phone)
                && user.UserType == UserType && EqualOverdues(overdues, checkoverdues);
        }

        // [FOR TEST] (TestingTool.xaml)
        /// <summary> Returns a collection of books checked by particular user </summary>
        public ObservableCollection<Pages.DocumentsTable> TestDocsTableUsersBooks(int user_id)
        {
            ObservableCollection<Pages.DocumentsTable> temp_table = new ObservableCollection<Pages.DocumentsTable>();
            var load_user_docs = from c in db.Checkouts
                                 join b in db.Documents on c.BookID equals b.Id
                                 where c.UserID == user_id
                                 select new
                                 {
                                     b.Id,
                                     b.Title,
                                     b.Autors,
                                     b.DocType,
                                     b.Price,
                                     b.Quantity
                                 };
            foreach (var element in load_user_docs)
            {
                Pages.DocumentsTable row = new Pages.DocumentsTable
                {
                    docID = element.Id,
                    docAutors = element.Autors,
                    docTitle = element.Title,
                    docType = SDM.Strings.DOC_TYPES[element.DocType],
                    docPrice = element.Price,
                    docQuantity = element.Quantity
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        // [FOR TEST] (TestingTool.xaml)
        /// <summary> Returns a collection of all users registered in system </summary>
        public ObservableCollection<Pages.UserTable> TestUsersTable()
        {
            ObservableCollection<Pages.UserTable> temp_table = new ObservableCollection<Pages.UserTable>();
            var load_users = from u in db.Users where u.UserType < 6 && !u.IsDeleted
                             select new
                             {
                                 u.Id,
                                 u.Name,
                                 u.Address,
                                 u.PhoneNumber,
                                 u.UserType
                             };
            foreach (var element in load_users)
            {
                Pages.UserTable row = new Pages.UserTable
                {
                    userID = element.Id,
                    userName = element.Name,
                    userAddress = element.Address,
                    userPhoneNumber = element.PhoneNumber,
                    userType = SDM.Strings.USER_TYPES[element.UserType]
                };
                temp_table.Add(row);
            }
            return temp_table;
        }

        #endregion
    }
}
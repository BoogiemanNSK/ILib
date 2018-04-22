/// <summary> 
/// Class of string constants to avoid harcoding and
///  (maybe sometime) provide multi-language support 
/// </summary>
class StringConstants {

    public string CONNECTION_STRING => "Server=tcp:ilib2018.database.windows.net,1433;" +
                                       "Initial Catalog=iLibDB2018;" +
                                       "Persist Security Info=False;" +
                                       "User ID=ilib2018admin;" +
                                       "Password=iLibpass2018;" +
                                       "MultipleActiveResultSets=False;" +
                                       "Encrypt=True;" +
                                       "TrustServerCertificate=False;" +
                                       "Connection Timeout=30;";

    public string MAIL_SERVER_LOGIN => "ilib.mail2018";
    public string MAIL_SERVER_PASSWORD => "Faraday28";
    public string MAIL_BOOK_AVAILIBLE_TITLE => "Document is Availible!";
    public string MAIL_BOOK_AVAILIBLE_TEXT(string docTitle, string docType) => "\r" + docType + " " + docTitle +
        " is now availible. Check it out in iLib!\n\rhttps://github.com/BoogiemanNSK/ILib";
    public string MAIL_BOOK_REQUESTED_TITLE => "You have been deleted from the queue";
    public string MAIL_BOOK_REQUESTED_TEXT(string docTitle, string docType) => "\rYou have been deleted from the queue for " + docType + " " + docTitle +
        " since it was requested by librarian and not availible anymore. Sorry for any inconvenience.";
    public string MAIL_RETURN_BOOK_TITLE => "You have to return the document";
    public string MAIL_RETURN_BOOK_TEXT(string docTitle, string docType) => "\rYou have one day to return " + docType + " " + docTitle +
        " since it was requested by librarian. Sorry for any inconvenience.";

    public string SUCCESSFUL_RENEW => "Successfuly renewed";

    public string USER_HAVE_FINE => "You have fine, pay please";
    public string FINE_CONFIRMATION_TEXT => "Are you sure you want to pay fine?";
    public string DOC_ALREADY_RENEWED => "You already renew this book";
    public string DOC_IN_QUEUE => "This book is in queue";
    public string DOC_IS_REQUESTED => "This book is requested by librarian";

    public string DB_DIRECTORY_NAME => "DataBases";
    public string DB_RELATIVE_PATH => "\\" + DB_DIRECTORY_NAME + "\\ILibDB.mdf";

    public string USER_NOT_FOUND_TEXT => "User not found";
    public string WRONG_PASSWORD_TEXT => "Wrong password";

    public string ALREADY_HAVE_TEXT => "You already have that book";
    public string PERSON_NOT_IN_QUEUE_TEXT => "There are no free copies of this book for now." +
        " You`ve been added to queue and will be notified when this book become availible to you";
    public string SUCCESS_CHECK_OUT_TEXT => "Successfully checked out";
    public string PERSON_IN_QUEUE_TEXT => "You are already in the queue for this book";
    public string PERSON_FIRST_IN_QUEUE_TEXT => "Since you are first for this book, you can check it out";
    public string DOC_DOES_NOT_EXIST => "Such document does not exist";

    public string DELETE_LIBRARIAN_CONFIRMATION_TEXT => "Are you sure you want to delete this librarian?";
    public string DELETE_USER_CONFIRMATION_TEXT => "Are you sure you want to delete this librarian?";
    public string CHECK_OUT_CONFIRMATION_TEXT => "Are you sure you want to check out this book?";
    public string RENEW_CONFIRMATION_TEXT => "Are you sure you want to renew this book?";
    public string RETURN_CONFIRMATION_TEXT => "Are you sure you want to return this book?";

    public string ATTENTION_TEXT => "Attention";
    public string SUCCESSFUL_RETURN => "Successfuly returned";

    public string SERIAL_NUMBER => "iamlibrarian";
    public string USER_EXIST_TEXT => "User with such login already exist!";

    public string REGISTER_PAGE_TITLE => "Sign Up";
    public string ADD_USER_PAGE_TITLE => "Register New User";

    public readonly string[] USER_TYPES = { "Student", "Instructor", "TA", "Visiting Professor", "Professor", "Librarian", "Admin" };
    public readonly string[] DOC_TYPES = {"Book", "Journal", "AV"};

}
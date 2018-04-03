/// <summary> 
/// Class of string constants to avoid harcoding and
///  (maybe sometime) provide multi-language support 
/// </summary>
class StringConstants {

    public string CONNECTION_STRING => "Server=tcp:ilibserver.database.windows.net,1433;" +
                                       "Initial Catalog=iLibDB;" +
                                       "Persist Security Info=False;" +
                                       "User ID=iLibAdmin;" +
                                       "Password=Faraday28;" +
                                       "MultipleActiveResultSets=False;" +
                                       "Encrypt=True;" +
                                       "TrustServerCertificate=False;" +
                                       "Connection Timeout=30;";

    public string MAIL_SERVER_LOGIN => "ilib.mail2018";
    public string MAIL_SERVER_PASSWORD => "Faraday28";
    public string MAIL_TITLE => "Book is Availible!";
    public string MAIL_TEXT(string docTitle, string docType) => "\r" + docType + " " + docTitle +
        " is now availible. Check it out in iLib!\n\rhttps://github.com/BoogiemanNSK/ILib";

    public string SUCCESSFUL_RENEW => "Successfuly renewed";

    public string USER_HAVE_FINE => "You have fine, paid please";
    public string FINE_CONFIRMATION_TEXT => "Are you sure you want paid fine?";
    public string SUCCESSFUL_PAID => "Successfuly paid";
    public string DOC_ALREADY_RENEWED => "You already renew this book";
    public string DOC_IN_QUEUE => "This book is in queue";
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


    public string CHECK_OUT_CONFIRMATION_TEXT => "Are you sure you want to check out this book?";

    public string RETURN_CONFIRMATION_TEXT => "Are you sure you want to return this book?";
    public string ATTENTION_TEXT => "Attention";
    public string SUCCESSFUL_RETURN => "Successfuly returned";

    public string SERIAL_NUMBER => "iamlibrarian";
    public string USER_EXIST_TEXT => "User with such login already exist!";
    public string USER_DOES_NOT_EXIST_TEXT => "User with such login does not exist!";

    public string USER_CARD_OBTAINING_TEXT => "User card successfully obtained!";
    public string OVERDUE_INFO_TEXT => "Overdue info successfully obtained!";

    public string REGISTER_PAGE_TITLE => "Sign Up";
    public string ADD_USER_PAGE_TITLE => "Register New User";

    public string[] USER_TYPES => new string[] { "Student", "Instructor", "TA", "Professor", "Visiting Professor", "Librarian" };
    public string[] DOC_TYPES => new string[] {"Book", "Journal", "AV"};

}
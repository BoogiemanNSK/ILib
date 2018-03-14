/// <summary> 
/// Class of string constants to avoid harcoding and
///  (maybe sometime) provide multi-language support 
/// </summary>
class StringConstants
{
    public string CONNECTION_STRING => "Server=tcp:ilibserver.database.windows.net,1433;" +
                                       "Initial Catalog=iLibDB;" +
                                       "Persist Security Info=False;" +
                                       "User ID=iLibAdmin;" +
                                       "Password=Faraday28;" +
                                       "MultipleActiveResultSets=False;" +
                                       "Encrypt=True;" +
                                       "TrustServerCertificate=False;" +
                                       "Connection Timeout=30;";

    public string DOC_ALREADY_RENEWED => "You already renew this book";
    public string DOC_IN_QUEUE => "This book is in queue";
    public string DB_DIRECTORY_NAME => "DataBases";
    public string DB_RELATIVE_PATH => "\\" + DB_DIRECTORY_NAME + "\\ILibDB.mdf";

    public string USER_NOT_FOUND_TEXT => "User not found";
    public string WRONG_PASSWORD_TEXT => "Wrong password";

    public string ALREADY_HAVE_TEXT => "You already have that book";
    public string NO_FREE_COPIES_TEXT => "There are no free copies of this book for now";
    public string SUCCESS_CHECK_OUT_TEXT => "Successfully checked out";

    public string WELCOME_TEXT => "Welcome";
    public string SELECT_CHECK_OUT => "Select a document you would like to check out";

    public string RETURN_CONFIRMATION_TEXT => "Are you sure you want return this book?";
    public string ATTENTION_TEXT => "Attention";
    public string SUCCESSFUL_RETURN => "Successfuly returned";

    public string SERIAL_NUMBER => "iamlibrarian";
    public string USER_EXIST_TEXT => "User with such login already exist!";
    public string USER_DOES_NOT_EXIST_TEXT => "User with such login does not exist!";

    public string USER_CARD_OBTAINING_TEXT => "User card successfully obtained!";
    public string OVERDUE_INFO_TEXT => "Ocerdue info successfully obtained!";

    public string[] DOC_TYPES => new string[] {"Book", "Journal", "Audio", "Video"};

}
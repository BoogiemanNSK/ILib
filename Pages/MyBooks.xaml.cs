using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace I2P_Project.Pages
{
    /// <summary> Interaction logic for MyBooks.xaml </summary>
    public partial class MyBooks : Page
    {
        List<String> searched_books = new List<String>(); // Data for autocomplete box
        
        public MyBooks()
        {
            InitializeComponent();
            UpdateUI();
            searched_books = LoadACB();
        }

        /// <summary> Updates table of user`s docs </summary>
        private void UpdateUI()
        {
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            myBooksTable.ItemsSource = SDM.LMS.GetUserBooks(SDM.CurrentUser.PersonID);
            pm.EndWaiting();
        }

        /// <summary> Updates table according to keyword </summary>
        private void UpdateTableAfterSearch()
        {
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            myBooksTable.ItemsSource = SDM.LMS.GetUserBooks(txt_searchMyBooks.Text);
            pm.EndWaiting();
        }

        /// <summary> Trying to return document </summary>
        private void OnReturn(object sender, RoutedEventArgs e)
        {
            if (myBooksTable.SelectedIndex == -1) return;

            MessageBoxResult result = MessageBox.Show(SDM.Strings.RETURN_CONFIRMATION_TEXT,
                SDM.Strings.ATTENTION_TEXT, MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    MyBooksTable mb_row = myBooksTable.SelectedItems[0] as MyBooksTable;
                    int bookID = mb_row.docID;
                    Patron currentPatron = (Patron)SDM.CurrentUser;

                    string returnResult = currentPatron.ReturnDoc(bookID);

                    if (returnResult.Equals(SDM.Strings.USER_HAVE_FINE))
                    {
                        MessageBoxResult askForFine = MessageBox.Show(SDM.Strings.FINE_CONFIRMATION_TEXT,
                            SDM.Strings.ATTENTION_TEXT, MessageBoxButton.YesNo);
                        switch (askForFine)
                        {
                            case MessageBoxResult.Yes:
                                currentPatron.PayFine(bookID);
                                returnResult = currentPatron.ReturnDoc(bookID);
                                MessageBox.Show(returnResult);
                                break;
                            case MessageBoxResult.No:
                                break;
                        }
                    } else {
                        MessageBox.Show(returnResult);
                    }

                    UpdateUI();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void OnRenew(object sender, RoutedEventArgs e)
        {
            if (myBooksTable.SelectedIndex == -1) return;

            MessageBoxResult result = MessageBox.Show(SDM.Strings.RENEW_CONFIRMATION_TEXT,
                SDM.Strings.ATTENTION_TEXT, MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    MyBooksTable mb_row = myBooksTable.SelectedItems[0] as MyBooksTable;
                    int bookID = mb_row.docID;
                    var currentPatron = (Patron)SDM.CurrentUser;
                    string returnResult = currentPatron.RenewDoc(bookID);

                    if (returnResult.Equals(SDM.Strings.USER_HAVE_FINE))
                    {
                        MessageBoxResult askForFine = MessageBox.Show(SDM.Strings.FINE_CONFIRMATION_TEXT,
                            SDM.Strings.ATTENTION_TEXT, MessageBoxButton.YesNo);
                        switch (askForFine)
                        {
                            case MessageBoxResult.Yes:
                                currentPatron.PayFine(bookID);
                                returnResult = currentPatron.RenewDoc(bookID);
                                MessageBox.Show(returnResult);
                                break;
                            case MessageBoxResult.No:
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show(returnResult);
                    }

                    UpdateUI();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        /// <summary> First load documents for auto complete box </summary>
        private List<String> LoadACB()
        {
            List<String> temp = SDM.LMS.GetSearchUserBooks();
            return temp;
        }

        /// <summary> Search doc method doc </summary>
        private void txt_searchMyBooks_Populating(object sender, PopulatingEventArgs e)
        {
            txt_searchMyBooks.ItemsSource = searched_books;
        }

        /// <summary> Select one of all drop down options </summary>
        private void txt_searchMyBooks_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            string txt = txt_searchMyBooks.Text;
            txt = txt.Split('\n')[0];
            txt_searchMyBooks.Text = txt;
        }

        /// <summary> Search book by keyword in AutoCompleteBox </summary>
        private void btn_SearchBook_Click(object sender, RoutedEventArgs e)
        {
            if (txt_searchMyBooks.Text == "")
            {
                UpdateUI();
            }
            else
            {
                UpdateTableAfterSearch();
            }
        }
    }

    class MyBooksTable
    {
        public int checkID { get; set; }
        public int docID { get; set; }
        public string docTitle { get; set; }
        public string docAutors { get; set; }
        public int docPrice { get; set; }
        public int docFine { get; set; }
        public DateTime checkDateTaked { get; set; }
        public DateTime checkTimeToBack { get; set; }
    }
}

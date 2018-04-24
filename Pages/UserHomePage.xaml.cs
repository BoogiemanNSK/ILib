using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace I2P_Project.Pages
{
    /// <summary> Interaction logic for UserHomePage.xaml </summary>
    public partial class UserHomePage : Page
    {
        List<String> searched_books = new List<String>(); // Data for autocomplete box

        public UserHomePage()
        {
            InitializeComponent();
            UpdateUI();
            searched_books = LoadACB();
            txt_searchBook.ItemsSource = searched_books;
        }

        /// <summary> Updates table of all docs </summary>
        private void UpdateUI()
        {
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            docTable.ItemsSource = SDM.LMS.GetDocsTable();
            pm.EndWaiting();
        }

        /// <summary> Updates table according to keyword </summary>
        private void UpdateTableAfterSearch()
        {
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            docTable.ItemsSource = SDM.LMS.GetDocsTable(txt_searchBook.Text);
            pm.EndWaiting();
        }

        /// <summary> Trying to check out selected doc </summary>
        private void OnCheckOut(object sender, RoutedEventArgs e)
        {
            if (docTable.SelectedIndex == -1) return;

            MessageBoxResult result = MessageBox.Show(SDM.Strings.CHECK_OUT_CONFIRMATION_TEXT,
                SDM.Strings.ATTENTION_TEXT, MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    DocumentsTable lb_row = docTable.SelectedItems[0] as DocumentsTable;
                    int bookID = lb_row.docID;
                    Patron currentPatron = (Patron)SDM.CurrentUser;
                    MessageBox.Show(currentPatron.CheckOut(bookID));
                    UpdateUI();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        /// <summary> First load documents for auto complete box </summary>
        private List<String> LoadACB()
        {
            List<String> temp = SDM.LMS.GetSearchBooks();
            return temp;
        }

        /// <summary> Select one of all drop down options </summary>
        private void txt_searchBook_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            string txt = txt_searchBook.Text;
            txt = txt.Split('\n')[0];
            txt_searchBook.Text = txt;
        }

        /// <summary> Search book by keyword in AutoCompleteBox </summary>
        private void btn_SearchBook_Click(object sender, RoutedEventArgs e)
        {
            switch(cb_SearchType.SelectedIndex)  // select search type
            {
                case 0: // standard search
                    if(txt_searchBook.Text == "")
                    {
                        UpdateUI();
                    }
                    else
                    {
                        UpdateTableAfterSearch();
                    }
                    break;
                case 1: // search by keyword
                    break;
            }
        }

        /// <summary> Select one of all drop down options </summary>
        private void cb_SearchType_DropDownClosed(object sender, EventArgs e)
        {
            switch (cb_SearchType.SelectedIndex)
            {
                case 0:
                    txt_searchBook.ItemsSource = searched_books; // enable DropDown
                    break;
                case 1:
                    txt_searchBook.ItemsSource = null;  // disable DropDown
                    break;
            }
        }
    }
}

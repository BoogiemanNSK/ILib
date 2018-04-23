using I2P_Project.Classes;
using System;
using System.Windows;
using System.Windows.Controls;
using I2P_Project.Classes.UserSystem;
using System.Collections.Generic;

namespace I2P_Project.Pages
{
    /// <summary> Логика взаимодействия для DocumentsManagementPage.xaml </summary>
    public partial class DocumentsManagementPage : Page
    {
        List<String> searched_docs = new List<String>(); // Data for autocomplete box
        public DocumentsManagementPage()
        {
            InitializeComponent();
            Librarian lb = (Librarian)SDM.CurrentUser;
            if (lb.LibrarianType < 2) {
                DeleteColumn.Visibility = Visibility.Hidden;
            }
            if (lb.LibrarianType < 1) {
                AddBookButton.Visibility = Visibility.Hidden;
                RequestColumn.Visibility = Visibility.Hidden;
            }
            UpdateTable();
            searched_docs = LoadACB();
        }

        private void OnAddBook(object sender, RoutedEventArgs e)
        {
            AddDocPage page = new AddDocPage(this, true, 0);
            page.ShowDialog();
        }

        public void UpdateTable()
        {
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            dgLibrarianDocuments.ItemsSource = SDM.LMS.GetDocsTable();
            pm.EndWaiting();
        }

        /// <summary> Updates table according to keyword </summary>
        private void UpdateTableAfterSearch()
        {
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            dgLibrarianDocuments.ItemsSource = SDM.LMS.GetDocsTable(txt_searchDocument.Text);
            pm.EndWaiting();
        }

        private void OnModifyBook(object sender, RoutedEventArgs e)
        {
            if (dgLibrarianDocuments.SelectedIndex != -1 && dgLibrarianDocuments.SelectedItems[0] != null)
            {
                DocumentsTable docRow = dgLibrarianDocuments.SelectedItems[0] as DocumentsTable;
                Window page = null;
                switch (docRow.docType) {
                    case "Book":
                        page = new ModifyBookPage(docRow.docID, this);
                        break;
                    case "Journal":
                        page = new ModifyJournalPage(docRow.docID, this);
                        break;
                    case "AV":
                        page = new ModifyAVPage(docRow.docID, this);
                        break;
                    default:
                        throw new Exception("Unhandled doc type!");
                }
                page.ShowDialog();
            }
        }

        private void OnDeleteBook(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure to remove this entry?", "Attention", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        Librarian lib = (Librarian)SDM.CurrentUser;
                        DocumentsTable doc_row = dgLibrarianDocuments.SelectedItems[0] as DocumentsTable;
                        int doc_id = doc_row.docID;
                        lib.DeleteDoc(doc_id);
                        UpdateTable();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString(), "Error");
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void OnRequestBook(object sender, RoutedEventArgs e)
        {
            DocumentsTable docRow = dgLibrarianDocuments.SelectedItems[0] as DocumentsTable;

            Librarian lb = (Librarian)SDM.CurrentUser;
            lb.OutstandingRequest(docRow.docID);

            UpdateTable();
            MessageBox.Show("You have successfully deleted users queue for that document.", "Success!", MessageBoxButton.OK);
        }

        /// <summary> Search doc method doc </summary>
        private void txt_searchDocument_Populating(object sender, PopulatingEventArgs e)
        {
            txt_searchDocument.ItemsSource = searched_docs;
        }

        /// <summary> First load documents for auto complete box </summary>
        private List<String> LoadACB()
        {
            List<String> temp = SDM.LMS.GetSearchBooks();
            return temp;
        }

        /// <summary> Select one of all drop down options </summary>
        private void txt_searchDocument_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            string txt = txt_searchDocument.Text;
            txt = txt.Split('\n')[0];
            txt_searchDocument.Text = txt;
        }

        /// <summary> Search book by keyword in AutoCompleteBox </summary>
        private void btn_searchDoc_Click(object sender, RoutedEventArgs e)
        {
            if (txt_searchDocument.Text == "")
            {
                UpdateTable();
            }
            else
            {
                UpdateTableAfterSearch();
            }
        }
    }

    class DocumentsTable
    {
        public int docID { get; set; }
        public string docTitle { get; set; }
        public string docAutors { get; set; }
        public string docType { get; set; }
        public int docPrice { get; set; }
        public int docQuantity { get; set; }

    }
}

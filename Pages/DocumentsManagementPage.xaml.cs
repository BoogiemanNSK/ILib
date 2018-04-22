using I2P_Project.Classes;
using System;
using System.Windows;
using System.Windows.Controls;
using I2P_Project.Classes.UserSystem;

namespace I2P_Project.Pages
{
    /// <summary> Логика взаимодействия для DocumentsManagementPage.xaml </summary>
    public partial class DocumentsManagementPage : Page
    {
        public DocumentsManagementPage()
        {
            InitializeComponent();
            Librarian lb = (Librarian)SDM.CurrentUser;
            if (lb.LibrarianType < 2) {
                DeleteColumn.Visibility = Visibility.Hidden;
            }
            if (lb.LibrarianType < 1) {
                AddBookButton.Visibility = Visibility.Hidden;
            }
            UpdateTable();
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

        private void myBooksTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {

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

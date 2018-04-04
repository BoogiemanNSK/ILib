using I2P_Project.Classes;
using I2P_Project.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using I2P_Project.Classes.UserSystem;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для DocumentsManagementPage.xaml
    /// </summary>
    public partial class DocumentsManagementPage : Page
    {
        public DocumentsManagementPage()
        {
            InitializeComponent();
            updateTable();
        }

        private void OnAddBook(object sender, RoutedEventArgs e)
        {
            AddDocPage page = new AddDocPage(this);
            page.ShowDialog();
        }

        public void updateTable()
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
                DocumentsTable doc_row = dgLibrarianDocuments.SelectedItems[0] as DocumentsTable;
                int doc_id = doc_row.docID;
                ModifyBooksPage page =  new ModifyBooksPage(doc_id, this);
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
                        //remove document
                        DocumentsTable doc_row = dgLibrarianDocuments.SelectedItems[0] as DocumentsTable;
                        int doc_id = doc_row.docID;
                        lib.DeleteDoc(doc_id);
                        updateTable();
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
            SDM.LMS.SetOutstandingRequest(docRow.docID);
            updateTable();
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

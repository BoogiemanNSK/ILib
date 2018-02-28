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

namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для DocumentsManagementPage.xaml
    /// </summary>
    public partial class DocumentsManagementPage : Window
    {
        public DocumentsManagementPage()
        {
            InitializeComponent();
            updateTable();
        }

        private void OnAddBook(object sender, RoutedEventArgs e)
        {
            AddBookPage page = new AddBookPage();
            page.Show();
            Close();
        }

        private void updateTable()
        {
            dgLibrarianDocuments.ItemsSource = SDM.LMS.GetDocsTableForLibrarian();
        }

        private void myBooksTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {

        }


        private void OnBack(object sender, RoutedEventArgs e)
        {
            LibrarianHomePage librarianHome = new LibrarianHomePage();
            librarianHome.Show();
            Close();
        }

        private void OnModifyBook(object sender, RoutedEventArgs e)
        {
            if (dgLibrarianDocuments.SelectedIndex != -1 && dgLibrarianDocuments.SelectedItems[0] != null)
            {
                DocumentsTable doc_row = dgLibrarianDocuments.SelectedItems[0] as DocumentsTable;
                int doc_id = doc_row.docID;
                ModifyBooksPage page =  new ModifyBooksPage(doc_id);
                page.Show();
                Close();
            }
            //TODO: initialize and so on

        }

        private void OnDeleteBook(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure to remove this book?", "Attention", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        //remove document
                        DocumentsTable doc_row = dgLibrarianDocuments.SelectedItems[0] as DocumentsTable;
                        int doc_id = doc_row.docID;
                        SDM.LMS.RemoveDocument(doc_id);
                        updateTable();
                    }
                    catch
                    {
                        MessageBox.Show("The row is empty", "Error");
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }

    class DocumentsTable
    {
        public DateTime dateTaked { get; set; }
        public int docID { get; set; }
        public int docOwnerID { get; set; }
        public string docTitle { get; set; }
        public string docType { get; set; }
        public bool isReference { get; set; }
        public DateTime timeToBack { get; set; }

    }
}

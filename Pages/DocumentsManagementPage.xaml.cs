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
        class DocsTable
        {
            public DateTime dateTaked { get; set; }
            public int docID { get; set; }
            public int docOwnerID { get; set; }
            public string docTitle { get; set; }
            public string docType { get; set; }
            public bool isReference { get; set; }
            public DateTime timeToBack { get; set; }

        }
        private void updateTable()
        {
            DocTable.ItemsSource = SDM.LMS.TestDocsTableOnlyBooks();
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
            if (DocTable.SelectedIndex != -1 && DocTable.SelectedItems[0] != null)
            {
                DocsTable ut_row = DocTable.SelectedItems[0] as DocsTable;
                int doc_id = ut_row.docID;
                //ModifyBooksPage page;
            }
            //TODO: initialize and so on
            
        }

        private void OnDeleteBook(object sender, RoutedEventArgs e)
        {
            if (DocTable.SelectedIndex != -1 && DocTable.SelectedItems[0] != null)
            {
                int doc_id = (int) DocTable.CurrentCell.Item;
                SDM.LMS.DeleteDoc(doc_id);
                updateTable();
            }
        }
    }
}

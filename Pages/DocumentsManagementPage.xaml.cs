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
        }

        private void OnAddBook(object sender, RoutedEventArgs e)
        {
            AddBookPage page = new AddBookPage();
            page.Show();
            Close();
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

        }

        private void OnDeleteBook(object sender, RoutedEventArgs e)
        {

        }
    }
}

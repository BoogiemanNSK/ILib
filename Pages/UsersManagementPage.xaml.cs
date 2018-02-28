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
using I2P_Project.Classes;
using I2P_Project.Pages;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Interaction logic for UsersManagementPage.xaml
    /// </summary>
    public partial class UsersManagementPage : Window
    {
        public UsersManagementPage()
        {
            InitializeComponent();
            UsersTable.ItemsSource = SDM.LMS.LibrarianViewUserTable();
        }

        private void OnAddUser(object sender, RoutedEventArgs e)
        {
            RegisterPage Register = new RegisterPage(false);
            Register.Show();
            Close();
        }

        private void OnUserCard(object sender, RoutedEventArgs e)
        {

        }

        private void OnUserOverdueInfo(object sender, RoutedEventArgs e)
        {

        }

        private void OnBack(object sender, RoutedEventArgs e)
        {
            LibrarianHomePage librarianHome = new LibrarianHomePage();
            librarianHome.Show();
            Close();
        }
    }

    class LibrarianUserView
    {
        public int userID { get; set; }
        public string userLogin { get; set; }
        public int docsNumber { get; set; }
        public int userFine { get; set; }
    }
}

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
        }

        private void OnAddUser(object sender, RoutedEventArgs e)
        {
            RegisterPage Register = new RegisterPage(false);
            Register.Show();
            Close();
        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {

        }

        private void OnModifyUser(object sender, RoutedEventArgs e)
        {

        }

        private void OnDeleteUser(object sender, RoutedEventArgs e)
        {

        }

        private void OnBack(object sender, RoutedEventArgs e)
        {
            LibrarianHomePage librarianHome = new LibrarianHomePage();
            librarianHome.Show();
            Close();
        }
    }
}

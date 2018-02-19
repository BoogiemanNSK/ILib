using I2P_Project.Classes.Data_Managers;
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
    /// Interaction logic for LibrarianHomePage.xaml
    /// </summary>
    public partial class LibrarianHomePage : Window
    {
        public LibrarianHomePage()
        {
            InitializeComponent();
            WelcomeText.Content = "Welcome, " + SystemDataManager.CurrentUser.Name + "!";
        }

        private void UserManagementClick(object sender, RoutedEventArgs e)
        {
            UsersManagementPage UsersManagement = new UsersManagementPage();
            UsersManagement.Show();
            Close();
        }

        private void DocumentsManagementClick(object sender, RoutedEventArgs e)
        {
            DocumentsManagementPage DocManagement = new DocumentsManagementPage();
            DocManagement.Show();
            Close();
        }
    }
}

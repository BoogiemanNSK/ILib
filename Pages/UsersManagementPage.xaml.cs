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
    public partial class UsersManagementPage : Page
    {
        public UsersManagementPage()
        {
            InitializeComponent();
            UpdateTable();
        }

        private void UpdateTable()
        {
            UsersTable.ItemsSource = SDM.LMS.LibrarianViewUserTable();
        }

        private void OnAddUser(object sender, RoutedEventArgs e)
        {
            RegisterPage Register = new RegisterPage(false);
            Register.ShowDialog();
            UpdateTable();
        }

        private void OnUserCard(object sender, RoutedEventArgs e)
        {
            if (UsersTable.SelectedItem != null)
            {
                LibrarianUserView selectedUser = UsersTable.SelectedItem as LibrarianUserView;
                UserCard modifyUser = new UserCard(selectedUser.userID);
                modifyUser.ShowDialog();
                UpdateTable();
            }
        }

        private void OnUserOverdueInfo(object sender, RoutedEventArgs e)
        {
            if (UsersTable.SelectedItem != null)
            {
                LibrarianUserView selectedUser = UsersTable.SelectedItem as LibrarianUserView;
                OverdueInfo modifyUser = new OverdueInfo(selectedUser.userID);
                modifyUser.ShowDialog();
            }
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

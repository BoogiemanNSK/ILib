using System.Windows;
using System.Windows.Controls;
using I2P_Project.Classes;

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
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            UsersTable.ItemsSource = SDM.LMS.LibrarianViewUserTable();
            pm.EndWaiting();
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

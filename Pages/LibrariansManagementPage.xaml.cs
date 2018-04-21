using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using System.Windows;
using System.Windows.Controls;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Interaction logic for LibrariansManagementPage.xaml
    /// </summary>
    public partial class LibrariansManagementPage : Page
    {
        public LibrariansManagementPage()
        {
            InitializeComponent();
            Admin admin = (Admin)SDM.CurrentUser;
            UpdateTable();
        }

        private void UpdateTable()
        {
            LibrariansTable.ItemsSource = SDM.LMS.AdminViewUserTable();
        }

        private void OnAddLibrarian(object sender, RoutedEventArgs e)
        {
            AddLibrarianPage Add = new AddLibrarianPage();
            Add.ShowDialog();
            UpdateTable();
        }

        private void OnModifyLibrarian(object sender, RoutedEventArgs e)
        {
            if (LibrariansTable.SelectedItem != null) {
                AdminUserView selectedLibrarian = LibrariansTable.SelectedItem as AdminUserView;
                ModifyUserPage modifyLibrarian = new ModifyUserPage(selectedLibrarian.LibrarianID);
                modifyLibrarian.ShowDialog();
                UpdateTable();
            }
        }

        private void OnDeleteLibrarian(object sender, RoutedEventArgs e)
        {
            if (LibrariansTable.SelectedItem != null) {
                AdminUserView selectedLibrarian = LibrariansTable.SelectedItem as AdminUserView;
                Admin lib = (Admin)SDM.CurrentUser;
                lib.DeleteLibrarian(selectedLibrarian.LibrarianID);
                UpdateTable();
            }
        }
    }

    class AdminUserView
    {
        public int LibrarianID { get; set; }
        public string LibrarianLogin { get; set; }
        public string LibrarianName { get; set; }
        public int LibrarianType { get; set; }
    }
}

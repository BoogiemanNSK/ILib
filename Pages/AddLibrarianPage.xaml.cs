using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using System;
using System.Windows;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Interaction logic for AddLibrarianPage.xaml
    /// </summary>
    public partial class AddLibrarianPage : Window
    {
        public AddLibrarianPage()
        {
            InitializeComponent();
        }

        private void OnAddLibrarianClick(object sender, RoutedEventArgs e)
        {
            try {
                if (LibrarianName.Text.Length == 0 || LibrarianAddress.Text.Length == 0 || LibrarianPhoneNumber.Text.Length == 0) throw new Exception();
                
                Admin admin = (Admin)SDM.CurrentUser;
                admin.RegisterLibrarian
                    (
                        LibrarianLogin.Text,
                        LibrarianPassword.Text,
                        LibrarianName.Text,
                        LibrarianAddress.Text,
                        LibrarianPhoneNumber.Text
                    );
                Close();
            } catch {
                MessageBox.Show("One of fields is not filled!", "Error");
            }
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}

using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using System;
using System.Windows;

namespace I2P_Project.Pages
{
    /// <summary> Interaction logic for AddLibrarianPage.xaml </summary>
    public partial class AddLibrarianPage : Window
    {
        public AddLibrarianPage()
        {
            InitializeComponent();
        }

        private void OnAddLibrarianClick(object sender, RoutedEventArgs e)
        {
            if (LibrarianName.Text.Length == 0 || LibrarianAddress.Text.Length == 0 || LibrarianPhoneNumber.Text.Length == 0) {
                MessageBox.Show("One of fields is not filled!", "Error");
                return;
            }
            if (!ValidMail(LibrarianAddress.Text)) {
                MessageBox.Show("Invalid e-mail!", "Error");
                return;
            }

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
        }

        private bool ValidMail(string mail)
        {
            string[] mail_parts = mail.Split('@');
            if (mail_parts.Length != 2 || mail_parts[0].Length < 1 || mail_parts[1].Length < 1) { return false; }
            mail_parts = mail_parts[1].Split('.');
            if (mail_parts.Length != 2 || mail_parts[0].Length < 1 || mail_parts[1].Length < 1) { return false; }
            return true;
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}

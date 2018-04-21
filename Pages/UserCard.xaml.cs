using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using I2P_Project.DataBase;
using System;
using System.Windows;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserCard.xaml
    /// </summary>
    public partial class UserCard : Window
    {
        private int _patronID;

        public UserCard(int patronID)
        {
            InitializeComponent();
            Librarian lb = (Librarian)SDM.CurrentUser;
            if (lb.LibrarianType < 2) {
                DeleteUserButton.Visibility = Visibility.Hidden;
            }
            _patronID = patronID;

            Users user = SDM.LMS.GetUser(patronID);
            UserLogin.Content = user.Login;
            UserName.Text = user.Name;
            UserAdress.Text = user.Address;
            UserPhoneNumber.Text = user.PhoneNumber;
            UserType.Text = SDM.Strings.USER_TYPES[user.UserType];

            UserDocsTable.ItemsSource = SDM.LMS.GetUserDocsFromLibrarian(patronID);
        }

        private void OnModifyUserClick(object sender, RoutedEventArgs e)
        {
            ModifyUserPage modifyUser = new ModifyUserPage(_patronID);
            Close();
            modifyUser.ShowDialog();
        }

        private void OnDeleteUserClick(object sender, RoutedEventArgs e)
        {
            Close();
            Librarian lib = (Librarian)SDM.CurrentUser;
            lib.DeleteUser(_patronID);
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    class UserDocsTable
    {
        public string DocTitle { get; set; }
        public string DocType { get; set; }
        public DateTime DateTaked { get; set; }
        public DateTime DeadLine { get; set; }
    }
}

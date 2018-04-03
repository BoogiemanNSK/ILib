using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using I2P_Project.DataBase;
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
    /// Логика взаимодействия для UserCard.xaml
    /// </summary>
    public partial class UserCard : Window
    {
        private int _patronID;

        public UserCard(int patronID)
        {
            InitializeComponent();
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

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
    /// Interaction logic for ModifyUserPage.xaml
    /// </summary>
    public partial class ModifyUserPage : Window
    {

        private int userID;

        public ModifyUserPage(int ID)
        {
            userID = ID;
            InitializeComponent();

            Users user = SDM.LMS.GetUser(ID);
            UserLogin.Content = user.Login;
            UserName.Text = user.Name;
            UserAdress.Text = user.Address;
            UserPhoneNumber.Text = user.PhoneNumber;
            UserType.SelectedIndex = user.UserType - 1;
        }

        private void OnModifyUserClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserName.Text.Length == 0 || UserAdress.Text.Length == 0 || UserPhoneNumber.Text.Length == 0) throw new Exception();

                Librarian lib = (Librarian)SDM.CurrentUser;
                lib.ModifyUser
                    (
                        userID,
                        UserName.Text,
                        UserAdress.Text,
                        UserPhoneNumber.Text,
                        UserType.SelectedIndex + 1
                    );
                UsersManagementPage page = new UsersManagementPage();
                page.Show();
                Close();
            }
            catch
            {
                InfoText.Content = "One of fields is not filled!";
            }
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            UsersManagementPage page = new UsersManagementPage();
            page.Show();
            Close();
        }
    }
}

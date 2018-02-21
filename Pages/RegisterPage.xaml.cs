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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using I2P_Project.Classes;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {
        bool register;
        public RegisterPage(bool b)
        {
            register = b;
            InitializeComponent();
        }

        private const string serialNum = "iamlibrarian";

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            bool isLibrarian = (SerialNumTB.Text == serialNum);

            if (SDM.LMS.RegisterUser
                (
                    LoginTB.Text,
                    PasswordTB.Password,
                    NameTB.Text,
                    AdressTB.Text,
                    PhoneNumberTB.Text,
                    isLibrarian
                ))
            {
                if (register)
                {
                    LogInPage LogIn = new LogInPage();
                    LogIn.Show();
                    Close();
                }
                else
                {
                    UsersManagementPage UserManagement = new UsersManagementPage();
                    UserManagement.Show();
                    Close();
                }
            }
            else
            {
                InfoText.Content = "User with such login already exist!";
            }
            
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            if (register)
                {
                LogInPage LogIn = new LogInPage();
                LogIn.Show();
                Close();
            }
            else
            {
                UsersManagementPage UserManagement = new UsersManagementPage();
                UserManagement.Show();
                Close();
            }
        }

    }
}

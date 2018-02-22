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
        private bool _register;

        public RegisterPage(bool b)
        {
            _register = b;
            InitializeComponent();
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            bool isLibrarian = (SerialNumTB.Text == SDM.Strings.SERIAL_NUMBER);

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
                if (_register)
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
                InfoText.Content = SDM.Strings.USER_EXIST_TEXT;
            }
            
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            if (_register)
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

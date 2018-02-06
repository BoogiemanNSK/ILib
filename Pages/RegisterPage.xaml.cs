using I2P_Project.Classes.Data_Managers;
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

namespace I2P_Project.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private const string serialNum = "iamlibrarian";

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            bool isLibrarian = (SerialNumTB.Text == serialNum);

            if (DataBaseManager.RegisterUser
                (
                    LoginTB.Text,
                    PasswordTB.Password,
                    NameTB.Text,
                    AdressTB.Text,
                    PhoneNumberTB.Text,
                    isLibrarian
                ))
            {
                LogInPage LogIn = new LogInPage();
                LogIn.Show();
                Close();
            }
            else
            {
                InfoText.Content = "User with such login already exist!";
            }
            
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            LogInPage LogIn = new LogInPage();
            LogIn.Show();
            Close();
        }

    }
}

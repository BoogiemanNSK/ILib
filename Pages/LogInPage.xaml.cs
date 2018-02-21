using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
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
    /// Логика взаимодействия для LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Window
    {
        public LogInPage()
        {
            SDM.LMS.Initialize();
            InitializeComponent();
        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            if (SDM.LMS.CheckLogin(LoginTB.Text))
            {
                if (SDM.LMS.CheckPassword(LoginTB.Text, PasswordTB.Password))
                {
                    LogIn();
                }
                else
                    WrongLabel.Content = "Wrong password"; // TODO String constants
            }
            else
                WrongLabel.Content = "User not found"; // TODO String constants
        }

        private void LogIn()
        {
            SetCurrentUser();
            if (SDM.CurrentUser.IsLibrarian)
            {
                LibrarianHomePage LibHomePage = new LibrarianHomePage();
                LibHomePage.Show();
                Close();
            }
            else
            {
                UserHomePage HomePage = new UserHomePage();
                HomePage.Show();
                Close();
            }
        }

        private void SetCurrentUser()
        {
            int userType = SDM.LMS.GetUserType(LoginTB.Text);
            switch (userType)
            {
                case 0:
                    SDM.CurrentUser = new Student(LoginTB.Text);
                    break;
                case 1:
                    SDM.CurrentUser = new Faculty(LoginTB.Text);
                    break;
                case 2:
                    SDM.CurrentUser = new Librarian(LoginTB.Text);
                    break;
                default:
                    throw new Exception("Unhandled user type!");
            }
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            RegisterPage Register = new RegisterPage(true);
            Register.Show();
            Close();
        }

        private void TestingClick(object sender, RoutedEventArgs e)
        {
            TestingTool Test = new TestingTool();
            Test.Show();
        }
    }
}

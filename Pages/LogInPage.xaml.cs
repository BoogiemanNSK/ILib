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
            SDM.InitializeSystem();
            InitializeComponent();
            animation_id = 0;
        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            OnUserLogin();
        }

        private void LogIn()
        {
            SetCurrentUser();
            if (SDM.CurrentUser.UserType == 3)
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
            int userType = new Student(LoginTB.Text).UserType;
            switch (userType)
            {
                case 1:
                    SDM.CurrentUser = new Student(LoginTB.Text);
                    break;
                case 2:
                    SDM.CurrentUser = new Faculty(LoginTB.Text);
                    break;
                case 3:
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

        private void OnUserLogin()
        {
            if (SDM.LMS.CheckLogin(LoginTB.Text))
            {
                if (SDM.LMS.CheckPassword(LoginTB.Text, PasswordTB.Password))
                {
                    LogIn();
                }
                else
                    MessageBox.Show(SDM.Strings.WRONG_PASSWORD_TEXT);
            }
            else
                MessageBox.Show(SDM.Strings.USER_NOT_FOUND_TEXT);
        }

        // Front-end by Valeriy Borisov
        public int animation_id = 0;

        private void PlayAnimation(string filename)
        {
            startupImage.Visibility = Visibility.Hidden;
            string path = @"media/" + filename;
            animationPlayer.Source = new Uri(path, UriKind.Relative);
            animationPlayer.Play();
        }

        private void SelectAnimation()
        {
            switch (animation_id)
            {
                case 0:
                    PlayAnimation("user_look_down.mp4");
                    break;
                case 1:
                    PlayAnimation("user_close_eyes.mp4");
                    break;
                case 2:
                    PlayAnimation("user_open_eyes.mp4");
                    break;
            }
        }

        private void LoginTB_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectAnimation();
        }

        private void PasswordTB_GotFocus(object sender, RoutedEventArgs e)
        {
            animation_id = 1;
            SelectAnimation();
            animation_id = 2;
        }

        private void PasswordTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)  // If user pressed Enter, when password box got focus
            {
                OnUserLogin();
            }
        }
    }
}

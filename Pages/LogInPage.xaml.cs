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
            DataBaseManager.Initialize();
            InitializeComponent();
        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            if (DataBaseManager.CheckEmail(EMailTB.Text))
            {
                if (DataBaseManager.CheckPassword(EMailTB.Text, PasswordTB.Password))
                    WrongLabel.Content = "Correct e-mail and password!";
                else
                    WrongLabel.Content = "Wrong password"; // TODO String constants
            }
            else
                WrongLabel.Content = "User not found"; // TODO String constants
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            RegisterPage Register = new RegisterPage();
            Register.Show();
            Close();
        }
    }
}

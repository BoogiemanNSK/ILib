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

namespace I2P_Project
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            // TODO
            // Check if such e-mail is in DB
                // Check if e-mail matches with password in DB
                    // Set current user state and then move to next (home) page
                // else
                    WrongLabel.Content = "Wrong password"; // TODO String constants
            // else
                WrongLabel.Content = "User not found"; // TODO String constants
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            // TODO Move to registration page
        }
    }
}

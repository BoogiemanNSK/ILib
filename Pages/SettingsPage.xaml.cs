using I2P_Project.Classes;
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

namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void btn_changeLogin_Click(object sender, RoutedEventArgs e)  // Change login
        {
            if(txt_changeLogin.Text != "")
            {
                if(!SDM.LMS.CheckLogin(txt_changeLogin.Text))
                {
                    int user_id = SDM.CurrentUser.PersonID;

                    ProcessManager pm = new ProcessManager(); // Process Manager for long operations
                    pm.BeginWaiting(); // Starts Loading Flow
                    SDM.LMS.UpdateUserLogin(user_id, txt_changeLogin.Text);
                    pm.EndWaiting(); // Ends loading flow
                    MessageBox.Show("You updated your login.");
                }
                else
                {
                    MessageBox.Show("This login is already in use.");
                }
            }
            else
            {
                MessageBox.Show("Login field is empty!", "Warning");
            }
        }

        private void btn_changeName_Click(object sender, RoutedEventArgs e)  // Change Name
        {
            if (txt_changeName.Text != "")
            {
                    int user_id = SDM.CurrentUser.PersonID;

                    ProcessManager pm = new ProcessManager(); // Process Manager for long operations
                    pm.BeginWaiting(); // Starts Loading Flow
                    SDM.LMS.UpdateUserName(user_id, txt_changeName.Text);
                    pm.EndWaiting(); // Ends loading flow
                    MessageBox.Show("You updated your name.");
            }
            else
            {
                MessageBox.Show("Name field is empty!", "Warning");
            }
        }

        private void btn_changePassword_Click(object sender, RoutedEventArgs e)  // Change password
        {
            if(txt_newPassword.Password != "" || txt_confPassword.Password != "")
            {
                if (txt_newPassword.Password == txt_confPassword.Password)
                {
                    int user_id = SDM.CurrentUser.PersonID;
                    ProcessManager pm = new ProcessManager(); // Process Manager for long operations
                    pm.BeginWaiting(); // Starts Loading Flow
                    SDM.LMS.UpdateUserPassword(user_id, txt_confPassword.Password);
                    pm.EndWaiting();
                    txt_newPassword.Password = "";
                    txt_confPassword.Password = "";
                    MessageBox.Show("You changed your password.");
                }
                else
                {
                    MessageBox.Show("Passwords do not match!", "Warning");
                }
            }
            else
            {
                MessageBox.Show("Password field is empty!", "Warning");
            }
        }
    }
}

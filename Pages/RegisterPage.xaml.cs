﻿using System;
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
            if (b) Title.Content = SDM.Strings.ADD_USER_PAGE_TITLE;
            else Title.Content = SDM.Strings.REGISTER_PAGE_TITLE;
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            OnRegisterProcess();
        }

        private void OnRegisterProcess()
        {
            if (LoginTB.Text == "" || PasswordTB.Password == "" || NameTB.Text == "" || AdressTB.Text == "" || PhoneNumberTB.Text == "")
            {
                MessageBox.Show("Not all fields are filled out!", "Warning");
                return;
            }

            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            RegisterUser();
            pm.EndWaiting();
        }

        private void RegisterUser()
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
                    Close();
                }
            }
            else
            {
                MessageBox.Show(SDM.Strings.USER_EXIST_TEXT);
            }
        }

        private void SerialNumTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)  // If user pressed Enter, when password box got focus
            {
                OnRegisterProcess();
            }
        }
    }
}

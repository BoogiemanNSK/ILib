﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;

namespace I2P_Project.Pages
{
    /// <summary> Interaction logic for UsersManagementPage.xaml </summary>
    public partial class UsersManagementPage : Page
    {
        List<String> searched_users = new List<String>(); // Data for autocomplete box
        public UsersManagementPage()
        {
            InitializeComponent();
            Librarian lb = (Librarian)SDM.CurrentUser;
            if (lb.LibrarianType < 1) {
                AddUserButton.Visibility = Visibility.Hidden;
            }
            UpdateTable();
            searched_users = LoadACB();
        }

        private void UpdateTable()
        {
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            UsersTable.ItemsSource = SDM.LMS.LibrarianViewUserTable(3, "");
            pm.EndWaiting();
        }

        /// <summary> Updates table according to keyword </summary>
        private void UpdateTableAfterSearch()
        {
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            int flags = 0;
            flags += ((bool)cb_ByLogin.IsChecked ? 1 << 1 : 0);
            flags += ((bool)cb_ByMail.IsChecked  ? 1 << 0 : 0);
            UsersTable.ItemsSource = SDM.LMS.LibrarianViewUserTable(flags, txt_searchUser.Text);
            pm.EndWaiting();
        }

        private void OnAddUser(object sender, RoutedEventArgs e)
        {
            RegisterPage Register = new RegisterPage(false);
            Register.ShowDialog();
            UpdateTable();
        }

        private void OnUserCard(object sender, RoutedEventArgs e)
        {
            if (UsersTable.SelectedItem != null)
            {
                LibrarianUserView selectedUser = UsersTable.SelectedItem as LibrarianUserView;
                UserCard modifyUser = new UserCard(selectedUser.userID);
                modifyUser.ShowDialog();
                UpdateTable();
            }
        }

        private void OnUserOverdueInfo(object sender, RoutedEventArgs e)
        {
            if (UsersTable.SelectedItem != null)
            {
                LibrarianUserView selectedUser = UsersTable.SelectedItem as LibrarianUserView;
                OverdueInfo modifyUser = new OverdueInfo(selectedUser.userID);
                modifyUser.ShowDialog();
            }
        }

        private void txt_searchUser_Populating(object sender, PopulatingEventArgs e)
        {
            txt_searchUser.ItemsSource = searched_users;
        }

        /// <summary> First load users for auto complete box </summary>
        private List<String> LoadACB()
        {
            List<String> temp = SDM.LMS.GetSearchUser();
            return temp;
        }

        /// <summary> Select one of all drop down options </summary>
        private void txt_searchUser_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            string txt = txt_searchUser.Text;
            txt = txt.Split('\n')[0];
            txt_searchUser.Text = txt;
        }

        /// <summary> Search book by keyword in AutoCompleteBox </summary>
        private void btn_searchUser_Click(object sender, RoutedEventArgs e)
        {
            if (txt_searchUser.Text == "")
            {
                UpdateTable();
            }
            else
            {
                UpdateTableAfterSearch();
            }
        }
    }

    class LibrarianUserView
    {
        public int userID { get; set; }
        public string userLogin { get; set; }
        public string userMail { get; set; }
        public int docsNumber { get; set; }
        public int userFine { get; set; }
    }
}

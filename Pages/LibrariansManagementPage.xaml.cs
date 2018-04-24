using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace I2P_Project.Pages
{
    /// <summary> Interaction logic for LibrariansManagementPage.xaml </summary>
    public partial class LibrariansManagementPage : Page
    {
        List<String> searched_librarians = new List<String>(); // Data for autocomplete box
        public LibrariansManagementPage()
        {
            InitializeComponent();
            Admin admin = (Admin)SDM.CurrentUser;
            UpdateTable();
            searched_librarians = LoadACB();
        }

        private void UpdateTable()
        {
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            LibrariansTable.ItemsSource = SDM.LMS.AdminViewUserTable(7, "");
            pm.EndWaiting();
        }

        /// <summary> Updates table according to keyword </summary>
        private void UpdateTableAfterSearch()
        {
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            int flags = 0;
            flags += ((bool)cb_ByLogin.IsChecked ? 1 << 2 : 0);
            flags += ((bool)cb_ByName.IsChecked  ? 1 << 1 : 0);
            flags += ((bool)cb_ByMail.IsChecked  ? 1 << 0 : 0);
            LibrariansTable.ItemsSource = SDM.LMS.AdminViewUserTable(flags, txt_searchLibrarian.Text);
            pm.EndWaiting();
        }

        private void OnAddLibrarian(object sender, RoutedEventArgs e)
        {
            AddLibrarianPage Add = new AddLibrarianPage();
            Add.ShowDialog();
            UpdateTable();
        }

        private void OnModifyLibrarian(object sender, RoutedEventArgs e)
        {
            if (LibrariansTable.SelectedItem != null) {
                AdminUserView selectedLibrarian = LibrariansTable.SelectedItem as AdminUserView;
                ModifyUserPage modifyLibrarian = new ModifyUserPage(selectedLibrarian.LibrarianID);
                modifyLibrarian.ShowDialog();
                UpdateTable();
            }
        }

        private void OnDeleteLibrarian(object sender, RoutedEventArgs e)
        {
            if (LibrariansTable.SelectedItem != null) {
                MessageBoxResult result = MessageBox.Show(SDM.Strings.DELETE_LIBRARIAN_CONFIRMATION_TEXT,
                SDM.Strings.ATTENTION_TEXT, MessageBoxButton.YesNo);

                switch (result) {
                    case MessageBoxResult.Yes:
                        AdminUserView selectedLibrarian = LibrariansTable.SelectedItem as AdminUserView;
                        Admin lib = (Admin)SDM.CurrentUser;
                        lib.DeleteLibrarian(selectedLibrarian.LibrarianID);
                        UpdateTable();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        /// <summary> Search librarian method </summary>
        private void txt_searchLibrarian_Populating(object sender, PopulatingEventArgs e)
        {
            txt_searchLibrarian.ItemsSource = searched_librarians;
        }

        /// <summary> First load librarians for auto complete box </summary>
        private List<String> LoadACB()
        {
            List<String> temp = SDM.LMS.GetSearchLibrarian();
            return temp;
        }

        /// <summary> Select one of all drop down options </summary>
        private void txt_searchLibrarian_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            string txt = txt_searchLibrarian.Text;
            txt = txt.Split('\n')[0];
            txt_searchLibrarian.Text = txt;
        }

        /// <summary> Search book by keyword in AutoCompleteBox </summary>
        private void btn_searchLibrarian_Click(object sender, RoutedEventArgs e)
        {
            if (txt_searchLibrarian.Text == "")
            {
                UpdateTable();
            }
            else
            {
                UpdateTableAfterSearch();
            }
        }
    }

    class AdminUserView
    {
        public int LibrarianID { get; set; }
        public string LibrarianLogin { get; set; }
        public string LibrarianName { get; set; }
        public string LibrarianMail { get; set; }
        public string LibrarianType { get; set; }
    }
}

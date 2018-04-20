using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using System.Windows;
using System.Windows.Controls;

namespace I2P_Project.Pages
{
    /// <summary> Interaction logic for UserHomePage.xaml </summary>
    public partial class UserHomePage : Page
    {
        public UserHomePage()
        {
            InitializeComponent();
            UpdateUI();
        }

        /// <summary> Updates table of all docs </summary>
        private void UpdateUI()
        {
            ProcessManager pm = new ProcessManager(); // Process Manager for long operations
            pm.BeginWaiting(); // Starts Loading Flow
            docTable.ItemsSource = SDM.LMS.GetDocsTable();
            pm.EndWaiting();
        }

        /// <summary> Trying to check out selected doc </summary>
        private void OnCheckOut(object sender, RoutedEventArgs e)
        {
            if (docTable.SelectedIndex == -1) return;

            MessageBoxResult result = MessageBox.Show(SDM.Strings.CHECK_OUT_CONFIRMATION_TEXT,
                SDM.Strings.ATTENTION_TEXT, MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    DocumentsTable lb_row = docTable.SelectedItems[0] as DocumentsTable;
                    int bookID = lb_row.docID;
                    Patron currentPatron = (Patron)SDM.CurrentUser;
                    MessageBox.Show(currentPatron.CheckOut(bookID));
                    UpdateUI();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }

}

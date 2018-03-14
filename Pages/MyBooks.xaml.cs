using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MyBooks.xaml
    /// </summary>
    public partial class MyBooks : Page
    {

        public MyBooks()
        {
            InitializeComponent();
            UpdateUI();
        }

        /// <summary> Updates table of user`s docs </summary>
        private void UpdateUI()
        {        
            myBooksTable.ItemsSource = SDM.LMS.GetUserBooks();
        }

        /// <summary> Trying to return document </summary>
        private void OnReturn(object sender, RoutedEventArgs e)
        {
            if (myBooksTable.SelectedIndex == -1) return;

            MessageBoxResult result = MessageBox.Show(SDM.Strings.RETURN_CONFIRMATION_TEXT,
                SDM.Strings.ATTENTION_TEXT, MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    MyBooksTable mb_row = myBooksTable.SelectedItems[0] as MyBooksTable;
                    int bookID = mb_row.bookID;
                    Patron currentPatron = (Patron)SDM.CurrentUser;
                    MessageBox.Show(currentPatron.ReturnDoc(bookID));
                    UpdateUI();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void OnRenew(object sender, RoutedEventArgs e)
        {
            if (myBooksTable.SelectedIndex == -1) return;

            MessageBoxResult result = MessageBox.Show(SDM.Strings.RETURN_CONFIRMATION_TEXT,
                SDM.Strings.ATTENTION_TEXT, MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    MyBooksTable mb_row = myBooksTable.SelectedItems[0] as MyBooksTable;
                    int bookID = mb_row.bookID;
                    var currentPatron = (Patron)SDM.CurrentUser;
                    MessageBox.Show(currentPatron.RenewDoc(bookID));
                    UpdateUI();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }

    class MyBooksTable
    {
        public int checkID { get; set; }
        public int bookID { get; set; }
        public string b_title { get; set; }
        public DateTime c_dateTaked { get; set; }
        public DateTime c_timeToBack { get; set; }
    }
}

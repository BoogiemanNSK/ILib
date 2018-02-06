using I2P_Project.Classes.Data_Managers;
using I2P_Project.Classes.UserSystem;
using I2P_Project.DataBases;
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
    public partial class MyBooks : Window
    {
        ObservableCollection<MyBooksTable> bk_data;

        public MyBooks()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI()
        {
            //while (DocList.Items.Count > 0) DocList.Items.RemoveAt(0);
            //Patron currentPatron = (Patron)SystemDataManager.CurrentUser;
            //foreach (int docID in currentPatron.CheckedDocs)
            //{
            //    documents doc = DataBaseManager.GetDoc(docID);
            //    string line = doc.Id + "| " + doc.Title;
            //    DocList.Items.Add(line);
            //}            
            myBooksTable.ItemsSource = DataBaseManager.GetUserBooks();
        }

        private void OnReturn(object sender, RoutedEventArgs e)
        {
            //if (DocList.SelectedItem == null) InfoText.Content = "Select a document you would like to return";
            //else
            //{
            //    Patron currentPatron = (Patron)SystemDataManager.CurrentUser;
            //    string s, item = (string)DocList.SelectedItem;
            //    s = item.Substring(0, item.IndexOf('|'));
            //    int docID = Convert.ToInt32(s);
            //    InfoText.Content = currentPatron.ReturnDoc(docID);
            //    UpdateUI();
            //}
            if (myBooksTable.SelectedIndex == -1) return;
            MessageBoxResult result = MessageBox.Show("Are you sure you want return this book?", "Attention", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    //действия по удалению команды из чемпионата
                    MyBooksTable mb_row = myBooksTable.SelectedItems[0] as MyBooksTable;
                    int book_id = mb_row.bookID;
                    
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void OnBack(object sender, RoutedEventArgs e)
        {
            UserHomePage HomePage = new UserHomePage();
            HomePage.Show();
            Close();
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

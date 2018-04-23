using I2P_Project.Classes;
using System;
using System.Windows;

namespace I2P_Project.Pages
{
    /// <summary> Interaction logic for OverdueInfo.xaml </summary>
    public partial class OverdueInfo : Window
    {
        public OverdueInfo(int userID)
        {
            InitializeComponent();
            OverduedDocsTable.ItemsSource = SDM.LMS.OverdueInfo(userID);
            TitleInfo.Content = SDM.LMS.GetUser(userID).Name + "`s overdued docs";
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    class OverdueInfoTable
    {
        public int docID { get; set; }
        public string docTitle { get; set; }
        public string docType { get; set; }
        public DateTime dateTaked { get; set; }
        public DateTime timeToBack { get; set; }
        public int fine { get; set; }
    }
}

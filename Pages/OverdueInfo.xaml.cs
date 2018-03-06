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
using System.Windows.Shapes;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Interaction logic for OverdueInfo.xaml
    /// </summary>
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
            UsersManagementPage page = new UsersManagementPage();
            page.Show();
            Close();
        }
    }

    class OverdueInfoTable
    {
        public int docID { get; set; }
        public string docTitle { get; set; }
        public bool isReference { get; set; }
        public string docType { get; set; }
        public DateTime dateTaked { get; set; }
        public DateTime timeToBack { get; set; }
        public int fine { get; set; }
    }
}

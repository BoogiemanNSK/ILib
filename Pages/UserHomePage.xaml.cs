using I2P_Project.Classes.Data_Managers;
using I2P_Project.DataBases;
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
    /// Interaction logic for UserHomePage.xaml
    /// </summary>
    public partial class UserHomePage : Window
    {
        public UserHomePage()
        {
            InitializeComponent();
            WelcomeText.Content = "Welcome, " + SystemDataManager.CurrentUser.Name + "!";
            foreach (document doc in DataBaseManager.GetAllDocs()) {
                string line = doc.Title;
                DocList.Items.Add(line);
            }
        }

        private void OnCheckOut(object sender, RoutedEventArgs e) {
            if (DocList.SelectedItem == null) InfoText.Content = "Select a document you would like to check out";
            else
            {
                InfoText.Content = "Checked out " + DocList.SelectedItem;
            }
        }

        private void OnReturn(object sender, RoutedEventArgs e)
        {

        }
    }
}

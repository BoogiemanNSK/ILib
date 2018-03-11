using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
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
            while (DocList.Items.Count > 0) DocList.Items.RemoveAt(0);
            foreach (DataBase.Document doc in SDM.LMS.GetAllDocs())
            {
                if (!doc.IsReference) {
                    string line = doc.Id + "| " + doc.Title;
                    DocList.Items.Add(line);
                }
            }
        }

        /// <summary> Trying to check out selected doc </summary>
        private void OnCheckOut(object sender, RoutedEventArgs e)
        {
            if (DocList.SelectedItem == null) MessageBox.Show(SDM.Strings.SELECT_CHECK_OUT);
            else
            {
                Patron currentPatron = (Patron)SDM.CurrentUser;
                string s, item = (string)DocList.SelectedItem;
                s = item.Substring(0, item.IndexOf('|'));
                int docID = Convert.ToInt32(s);
                MessageBox.Show(currentPatron.CheckOut(docID));
                UpdateUI();
            }
        }
    }
}

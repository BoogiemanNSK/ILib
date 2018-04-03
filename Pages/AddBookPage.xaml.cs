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
using I2P_Project.Classes.UserSystem;
using I2P_Project.Classes;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Window
    {
        DocumentsManagementPage previousPage;
        public AddBookPage(DocumentsManagementPage page)
        {
            previousPage = page;
            InitializeComponent();
            cmb_DocType.ItemsSource = SDM.Strings.DOC_TYPES;
        }

        private void OnAddBookClick(object sender, RoutedEventArgs e)
        {
            Librarian currentUser = (Librarian)SDM.CurrentUser;
            int dt = cmb_DocType.SelectedIndex;
            int price = Convert.ToInt32(PriceTB.Text);
            int n = Convert.ToInt32(CopiesTB.Text);
            bool ib = IsBestseller.SelectedIndex == 0;

            // TODO Change it, of course
            currentUser.AddAV
                (
                    TitleTB.Text,
                    DescriptionTB.Text,
                    price,
                    n
                );

            previousPage.updateTable();
            Close();
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

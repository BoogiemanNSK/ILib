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
        public AddBookPage()
        {
            InitializeComponent();
        }

        private void OnAddBookClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Librarian currentUser = (Librarian)SDM.CurrentUser;
                string docType = DocTypeTB.Text.ToLower();
                int dt = 0;
                if (docType.Equals("book"))
                {
                    dt = 0;
                }
                else if (docType.Equals("journal"))
                {
                    dt = 1;
                }
                else
                {
                    dt = 2; //AV
                }
                int price = Convert.ToInt32(PriceTB.Text);
                string isBestseller = IsBestsellerTB.Text.ToLower();
                bool ib = false;
                if (isBestseller.Equals("yes"))
                {
                    ib = true;
                }
                int n = Convert.ToInt32(CopiesTB.Text);
                for (int i = 0; i < n; i++)
                {
                    currentUser.AddDoc
                        (
                            TitleTB.Text,
                            DescriptionTB.Text,
                            dt,
                            price,
                            ib
                        );
                }
            } catch
            {
                MessageBox.Show("The row is empty", "Error");
            }
            Close();
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

using System;
using System.Windows;
using I2P_Project.Classes.UserSystem;
using I2P_Project.Classes;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Window
    {
        private DocumentsManagementPage _previousPage;

        public AddBookPage(DocumentsManagementPage page)
        {
            _previousPage = page;
            InitializeComponent();
        }

        private void OnAddBookClick(object sender, RoutedEventArgs e)
        {
            Librarian currentUser = (Librarian)SDM.CurrentUser;
            int price = Convert.ToInt32(PriceTB.Text);
            int quantity = Convert.ToInt32(CopiesTB.Text);
            bool isBestseller = IsBestseller.SelectedIndex == 0;
            
            currentUser.AddBook
                (
                    TitleTB.Text,
                    AutorsTB.Text,
                    PublisherTB.Text,
                    Convert.ToInt32(PublishYearTB.Text),
                    EditionTB.Text,
                    DescriptionTB.Text,
                    price,
                    isBestseller,
                    quantity
                );

            _previousPage.UpdateTable();
            Close();
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

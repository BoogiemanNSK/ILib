using System;
using System.Windows;
using I2P_Project.Classes.UserSystem;
using I2P_Project.Classes;

namespace I2P_Project.Pages
{
    /// <summary> Логика взаимодействия для AddBookPage.xaml </summary>
    public partial class AddAVPage : Window
    {
        private DocumentsManagementPage _previousPage;
        public AddAVPage(DocumentsManagementPage page)
        {
            _previousPage = page;
            InitializeComponent();
        }

        private void OnAddBookClick(object sender, RoutedEventArgs e)
        {
            Librarian currentUser = (Librarian)SDM.CurrentUser;
            int price = Convert.ToInt32(PriceTB.Text);
            int quantity = Convert.ToInt32(CopiesTB.Text);
            
            currentUser.AddAV
                (
                    TitleTB.Text,
                    AutorsTB.Text,
                    price,
                    quantity,
                    TagsTB.Text
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

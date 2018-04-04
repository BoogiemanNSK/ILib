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
    public partial class AddJournalPage : Window
    {
        private DocumentsManagementPage _previousPage;

        public AddJournalPage(DocumentsManagementPage page)
        {
            _previousPage = page;
            InitializeComponent();
        }

        private void OnAddBookClick(object sender, RoutedEventArgs e)
        {
            Librarian currentUser = (Librarian)SDM.CurrentUser;
            int price = Convert.ToInt32(PriceTB.Text);
            int quantity = Convert.ToInt32(CopiesTB.Text);
            
            currentUser.AddJournal
                (
                    TitleTB.Text,
                    AutorsTB.Text,
                    PublishedInTB.Text,
                    IssueTitleTB.Text,
                    IssueEditorTB.Text,
                    price,
                    quantity
                );

            _previousPage.updateTable();
            Close();
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

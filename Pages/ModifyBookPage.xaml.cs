using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using I2P_Project.DataBase;
using System;
using System.Windows;

namespace I2P_Project.Pages
{
    /// <summary> Interaction logic for ModifyBookPage.xaml </summary>
    public partial class ModifyBookPage : Window
    {
        private int _docID;
        DocumentsManagementPage prevPage;

        public ModifyBookPage(int docID, DocumentsManagementPage page)
        {
            _docID = docID;
            InitializeComponent();

            Document doc = SDM.LMS.GetDoc(docID);
            TitleTB.AppendText(doc.Title);
            AutorsTB.AppendText(doc.Autors);
            PublisherTB.AppendText(doc.Publisher);
            PublishYearTB.AppendText(doc.PublishYear.ToString());
            EditionTB.AppendText(doc.Edition);
            DescriptionTB.AppendText(doc.Description);
            PriceTB.AppendText(doc.Price.ToString());
            IsBestsellerCB.SelectedIndex = doc.IsBestseller ? 0 : 1;
            CopiesTB.AppendText(doc.Quantity.ToString());

            prevPage = page;
        }

        private void OnModifyBookClick(object sender, RoutedEventArgs e)
        {
            try {
                Librarian lib = (Librarian)SDM.CurrentUser;
                int price = Convert.ToInt32(PriceTB.Text);
                int quantity = Convert.ToInt32(CopiesTB.Text);
                bool isBestseller = IsBestsellerCB.SelectedIndex == 0;
                SDM.LMS.ModifyBook
                    (
                    _docID,
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
            } catch {
                MessageBox.Show("The row is empty", "Error");
            }
            prevPage.updateTable();
            Close();
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

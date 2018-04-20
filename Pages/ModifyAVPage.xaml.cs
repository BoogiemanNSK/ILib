using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using I2P_Project.DataBase;
using System;
using System.Windows;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Interaction logic for ModifyAVPage.xaml
    /// </summary>
    public partial class ModifyAVPage : Window
    {

        private int _docID;
        DocumentsManagementPage prevPage;

        public ModifyAVPage(int docID, DocumentsManagementPage page)
        {
            _docID = docID;
            InitializeComponent();

            Document doc = SDM.LMS.GetDoc(docID);
            TitleTB.AppendText(doc.Title);
            AutorsTB.AppendText(doc.Autors);
            PriceTB.AppendText(doc.Price.ToString());
            CopiesTB.AppendText(doc.Quantity.ToString());

            prevPage = page;
        }

        private void OnModifyAVClick(object sender, RoutedEventArgs e)
        {
            try {
                Librarian lib = (Librarian)SDM.CurrentUser;
                int price = Convert.ToInt32(PriceTB.Text);
                int quantity = Convert.ToInt32(CopiesTB.Text);
                SDM.LMS.ModifyAV
                    (
                    _docID,
                    TitleTB.Text,
                    AutorsTB.Text,
                    price,
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

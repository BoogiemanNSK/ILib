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
using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using I2P_Project.DataBase;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для ModifyBooksPage.xaml
    /// </summary>
    public partial class ModifyBooksPage : Window
    {
        private int _docID;
        DocumentsManagementPage prevPage;

        public ModifyBooksPage(int docID, DocumentsManagementPage page)
        {
            _docID = docID;
            InitializeComponent();
            DocType.ItemsSource = SDM.Strings.DOC_TYPES;

            Document doc = SDM.LMS.GetDocByID(docID);
            TitleTB.AppendText(doc.Title);
            DescriptionTB.AppendText(doc.Description);
            PriceTB.AppendText(doc.Price.ToString());
            IsBestseller.IsChecked = doc.IsBestseller;
            DocType.SelectedIndex = doc.DocType;

            prevPage = page;
        }

        private void OnModifyDocClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Librarian lib = (Librarian)SDM.CurrentUser;
                lib.ModifyDoc
                    (
                        _docID,
                        TitleTB.ToString().Substring(TitleTB.ToString().IndexOf(":") + 2),
                        DescriptionTB.ToString().Substring(DescriptionTB.ToString().IndexOf(":") + 2),
                        PriceTB.ToString().Substring(PriceTB.ToString().IndexOf(":") + 2),
                        (bool)IsBestseller.IsChecked,
                        DocType.SelectedIndex
                    );
            }
            catch
            {
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

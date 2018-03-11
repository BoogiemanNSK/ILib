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

namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для ModifyBooksPage.xaml
    /// </summary>
    public partial class ModifyBooksPage : Window
    {
        int doc_id;
        DocumentsManagementPage prevPage;
        public ModifyBooksPage(int docID, DocumentsManagementPage page)
        {
            doc_id = docID;
            InitializeComponent();
            Document doc = SDM.LMS.GetDocByID(docID);
            TitleTB.AppendText(doc.docTitle);
            DescriptionTB.AppendText(doc.descriptiion);
            PriceTB.AppendText("500");
            IsBestsellerTB.AppendText(doc.isBestseller ? "yes" : "no");
            DocTypeTB.AppendText(doc.docType);
            prevPage = page;
        }

        private void OnModifyDocClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Librarian lib = (Librarian)SDM.CurrentUser;
                lib.ModifyDoc
                    (
                        doc_id,
                        TitleTB.ToString().Substring(TitleTB.ToString().IndexOf(":") + 2),
                        DescriptionTB.ToString().Substring(DescriptionTB.ToString().IndexOf(":") + 2),
                        PriceTB.ToString().Substring(PriceTB.ToString().IndexOf(":") + 2),
                        IsBestsellerTB.ToString().Substring(IsBestsellerTB.ToString().IndexOf(":") + 2),
                        DocTypeTB.ToString().Substring(DocTypeTB.ToString().IndexOf(":") + 2)
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

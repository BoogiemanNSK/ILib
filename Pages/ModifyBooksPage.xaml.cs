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
namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для ModifyBooksPage.xaml
    /// </summary>
    public partial class ModifyBooksPage : Window
    {
        public ModifyBooksPage(int docID)
        {
            InitializeComponent();
            Document doc = SDM.LMS.GetDoc(docID);
            TitleTB.AppendText(doc.docTitle);
            DescriptionTB.AppendText(doc.descriptiion);
            PriceTB.AppendText("500");
            IsBestsellerTB.AppendText(doc.isBestseller ? "yes" : "no");
            DocTypeTB.AppendText(doc.docType);
        }

        private void OnModifyBookClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            DocumentsManagementPage page = new DocumentsManagementPage();
            page.Show();
            Close();
        }
    }
}

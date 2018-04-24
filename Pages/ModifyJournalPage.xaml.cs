using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using I2P_Project.DataBase;
using System;
using System.Windows;

namespace I2P_Project.Pages
{
    /// <summary> Interaction logic for ModifyJournalPage.xaml </summary>
    public partial class ModifyJournalPage : Window
    {
        private int _docID;
        DocumentsManagementPage prevPage;

        public ModifyJournalPage(int docID, DocumentsManagementPage page)
        {
            _docID = docID;
            InitializeComponent();

            Document doc = SDM.LMS.GetDoc(docID);
            TitleTB.AppendText(doc.Title);
            AutorsTB.AppendText(doc.Autors);
            PublishedInTB.AppendText(doc.PublishedIn);
            IssueTitleTB.AppendText(doc.IssueTitle);
            IssueEditorTB.AppendText(doc.IssueEditor);
            PriceTB.AppendText(doc.Price.ToString());
            CopiesTB.AppendText(doc.Quantity.ToString());
            TagsTB.AppendText(doc.Tags);

            prevPage = page;
        }

        private void OnModifyJournalClick(object sender, RoutedEventArgs e)
        {
            try {
                Librarian lib = (Librarian)SDM.CurrentUser;
                int price = Convert.ToInt32(PriceTB.Text);
                int quantity = Convert.ToInt32(CopiesTB.Text);
                lib.ModifyJournal
                    (
                    _docID,
                    TitleTB.Text,
                    AutorsTB.Text,
                    PublishedInTB.Text,
                    IssueTitleTB.Text,
                    IssueEditorTB.Text,
                    price,
                    quantity,
                    TagsTB.Text
                    );
            } catch {
                MessageBox.Show("The row is empty", "Error");
            }
            prevPage.UpdateTable();
            Close();
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

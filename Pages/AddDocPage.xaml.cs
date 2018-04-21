using System.Windows;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddBookPage.xaml
    /// </summary>
    public partial class AddDocPage : Window
    {
        private DocumentsManagementPage _previousPage;
        private bool _add;
        private int _docID;

        public AddDocPage(DocumentsManagementPage page, bool add, int docID)
        {
            _add = add;
            _docID = docID;
            _previousPage = page;
            InitializeComponent();
        }

        private void BookClick(object sender, RoutedEventArgs e)
        {
            if (_add) {
                AddBookPage page = new AddBookPage(_previousPage);
                page.ShowDialog();
            } else {
                ModifyBookPage page = new ModifyBookPage(_docID, _previousPage);
                page.ShowDialog();
            }
            Close();
        }

        private void JournalClick(object sender, RoutedEventArgs e)
        {
            if (_add) {
                AddJournalPage page = new AddJournalPage(_previousPage);
                page.ShowDialog();
            }
            else {
                ModifyJournalPage page = new ModifyJournalPage(_docID, _previousPage);
                page.ShowDialog();
            }
            Close();
        }

        private void AVClick(object sender, RoutedEventArgs e)
        {
            if (_add) {
                AddAVPage page = new AddAVPage(_previousPage);
                page.ShowDialog();
            }
            else {
                ModifyAVPage page = new ModifyAVPage(_docID, _previousPage);
                page.ShowDialog();
            }
            Close();
        }
    }
}

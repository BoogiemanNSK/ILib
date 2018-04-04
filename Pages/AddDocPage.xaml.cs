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
    public partial class AddDocPage : Window
    {
        private DocumentsManagementPage _previousPage;

        public AddDocPage(DocumentsManagementPage page)
        {
            _previousPage = page;
            InitializeComponent();
        }

        private void BookClick(object sender, RoutedEventArgs e)
        {
            AddBookPage page = new AddBookPage(_previousPage);
            page.ShowDialog();
            Close();
        }

        private void JournalClick(object sender, RoutedEventArgs e)
        {
            AddJournalPage page = new AddJournalPage(_previousPage);
            page.ShowDialog();
            Close();
        }

        private void AVClick(object sender, RoutedEventArgs e)
        {
            AddAVPage page = new AddAVPage(_previousPage);
            page.ShowDialog();
            Close();
        }
    }
}

using I2P_Project.Classes;
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

namespace I2P_Project.Pages
{
    /// <summary>
    /// Interaction logic for CheckAdminPassword.xaml
    /// </summary>
    public partial class CheckAdminPassword : Window
    {
        private Window _prevPage;

        public CheckAdminPassword(Window prevPage)
        {
            InitializeComponent();
            _prevPage = prevPage;
        }

        private void OnEnterClick(object sender, RoutedEventArgs e)
        {
            if (CheckCB.Text == SDM.Strings.ADMIN_PASS) {
                MainWindow mw = new MainWindow();
                mw.Show();
                _prevPage.Close();
                Close();
            } else {
                MessageBox.Show(SDM.Strings.WRONG_PASSWORD_TEXT);
            }
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

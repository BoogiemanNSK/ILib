using I2P_Project.Classes;
using System;
using System.Windows.Controls;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageHome.xaml
    /// </summary>
    public partial class PageHome : Page
    {
        public PageHome()
        {
            InitializeComponent();
            string hi_str = String.Format("Hi, {0}!", SDM.CurrentUser.Name);
            lbl_hi.Content = hi_str;
        }
    }
}

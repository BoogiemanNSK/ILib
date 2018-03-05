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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace I2P_Project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool taskMenu;  // task menu is open or closed?

        public MainWindow()
        {
            InitializeComponent();
            CloseTaskMenu();
            taskMenu = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Menu_Click(object sender, RoutedEventArgs e)  // Open/close task menu
        {
            if(taskMenu)
            {
                CloseTaskMenu();
            }
            else
            {
                OpenTaskMenu();
            }
            taskMenu = !taskMenu;
        }

        // UI Methods
        private void OpenTaskMenu()
        {
            DoubleAnimation anim_menu = new DoubleAnimation();
            anim_menu.From = 50;
            anim_menu.To = 180;
            anim_menu.Duration = TimeSpan.FromSeconds(0.2);
            lst_Menu.BeginAnimation(ListView.WidthProperty, anim_menu);
        }

        private void CloseTaskMenu()
        {
            DoubleAnimation anim_menu = new DoubleAnimation();
            anim_menu.From = 180;
            anim_menu.To = 50;
            anim_menu.Duration = TimeSpan.FromSeconds(0.2);
            lst_Menu.BeginAnimation(ListView.WidthProperty, anim_menu);
        }
    }
}

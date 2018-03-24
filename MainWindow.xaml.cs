using I2P_Project.Classes;
using I2P_Project.Pages;
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
            OnLoadWindow();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            LogInPage sign_in_page =  new LogInPage();
            sign_in_page.Show();
            Close();
            //page_Viewer.NavigationService.GoBack(); // Return to previous page
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
        private void OpenTaskMenu()  // Menu animations
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

        private void OnLoadWindow()
        {
            switch (Classes.SDM.CurrentUser.UserType)
            {
                case 0:  // Student          
                    li_page_LibrarianHome.Visibility = Visibility.Collapsed;
                    li_page_DocumentsManagement.Visibility = Visibility.Collapsed;
                    li_page_UsersManagement.Visibility = Visibility.Collapsed;
                    break;
                case 1:  // Faculty             
                    li_page_LibrarianHome.Visibility = Visibility.Collapsed;
                    li_page_DocumentsManagement.Visibility = Visibility.Collapsed;
                    li_page_UsersManagement.Visibility = Visibility.Collapsed;
                    break;
                case 2:  // Librarian
                    li_page_userHome.Visibility = Visibility.Collapsed;
                    li_page_UserLibrary.Visibility = Visibility.Collapsed;
                    li_page_UserMyBooks.Visibility = Visibility.Collapsed;
                    break;
            }

            CloseTaskMenu();
            taskMenu = false;
            ChangePage("PageHome.xaml");
        }

        private void lst_Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)  // Clear selected index
        {
            lst_Menu.SelectedIndex = -1;
        }

        private void page_userHome_Click(object sender, RoutedEventArgs e) // Faculty open Library Window
        {
            //CloseTaskMenu();
            ChangePage("PageHome.xaml");
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)  // Faculty open Library page
        {
            //CloseTaskMenu();
            ChangePage("UserHomePage.xaml");
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e) // Faculty open my books page
        {
            ChangePage("MyBooks.xaml");
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)  // Librarian open Home page
        {
            ChangePage("PageHome.xaml");
        }

        private void RadioButton_Click_2(object sender, RoutedEventArgs e)  // Librarian open Users Management page
        {
            ChangePage("UsersManagementPage.xaml");
        }

        private void RadioButton_Click_3(object sender, RoutedEventArgs e)
        {
            ChangePage("DocumentsManagementPage.xaml");
        }

        private void ChangePage(string page_name)  // Changes page on page viewer
        {
            string link = "/I2P-Project;component/Pages/" + page_name;
            page_Viewer.Source = new Uri(link, UriKind.Relative);
        }
    }
}

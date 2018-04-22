using I2P_Project.Classes;
using I2P_Project.Pages;
using System;
using System.Windows;
using System.Windows.Media.Animation;

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
            lst_Menu.BeginAnimation(WidthProperty, anim_menu);
        }

        private void CloseTaskMenu()
        {
            DoubleAnimation anim_menu = new DoubleAnimation();
            anim_menu.From = 180;
            anim_menu.To = 50;
            anim_menu.Duration = TimeSpan.FromSeconds(0.2);
            lst_Menu.BeginAnimation(WidthProperty, anim_menu);
        }

        private void OnLoadWindow()
        {
            switch (SDM.CurrentUser.UserType)
            {
                case 0: // Student
                case 1: // Instructor
                case 2: // TA
                case 3: // Visiting proffesor
                case 4: // Proffesor
                    li_page_LibrarianHome.Visibility = Visibility.Collapsed;
                    li_page_DocumentsManagement.Visibility = Visibility.Collapsed;
                    li_page_UsersManagement.Visibility = Visibility.Collapsed;
                    li_page_LibrariansManagement.Visibility = Visibility.Collapsed;
                    li_page_AdminHome.Visibility = Visibility.Collapsed;
                    li_page_userHome.IsChecked = true;
                    break;
                case 5:  // Librarian
                    li_page_userHome.Visibility = Visibility.Collapsed;
                    li_page_UserLibrary.Visibility = Visibility.Collapsed;
                    li_page_UserMyBooks.Visibility = Visibility.Collapsed;
                    li_page_LibrariansManagement.Visibility = Visibility.Collapsed;
                    li_page_AdminHome.Visibility = Visibility.Collapsed;
                    li_page_LibrarianHome.IsChecked = true;
                    break;
                case 6: // Admin
                    li_page_LibrarianHome.Visibility = Visibility.Collapsed;
                    li_page_DocumentsManagement.Visibility = Visibility.Collapsed;
                    li_page_UsersManagement.Visibility = Visibility.Collapsed;
                    li_page_userHome.Visibility = Visibility.Collapsed;
                    li_page_UserLibrary.Visibility = Visibility.Collapsed;
                    li_page_UserMyBooks.Visibility = Visibility.Collapsed;
                    li_page_AdminHome.IsChecked = true;
                    break;
            }

            CloseTaskMenu();
            taskMenu = false;
            ChangePage("PageHome.xaml");
        }

        private void Page_userHome_Click(object sender, RoutedEventArgs e) // Faculty open Library Window
        {
            CloseMenuAfterClick();
            ChangePage("PageHome.xaml");
        }

        private void Page_userLibrary_Click(object sender, RoutedEventArgs e)  // Faculty open Library page
        {
            CloseMenuAfterClick();
            ChangePage("UserHomePage.xaml");
        }

        private void Page_userMyBooks_Click(object sender, RoutedEventArgs e) // Faculty open my books page
        {
            CloseMenuAfterClick();
            ChangePage("MyBooks.xaml");
        }

        private void Page_librarianHome_Click(object sender, RoutedEventArgs e)  // Librarian open Home page
        {
            CloseMenuAfterClick();
            ChangePage("PageHome.xaml");
        }

        private void Page_librarianUsersManagement_Click(object sender, RoutedEventArgs e)  // Librarian open Users Management page
        {
            CloseMenuAfterClick();
            ChangePage("UsersManagementPage.xaml");
        }

        private void Page_librarianDocumentsManagement_Click(object sender, RoutedEventArgs e)  // Librarian open Documents Management page
        {
            CloseMenuAfterClick();
            ChangePage("DocumentsManagementPage.xaml");
        }

        private void Page_AdminHome_Click(object sender, RoutedEventArgs e)  // Admin open Home page
        {
            CloseMenuAfterClick();
            ChangePage("PageHome.xaml");
        }

        private void Page_LibrariansManagement_Click(object sender, RoutedEventArgs e)  // Admin open Librarians Management page
        {
            CloseMenuAfterClick();
            ChangePage("LibrariansManagementPage.xaml");
        }

        private void ChangePage(string page_name)  // Changes page on page viewer
        {
            string link = "/I2P-Project;component/Pages/" + page_name;
            page_Viewer.Source = new Uri(link, UriKind.Relative);
        }

        private void CloseMenuAfterClick()  // Closes task menu after click on menu button
        {
            if (taskMenu)
            {
                CloseTaskMenu();
                taskMenu = !taskMenu;
            }
        }

    }
}

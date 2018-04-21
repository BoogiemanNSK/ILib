using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using I2P_Project.DataBase;
using System;
using System.Linq;
using System.Windows;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Interaction logic for ModifyUserPage.xaml
    /// </summary>
    public partial class ModifyUserPage : Window
    {
        private int userID;

        public ModifyUserPage(int ID)
        {
            userID = ID;
            InitializeComponent();

            Users user = SDM.LMS.GetUser(ID);
            UserLogin.Content = user.Login;
            UserName.Text = user.Name;
            UserAdress.Text = user.Address;
            UserPhoneNumber.Text = user.PhoneNumber;

            if (user.UserType == (int) I2P_Project.Classes.UserSystem.UserType.Librarian) {
                Title = "Modify Librarian";
                UserTypeTitle.Content = "Librarian Type";
                ModifyButtonContent.Content = "Modify Librarian";
                UserType.ItemsSource = new string[] { "Priv1", "Priv2", "Priv3" };
                UserType.SelectedIndex = user.LibrarianType;
            } else {
                UserType.ItemsSource = SDM.Strings.USER_TYPES.Take(SDM.Strings.USER_TYPES.Length - 1);
                UserType.SelectedIndex = user.UserType;
            }
        }

        private void OnModifyUserClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserName.Text.Length == 0 || UserAdress.Text.Length == 0 || UserPhoneNumber.Text.Length == 0) throw new Exception();

                if (userID == 5) {
                    Admin admin = (Admin)SDM.CurrentUser;
                    admin.ModifyLibrarian
                        (
                            userID,
                            UserName.Text,
                            UserAdress.Text,
                            UserPhoneNumber.Text,
                            UserType.SelectedIndex
                        );
                    Close();
                } else {
                    Librarian lib = (Librarian)SDM.CurrentUser;
                    lib.ModifyUser
                        (
                            userID,
                            UserName.Text,
                            UserAdress.Text,
                            UserPhoneNumber.Text,
                            UserType.SelectedIndex
                        );
                    UserCard page = new UserCard(userID);
                    Close();
                    page.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("One of fields is not filled!", "Error");
            }
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            if (userID == 5) {
                Close();
            }
            else {
                UserCard page = new UserCard(userID);
                Close();
                page.ShowDialog();
            }
            
        }
    }
}

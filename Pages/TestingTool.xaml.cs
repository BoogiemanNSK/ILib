using I2P_Project.Classes;
using I2P_Project.Tests;
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
    /// Interaction logic for TestingTool.xaml
    /// </summary>
    public partial class TestingTool : Window
    {
        private Test test;

        public TestingTool()
        {
            InitializeComponent();
            UpdateTables();
            test = new Test();
        }

        private void UpdateTables()
        {
            DocumentsTable.ItemsSource = SDM.LMS.GetDocsTableForLibrarian();
            UserTable.ItemsSource = SDM.LMS.TestUsersTable();
        }

        private void OnTest(object sender, RoutedEventArgs e)
        {
            TestOutput.Text = "Test " + TestNumber.Text + ":\n";
            switch (TestNumber.Text)
            {
                case "1":
                    TestOutput.Text += test.test1(false);
                    UpdateTables();
                    break;
                case "2":
                    TestOutput.Text += test.test2(false);
                    UpdateTables();
                    break;
                case "3":
                    TestOutput.Text += test.test3(false);
                    UpdateTables();
                    break;
                case "4":
                    TestOutput.Text += test.test4(false);
                    UpdateTables();
                    break;
                case "5":
                    TestOutput.Text += test.test5(false);
                    UpdateTables();
                    break;
                case "6":
                    TestOutput.Text += test.test6(false);
                    UpdateTables();
                    break;
                case "7":
                    TestOutput.Text += test.test7(false);
                    UpdateTables();
                    break;
                case "8":
                    TestOutput.Text += test.test8(false);
                    UpdateTables();
                    break;
                case "9":
                    TestOutput.Text += test.test9(false);
                    UpdateTables();
                    break;
                case "10":
                    TestOutput.Text += test.test10(false);
                    UpdateTables();
                    break;
                case "11":
                    TestOutput.Text += test.test11(false);
                    UpdateTables();
                    break;
                case "12":
                    TestOutput.Text += test.test12(false);
                    UpdateTables();
                    break;
                case "13":
                    TestOutput.Text += test.test13(false);
                    UpdateTables();
                    break;
                case "14":
                    TestOutput.Text += test.test14(false);
                    UpdateTables();
                    break;
                case "15":
                    TestOutput.Text += test.test15(false);
                    UpdateTables();
                    break;
                case "16":
                    TestOutput.Text += test.test16(false);
                    UpdateTables();
                    break;
               case "17":
                    TestOutput.Text += test.test17(false);
                    UpdateTables();
                    break;
                case "18":
                    TestOutput.Text += test.test18(false);
                    UpdateTables();
                    break;
                case "19":
                    TestOutput.Text += test.test19(false);
                    UpdateTables();
                    break;
                default:
                    TestOutput.Text += "No such test found";
                    break;
            }
        }

        private void OnShow(object sender, RoutedEventArgs e)
        {
            UserTable ut_row = UserTable.SelectedItems[0] as UserTable;
            int user_id = ut_row.userID;
            DocumentsTable.ItemsSource = SDM.LMS.TestDocsTableUsersBooks(user_id);
        }

        private void OnOverall(object sender, RoutedEventArgs e) // Shows books without user
        {
            DocumentsTable.ItemsSource = SDM.LMS.GetDocsTableForLibrarian(); 
        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    class UserTable
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string userAddress { get; set; }
        public string userPhoneNumber { get; set; }
        public string userType { get; set; }
    }
}

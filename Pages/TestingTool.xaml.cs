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
            DocsTable.ItemsSource = SDM.LMS.TestDocsTableOnlyBooks();
            UserTable.ItemsSource = SDM.LMS.TestUsersTable();
        }

        private void OnTest(object sender, RoutedEventArgs e)
        {
            TestOutput.Text = "Test " + TestNumber.Text + ":\n";
            switch (TestNumber.Text)
            {
                case "1":
                    TestOutput.Text += test.test1();
                    UpdateTables();
                    break;
                case "2":
                    TestOutput.Text += test.test2();
                    UpdateTables();
                    break;
                case "3":
                    TestOutput.Text += test.test3();
                    UpdateTables();
                    break;
                case "4":
                    TestOutput.Text += test.test4();
                    UpdateTables();
                    break;
                case "5":
                    TestOutput.Text += test.test5();
                    UpdateTables();
                    break;
                case "6":
                    TestOutput.Text += test.test6();
                    UpdateTables();
                    break;
                case "7":
                    TestOutput.Text += test.test7();
                    UpdateTables();
                    break;
                case "8":
                    TestOutput.Text += test.test8();
                    UpdateTables();
                    break;
                case "9":
                    TestOutput.Text += test.test9();
                    UpdateTables();
                    break;
                case "10":
                    TestOutput.Text += test.test10();
                    UpdateTables();
                    break;
                case "11":
                    TestOutput.Text += test.test11();
                    UpdateTables();
                    break;
                case "12":
                    TestOutput.Text += test.test12();
                    UpdateTables();
                    break;
                case "13":
                    TestOutput.Text += test.test13();
                    UpdateTables();
                    break;
              /*  case "14":
                    TestOutput.Text += test.test14();
                    UpdateTables();
                    break;
                case "15":
                    TestOutput.Text += test.test15();
                    UpdateTables();
                    break;
                case "16":
                    TestOutput.Text += test.test16();
                    UpdateTables();
                    break;
                case "17":
                    TestOutput.Text += test.test17();
                    UpdateTables();
                    break;
                case "18":
                    TestOutput.Text += test.test18();
                    UpdateTables();
                    break;
                case "19":
                    TestOutput.Text += test.test19();
                    UpdateTables();
                    break;
                case "20":
                    TestOutput.Text += test.test20();
                    UpdateTables();
                    break;
                default:
                    TestOutput.Text += "No such test found";
                    break;*/
            }
        }

        private void OnShow(object sender, RoutedEventArgs e)
        {
            UserTable ut_row = UserTable.SelectedItems[0] as UserTable;
            int user_id = ut_row.userID;
            DocsTable.ItemsSource = SDM.LMS.TestDocsTableUsersBooks(user_id);
        }

        private void OnOverall(object sender, RoutedEventArgs e) // Shows books without user
        {
            DocsTable.ItemsSource = SDM.LMS.TestDocsTableOnlyBooks(); 
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

    class DocsTable
    {
        public int docID { get; set; }
        public string docTitle { get; set; }
        public string docType { get; set; }
        public int docOwnerID { get; set; }
        public DateTime dateTaked { get; set; }
        public DateTime timeToBack { get; set; }
        public bool isReference { get; set; }
    }
}

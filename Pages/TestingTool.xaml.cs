using I2P_Project.Classes;
using I2P_Project.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            DocumentsTable.ItemsSource = SDM.LMS.GetDocsTable();
            UserTable.ItemsSource = SDM.LMS.TestUsersTable();
        }

        private void OnTest(object sender, RoutedEventArgs e)
        {
            TestOutput.Text = "Test " + TestNumber.Text + ":\n";
            switch (TestNumber.Text)
            {
                case "Test all":
                    try
                    {
                        test.Test1();
                        test.Test2();
                        test.Test3();
                        test.Test4();
                        test.Test5();
                        test.Test6();
                        test.Test7();
                        test.Test8();
                        test.Test9();
                        test.Test10();
                        test.Test11();
                        test.Test12();
                        test.Test13();
                        test.Test14();
                        test.Test15();
                        test.Test16();
                        test.Test17();
                        test.Test18();
                        test.Test20();
                        test.Test21();
                        test.Test22();
                        test.Test23();
                        test.Test25();
                        test.Test26();
                        test.Test27();
                        test.Test28();
                        test.Test29();
                        test.Test30();
                        TestOutput.Text += "Tests are passed, restart system for test 19";
                    } catch
                    {
                        TestOutput.Text += "Tests not passed";
                    }
                    UpdateTables();
                    break;
                case "1":
                    test.Test1();
                    UpdateTables();         
                    break;                  
                case "2":                   
                    test.Test2();
                    UpdateTables();         
                    break;                  
                case "3":                   
                    test.Test3();
                    UpdateTables();         
                    break;                  
                case "4":                   
                    test.Test4();
                    UpdateTables();         
                    break;                  
                case "5":                   
                    test.Test5();
                    UpdateTables();         
                    break;                  
                case "6":                   
                    test.Test6();
                    UpdateTables();         
                    break;                  
                case "7":                   
                    test.Test7();
                    UpdateTables();         
                    break;                  
                case "8":                   
                    test.Test8();
                    UpdateTables();         
                    break;                  
                case "9":                   
                    test.Test9();
                    UpdateTables();         
                    break;                  
                case "10":                  
                    test.Test10();
                    UpdateTables();         
                    break;                  
                case "11":                  
                    test.Test11();
                    UpdateTables();         
                    break;                  
                case "12":                  
                    test.Test12();
                    UpdateTables();         
                    break;                  
                case "13":                  
                    test.Test13();
                    UpdateTables();         
                    break;                  
                case "14":                  
                    test.Test14();
                    UpdateTables();         
                    break;                  
                case "15":                  
                    test.Test15();
                    UpdateTables();         
                    break;                  
                case "16":                  
                    test.Test16();
                    UpdateTables();         
                    break;                  
               case "17":                   
                    test.Test17();
                    UpdateTables();         
                    break;                  
                case "18":                  
                    test.Test18();
                    UpdateTables();         
                    break;                  
                case "19":                  
                    test.Test19();
                    UpdateTables();         
                    break;                  
                case "20":                  
                    test.Test20();
                    UpdateTables();         
                    break;                  
                case "21":                  
                    test.Test21();
                    UpdateTables();         
                    break;                  
                case "22":                  
                    test.Test22();
                    UpdateTables();         
                    break;                  
                case "23":                  
                    test.Test23();
                    UpdateTables();         
                    break;                  
                case "25":                  
                    test.Test25();
                    UpdateTables();         
                    break;                  
                case "26":                  
                    test.Test26();
                    UpdateTables();         
                    break;                  
                case "27":                  
                    test.Test27();
                    UpdateTables();         
                    break;                  
                case "28":                  
                    test.Test28();
                    UpdateTables();         
                    break;                  
                case "29":                  
                    test.Test29();
                    UpdateTables();         
                    break;                  
                case "30":                  
                    test.Test30();
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
            DocumentsTable.ItemsSource = SDM.LMS.GetDocsTable(); 
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
        public string userAddress{ get; set; }
        public string userPhoneNumber { get; set; }
        public string userType { get; set; }
    }
}

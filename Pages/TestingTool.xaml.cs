using I2P_Project.Classes;
using I2P_Project.Tests;
using System.Windows;

namespace I2P_Project.Pages
{
    /// <summary> Interaction logic for TestingTool.xaml </summary>
    public partial class TestingTool : Window
    {
        private Test test;

        public TestingTool()
        {
            InitializeComponent();
            test = new Test();
            UpdateTables();
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
                        test.Test31();
                        test.Test32();
                        test.Test33();
                        test.Test34();
                        test.Test35();
                        test.Test36();
                        test.Test37();
                        test.Test38();
                        test.Test39();
                        test.Test40();
                        test.Test41();
                        test.Test42();
                        test.Test43();
                        test.Test44();
                        TestOutput.Text += "Tests are passed, restart system for test 19";
                    } catch
                    {
                        TestOutput.Text += "Tests are not passed";
                    }
                    UpdateTables();
                    break;
                case "Delivery 1":
                    try {
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
                        TestOutput.Text += "Tests are passed";
                    } catch {
                        TestOutput.Text += "Tests are not passed";
                    }
                    UpdateTables();
                    break;
                case "Delivery 2":
                    try { 
                        test.Test10();
                        test.Test11();
                        test.Test12();
                        test.Test13();
                        test.Test14();
                        test.Test15();
                        test.Test16();
                        test.Test17();
                        test.Test18();
                        TestOutput.Text += "Tests are passed, restart the system for 19th test";
                    } catch {
                        TestOutput.Text += "Tests are not passed";
                    }
                    UpdateTables();
                    break;
                case "Delivery 3":
                    try { 
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
                        TestOutput.Text += "Tests are passed";
                    } catch {
                        TestOutput.Text += "Tests are not passed";
                    }
                    UpdateTables();
                    break;
                case "Delivery 4":
                    try {
                        test.Test31();
                        test.Test32();
                        test.Test33();
                        test.Test34();
                        test.Test35();
                        test.Test36();
                        test.Test37();
                        test.Test38();
                        test.Test39();
                        test.Test40();
                        test.Test41();
                        test.Test42();
                        test.Test43();
                        test.Test44();
                        TestOutput.Text += "Tests are passed";
                    } catch {
                        TestOutput.Text += "Tests are not passed";
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
                case "31":
                    test.Test31();
                    UpdateTables();
                    break;
                case "32":
                    test.Test32();
                    UpdateTables();
                    break;
                case "33":
                    test.Test33();
                    UpdateTables();
                    break;
                case "34":
                    test.Test34();
                    UpdateTables();
                    break;
                case "35":
                    test.Test35();
                    UpdateTables();
                    break;
                case "36":
                    test.Test36();
                    UpdateTables();
                    break;
                case "37":
                    test.Test37();
                    UpdateTables();
                    break;
                case "38":
                    test.Test38();
                    UpdateTables();
                    break;
                case "39":
                    test.Test39();
                    UpdateTables();
                    break;
                case "40":
                    test.Test40();
                    UpdateTables();
                    break;
                case "41":
                    test.Test41();
                    UpdateTables();
                    break;
                case "42":
                    test.Test42();
                    UpdateTables();
                    break;
                case "43":
                    test.Test43();
                    UpdateTables();
                    break;
                case "44":
                    test.Test44();
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
            DocumentsTable.ItemsSource = SDM.LMS.GetUserBooks(user_id);
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

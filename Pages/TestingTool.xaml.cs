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
            test = new Test();

        }

        private void OnTest(object sender, RoutedEventArgs e)
        {
            TestOutput.Text = "Test " + TestNumber.Text + ":\n";
            switch (TestNumber.Text)
            {
                case "1":
                    TestOutput.Text += test.test1();
                    break;
                case "2":

                    break;
                case "3":

                    break;
                case "4":

                    break;
                case "5":

                    break;
                case "6":

                    break;
                case "7":

                    break;
                case "8":

                    break;
                case "9":

                    break;
                case "10":

                    break;
                default:
                    TestOutput.Text += "No such test found";
                    break;
            }
        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

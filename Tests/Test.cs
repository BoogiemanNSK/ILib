using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace I2P_Project.Tests
{
    class Test
    {

        public string test1()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering student st in the system...\n";
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Logging In as librarian lb...\n";
            SDM.CurrentUser = new Librarian("lb"); // Log In librarian lb
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Adding reference book b and two copies...\n";
            lb.AddDoc("b", "B", 0, 0, false); // Adding Reference book
            lb.AddDoc("b", "B", 0, 0, false);
            lb.AddDoc("b", "B", 0, 0, false);

            output += "Logging In as student st...\n";
            SDM.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SDM.CurrentUser;

            output += "Student st checking out book b...\n";
            st.CheckOut("b");

            output += "Test passed with no exceptions!\n";

            try
            {
                Debug.Assert(SDM.LMS.UserExists("lb"));
                Debug.Assert(SDM.LMS.UserExists("st"));
                Debug.Assert(SDM.LMS.DocExists("b"));
                Debug.Assert(SDM.LMS.AmountOfDocs("b", 3));
                Debug.Assert(SDM.LMS.GetUserBooks().Count == 1);

                output = "Test1 OK";
            }
            catch
            {
                output = "Test1 FAIL";
            }


            return output;
        }


        public string test2()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering student st in the system...\n";
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Logging In as student st...\n";
            SDM.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SDM.CurrentUser;

            output += "Student st checking out book by author A...\n";
            st.CheckOut("A");

            output += "Test passed with no exceptions!\n";

            try
            {
                Debug.Assert(SDM.LMS.UserExists("lb"));
                Debug.Assert(SDM.LMS.UserExists("st"));
                Debug.Assert(!SDM.LMS.DocExists("A"));
                Debug.Assert(SDM.LMS.GetUserBooks().Count == 0);

                output = "Test2 OK";
            }
            catch
            {
                output = "Test2 FAIL";
            }

            return output;
        }

        public string test3()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering student st in the system...\n";
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);

            output += "Registering faculty ft in the system...\n";
            SDM.LMS.RegisterUser("ft", "ft", "ft", "ft", "ft", false);

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Logging In as faculty ft...\n";
            SDM.CurrentUser = new Faculty("ft"); // Log In student st
            Faculty ft = (Faculty)SDM.CurrentUser;

            output += "Adding reference book b and copy...\n";
            SDM.LMS.AddDoc("b", "B", 0, 0, false); // Adding Reference book
            SDM.LMS.AddDoc("b", "B", 0, 0, false);

            output += "Faculty ft checking out book b...\n";
            ft.CheckOut("b");

            output += "Test passed with no exceptions!\n";

            try
            {
                Debug.Assert(SDM.LMS.UserExists("lb"));
                Debug.Assert(SDM.LMS.UserExists("ft"));
                Debug.Assert(SDM.LMS.DocExists("b"));
                Debug.Assert(SDM.LMS.AmountOfDocs("b", 2));
                Debug.Assert(SDM.LMS.GetUserBooks().Count == 1);
                Debug.Assert((SDM.LMS.GetUserBooks().FirstOrDefault().c_timeToBack.Subtract(SDM.LMS.GetUserBooks().FirstOrDefault().c_dateTaked).TotalDays / 7) == 4);

                output = "Test3 OK";

            }
            catch
            {
                output = "Test3 FAIL";
            }

            return output;
        }

        public string test4()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering student st in the system...\n";
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);

            output += "Registering student st in the system...\n";
            SDM.LMS.RegisterUser("ft", "ft", "ft", "ft", "ft", false);

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Logging In as student st...\n";
            SDM.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SDM.CurrentUser;

            output += "Adding reference book b and copy...\n";
            SDM.LMS.AddDoc("b", "B", 0, 0, true); // Adding Reference book
            SDM.LMS.AddDoc("b", "B", 0, 0, true);

            output += "Studebt st checking out book b...\n";
            st.CheckOut("b");

            output += "Test passed with no exceptions!\n";

            try
            {
                Debug.Assert(SDM.LMS.UserExists("lb"));
                Debug.Assert(SDM.LMS.UserExists("st"));
                Debug.Assert(SDM.LMS.DocExists("b"));
                Debug.Assert(SDM.LMS.AmountOfDocs("b", 2));
                Debug.Assert(SDM.LMS.GetUserBooks().Count == 1);
                Debug.Assert((SDM.LMS.GetUserBooks().FirstOrDefault().c_timeToBack.Subtract(SDM.LMS.GetUserBooks().FirstOrDefault().c_dateTaked).TotalDays / 7) == 2);

                output = "Test4 OK";

            }
            catch
            {
                output = "Test4 FAIL";
            }

            return output;
        }

        public string test5()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering student st in the system...\n";
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);

            output += "Registering student st1 in the system...\n";
            SDM.LMS.RegisterUser("st1", "st1", "st1", "st1", "st1", false);

            output += "Registering student st2 in the system...\n";
            SDM.LMS.RegisterUser("st2", "st2", "st2", "st2", "st2", false);

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Adding reference book A and two copies...\n";
            SDM.LMS.AddDoc("a", "a", 0, 0, false); // Adding Reference book
            SDM.LMS.AddDoc("a", "a", 0, 0, false);
            SDM.LMS.AddDoc("a", "a", 0, 0, false);

            output += "Logging In as student st...\n";
            SDM.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SDM.CurrentUser;

            output += "Student st checking out book A...\n";
            st.CheckOut("a");

            output += "Logging In as student st1...\n";
            SDM.CurrentUser = new Student("st1"); // Log In student st1
            Student st1 = (Student)SDM.CurrentUser;

            output += "Student st1 checking out book A...\n";
            st1.CheckOut("a");

            output += "Logging In as student st2...\n";
            SDM.CurrentUser = new Student("st2"); // Log In student st2
            Student st2 = (Student)SDM.CurrentUser;

            output += "Student st2 checking out book A...\n";
            st2.CheckOut("a");

            try
            {
                Debug.Assert(SDM.LMS.UserExists("lb"));
                Debug.Assert(SDM.LMS.UserExists("st"));
                Debug.Assert(SDM.LMS.UserExists("st1"));
                Debug.Assert(SDM.LMS.UserExists("st2"));
                Debug.Assert(SDM.LMS.DocExists("a"));
                Debug.Assert(SDM.LMS.AmountOfDocs("a", 3));

                Debug.Assert(SDM.LMS.GetUserBooks().Count == 0);

                SDM.CurrentUser = new Student("st1");
                //   st1 = (Student)SDM.CurrentUser;
                Debug.Assert(SDM.LMS.GetUserBooks().Count == 1);

                SDM.CurrentUser = new Student("st");
                st = (Student)SDM.CurrentUser;
                Debug.Assert(SDM.LMS.GetUserBooks().Count == 1);

                output = "Test5 OK";

            }
            catch
            {
                output = "Test5 FAIL";
            }

            output += "Test passed with no exceptions!\n";
            return output;
        }

        public string test6()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering student st in the system...\n";
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Logging In as student st...\n";
            SDM.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SDM.CurrentUser;

            output += "Adding reference book b and two copies...\n";
            SDM.LMS.AddDoc("b", "B", 0, 0, false); // Adding Reference book
            SDM.LMS.AddDoc("b", "B", 0, 0, false);
            SDM.LMS.AddDoc("b", "B", 0, 0, false);

            output += "Student st checking out book b...\n";
            st.CheckOut("b");

            output += "Student checking out the book b...\n";
            st.CheckOut("b");

            output += "Test passed with no exceptions!\n";
            try
            {
                Debug.Assert(SDM.LMS.UserExists("lb"));
                Debug.Assert(SDM.LMS.UserExists("st"));
                Debug.Assert(SDM.LMS.DocExists("b"));
                Debug.Assert(SDM.LMS.AmountOfDocs("b", 3));
                Debug.Assert(SDM.LMS.GetUserBooks().Count == 1);

                output = "Test6 OK";

            }
            catch
            {
                output = "Test6 FAIL";
            }

            return output;
        }

        public string test7()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering student 'p1' in the system...\n";
            SDM.LMS.RegisterUser("p1", "p1", "p1", "p1", "p1", false);

            output += "Registering student 'p2' in the system...\n";
            SDM.LMS.RegisterUser("p2", "p2", "p2", "p2", "p2", false);

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Adding reference book b1 and two copies...\n";
            SDM.LMS.AddDoc("b1", "B", 0, 0, false); // Adding Reference book
            SDM.LMS.AddDoc("b1", "B", 0, 0, false);
            SDM.LMS.AddDoc("b1", "B", 0, 0, false);

            output += "Logging In as student p1...\n";
            SDM.CurrentUser = new Student("p1"); // Log In student st
            Student p1 = (Student)SDM.CurrentUser;

            output += "Student p1 checking out book b1...\n";
            p1.CheckOut("b1");

            output += "Logging In as student p2...\n";
            SDM.CurrentUser = new Student("p2"); // Log In student st
            Student p2 = (Student)SDM.CurrentUser;

            output += "Student p2 checking out book b1...\n";
            p2.CheckOut("b1");

            output += "Test passed with no exceptions!\n";
            try
            {
                Debug.Assert(SDM.LMS.UserExists("lb"));
                Debug.Assert(SDM.LMS.UserExists("p1"));
                Debug.Assert(SDM.LMS.UserExists("p2"));
                Debug.Assert(SDM.LMS.DocExists("b1"));
                Debug.Assert(SDM.LMS.AmountOfDocs("b1", 3));
                Debug.Assert(SDM.LMS.GetUserBooks().Count == 1);

                SDM.CurrentUser = new Student("p1"); // Log In student st
                p1 = (Student)SDM.CurrentUser;

                Debug.Assert(SDM.LMS.GetUserBooks().Count == 1);

                output = "Test7 OK";

            }
            catch
            {
                output = "Test7 FAIL";
            }

            return output;
        }

        public string test8()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering faculty 'f' in the system...\n";
            SDM.LMS.RegisterUser("f", "f", "f", "f", "f", false);

            output += "Registering student 's' in the system...\n";
            SDM.LMS.RegisterUser("s", "s", "s", "s", "s", false);

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Adding reference book b and two copies...\n";
            SDM.LMS.AddDoc("b", "B", 0, 0, false); // Adding Reference book
            SDM.LMS.AddDoc("b", "B", 0, 0, false);

            SDM.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Logging In as student s...\n";
            SDM.CurrentUser = new Student("s"); // Log In student st
            Student s = (Student)SDM.CurrentUser;

            output += "Student s checking out book b...\n";
            s.CheckOut("b");

            output += "Test passed with no exceptions!\n";

            try
            {
                Debug.Assert(SDM.LMS.UserExists("lb"));
                Debug.Assert(SDM.LMS.UserExists("f"));
                Debug.Assert(SDM.LMS.UserExists("s"));
                Debug.Assert(SDM.LMS.DocExists("b"));
                Debug.Assert(SDM.LMS.AmountOfDocs("b", 2));
                Debug.Assert(SDM.LMS.GetUserBooks().Count == 1);
                Debug.Assert((SDM.LMS.GetUserBooks().FirstOrDefault().c_timeToBack.Subtract(SDM.LMS.GetUserBooks().FirstOrDefault().c_dateTaked).TotalDays / 7) == 3);

                output = "Test8 OK";

            }
            catch
            {
                output = "Test8 FAIL";
            }

            return output;
        }

        public string test9()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering faculty 'f' in the system...\n";
            SDM.LMS.RegisterUser("f", "f", "f", "f", "f", false);

            output += "Registering student 's' in the system...\n";
            SDM.LMS.RegisterUser("s", "s", "s", "s", "s", false);

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Adding reference book b and copy...\n";
            SDM.LMS.AddDoc("b", "B", 0, 0, true); // Adding Reference book
            SDM.LMS.AddDoc("b", "B", 0, 0, true);

            SDM.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Logging In as student s...\n";
            SDM.CurrentUser = new Student("s"); // Log In student st
            Student s = (Student)SDM.CurrentUser;

            output += "Student s checking out book b...\n";
            s.CheckOut("b");

            output += "Test passed with no exceptions!\n";

            try
            {
                Debug.Assert(SDM.LMS.UserExists("lb"));
                Debug.Assert(SDM.LMS.UserExists("f"));
                Debug.Assert(SDM.LMS.UserExists("s"));
                Debug.Assert(SDM.LMS.DocExists("b"));
                Debug.Assert(SDM.LMS.AmountOfDocs("b", 2));
                Debug.Assert(SDM.LMS.GetUserBooks().Count == 1);
                Debug.Assert((SDM.LMS.GetUserBooks().FirstOrDefault().c_timeToBack.Subtract(SDM.LMS.GetUserBooks().FirstOrDefault().c_dateTaked).TotalDays / 7) == 2);

                output = "Test9 OK";

            }
            catch
            {
                output = "Test9 FAIL";
            }

            return output;
        }

        public string test10()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering student st in the system...\n";
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Adding reference books a and b and copy of a...\n";
            SDM.LMS.AddDoc("a", "A", 0, 0, false); // Adding Reference book
            SDM.LMS.AddDoc("a", "A", 0, 0, false);
            SDM.LMS.AddDoc("b", "B", 0, 0, false);

            output += "Logging In as student st...\n";
            SDM.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SDM.CurrentUser;

            output += "Student st checking out book a abd b...\n";
            st.CheckOut("a");
            st.CheckOut("b");

            output += "Test passed with no exceptions!\n";
            try
            {
                Debug.Assert(SDM.LMS.UserExists("lb"));
                Debug.Assert(SDM.LMS.UserExists("st"));
                Debug.Assert(SDM.LMS.DocExists("b"));
                Debug.Assert(SDM.LMS.DocExists("a"));
                Debug.Assert(SDM.LMS.AmountOfDocs("a", 2));
                Debug.Assert(SDM.LMS.AmountOfDocs("b", 1));
                Debug.Assert(SDM.LMS.GetUserBooks().Count == 1);

            }
            catch
            {
                output = "Test10 FAIL";
            }

            return output;

        }
        public string test11()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Adding reference book Introduction to Algorithms and 3 copy of Introduction to Algorithms...\n";
            for (int i = 0; i < 4; i++)
            {
                SDM.LMS.AddBook
                    (
                        "Introduction to Algorithms",
                        "Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest and Clifford Stein",
                        "MIT Press",
                        2009,
                        "Third Edition",
                        "Alghorithm techniques and design",
                        0,
                        1800,
                        false
                    );
            }
            for (int i = 0; i < 3; i++)
            {
                SDM.LMS.AddBook
                   (
                       "Design Patterns: Elements of Reusable Object-Oriented Software",
                       "Erich Gamma, Ralph Johnson, John Vlissides, Richard Helm",
                       "Addison-Wesley Professional",
                       2003,
                       "First Edition",
                       "Programm patterns, how to programm well w/o headache",
                       0,
                       2000,
                       true
                   );
            }
            output += "Adding reference book Design Patterns: Elements of Reusable Object-Oriented Software and 2 copy of Introduction to Algorithms...\n";
            for (int i = 0; i < 2; i++)
            {
                SDM.LMS.AddAV("Null References: The Billion Dollar Mistake", "Tony Hoare", "Some AV", 400);
                SDM.LMS.AddAV("Information Entropy", "Claude Shannon", "Another AV", 700);
            }
            //reference book
            SDM.LMS.AddBook("The Mythical Man-month", "Brooks,Jr., Frederick P",
           "Addison-Wesley Longman Publishing Co., Inc.", 1995,
           "Second edition", "How to do everything and live better",
           0, 800, false);
            output += "Adding reference book The Mythical Man-month and copy of Introduction to Algorithms...\n";
            output += "Adding reference video and copy of Null References: The Billion Dollar Mistake...\n";
            output += "Adding reference video and copy of Information Entropy...\n";
            output += "Logging In as librarian lb...\n";
            SDM.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Registering patrons Sergey Afonso, Nadia Teixeira, Elvira Espindola...\n";
            lb.RegisterUser("Sergey Afonso", "Sergey Afonso", "Sergey Afonso", "Via Margutta, 3", "30001", false);
            lb.UpgradeUser("Sergey Afonso");
            lb.RegisterUser("Nadia Teixeira", "Nadia Teixeira", "Nadia Teixeira", "Via Sacra, 13", "30002", false);
            lb.RegisterUser("Elvira Espindola", "Elvira Espindola", "Elvira Espindola", "Via del Corso, 22", "30003", false);
            //Assertions for auto-tests
            try
            {
                Debug.Assert(SDM.LMS.DocExists("Introduction to Algorithms"));
                //included reference book
                Debug.Assert(SDM.LMS.AmountOfDocs("Introduction to Algorithms", 4));
                Debug.Assert(SDM.LMS.DocExists("Design Patterns: Elements of Reusable Object-Oriented Software"));
                Debug.Assert(SDM.LMS.AmountOfDocs("Design Patterns: Elements of Reusable Object-Oriented Software", 3));
                Debug.Assert(SDM.LMS.DocExists("The Mythical Man-month"));
                Debug.Assert(SDM.LMS.AmountOfDocs("The Mythical Man-month", 1));
                Debug.Assert(SDM.LMS.DocExists("Null References: The Billion Dollar Mistake"));
                Debug.Assert(SDM.LMS.AmountOfDocs("Null References: The Billion Dollar Mistake", 2));
                Debug.Assert(SDM.LMS.DocExists("Information Entropy"));
                Debug.Assert(SDM.LMS.AmountOfDocs("Information Entropy", 2));
                Debug.Assert(SDM.LMS.CheckLogin("Sergey Afonso"));
                Debug.Assert(SDM.LMS.CheckLogin("Nadia Teixeira"));
                Debug.Assert(SDM.LMS.CheckLogin("Elvira Espindola"));
            }
            catch
            {
                return "Test11 not passed";
            }
            return output;
        }

        public string test12()
        {
            string output = "";
            output += "Running TC11...\n";

            test11();

            output += "Logging In as librarian lb...\n";
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Obtaining ID of Nadia Teixeira patron...\n";
            int idp2 = SDM.LMS.GetPatronByName("Nadia Teixeira").userID;

            output += "Removing Introduction to Algorithms & The Mythical Man-month documents and Nadia Teixeira patron...\n";
            lb.DeleteDoc("Introduction to Algorithms");
            lb.DeleteDoc("The Mythical Man-month");
            lb.DeleteUser(idp2);
            //Assertions for auto-tests
            try
            {
                Debug.Assert(!SDM.LMS.CheckLogin("Nadia Teixeira"));
                Debug.Assert(SDM.LMS.AmountOfDocs("Introduction to Algorithms", 4 - 1));
                Debug.Assert(SDM.LMS.AmountOfDocs("The Mythical Man-month", 0));
            }
            catch
            {
                return "Test12 not passed";
            }
            return output;
        }

        public string test13()
        {
            string output = "Cleared DB...\n";

            output += "Running TC11...\n";

            test11();

            output += "Logging In as librarian lb...\n";
            Librarian lb = (Librarian)SDM.CurrentUser;

            lb.UpgradeUser("Sergey Afonso");
            output += "Creating new window with user card of Sergey Afonso...\n";
            // output += lb.ShowUserCard("Sergey Afonso") + "...\n";

            output += "Creating new window with user card of Elvira Espindola...\n";
            //output += lb.ShowUserCard("Elvira Espindola") + "...\n";
            //Assertions for auto-tests
            try
            {
                List<CheckedOut> CheckedOutInfo = new List<CheckedOut>();
                Debug.Assert(SDM.LMS.CheckUserInfo("Sergey Afonso", "Via Margutta, 3", "30001", 2, CheckedOutInfo));
                Debug.Assert(SDM.LMS.CheckUserInfo("Elvira Espindola", "Via del Corso, 22", "30003", 1, CheckedOutInfo));
            }
            catch
            {
                return "Test13 not passed";
            }
            return output;
        }

        public string test14()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Running TC12...\n";

            test12();

            output += "Logging In as librarian lb...\n";
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Creating new window with user card of Nadia Teixeira...\n";
            //output += lb.ShowUserCard("Nadia Teixeira") + "...\n";

            output += "Creating new window with user card of Elvira Espindola...\n";
            //output += lb.ShowUserCard("Elvira Espindola") + "...\n";
            try
            {
                List<CheckedOut> CheckedOutInfo = new List<CheckedOut>();
                Debug.Assert(!SDM.LMS.CheckLogin("Nadia Teixeira"));
                Debug.Assert(SDM.LMS.CheckUserInfo("Elvira Espindola", "Via del Corso, 22", "30003", 1, CheckedOutInfo));
            }
            catch
            {
                return "Test14 not passed";
            }
            return output;
        }

        public string test15()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Running TC12...\n";

            test12();

            output += "Checking existence of Nadia Teixeira...\n";
            if (SDM.LMS.CheckLogin("Nadia Teixeira"))
            {
                SDM.CurrentUser = new Student("Nadia Teixeira");
                Student p2 = (Student)SDM.CurrentUser;
                p2.CheckOut("Introduction to Algorithms");
                output += "Successfully checked...\n";
            }
            else
                output += "There is no such patron Nadia Teixeira...\n";
            try
            {
                Debug.Assert(!SDM.LMS.CheckLogin("Nadia Teixeira"));
            }
            catch
            {
                return "Test15 not passed";
            }
            return output;
        }

        public string test16()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Running TC12...\n";

            test11();

            output += "Logging In as Sergey Afonso patron...\n";
            SDM.CurrentUser = new Faculty("Sergey Afonso");
            Faculty p1 = (Faculty)SDM.CurrentUser;

            output += "Checking Introduction to Algorithms out by Sergey Afonso patron...\n";
            p1.CheckOut("Introduction to Algorithms");

            output += "Logging In as Elvira Espindola patron...\n";
            SDM.CurrentUser = new Student("Elvira Espindola");
            Student p3 = (Student)SDM.CurrentUser;

            output += "Introduction to Algorithms out by Elvira Espindola patron...\n";
            p3.CheckOut("Introduction to Algorithms");

            output += "Logging In as Sergey Afonso patron...\n";
            SDM.CurrentUser = new Faculty("Sergey Afonso");
            p1 = (Faculty)SDM.CurrentUser;

            output += "Checking Design Patterns: Elements of Reusable Object-Oriented Software out by Sergey Afonso patron...\n";
            p1.CheckOut("Design Patterns: Elements of Reusable Object-Oriented Software");

            output += "Logging In as librarian...\n";
            SDM.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Creating new window with user card of Sergey Afonso...\n";
            //output += lb.ShowUserCard("Sergey Afonso") + "...\n";

            output += "Creating new window with user card of Elvira Espindola...\n";
            //output += lb.ShowUserCard("Elvira Espindola") + "...\n";
            try
            {
                List<CheckedOut> CheckedOutInfo = new List<CheckedOut>();
                CheckedOut temp = new CheckedOut();
                temp.CheckOutTime = DateTime.Now.AddDays(21).Day;
                temp.DocumentCheckedOut = "Introduction to Algorithms";
                CheckedOutInfo.Add(temp);
                Debug.Assert(SDM.LMS.CheckUserInfo("Elvira Espindola", "Via del Corso, 22", "30003", 1, CheckedOutInfo));
                CheckedOutInfo = new List<CheckedOut>();
                temp.CheckOutTime = DateTime.Now.AddDays(28).Day;
                temp.DocumentCheckedOut = "Design Patterns: Elements of Reusable Object-Oriented Software";
                CheckedOutInfo.Insert(0, temp);
                temp.CheckOutTime = DateTime.Now.AddDays(28).Day;
                temp.DocumentCheckedOut = "Introduction to Algorithms";
                CheckedOutInfo.Add(temp);
                Debug.Assert(SDM.LMS.CheckUserInfo("Sergey Afonso", "Via Margutta, 3", "30001", 2, CheckedOutInfo));

            }
            catch
            {
                return "Test16 not passed";
            }
            return output;
        }

        public string test17()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Running TC11...\n";

            test11();

            output += "Logging In as Sergey Afonso patron...\n";
            SDM.CurrentUser = new Faculty("Sergey Afonso");
            Faculty p1 = (Faculty)SDM.CurrentUser;

            output += " Checking out Introduction to Algorithms, Design Patterns: Elements of Reusable Object-Oriented Software, The Mythical Man-month, Null References: The Billion Dollar Mistake by Sergey Afonso patron...\n";
            p1.CheckOut("Introduction to Algorithms");
            p1.CheckOut("Design Patterns: Elements of Reusable Object-Oriented Software");
            p1.CheckOut("The Mythical Man-month");
            p1.CheckOut("Null References: The Billion Dollar Mistake");

            output += "Logging In as Nadia Teixeira patron...\n";
            SDM.CurrentUser = new Student("Nadia Teixeira");
            Student p2 = (Student)SDM.CurrentUser;

            output += " Checking out Introduction to Algorithms, Design Patterns: Elements of Reusable Object-Oriented Software, Information Entropy...\n";
            p2.CheckOut("Introduction to Algorithms");
            p2.CheckOut("Design Patterns: Elements of Reusable Object-Oriented Software");
            p2.CheckOut("Information Entropy");

            output += "Logging In as librarian...\n";
            SDM.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Creating new window with user card of Sergey Afonso...\n";
            // output += lb.ShowUserCard("Sergey Afonso") + "...\n";

            output += "Creating new window with user card of Elvira Espindola...\n";
            //  output += lb.ShowUserCard("Nadia Teixeira") + "...\n";
            try
            {
                List<CheckedOut> CheckedOutInfo = new List<CheckedOut>();
                CheckedOut temp = new CheckedOut();
                temp.CheckOutTime = DateTime.Now.AddDays(21).Day;
                temp.DocumentCheckedOut = "Introduction to Algorithms";
                CheckedOutInfo.Add(temp);
                temp.CheckOutTime = DateTime.Now.AddDays(14).Day;
                temp.DocumentCheckedOut = "Design Patterns: Elements of Reusable Object-Oriented Software";
                CheckedOutInfo.Add(temp);
                temp.CheckOutTime = DateTime.Now.AddDays(14).Day;
                temp.DocumentCheckedOut = "Information Entropy";
                CheckedOutInfo.Add(temp);

                Debug.Assert(SDM.LMS.CheckUserInfo("Nadia Teixeira", "Via Sacra, 13", "30002", 1, CheckedOutInfo));

                CheckedOutInfo = new List<CheckedOut>();
                temp.CheckOutTime = DateTime.Now.AddDays(28).Day;
                temp.DocumentCheckedOut = "Introduction to Algorithms";
                CheckedOutInfo.Add(temp);
                temp.CheckOutTime = DateTime.Now.AddDays(28).Day;
                temp.DocumentCheckedOut = "Design Patterns: Elements of Reusable Object-Oriented Software";
                CheckedOutInfo.Add(temp);
                temp.CheckOutTime = DateTime.Now.AddDays(14).Day;
                temp.DocumentCheckedOut = "Null References: The Billion Dollar Mistake";
                CheckedOutInfo.Add(temp);

                Debug.Assert(SDM.LMS.CheckUserInfo("Sergey Afonso", "Via Margutta, 3", "30001", 2, CheckedOutInfo));

            }
            catch
            {
                return "Test17 not passed";
            }
            return output;

        }

        public string test18()
        {
            string output = "";
            output += "Logging In as Sergey Afonso patron...\n";
            SDM.CurrentUser = new Faculty("Sergey Afonso");
            Faculty p1 = (Faculty)SDM.CurrentUser;

            output += "breaking through space and time to February 2nd 2018...\n";
            int[] timeCheat = { 09, 02, 2018 };
            p1.CheckOut("Design Patterns: Elements of Reusable Object-Oriented Software", timeCheat);

            output += "Logging In as Nadia Teixeira patron...\n";
            SDM.CurrentUser = new Student("Nadia Teixeira");
            Student p2 = (Student)SDM.CurrentUser;

            output += "breaking through space and time to February 5th 2018...\n";
            timeCheat[0] = 05;
            p2.CheckOut("Introduction to Algorithms", timeCheat);

            output += "Logging In as Sergey Afonso patron...\n";
            SDM.CurrentUser = new Faculty("Sergey Afonso");
            p1 = (Faculty)SDM.CurrentUser;

            output += "breaking through space and time to February 9th 2018...\n";
            timeCheat[0] = 02;
            p1.CheckOut("Introduction to Algorithms", timeCheat);

            output += "Logging In as Nadia Teixeira patron...\n";
            SDM.CurrentUser = new Student("Nadia Teixeira");
            p2 = (Student)SDM.CurrentUser;

            output += "breaking through space and time to February 17th 2018...\n";
            timeCheat[0] = 17;
            p2.CheckOut("Null References: The Billion Dollar Mistake", timeCheat);

            output += "Logging In as librarian...\n";
            SDM.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Creating new window with overdue info of Sergey Afonso...\n";
            //lb.ShowOverdue("Sergey Afonso");

            output += "Creating new window with overdue info of Nadia Teixeira...\n";
            //lb.ShowOverdue("Nadia Teixeira");
            try
            {
                List<OverdueInfo> overdueInfos = new List<OverdueInfo>();
                OverdueInfo temp = new OverdueInfo();
                DateTime timetoback = new DateTime(2018, 02, 05).AddDays(21);
                DateTime assumingTime = new DateTime(2018, 03, 05);

                temp.overdue = (int)DateTime.Now.Subtract(timetoback).TotalDays;
                temp.DocumentChekedOut = "Introduction to Algorithms";
                overdueInfos.Add(temp);
                timetoback = new DateTime(2018, 02, 17).AddDays(14);
                temp.overdue = (int)DateTime.Now.Subtract(timetoback).TotalDays;
                temp.DocumentChekedOut = "Null References: The Billion Dollar Mistake";
                overdueInfos.Add(temp);

                Debug.Assert(SDM.LMS.CheckUserInfo("Nadia Teixeira", "Via Sacra, 13", "30002", 1, overdueInfos));

                overdueInfos = new List<OverdueInfo>();
                temp = new OverdueInfo();
                timetoback = new DateTime(2018, 02, 02).AddDays(28);
                temp.overdue = (int)DateTime.Now.Subtract(timetoback).TotalDays;
                temp.DocumentChekedOut = "Introduction to Algorithms";
                overdueInfos.Add(temp);

                Debug.Assert(SDM.LMS.CheckUserInfo("Sergey Afonso", "Via Margutta, 3", "30001", 2, overdueInfos));

            }
            catch
            {
                return "Test18 not passed";
            }
            return output;
        }

        public string test19()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Running TC11...\n";

            test11();

            output += "Logging In as librarian lb...\n";
            Librarian lb = (Librarian)SDM.CurrentUser;
            Environment.Exit(0);

            return output;
        }
    }
}
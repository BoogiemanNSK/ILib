using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using System;
using System.Linq;
using System.Threading;
using System.Windows;

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
            return output;
        }

        public string test4()
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
            SDM.LMS.AddDoc("b", "B", 0, 0, true); // Adding Reference book
            SDM.LMS.AddDoc("b", "B", 0, 0, true);

            output += "Faculty ft checking out book b...\n";
            ft.CheckOut("b");

            output += "Test passed with no exceptions!\n";
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
            SDM.LMS.AddDoc("A", "a", 0, 0, false); // Adding Reference book
            SDM.LMS.AddDoc("A", "a", 0, 0, false);
            SDM.LMS.AddDoc("A", "a", 0, 0, false);

            output += "Logging In as student st...\n";
            SDM.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SDM.CurrentUser;

            output += "Student st checking out book A...\n";
            st.CheckOut("A");

            output += "Logging In as student st1...\n";
            SDM.CurrentUser = new Student("st1"); // Log In student st1
            Student st1 = (Student)SDM.CurrentUser;

            output += "Student st1 checking out book A...\n";
            st1.CheckOut("A");

            output += "Logging In as student st2...\n";
            SDM.CurrentUser = new Student("st2"); // Log In student st2
            Student st2 = (Student)SDM.CurrentUser;

            output += "Student st2 checking out book A...\n";
            st2.CheckOut("A");

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
            p1.CheckOut("b");

            output += "Logging In as student p2...\n";
            SDM.CurrentUser = new Student("p2"); // Log In student st
            Student p2 = (Student)SDM.CurrentUser;

            output += "Student p2 checking out book b1...\n";
            p2.CheckOut("b1");

            output += "Test passed with no exceptions!\n";
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
            return output;

        }

        public string test11()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Registering librarian lb in the system...\n";
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Adding reference book b1 and 3 copy of b1...\n";
            SDM.LMS.AddDoc("b1", "b1", 0, 0, false);
            SDM.LMS.AddDoc("b1", "b1", 0, 0, false);
            SDM.LMS.AddDoc("b1", "b1", 0, 0, false);
            SDM.LMS.AddDoc("b1", "b1", 0, 0, false);

            output += "Adding reference book b2 and 2 copy of b1...\n";
            SDM.LMS.AddDoc("b2", "b2", 0, 0, false);
            SDM.LMS.AddDoc("b2", "b2", 0, 0, false);
            SDM.LMS.AddDoc("b2", "b2", 0, 0, false);

            output += "Adding reference book b3 and copy of b1...\n";
            SDM.LMS.AddDoc("b3", "b3", 0, 0, false);
            SDM.LMS.AddDoc("b3", "b3", 0, 0, false);

            output += "Adding reference video and copy of av1...\n";
            SDM.LMS.AddDoc("av1", "av1", 3, 0, false);
            SDM.LMS.AddDoc("av1", "av1", 3, 0, false);

            output += "Adding reference video and copy of av2...\n";
            SDM.LMS.AddDoc("av2", "av2", 3, 0, false);
            SDM.LMS.AddDoc("av2", "av2", 3, 0, false);

            output += "Logging In as librarian lb...\n";
            SDM.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Registering patrons p1, p2, p3...\n";
            lb.RegisterUser("p1", "p1", "p1", "p1", "p1", false);
            lb.RegisterUser("p2", "p2", "p2", "p2", "p2", false);
            lb.RegisterUser("p3", "p3", "p3", "p3", "p3", false);

            return output;
        }

        public string test12()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Running TC11...\n";

            test11();

            output += "Logging In as librarian lb...\n";
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Obtaining ID of p2 patron...\n";
            int idP2 = SDM.LMS.PatronbyName("p2").userID;

            output += "Removing b1 & b3 documents and p2 patron...\n";
            lb.DeleteDoc("b1");
            lb.DeleteDoc("b1");
            lb.DeleteUser(idP2);

            return output;
        }

        public string test13()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Running TC11...\n";

            test11();

            output += "Logging In as librarian lb...\n";
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Creating new window with user card of p1...\n";
            output += lb.ShowUserCard("p1") + "...\n";

            output += "Creating new window with user card of p3...\n";
            output += lb.ShowUserCard("p3") + "...\n";

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

            output += "Creating new window with user card of p2...\n";
            output += lb.ShowUserCard("p2") + "...\n";

            output += "Creating new window with user card of p3...\n";
            output += lb.ShowUserCard("p3") + "...\n";

            return output;
        }

        public string test15()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Running TC12...\n";

            test12();

            output += "Checking existence of p2...\n";
            if (SDM.LMS.CheckLogin("p2"))
            {
                SDM.CurrentUser = new Student("p2");
                Student p2 = (Student)SDM.CurrentUser;
                p2.CheckOut("b1");
                output += "Successfully checked...\n";
            }
            else
                output += "There is no such patron p2...\n";
            return output;
        }

        public string test16()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Running TC12...\n";

            test12();

            output += "Logging In as p1 patron...\n";
            SDM.CurrentUser = new Student("p1");
            Student p1 = (Student)SDM.CurrentUser;

            output += "Checking b1 out by p1 patron...\n";
            p1.CheckOut("b1");

            output += "Logging In as p3 patron...\n";
            SDM.CurrentUser = new Student("p3");
            Student p3 = (Student)SDM.CurrentUser;

            output += "Checking b1 out by p3 patron...\n";
            p1.CheckOut("b1");

            output += "Logging In as p1 patron...\n";
            SDM.CurrentUser = new Student("p1");
            p1 = (Student)SDM.CurrentUser;

            output += "Checking b2 out by p1 patron...\n";
            p1.CheckOut("b2");

            output += "Logging In as librarian...\n";
            SDM.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Creating new window with user card of p1...\n";
            output += lb.ShowUserCard("p1") + "...\n";

            output += "Creating new window with user card of p3...\n";
            output += lb.ShowUserCard("p3") + "...\n";

            return output;
        }

        public string test17()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Running TC11...\n";

            test11();

            output += "Logging In as p1 patron...\n";
            SDM.CurrentUser = new Student("p1");
            Student p1 = (Student)SDM.CurrentUser;

            output += " Checking out b1, b2, b3, av1 by p1 patron...\n";
            p1.CheckOut("b1");
            p1.CheckOut("b2");
            p1.CheckOut("b3");
            p1.CheckOut("av1");

            output += "Logging In as p2 patron...\n";
            SDM.CurrentUser = new Student("p2");
            Student p2 = (Student)SDM.CurrentUser;

            output += " Checking out b1, b2, av2 by p2 patron...\n";
            p2.CheckOut("b1");
            p2.CheckOut("b2");
            p2.CheckOut("av2");

            output += "Logging In as librarian...\n";
            SDM.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SDM.CurrentUser;

            output += "Creating new window with user card of p1...\n";
            output += lb.ShowUserCard("p1") + "...\n";

            output += "Creating new window with user card of p3...\n";
            output += lb.ShowUserCard("p3") + "...\n";

            return output;

        }

        public string test18()
        {
            string output = "Cleared DB...\n";
            SDM.LMS.ClearDB();

            output += "Running TC11...\n";

            test11();

            output += "Logging In as p1 patron...\n";
            SDM.CurrentUser = new Student("p1");
            Student p1 = (Student)SDM.CurrentUser;

            output += "breaking through space and time to February 2nd 2018...\n";
            int[] timeCheat = { 09, 02, 2018 };
            p1.CheckOut("b2", timeCheat);

            output += "Logging In as p2 patron...\n";
            SDM.CurrentUser = new Student("p2");
            Student p2 = (Student)SDM.CurrentUser;

            output += "breaking through space and time to February 5th 2018...\n";
            timeCheat[0] = 05;
            p2.CheckOut("b1", timeCheat);

            output += "Logging In as p1 patron...\n";
            SDM.CurrentUser = new Student("p1");
            p1 = (Student)SDM.CurrentUser;

            output += "breaking through space and time to February 9th 2018...\n";
            timeCheat[0] = 02;
            p1.CheckOut("b1", timeCheat);

            output += "Logging In as p2 patron...\n";
            SDM.CurrentUser = new Student("p2");
            p2 = (Student)SDM.CurrentUser;

            output += "breaking through space and time to February 17th 2018...\n";
            timeCheat[0] = 17;
            p2.CheckOut("av1", timeCheat);

            output += "Logging In as librarian...\n";
            SDM.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SDM.CurrentUser;

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

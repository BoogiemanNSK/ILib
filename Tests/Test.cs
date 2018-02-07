﻿
using I2P_Project.Classes.UserSystem;
using I2P_Project.Classes.Data_Managers;
namespace I2P_Project.Tests
{
    class Test
    {
        public string test1()
        {
            string output = "Cleared DB...\n";
            DataBaseManager.ClearDB();

            output += "Registering student st in the system...\n";
            DataBaseManager.RegisterUser("st", "st", "st", "st", "st", false);
            output += "Registering librarian lb in the system...\n";
            DataBaseManager.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Logging In as student st...\n";
            SystemDataManager.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SystemDataManager.CurrentUser;

            output += "Adding reference book b and two copies...\n";
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false); // Adding Reference book
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false); 
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false);

            output += "Student st checking out book b...\n";
            st.CheckOut("b");

            output += "Test passed with no exceptions!\n";
            return output;
        }

        public string test2()
        {
            string output = "Cleared DB...\n";
            DataBaseManager.ClearDB();

            output += "Registering student st in the system...\n";
            DataBaseManager.RegisterUser("st", "st", "st", "st", "st", false);
            output += "Registering librarian lb in the system...\n";
            DataBaseManager.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Logging In as student st...\n";
            SystemDataManager.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SystemDataManager.CurrentUser;

            output += "Student st checking out book by author A...\n";
            st.CheckOut("A");

            output += "Test passed with no exceptions!\n";
            return output;
        }

        public string test3()
        {
            string output = "Cleared DB...\n";
            DataBaseManager.ClearDB();

            output += "Registering student st in the system...\n";
            DataBaseManager.RegisterUser("st", "st", "st", "st", "st", false);
            output += "Registering faculty ft in the system...\n";
            DataBaseManager.RegisterUser("ft", "ft", "ft", "ft", "ft", false);
            output += "Registering librarian lb in the system...\n";
            DataBaseManager.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Logging In as faculty ft...\n";
            SystemDataManager.CurrentUser = new Faculty("ft"); // Log In student st
            Faculty ft = (Faculty)SystemDataManager.CurrentUser;

            output += "Adding reference book b and copy...\n";
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false); // Adding Reference book
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false);
            
            output += "Faculty ft checking out book b...\n";
            ft.CheckOut("b");

            output += "Test passed with no exceptions!\n";
            return output;
        }

        public string test4()
        {
            string output = "Cleared DB...\n";
            DataBaseManager.ClearDB();

            output += "Registering student st in the system...\n";
            DataBaseManager.RegisterUser("st", "st", "st", "st", "st", false);
            output += "Registering faculty ft in the system...\n";
            DataBaseManager.RegisterUser("ft", "ft", "ft", "ft", "ft", false);
            output += "Registering librarian lb in the system...\n";
            DataBaseManager.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Logging In as faculty ft...\n";
            SystemDataManager.CurrentUser = new Faculty("ft"); // Log In student st
            Faculty ft = (Faculty)SystemDataManager.CurrentUser;

            output += "Adding reference book b and copy...\n";
            DataBaseManager.AddDocToDB("b", "B", 0, 0, true); // Adding Reference book
            DataBaseManager.AddDocToDB("b", "B", 0, 0, true);
            
            output += "Faculty ft checking out book b...\n";
            ft.CheckOut("b");

            output += "Test passed with no exceptions!\n";
            return output;
        }
        
        public string test5()
        {
            string output = "Cleared DB...\n";
            DataBaseManager.ClearDB();

            output += "Registering student st in the system...\n";
            DataBaseManager.RegisterUser("st", "st", "st", "st", "st", false);
            output += "Registering student st1 in the system...\n";
            DataBaseManager.RegisterUser("st1", "st1", "st1", "st1", "st1", false);
            output += "Registering student st2 in the system...\n";
            DataBaseManager.RegisterUser("st2", "st2", "st2", "st2", "st2", false);
            output += "Registering librarian lb in the system...\n";
            DataBaseManager.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            
            output += "Adding reference book A and two copies...\n";
            DataBaseManager.AddDocToDB("A", "a", 0, 0, false); // Adding Reference book
            DataBaseManager.AddDocToDB("A", "a", 0, 0, false);
            DataBaseManager.AddDocToDB("A", "a", 0, 0, false);
            
            output += "Logging In as student st...\n";
            SystemDataManager.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SystemDataManager.CurrentUser;

            output += "Student st checking out book A...\n";
            st.CheckOut("A");

            output += "Logging In as student st1...\n";
            SystemDataManager.CurrentUser = new Student("st1"); // Log In student st1
            Student st1 = (Student)SystemDataManager.CurrentUser;

            output += "Student st1 checking out book A...\n";
            st1.CheckOut("A");

            output += "Logging In as student st2...\n";
            SystemDataManager.CurrentUser = new Student("st2"); // Log In student st2
            Student st2 = (Student)SystemDataManager.CurrentUser;
            
            output += "Student st2 checking out book A...\n";
            st2.CheckOut("A");

            output += "Test passed with no exceptions!\n";
            return output;
        }
      
        public string test6()
        {
            string output = "Cleared DB...\n";
            DataBaseManager.ClearDB();

            output += "Registering student st in the system...\n";
            DataBaseManager.RegisterUser("st", "st", "st", "st", "st", false);
            output += "Registering librarian lb in the system...\n";
            DataBaseManager.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Logging In as student st...\n";
            SystemDataManager.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SystemDataManager.CurrentUser;

            output += "Adding reference book b and two copies...\n";
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false); // Adding Reference book
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false);
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false);

            output += "Student st checking out book b...\n";
            st.CheckOut("b");
            output += "Student checking out the book b...\n";
            st.CheckOut("b");

            output += "Test passed with no exceptions!\n";
            return output;
        }
      

        public string test8()
        {
            string output = "Cleared DB...\n";
            DataBaseManager.ClearDB();

            output += "Registering faculty 'f' in the system...\n";
            DataBaseManager.RegisterUser("f", "f", "f", "f", "f", false);
            output += "Registering student 's' in the system...\n";
            DataBaseManager.RegisterUser("s", "s", "s", "s", "s", false);
            output += "Registering librarian lb in the system...\n";
            DataBaseManager.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            SystemDataManager.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SystemDataManager.CurrentUser;
            lb.SwapUserType(DataBaseManager.GetIDByName("f")); // Upgrade first student to faculty

            output += "Logging In as student s...\n";
            SystemDataManager.CurrentUser = new Student("s"); // Log In student st
            Student s = (Student)SystemDataManager.CurrentUser;

            output += "Adding reference book b and two copies...\n";
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false); // Adding Reference book
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false);

            output += "Student s checking out book b...\n";
            s.CheckOut(DataBaseManager.GetIDByTitle("b"));

            output += "Test passed with no exceptions!\n";
            return output;
        }

        public string test9()
        {
            string output = "Cleared DB...\n";
            DataBaseManager.ClearDB();

            output += "Registering faculty 'f' in the system...\n";
            DataBaseManager.RegisterUser("f", "f", "f", "f", "f", false);
            output += "Registering student 's' in the system...\n";
            DataBaseManager.RegisterUser("s", "s", "s", "s", "s", false);
            output += "Registering librarian lb in the system...\n";
            DataBaseManager.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            SystemDataManager.CurrentUser = new Librarian("lb");
            Librarian lb = (Librarian)SystemDataManager.CurrentUser;
            lb.SwapUserType(DataBaseManager.GetIDByName("f")); //upgrade first student to faculty

            output += "Logging In as student s...\n";
            SystemDataManager.CurrentUser = new Student("s"); // Log In student st
            Student s = (Student)SystemDataManager.CurrentUser;

            output += "Adding reference book b and copy...\n";
            DataBaseManager.AddDocToDB("b", "B", 1, 0, false); // Adding Reference book
            DataBaseManager.AddDocToDB("b", "B", 1, 0, false);

            output += "Student s checking out book b...\n";
            s.CheckOut(DataBaseManager.GetIDByTitle("b"));

            output += "Test passed with no exceptions!\n";
            return output;
        }

        public string test10()
        {
            string output = "Cleared DB...\n";
            DataBaseManager.ClearDB();

            output += "Registering student st in the system...\n";
            DataBaseManager.RegisterUser("st", "st", "st", "st", "st", false);
            output += "Registering librarian lb in the system...\n";
            DataBaseManager.RegisterUser("lb", "lb", "lb", "lb", "lb", true);

            output += "Logging In as student st...\n";
            SystemDataManager.CurrentUser = new Student("st"); // Log In student st
            Student st = (Student)SystemDataManager.CurrentUser;

            output += "Adding reference books A and B...\n";
            DataBaseManager.AddDocToDB("a", "A", 0, 0, false); // Adding Reference book
            DataBaseManager.AddDocToDB("a", "A", 0, 0, false);
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false);

            output += "Student st checking out book b...\n";
            st.CheckOut(DataBaseManager.GetIDByTitle("a"));
            st.CheckOut(DataBaseManager.GetIDByTitle("b"));

            output += "Test passed with no exceptions!\n";
            return output;
        }

    }
}

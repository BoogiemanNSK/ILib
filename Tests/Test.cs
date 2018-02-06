using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            st.CheckOut(DataBaseManager.GetIDByTitle("b"));

            return output;
        }

        public string test6()
        {
            string output = test1();

            Student st = (Student)SystemDataManager.CurrentUser;

            output += "Student st try to checking out the book b...\n";
            st.CheckOut(DataBaseManager.GetIDByTitle("b"));

            output += "No changes as well\n";

            return output;
        }

        public string test7()
        {
            string output = "Cleared DB...\n";
            DataBaseManager.ClearDB();

            output += "Registering student 'p1' in the system...\n";
            DataBaseManager.RegisterUser("p1", "p1", "p1", "p1", "p1", false);
            output += "Registering student 'p2' in the system...\n";
            DataBaseManager.RegisterUser("p2", "p2", "p2", "p2", "p2", false);
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
            st.CheckOut(DataBaseManager.GetIDByTitle("b"));

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
            lb.UpgradePatron(0); //upgrade first student to faculty

            output += "Logging In as student s...\n";
            SystemDataManager.CurrentUser = new Student("s"); // Log In student st
            Student s = (Student)SystemDataManager.CurrentUser;

            output += "Adding reference book b and two copies...\n";
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false); // Adding Reference book
            DataBaseManager.AddDocToDB("b", "B", 0, 0, false);

            output += "Student s checking out book b...\n";
            s.CheckOut(DataBaseManager.GetIDByTitle("b"));

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
            lb.UpgradePatron(0); //upgrade first student to faculty

            output += "Logging In as student s...\n";
            SystemDataManager.CurrentUser = new Student("s"); // Log In student st
            Student s = (Student)SystemDataManager.CurrentUser;

            output += "Adding reference book b and two copies...\n";
            DataBaseManager.AddDocToDB("b", "B", 1, 0, false); // Adding Reference book
            DataBaseManager.AddDocToDB("b", "B", 1, 0, false);

            output += "Student s checking out book b...\n";
            s.CheckOut(DataBaseManager.GetIDByTitle("b"));

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
            return output;
        }

    }
}

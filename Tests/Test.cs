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
    }
}

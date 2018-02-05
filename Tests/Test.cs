using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I2P_Project.Classes.Documents;
using I2P_Project.Classes.UserSystem;
using I2P_Project.Classes.Data_Managers;
namespace I2P_Project.Tests
{
    class Test
    {
        public bool test1()
        {

            DataBaseManager.ClearDB();


            DataBaseManager.RegisterUser("st", "st", "st", "st", "st", false);
            DataBaseManager.RegisterUser("lb", "lb", "lb", "lb", "lb", true);



            SystemDataManager.CurrentUser = new Student("st");

            return true;
        }
    }
}

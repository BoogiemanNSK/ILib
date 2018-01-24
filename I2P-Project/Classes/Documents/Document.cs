using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2P_Project.Classes.Documents
{
    abstract class Document
    {
        private String title;
        private String description;
        private int price; //in rubles
        private bool isBesteller; //2 weeks for check out
        private DateTime timeOfCheckOut;
        private int personID;
        //TODO
            //Connect with DB
        public string Description { get => description; set => description = value; }
        public string Title { get => title; set => title = value; }
        public int Price { get => price; set => price = value; }
        public bool IsBesteller { get => isBesteller; set => isBesteller = value; }
        public DateTime TimeOfCheckOut { get => timeOfCheckOut; set => timeOfCheckOut = value; }
        public int PersonID { get => personID; set => personID = value; }
    }
}

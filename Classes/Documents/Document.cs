
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2P_Project.Classes.Documents
{
    abstract class Document
    {
        private string title;
        private string description;
        private int price; //in rubles
        private bool isBesteller; //2 weeks for check out
        private DateTime timeOfCheckOut;
        private int personID;
        private int bookType;
        //TODO
            //Connect with DB
        
    }
}

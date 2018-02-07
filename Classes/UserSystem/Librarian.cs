using System;
using System.Collections.Generic;
using I2P_Project.Classes.Data_Managers;

namespace I2P_Project.Classes.UserSystem
{

    class Librarian : User
    {

        public Librarian(string eMail)
        {
            SetCurrent(eMail);
        }

        public List<OverdueInfo> CheckOverdue()
        {
            List<OverdueInfo> ovList = new List<OverdueInfo>();
            // TODO
            return ovList;
        }

        public void SwapUserType(int patronID)
        {
            DataBaseManager.SwapUserType(patronID);         
        }

        public void AddDoc(string path)
        {
            // TODO
        }

        public void DeleteDoc(int docID)
        {
            // TODO
        }

        public void ModifyDoc(int docID, string newName, string newDescription)
        {
            // TODO
        }

    }

    public struct OverdueInfo
    {
        Patron OverduedPatron { get; }
        
        DateTime CheckOutTime { get; }
    }

}

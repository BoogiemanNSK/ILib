using I2P_Project.Classes.UserSystem;

namespace I2P_Project.Classes.Data_Managers
{
    /// <summary> Static class to manage information that is equal for whole system </summary>
    static class SystemDataManager
    {
        /// <summary> Logged In User </summary>
        public static User CurrentUser { get; set; }
    }
}

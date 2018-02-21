using I2P_Project.Classes.UserSystem;

namespace I2P_Project.Classes
{
    /// <summary> Static class to manage information that is equal for whole system </summary>
    static class SDM
    {
        /// <summary> Logged In User </summary>
        public static User CurrentUser { get; set; }

        /// <summary> Instance of library </summary>
        public static Library LMS { get; set; }
    }
}

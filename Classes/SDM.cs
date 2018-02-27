using I2P_Project.Classes.UserSystem;

namespace I2P_Project.Classes
{
    /// <summary> Static class to manage information that is equal for whole system </summary>
    static class SDM
    {
        public static void InitializeSystem()
        {
            LMS = new Library();
            Strings = new StringConstants();
        }

        /// <summary> Logged In User </summary>
        public static User CurrentUser { get; set; }

        /// <summary> Instance of library </summary>
        public static Library LMS { get; private set; }

        /// <summary> Instance of strings class </summary>
        public static StringConstants Strings { get; private set; }
    }
}

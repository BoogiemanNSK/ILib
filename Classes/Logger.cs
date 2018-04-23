using System;
using System.IO;

namespace I2P_Project.Classes
{
    class Logger
    {
        private string _file;

        public Logger()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (Path.GetDirectoryName(executable));
            _file = path + "\\Log.txt";
            File.WriteAllText(_file, "");

            Write("Log");
            using (StreamWriter file = new StreamWriter(_file, true)) {
                file.WriteLine("----------------------------------------------------------------------------");
            }
        }

        public void Write(string text)
        {
            using (StreamWriter file = new StreamWriter(_file, true)) {
                file.WriteLine(DateTime.Now.ToString() + " | " + text);
            }
        }
    }
}

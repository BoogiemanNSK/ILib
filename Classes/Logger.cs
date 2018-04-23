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

            if (File.Exists(_file)) {
                Separator();
                Space();
                Space();
            }

            Separator();
            Write("Log");
            Separator();
        }

        public void Write(string text)
        {
            using (StreamWriter file = new StreamWriter(_file, true)) {
                file.WriteLine("| " + DateTime.Now.ToString() + " | " + text);
            }
        }

        private void Separator()
        {
            using (StreamWriter file = new StreamWriter(_file, true)) {
                file.WriteLine("----------------------------------------------------------------------------");
            }
        }

        private void Space()
        {
            using (StreamWriter file = new StreamWriter(_file, true)) {
                file.WriteLine("");
            }
        }
    }
}

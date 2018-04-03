using I2P_Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

/// <summary>
/// Uses for long operations. Shows loading page.
/// </summary>

namespace I2P_Project.Classes
{
    class ProcessManager
    {
        private Thread thread;
        private WaitWindow window;

        /// <summary>
        /// Starts waiting thread
        /// </summary>
        public void BeginWaiting()
        {
            this.thread = new Thread(this.RunThread);
            this.thread.IsBackground = true;
            this.thread.SetApartmentState(ApartmentState.STA);
            this.thread.Start();
        }

        /// <summary>
        /// Ends Waiting flow
        /// </summary>
        public void EndWaiting()
        {
            if (this.window != null)
            {
                this.window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    this.window.Close();
                }));
            }
            this.thread.Abort();
        }

        private void RunThread()  // Opens WaitWindow
        {
            this.window = new WaitWindow();
            this.window.Closed += new EventHandler(waitingWindow_Closed);
            try
            {
                this.window.ShowDialog(); // Fix "Поток находился в процессе прерывания" если выпадет исключение, пишите в чат.
            } catch { }
        }

        private void waitingWindow_Closed(object sender, EventArgs e)  // When WaitWindow is closed, flow shutdowns
        {
            Dispatcher.CurrentDispatcher.InvokeShutdown();
        }
    }
}

using I2P_Project.Pages;
using System;
using System.Threading;
using System.Windows.Threading;

namespace I2P_Project.Classes
{
    /// <summary> Uses for long operations. Shows loading page. </summary>
    class ProcessManager
    {
        private Thread thread;
        private WaitWindow window;

        /// <summary> Starts waiting thread </summary>
        public void BeginWaiting()
        {
            try
            {
                thread = new Thread(RunThread);
            }
            catch (ThreadAbortException) { }
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        /// <summary> Ends Waiting flow </summary>
        public void EndWaiting()
        {
            if (window != null)
            {
                window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    window.Close();
                }));
            }
        }

        private void RunThread()  // Opens WaitWindow
        {
            window = new WaitWindow();
            window.Closed += new EventHandler(WaitingWindowClosed);
            window.ShowDialog(); // Fix "Поток находился в процессе прерывания" если выпадет исключение, пишите в чат.
        }

        private void WaitingWindowClosed(object sender, EventArgs e)  // When WaitWindow is closed, flow shutdowns
        {
            Dispatcher.CurrentDispatcher.InvokeShutdown();
        }
    }
}

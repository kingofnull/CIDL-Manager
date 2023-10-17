using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIDL_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainFrm mainForm = new MainFrm();


            if (!SingleInstance.Start())
            {
                Process current = Process.GetCurrentProcess();
                foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                {
                    if (process.Id != current.Id)
                    {

                    

                        var h = WinApi.FindWindowInPerocess(process, mainForm.Text);
                        WinApi.ShowToFront(h);

                        break;
                    }
                }
                Environment.Exit(0);
                //SingleInstance.ShowFirstInstance();
                return;
            }





            Application.Run(mainForm);

            SingleInstance.Stop();


            /* bool createdNew = true;
             using (Mutex mutex = new Mutex(true, "MyApplicationName", out createdNew))
             {
                 if (createdNew)
                 {
                     Application.EnableVisualStyles();
                     Application.SetCompatibleTextRenderingDefault(false);
                     Application.Run(new MainFrm());
                 }
                 else
                 {
                     Process current = Process.GetCurrentProcess();
                     foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                     {
                         if (process.Id != current.Id)
                         {
                             var window = WindowHandles(process).FirstOrDefault();
                             ShowWindow(window, SW_SHOWNORMAL);
                             SetForegroundWindow(window);
                             break;
                         }
                     }


                 }

             }
             Environment.Exit(0);*/
        }
    }
}

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

        public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(uint dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImportAttribute("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public const int HWND_BROADCAST = 0xffff;
        public const int SW_SHOWNORMAL = 1;


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam, uint fuFlags, uint uTimeout, out IntPtr lpdwResult);

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        private extern static int GetWindowText(IntPtr hWnd, StringBuilder text, int maxCount);

       





        private const string FRAME_WINDOW = "ApplicationFrameWindow";

        public static List<IntPtr> FindAppWindows(Process proc)
        {
            var appWindows = new List<IntPtr>();
            for (
                IntPtr appWindow = FindWindowEx(IntPtr.Zero, IntPtr.Zero, FRAME_WINDOW, null); 
                appWindow != IntPtr.Zero;
                appWindow = FindWindowEx(IntPtr.Zero, appWindow, FRAME_WINDOW, null)
                )
            {
                IntPtr coreWindow = FindWindowEx(appWindow, IntPtr.Zero, "Windows.UI.Core.CoreWindow", null);
                if (coreWindow != IntPtr.Zero)
                {
                    GetWindowThreadProcessId(coreWindow, out var corePid);
                    if (corePid == proc.Id)
                    {
                        appWindows.Add(appWindow);
                    }
                }
            }

            return appWindows;
        }


        public static IEnumerable<IntPtr> WindowHandles(Process process)
        {
            var handles = new List<IntPtr>();
            foreach (ProcessThread thread in process.Threads)
                EnumThreadWindows((uint)thread.Id, (hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);
            return handles;
        }

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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CIDL_Manager
{
    class WinApi
    {
		[DllImport("user32")]
		public static extern int RegisterWindowMessage(string message);

		/*public static int RegisterWindowMessage(string format, params object[] args)
		{
			string message = String.Format(format, args);
			return RegisterWindowMessage(message);
		}*/

		public const int HWND_BROADCAST = 0xffff;
		public const int SW_SHOWNORMAL = 1;



        [DllImport("user32")]
		public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);


        public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(uint dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImportAttribute("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam, uint fuFlags, uint uTimeout, out IntPtr lpdwResult);

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        public extern static int GetWindowText(IntPtr hWnd, StringBuilder text, int maxCount);




        /*private const string FRAME_WINDOW = "ApplicationFrameWindow";
        public static List<IntPtr> FindAppWindows(Process proc)
        {
            var appWindows = new List<IntPtr>();
            for (
                IntPtr appWindow = WinApi.FindWindowEx(IntPtr.Zero, IntPtr.Zero, FRAME_WINDOW, null);
                appWindow != IntPtr.Zero;
                appWindow = WinApi.FindWindowEx(IntPtr.Zero, appWindow, FRAME_WINDOW, null)
                )
            {
                IntPtr coreWindow = WinApi.FindWindowEx(appWindow, IntPtr.Zero, "Windows.UI.Core.CoreWindow", null);
                if (coreWindow != IntPtr.Zero)
                {
                    WinApi.GetWindowThreadProcessId(coreWindow, out var corePid);
                    if (corePid == proc.Id)
                    {
                        appWindows.Add(appWindow);
                    }
                }
            }

            return appWindows;
        }*/

        public static IEnumerable<IntPtr> WindowHandles(Process process)
        {
            var handles = new List<IntPtr>();
            foreach (ProcessThread thread in process.Threads)
                EnumThreadWindows((uint)thread.Id, (hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);
            return handles;
        }


        public static IntPtr FindWindowInPerocess(Process process, string title) {


            foreach (var h in WindowHandles(process).ToList()) {
                StringBuilder text = new StringBuilder(200);
                GetWindowText(h, text, 200);
                if (text.ToString() == title)
                {
                    return h;
                }
            }

            return IntPtr.Zero;
        }




		public static void ShowToFront(IntPtr window)
		{
			ShowWindow(window, SW_SHOWNORMAL);
			SetForegroundWindow(window);
		}

	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsApplicationTester
{
    /// <summary>Contains functionality to get info on the open windows.</summary>
    public static class RunningWindows
    {
        /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
        /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
        public static Window[] GetOpenedWindows()
        {
            IntPtr shellWindow = GetShellWindow();
            List<Window> windows = new List<Window>();

            EnumWindows(new EnumWindowsProc(delegate(IntPtr hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;
                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);
                var info = new Window(
                    GetProcess(hWnd),
                    builder.ToString(),
                    GetClassNameOfWindow(hWnd),
                    hWnd);

                info.IsToMostOfProcess = Win32Api.IsWindowEnabled(hWnd);
                windows.Add(info);
                return true;
            }), 0);


            return windows.ToArray();
        }

        private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        public static ProcessInfo GetProcess(IntPtr hwnd)
        {
            try
            {
                uint pid = 0;
                GetWindowThreadProcessId(hwnd, out pid);
                if (hwnd != IntPtr.Zero)
                {
                    if (pid != 0)
                    {
                        var process = Process.GetProcessById((int)pid);
                        return new ProcessInfo(pid, process.ProcessName, process.MainModule.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ProcessInfo(0, ex.Message, ex.Message);
            }

            return new ProcessInfo(0, "Unknown", "Unknown");
        }

        internal static string GetClassNameOfWindow(IntPtr hwnd)
        {
            var className = "";
            try
            {
                int cls_max_length = 1000;
                var classText = new StringBuilder("", cls_max_length + 5);
                Win32Api.GetClassName(hwnd, classText, cls_max_length + 2);

                if (!String.IsNullOrEmpty(classText.ToString()) && !String.IsNullOrWhiteSpace(classText.ToString()))
                    className = classText.ToString();
            }
            catch (Exception ex)
            {
                className = ex.Message;
            }
            return className;
        }


        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        //WARN: Only for "Any CPU":
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);




    }
}
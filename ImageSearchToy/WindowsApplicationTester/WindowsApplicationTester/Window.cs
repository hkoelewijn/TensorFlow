using System;

namespace WindowsApplicationTester
{
    public class Window
    {
        public ProcessInfo Process { get; }

        public string Title { get; }

        public string Class { get; }

        public IntPtr Handle { get; }

        public string Id { get; }

        public bool IsToMostOfProcess { get; set; } = true;

        public Window(ProcessInfo process, string title, string windowClass, IntPtr handle)
        {
            Process = process;
            Title = title;
            Class = windowClass;
            Handle = handle;
            Id = $"{(int) Handle:X8}";
        }
    }
}
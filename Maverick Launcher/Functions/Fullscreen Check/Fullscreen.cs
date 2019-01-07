using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace MaverickClient
{
    public class Fullscreen
    {
        //Windowed Mode Check
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        private struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static bool IsForegroundFullScreen(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
                return false;

            WINDOWPLACEMENT windowPlace = new WINDOWPLACEMENT();

            GetWindowPlacement(WindowHandle, ref windowPlace);

            RECT rect = new RECT();

            GetWindowRect(new HandleRef(null, WindowHandle), ref rect);

            Console.WriteLine("Window Points: {0}, {1}", windowPlace.rcNormalPosition.X, windowPlace.rcNormalPosition.Y);

            Console.WriteLine("Screen Points: {0}, {1}", Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y);

            if (windowPlace.rcNormalPosition.X == Screen.PrimaryScreen.WorkingArea.X && windowPlace.rcNormalPosition.Y == Screen.PrimaryScreen.WorkingArea.Y)
                return true;

            GetWindowRect(new HandleRef(null, WindowHandle), ref rect);

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.Width == (rect.right - rect.left) && screen.Bounds.Height == (rect.bottom - rect.top))
                    return true;
            }

            return false;
        }
    }
}

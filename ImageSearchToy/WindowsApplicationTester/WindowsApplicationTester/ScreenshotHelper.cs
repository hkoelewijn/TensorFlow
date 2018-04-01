using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography.X509Certificates;

namespace WindowsApplicationTester
{
    public class ScreenshotHelper
    {
        internal static Bitmap MakeSnapshot(IntPtr parentWinHandle,
            bool isClientWnd, Win32Api.WindowShowStyle nCmdShow)
        {



            if (parentWinHandle == IntPtr.Zero || !Win32Api.IsWindow(parentWinHandle) ||
                        !Win32Api.IsWindowVisible(parentWinHandle))
                return null;
            if (Win32Api.IsIconic(parentWinHandle))
                Win32Api.ShowWindow(parentWinHandle, nCmdShow);//show it

            if (!Win32Api.SetForegroundWindow(parentWinHandle))
                return null;//can't bring it to front

            Win32Api.ShowWindow(parentWinHandle, Win32Api.WindowShowStyle.ShowMinimized);
            Win32Api.ShowWindow(parentWinHandle, Win32Api.WindowShowStyle.Restore);


            var appWndHandle = parentWinHandle;

            System.Threading.Thread.Sleep(1000);//give it some time to redraw
            RECT appRect;
            bool res = Win32Api.GetWindowRect
                (appWndHandle, out appRect);
            if (!res || appRect.Height == 0 || appRect.Width == 0)
            {
                return null;//some hidden window
            }
            // calculate the app rectangle

            //Intersect with the Desktop rectangle and get what's visible
            IntPtr DesktopHandle = Win32Api.GetDesktopWindow();
            RECT desktopRect;
            Win32Api.GetWindowRect(DesktopHandle, out desktopRect);
            RECT visibleRect;
            if (!Win32Api.IntersectRect
                (out visibleRect, ref desktopRect, ref appRect))
            {
                visibleRect = appRect;
            }
            if (Win32Api.IsRectEmpty(ref visibleRect))
                return null;

            int Width = visibleRect.Width;
            int Height = visibleRect.Height;
            IntPtr hdcTo = IntPtr.Zero;
            IntPtr hdcFrom = IntPtr.Zero;
            IntPtr hBitmap = IntPtr.Zero;
            try
            {
                Bitmap result = null;

                // get device context of the window...
                hdcFrom = Win32Api.GetDC(appWndHandle);

                // create dc that we can draw to...
                hdcTo = Win32Api.CreateCompatibleDC(hdcFrom);
                hBitmap = Win32Api.CreateCompatibleBitmap(hdcFrom, Width, Height);

                //  validate
                if (hBitmap != IntPtr.Zero)
                {
                    // adjust and copy
                    int x = appRect.Left < 0 ? -appRect.Left : 0;
                    int y = appRect.Top < 0 ? -appRect.Top : 0;

                    IntPtr hLocalBitmap = Win32Api.SelectObject(hdcTo, hBitmap);

                    Win32Api.BitBlt(hdcTo, 0, 0, Width, Height,
                        hdcFrom, 0, 0, Win32Api.SRCCOPY);

                    Win32Api.SelectObject(hdcTo, hLocalBitmap);

                    //  create bitmap for window image...
                    using (var screenShot = Image.FromHbitmap(hBitmap))
                    { 
                        // For some reason, the area that was reserved for the screenshot is bigger than the actual
                        // result. We remove the black borders to the right and bottom here
                        result = CropBitmap(screenShot);
                    }
                }

                return result;
            }
            finally
            {
                //  release the unmanaged resources
                if (hdcFrom != IntPtr.Zero)
                    Win32Api.ReleaseDC(appWndHandle, hdcFrom);
                if (hdcTo != IntPtr.Zero)
                    Win32Api.DeleteDC(hdcTo);
                if (hBitmap != IntPtr.Zero)
                    Win32Api.DeleteObject(hBitmap);
            }
        }

        private static Bitmap CropBitmap(Bitmap input)
        {
            int x, y;
            for (x = input.Width-1; x >= 0; x--)
            {
                var hasColor = false;
                for (y = 0; y < input.Height; y++)
                {
                    var pixel = input.GetPixel(x, y);
                    if (!(pixel.R == 0 || pixel.G == 0 || pixel.B == 0))
                    {
                        hasColor = true;
                        break;
                    }
                }
                if (hasColor)
                {
                    break;
                }
            }
            var newWidth = x > 0 ? x : input.Width;
            for (y = input.Height - 1; y >= 0; y--)
            {
                var hasColor = false;
                for (x = 0; x < newWidth; x++)
                {
                    var pixel = input.GetPixel(x, y);
                    if (!(pixel.R == 0 || pixel.G == 0 || pixel.B == 0))
                    {
                        hasColor = true;
                        break;
                    }
                }
                if (hasColor)
                {
                    break;
                }
            }
            var newHeight = y > 0 ? y : input.Height;
            var cropArea = new Rectangle(0, 0, newWidth, newHeight);
            return input.Clone(cropArea, PixelFormat.Format24bppRgb);
        }
    }
}
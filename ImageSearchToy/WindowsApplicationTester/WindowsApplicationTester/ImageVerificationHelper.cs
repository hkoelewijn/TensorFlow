using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace WindowsApplicationTester
{
    public class ImageVerificationHelper
    {
        private const string ResultFilePath = @".\result.txt";

        public static Task<double> GetSuccessProbability(Image image)
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "WindowsApplicationTester");
            var path = Path.Combine(directory, "image.png");

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            if (File.Exists(ResultFilePath))
            {
                File.Delete(ResultFilePath);
            }

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            image.Save(path, ImageFormat.Png);

            var process = Process.Start("python", $"verify.py {path}");

            var result = -1.0;
            if (process != null)
            {
                process.WaitForExit(20000);

                var resultString = File.ReadAllText(@".\result.txt");

                result = double.Parse(resultString);
            }

            return Task.FromResult(result);

        }
    }
}
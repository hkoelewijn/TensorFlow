using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApplicationTester
{
    public partial class ImageVerificationDialog : Form
    {
        private delegate void InvokeCloseDialogDelegate();

        private string _errorMessage;

        private CancellationTokenSource _cancellationTokenSource;
        private Image _verificationImage;

        public double Result { get; private set; }

        public Image VerificationImage  
        {
            get => _verificationImage;
            set
            {
                if (_verificationImage != null)
                {
                    throw new InvalidOperationException("Image is already set.");
                }

                _verificationImage = value ?? throw new InvalidOperationException("Cannot set image to null");

                _verificationImage = ScaleImage(_verificationImage);

                pictureBox1.Image = new Bitmap(_verificationImage);
            }
        }

        public ImageVerificationDialog()
        {
            InitializeComponent();
        }

        public void StartValidation(Form parent)
        {
            _errorMessage = null;
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;
            Result = -1;

            Task.Run(async () =>
            {
                try
                {
                    Result = await ImageVerificationHelper.GetSuccessProbability(_verificationImage);
                }
                catch (TaskCanceledException)
                {
                }
                catch (AggregateException ex)
                {
                    if (!(ex.InnerException is TaskCanceledException ||
                          ex.InnerExceptions.Any(ix => ix is TaskCanceledException)))
                    {
                        _errorMessage = ex.Message;
                    }
                }
                catch (Exception ex)
                {
                    _errorMessage = ex.Message;
                }
                InvokeCloseClick();
            }, token);

            ShowDialog(parent);
        }

        private void CloseDialog()
        {
            Close();
            if (_errorMessage != null)
            {
                MessageBox.Show(this, $@"{_errorMessage}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }

        private void InvokeCloseClick()
        {
            cancelButton.Invoke(new InvokeCloseDialogDelegate(CloseDialog));
        }

        private Image ScaleImage(Image source)
        {
            var scale = Math.Min((double)pictureBox1.Width / source.Width ,
                (double)pictureBox1.Height / source.Height);

            var newSize = new Size((int)(source.Width * scale), (int)(source.Height*scale));

            var xOffset = (pictureBox1.Width - newSize.Width) / 2;

            var yOffset = (pictureBox1.Height - newSize.Height) / 2;

            var destRect = new Rectangle(xOffset, yOffset, newSize.Width, newSize.Height);

            var result = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format24bppRgb);
            var graph = Graphics.FromImage(result);
            graph.InterpolationMode = InterpolationMode.High;
            graph.CompositingQuality = CompositingQuality.HighQuality;
            graph.SmoothingMode = SmoothingMode.AntiAlias;

            graph.DrawImage(source, destRect, 0, 0, source.Width, source.Height, GraphicsUnit.Pixel);

            graph.Dispose();

            return result;
        }
    }
}

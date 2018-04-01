using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsApplicationTester
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            SetWindows(RunningWindows.GetOpenedWindows());
        }

        private void SetWindows(Window[] windows)
        {
            var currentItems = new List<ListViewItem>();
            foreach (ListViewItem item in windowsList.Items)
            {
                currentItems.Add(item);
            }

            foreach (var window in windows)
            {
                var item = currentItems.FirstOrDefault(i => ((Window) i.Tag).Id == window.Id);
                if (item is null)
                {
                    var newItem = windowsList.Items.Add(window.Id, window.Process.Name);
                    newItem.Tag = window;
                    for (var i = 0; i < 3; i++)
                    {
                        newItem.SubItems.Add("");
                    }
                    UpdateItem(window, newItem.SubItems);
                }
                else
                {
                    item.Text = window.Process.Name;
                    item.Tag = window;
                    UpdateItem(window, item.SubItems);
                }
            }

            var windowsToRemove = currentItems
                .Where(item => windows.All(w => w.Id != ((Window) item.Tag).Id));

            foreach (var item in windowsToRemove)
            {
                windowsList.Items.Remove(item);
            }
        }

        private void UpdateItem(Window window, ListViewItem.ListViewSubItemCollection subItems)
        {
            if (window.IsToMostOfProcess)
            {
                foreach (ListViewItem.ListViewSubItem subItem in subItems)
                {
                    subItem.Font = new Font(subItems[0].Font, FontStyle.Bold);
                }
            }
            else
            {
                foreach (ListViewItem.ListViewSubItem subItem in subItems)
                {
                    subItem.Font = new Font(subItems[0].Font, FontStyle.Regular);
                }
            }
            subItems[0].Text = window.Process.Name;
            subItems[1].Text = window.Title;
            subItems[2].Text = window.Class;
        }

        #region Events

        private void Main_Resize(object sender, EventArgs e)
        {
            SetColumnWidth();
        }

        private void SetColumnWidth()
        {
            var columnWidth = (mainPanel.ClientSize.Width - 10) / windowsList.Columns.Count;

            for (var i = 0; i < windowsList.Columns.Count; i++)
            {
                windowsList.Columns[i].Width = columnWidth;
            }
        }

        private void pasteImage_Click(object sender, EventArgs e)
        {
            var image = Clipboard.GetImage();
            screenshot.Image = image;
            ValidateImage();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            SetColumnWidth();
        }

        private void windowsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (windowsList.SelectedItems.Count > 0)
            {
                var window = (Window) windowsList.SelectedItems[0].Tag;
                if (!window.IsToMostOfProcess && window.Process.Id > 0)
                {
                    foreach (ListViewItem item in windowsList.Items)
                    {
                        var activeWindowCandidate = (Window) item.Tag;

                        if (activeWindowCandidate.IsToMostOfProcess &&
                            activeWindowCandidate.Process.Id == window.Process.Id)
                        {
                            window = activeWindowCandidate;
                            break;
                        }
                    }
                }
                var image = ScreenshotHelper.MakeSnapshot(window.Handle, false, Win32Api.WindowShowStyle.Restore);

                screenshot.Image?.Dispose();

                screenshot.Image = image;

                WindowState = FormWindowState.Minimized;
                Show();
                WindowState = FormWindowState.Normal;

                ValidateImage();
            }
            else
            {
                resultText.Text = @"Please select a window from the text.";
                resultPanel.BackColor = Color.Gray;
            }
        }

        private void RefreshProcesses(object sender, EventArgs e)
        {
            SetWindows(RunningWindows.GetOpenedWindows());
        }
        #endregion

        private void ValidateImage()
        {
            var validator = new ImageVerificationDialog {VerificationImage = screenshot.Image};

            validator.StartValidation(this);

            ShowResult(validator.Result);

            validator.Dispose();
        }

        [SuppressMessage("ReSharper", "LocalizableElement")]
        private void ShowResult(double percentage)
        {
            var resultPercentage = Math.Round(percentage * 100.0);
            if (resultPercentage < 0)
            {
                resultText.Text = $"An error occurred while validating the image";
                resultPanel.BackColor = Color.Red;
            }
            else if (resultPercentage < 30)
            {
                resultText.Text = $"With {100 - resultPercentage}% certainty, this indicates an error.";
                resultPanel.BackColor = Color.DarkRed;
            }
            else if (resultPercentage < 50)
            {
                resultText.Text = $"The result is indetermined. ({100 - resultPercentage}% certainty this indicates an error).";
                resultPanel.BackColor = Color.DarkOrange;
            }
            else if (resultPercentage < 70)
            {
                resultText.Text = $"The result is indetermined. ({resultPercentage}% certainty this indicates success).";
                resultPanel.BackColor = Color.DarkOrange;
            }
            else
            {
                resultText.Text = $"With {resultPercentage}% certainty, this indicates success.";
                resultPanel.BackColor = Color.DarkGreen;
            }
        }
    }
}

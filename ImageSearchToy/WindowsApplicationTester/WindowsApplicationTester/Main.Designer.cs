namespace WindowsApplicationTester
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.windowsList = new System.Windows.Forms.ListView();
            this.windowProcess = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.windowTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.windowClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.headerPanel = new System.Windows.Forms.Panel();
            this.pasteImage = new System.Windows.Forms.Button();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.resultText = new System.Windows.Forms.Label();
            this.screenshot = new System.Windows.Forms.PictureBox();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.layoutPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.resultPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screenshot)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            this.layoutPanel.ColumnCount = 2;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutPanel.Controls.Add(this.windowsList, 0, 1);
            this.layoutPanel.Controls.Add(this.headerPanel, 0, 0);
            this.layoutPanel.Controls.Add(this.footerPanel, 0, 2);
            this.layoutPanel.Controls.Add(this.mainPanel, 1, 1);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 3;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutPanel.Size = new System.Drawing.Size(1421, 839);
            this.layoutPanel.TabIndex = 0;
            // 
            // windowsList
            // 
            this.windowsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.windowProcess,
            this.windowTitle,
            this.windowClass});
            this.windowsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windowsList.Location = new System.Drawing.Point(0, 60);
            this.windowsList.Margin = new System.Windows.Forms.Padding(0);
            this.windowsList.MultiSelect = false;
            this.windowsList.Name = "windowsList";
            this.windowsList.Size = new System.Drawing.Size(710, 759);
            this.windowsList.TabIndex = 0;
            this.windowsList.UseCompatibleStateImageBehavior = false;
            this.windowsList.View = System.Windows.Forms.View.Details;
            this.windowsList.SelectedIndexChanged += new System.EventHandler(this.windowsList_SelectedIndexChanged);
            // 
            // windowProcess
            // 
            this.windowProcess.Text = "Process";
            this.windowProcess.Width = 344;
            // 
            // windowTitle
            // 
            this.windowTitle.Text = "Title";
            this.windowTitle.Width = 332;
            // 
            // windowClass
            // 
            this.windowClass.Text = "Class";
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.Black;
            this.layoutPanel.SetColumnSpan(this.headerPanel, 2);
            this.headerPanel.Controls.Add(this.pasteImage);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1421, 60);
            this.headerPanel.TabIndex = 0;
            // 
            // pasteImage
            // 
            this.pasteImage.Location = new System.Drawing.Point(29, 12);
            this.pasteImage.Name = "pasteImage";
            this.pasteImage.Size = new System.Drawing.Size(132, 35);
            this.pasteImage.TabIndex = 1;
            this.pasteImage.Text = "Paste image";
            this.pasteImage.UseVisualStyleBackColor = true;
            this.pasteImage.Click += new System.EventHandler(this.pasteImage_Click);
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.layoutPanel.SetColumnSpan(this.footerPanel, 2);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footerPanel.Location = new System.Drawing.Point(0, 819);
            this.footerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(1421, 20);
            this.footerPanel.TabIndex = 1;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.leftPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(710, 60);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(711, 759);
            this.mainPanel.TabIndex = 3;
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.Gray;
            this.leftPanel.Controls.Add(this.resultPanel);
            this.leftPanel.Controls.Add(this.screenshot);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Margin = new System.Windows.Forms.Padding(0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(711, 759);
            this.leftPanel.TabIndex = 3;
            // 
            // resultPanel
            // 
            this.resultPanel.Controls.Add(this.resultText);
            this.resultPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.resultPanel.Location = new System.Drawing.Point(0, 690);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(711, 69);
            this.resultPanel.TabIndex = 1;
            // 
            // resultText
            // 
            this.resultText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultText.Location = new System.Drawing.Point(0, 0);
            this.resultText.Name = "resultText";
            this.resultText.Size = new System.Drawing.Size(711, 69);
            this.resultText.TabIndex = 0;
            this.resultText.Text = "Please select a window form the list";
            this.resultText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // screenshot
            // 
            this.screenshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenshot.Location = new System.Drawing.Point(0, 0);
            this.screenshot.Name = "screenshot";
            this.screenshot.Size = new System.Drawing.Size(711, 759);
            this.screenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.screenshot.TabIndex = 0;
            this.screenshot.TabStop = false;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 1000;
            this.refreshTimer.Tick += new System.EventHandler(this.RefreshProcesses);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1421, 839);
            this.Controls.Add(this.layoutPanel);
            this.Name = "Main";
            this.Text = "Windows Application Tester";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.layoutPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.resultPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.screenshot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutPanel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ListView windowsList;
        private System.Windows.Forms.ColumnHeader windowProcess;
        private System.Windows.Forms.ColumnHeader windowTitle;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Button pasteImage;
        private System.Windows.Forms.PictureBox screenshot;
        private System.Windows.Forms.ColumnHeader windowClass;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.Label resultText;
    }
}


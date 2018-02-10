using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Roro.Activities
{
    public class PageForm : Form
    {
        private void InitializeComponent()
        {
            this.activityPanel = new System.Windows.Forms.Panel();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pauseButton = new System.Windows.Forms.Button();
            this.consoleButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // activityPanel
            // 
            this.activityPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activityPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityPanel.Location = new System.Drawing.Point(6, 41);
            this.activityPanel.Name = "activityPanel";
            this.activityPanel.Size = new System.Drawing.Size(244, 514);
            this.activityPanel.TabIndex = 0;
            // 
            // pagePanel
            // 
            this.pagePanel.AutoScroll = true;
            this.pagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.Location = new System.Drawing.Point(256, 41);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Size = new System.Drawing.Size(722, 514);
            this.pagePanel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.activityPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pagePanel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 561);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.saveButton);
            this.panel2.Controls.Add(this.newButton);
            this.panel2.Controls.Add(this.openButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 29);
            this.panel2.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(155, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(70, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.TabStop = false;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // newButton
            // 
            this.newButton.Location = new System.Drawing.Point(3, 3);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(70, 23);
            this.newButton.TabIndex = 2;
            this.newButton.TabStop = false;
            this.newButton.Text = "New";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(79, 3);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(70, 23);
            this.openButton.TabIndex = 0;
            this.openButton.TabStop = false;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pauseButton);
            this.panel1.Controls.Add(this.consoleButton);
            this.panel1.Controls.Add(this.stopButton);
            this.panel1.Controls.Add(this.runButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(256, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 29);
            this.panel1.TabIndex = 1;
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(79, 3);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(70, 23);
            this.pauseButton.TabIndex = 3;
            this.pauseButton.TabStop = false;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // consoleButton
            // 
            this.consoleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleButton.Location = new System.Drawing.Point(649, 3);
            this.consoleButton.Name = "consoleButton";
            this.consoleButton.Size = new System.Drawing.Size(70, 23);
            this.consoleButton.TabIndex = 2;
            this.consoleButton.TabStop = false;
            this.consoleButton.Tag = "";
            this.consoleButton.Text = "Console";
            this.consoleButton.UseVisualStyleBackColor = true;
            this.consoleButton.Click += new System.EventHandler(this.ConsoleButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(155, 3);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(70, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.TabStop = false;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(3, 3);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(70, 23);
            this.runButton.TabIndex = 0;
            this.runButton.TabStop = false;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // PageForm
            // 
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "PageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roro - Free RPA Software";
            this.Load += new System.EventHandler(this.PageForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PageForm_KeyUp);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private PageForm() => this.InitializeComponent();

        private Panel pagePanel;
        private Panel activityPanel;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button runButton;
        private Button stopButton;
        private Panel panel2;
        private Button saveButton;
        private Button openButton;
        private Button newButton;

        private Page page;
        private Button consoleButton;
        private Button pauseButton;
        private string Title = "Roro - Free RPA Software";

        public static PageForm Create()
        {
            return new PageForm();
        }

        private void PageForm_Load(object sender, System.EventArgs e)
        {
            this.consoleButton.PerformClick();
            ActivityForm.Create().Parent = this.activityPanel;
            this.newButton.PerformClick();
            this.page.OnStateChanged += Page_OnStateChanged;
            if (Environment.GetCommandLineArgs().ElementAtOrDefault(1) is string xmlFile)
            {
                if (Page.Open(xmlFile) is Page xPage)
                {
                    this.page = xPage;
                    this.page.Show(this.pagePanel);
                    this.page.OnStateChanged += Page_OnStateChanged;
                    this.Text = string.Format("{0} - {1}", this.page.FileName, this.Title);
                    this.runButton.PerformClick();
                    var exitOnComplete = true;
                    xPage.OnStateChanged += (ss, ee) =>
                    {
                        switch (xPage.State)
                        {
                            case PageState.Completed:
                                if (exitOnComplete)
                                {
                                    Environment.Exit(0);
                                }
                                break;

                            case PageState.Paused:
                            case PageState.Stopped:
                                exitOnComplete = false;
                                break;
                        }
                    };
                }
            }
        }

        private void Page_OnStateChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.Text = string.Format("{0} - {1} [{2}]", this.page.FileName, this.Title, this.page.State);
                switch (this.page.State)
                {
                    case PageState.Running:
                        this.runButton.Enabled = false;
                        this.pauseButton.Enabled = true;
                        this.stopButton.Enabled = true;
                        break;
                    case PageState.Paused:
                        this.runButton.Enabled = true;
                        this.pauseButton.Enabled = false;
                        this.stopButton.Enabled = true;
                        break;
                    case PageState.Stopped:
                    case PageState.Completed:
                        this.runButton.Enabled = true;
                        this.pauseButton.Enabled = false;
                        this.stopButton.Enabled = false;
                        break;
                }
            }));
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            if (this.page == null || this.page.Close())
            {
                this.page = Page.Create();
                this.page.Show(this.pagePanel);
                this.page.OnStateChanged += Page_OnStateChanged;
                this.Text = string.Format("{0} - {1}", this.page.FileName, this.Title);
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (this.page == null || this.page.Close())
            {
                if (Page.Open() is Page newPage)
                {
                    this.page = newPage;
                    this.page.Show(this.pagePanel);
                    this.page.OnStateChanged += Page_OnStateChanged;
                    this.Text = string.Format("{0} - {1}", this.page.FileName, this.Title);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.page != null)
            {
                this.page.Save();
                this.Text = string.Format("{0} - {1}", this.page.FileName, this.Title);
            }
        }

        private void RunButton_Click(object sender, System.EventArgs e)
        {
            if (this.page != null)
            {
                this.page.Run();
            }
        }


        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (this.page != null)
            {
                this.page.Pause();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (this.page != null)
            {
                this.page.Stop();
            }
        }

        private void PageForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.N:
                    this.newButton.PerformClick();
                    break;

                case Keys.Control | Keys.O:
                    this.openButton.PerformClick();
                    break;

                case Keys.Control | Keys.S:
                    this.saveButton.PerformClick();
                    break;
            }
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private void ConsoleButton_Click(object sender, EventArgs e)
        {
            var visible = this.consoleButton.Tag is bool tag ? tag : true;
            ShowWindow(GetConsoleWindow(), visible ? SW_HIDE : SW_SHOW);
            this.consoleButton.Tag = !visible;
        }
    }
}

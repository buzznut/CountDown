
namespace TimerApp
{
    partial class DisplayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayForm));
            this.timerDisplay = new System.Windows.Forms.Timer(this.components);
            this.notifyIconDisplay = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripDisplay = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.configStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerDisplay
            // 
            this.timerDisplay.Interval = 250;
            this.timerDisplay.Tick += new System.EventHandler(this.timerDisplay_Tick);
            // 
            // notifyIconDisplay
            // 
            this.notifyIconDisplay.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconDisplay.Icon")));
            this.notifyIconDisplay.Text = "CountDown";
            this.notifyIconDisplay.Visible = true;
            // 
            // contextMenuStripDisplay
            // 
            this.contextMenuStripDisplay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configStripMenuItem,
            this.quitToolStripMenuItem});
            this.contextMenuStripDisplay.Name = "contextMenuStripDisplay";
            this.contextMenuStripDisplay.Size = new System.Drawing.Size(111, 48);
            // 
            // configStripMenuItem
            // 
            this.configStripMenuItem.Name = "configStripMenuItem";
            this.configStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.configStripMenuItem.Text = "Config";
            this.configStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // DisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(384, 41);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DisplayForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Load += new System.EventHandler(this.DisplayForm_Load);
            this.Shown += new System.EventHandler(this.DisplayForm_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DisplayForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DisplayForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DisplayForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DisplayForm_MouseUp);
            this.contextMenuStripDisplay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerDisplay;
        private System.Windows.Forms.NotifyIcon notifyIconDisplay;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDisplay;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configStripMenuItem;
    }
}
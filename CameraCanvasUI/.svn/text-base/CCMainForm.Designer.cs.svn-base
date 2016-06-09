namespace CameraCanvas
{
    partial class CCMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCMainForm));
            this.arrowTimer = new System.Windows.Forms.Timer(this.components);
            this.msgTimer = new System.Windows.Forms.Timer(this.components);
            this.selectTimer = new System.Windows.Forms.Timer(this.components);
            this.contentPanel = new System.Windows.Forms.Panel();
            this.ccToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.imagePanel = new CameraCanvas.DoubleBufferPanel();
            this.contentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // arrowTimer
            // 
            this.arrowTimer.Tick += new System.EventHandler(this.checkArrow_tick);
            // 
            // msgTimer
            // 
            this.msgTimer.Interval = 5000;
            this.msgTimer.Tick += new System.EventHandler(this.msgTimer_Tick);
            // 
            // selectTimer
            // 
            this.selectTimer.Interval = 10;
            this.selectTimer.Tick += new System.EventHandler(this.selectTimer_Tick);
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.MidnightBlue;
            this.contentPanel.Controls.Add(this.imagePanel);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(784, 562);
            this.contentPanel.TabIndex = 11;
            // 
            // imagePanel
            // 
            this.imagePanel.BackColor = System.Drawing.Color.MidnightBlue;
            this.imagePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel.Location = new System.Drawing.Point(0, 0);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(784, 562);
            this.imagePanel.TabIndex = 3;
            this.imagePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.imagepanel_Paint);
            this.imagePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imagepanel_MouseClick);
            this.imagePanel.MouseEnter += new System.EventHandler(this.imagepanel_MouseEnter);
            this.imagePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imagepanel_MouseMove);
            // 
            // CCMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.contentPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CCMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CCForm_Load);
            this.contentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Timer arrowTimer;
        private System.Windows.Forms.Timer msgTimer;
        //private ToolsSlidingPanel toolsSldPnl;
        private System.Windows.Forms.Timer selectTimer;
        private DoubleBufferPanel imagePanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.ToolTip ccToolTip;
        //private CameraCanvas.DoubleBufferPanel contentPanel;
    }
}


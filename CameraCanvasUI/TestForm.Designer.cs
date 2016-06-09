namespace CameraCanvas
{
    partial class TestForm
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
            
            //stop dwellClickTimer when form is closed otherwise dwell events keep getting called
            this.mouseDwellTimer.Stop();
            
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textbox1 = new System.Windows.Forms.TextBox();
            this.testFollowPanel = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dockTestBtn = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.mouseDwellPanel = new CameraCanvas.DoubleBufferPanel();
            this.testFollowPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.mouseDwellPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // textbox1
            // 
            this.textbox1.Location = new System.Drawing.Point(15, 73);
            this.textbox1.Name = "textbox1";
            this.textbox1.Size = new System.Drawing.Size(100, 20);
            this.textbox1.TabIndex = 8;
            // 
            // testFollowPanel
            // 
            this.testFollowPanel.BackColor = System.Drawing.Color.SpringGreen;
            this.testFollowPanel.Controls.Add(this.button5);
            this.testFollowPanel.Controls.Add(this.button4);
            this.testFollowPanel.Controls.Add(this.button3);
            this.testFollowPanel.Controls.Add(this.button2);
            this.testFollowPanel.Controls.Add(this.button1);
            this.testFollowPanel.Location = new System.Drawing.Point(309, 138);
            this.testFollowPanel.Name = "testFollowPanel";
            this.testFollowPanel.Size = new System.Drawing.Size(89, 150);
            this.testFollowPanel.TabIndex = 12;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(24, 42);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(37, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(58, 78);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(31, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 78);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(28, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // rightBtn
            // 
            this.button2.Location = new System.Drawing.Point(24, 124);
            this.button2.Name = "rightBtn";
            this.button2.Size = new System.Drawing.Size(37, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "rightBtn";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // leftBtn
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(24, 13);
            this.button1.Name = "leftBtn";
            this.button1.Size = new System.Drawing.Size(37, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "leftBtn";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dockTestBtn
            // 
            this.dockTestBtn.Location = new System.Drawing.Point(419, 29);
            this.dockTestBtn.Name = "dockTestBtn";
            this.dockTestBtn.Size = new System.Drawing.Size(75, 47);
            this.dockTestBtn.TabIndex = 13;
            this.dockTestBtn.Text = "Launch Dock Test";
            this.dockTestBtn.UseVisualStyleBackColor = true;
            this.dockTestBtn.Click += new System.EventHandler(this.dockTestBtn_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.dockTestBtn);
            this.contentPanel.Controls.Add(this.mouseDwellPanel);
            this.contentPanel.Controls.Add(this.textbox1);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(784, 562);
            this.contentPanel.TabIndex = 14;
            // 
            // mouseDwellPanel
            // 
            this.mouseDwellPanel.AutoSize = true;
            this.mouseDwellPanel.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.mouseDwellPanel.BackgroundImage = global::CameraCanvas.Properties.Resources.arrow_down;
            this.mouseDwellPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mouseDwellPanel.Controls.Add(this.testFollowPanel);
            this.mouseDwellPanel.Location = new System.Drawing.Point(60, 29);
            this.mouseDwellPanel.Name = "mouseDwellPanel";
            this.mouseDwellPanel.Size = new System.Drawing.Size(495, 291);
            this.mouseDwellPanel.TabIndex = 11;
            this.mouseDwellPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mouseDwellPanel_Paint);
            this.mouseDwellPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseDwellPanel_MouseMove);
            this.mouseDwellPanel.Click += new System.EventHandler(this.mouseDwellPanel_Click);
            this.mouseDwellPanel.Resize += new System.EventHandler(this.mouseDwellPanel_Resize);
            this.mouseDwellPanel.MouseEnter += new System.EventHandler(this.mouseDwellPanel_MouseEnter);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.contentPanel);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.testFollowPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.mouseDwellPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        //private CameraCanvas.DoubleBufferPanel mouseDwellPanel;
        private System.Windows.Forms.TextBox textbox1;
        private System.Windows.Forms.Panel testFollowPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private DoubleBufferPanel mouseDwellPanel;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button dockTestBtn;
        private System.Windows.Forms.Panel contentPanel;

    }
}
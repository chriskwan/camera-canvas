namespace CameraCanvas
{
    partial class PopGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopGame));
            this.msgLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.recSizeLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.launchBtn = new CameraCanvas.CCButton();
            this.playAgainBtn = new CameraCanvas.CCButton();
            this.SuspendLayout();
            // 
            // msgLabel
            // 
            this.msgLabel.AutoSize = true;
            this.msgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgLabel.Location = new System.Drawing.Point(5, 39);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(290, 20);
            this.msgLabel.TabIndex = 2;
            this.msgLabel.Text = "Hold the mouse on the balloon to pop it!";
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(12, 69);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(35, 13);
            this.sizeLabel.TabIndex = 3;
            this.sizeLabel.Text = "label2";
            // 
            // recSizeLabel
            // 
            this.recSizeLabel.AutoSize = true;
            this.recSizeLabel.Location = new System.Drawing.Point(12, 92);
            this.recSizeLabel.Name = "recSizeLabel";
            this.recSizeLabel.Size = new System.Drawing.Size(35, 13);
            this.recSizeLabel.TabIndex = 4;
            this.recSizeLabel.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // launchBtn
            // 
            this.launchBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.launchBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.launchBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.launchBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.launchBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.launchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.launchBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.launchBtn.Location = new System.Drawing.Point(541, 5);
            this.launchBtn.Name = "launchBtn";
            this.launchBtn.Size = new System.Drawing.Size(100, 100);
            this.launchBtn.TabIndex = 5;
            this.launchBtn.Text = "Close";
            this.launchBtn.UseVisualStyleBackColor = true;
            this.launchBtn.Click += new System.EventHandler(this.launchBtn_Click);
            // 
            // playAgainBtn
            // 
            this.playAgainBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.playAgainBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.playAgainBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.playAgainBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.playAgainBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.playAgainBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playAgainBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.playAgainBtn.Location = new System.Drawing.Point(377, 5);
            this.playAgainBtn.Name = "playAgainBtn";
            this.playAgainBtn.Size = new System.Drawing.Size(100, 100);
            this.playAgainBtn.TabIndex = 1;
            this.playAgainBtn.Text = "Play Again";
            this.playAgainBtn.UseVisualStyleBackColor = true;
            this.playAgainBtn.Click += new System.EventHandler(this.playAgainBtn_Click);
            // 
            // PopGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(771, 406);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.launchBtn);
            this.Controls.Add(this.recSizeLabel);
            this.Controls.Add(this.sizeLabel);
            this.Controls.Add(this.msgLabel);
            this.Controls.Add(this.playAgainBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "PopGame";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PopGame";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PopGame_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.Button playAgainBtn;
        private CCButton playAgainBtn;
        private System.Windows.Forms.Label msgLabel;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Label recSizeLabel;
        //private System.Windows.Forms.Button launchBtn;
        private CCButton launchBtn;
        private System.Windows.Forms.Button button1;
    }
}
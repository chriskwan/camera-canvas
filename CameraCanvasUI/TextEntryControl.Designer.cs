namespace CameraCanvas
{
    partial class TextEntryControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ccButton1 = new CameraCanvas.CCButton();
            this.ccButton2 = new CameraCanvas.CCButton();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(263, 126);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // ccButton1
            // 
            this.ccButton1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ccButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ccButton1.FlatAppearance.BorderColor = System.Drawing.Color.SaddleBrown;
            this.ccButton1.FlatAppearance.BorderSize = 0;
            this.ccButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.ccButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.ccButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ccButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.ccButton1.Location = new System.Drawing.Point(166, 147);
            this.ccButton1.Name = "ccButton1";
            this.ccButton1.Size = new System.Drawing.Size(100, 100);
            this.ccButton1.TabIndex = 1;
            this.ccButton1.Text = "ccButton1";
            this.ccButton1.UseVisualStyleBackColor = true;
            this.ccButton1.Click += new System.EventHandler(this.ccButton1_Click);
            // 
            // ccButton2
            // 
            this.ccButton2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ccButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ccButton2.FlatAppearance.BorderColor = System.Drawing.Color.SaddleBrown;
            this.ccButton2.FlatAppearance.BorderSize = 0;
            this.ccButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.ccButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.ccButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ccButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.ccButton2.Location = new System.Drawing.Point(3, 147);
            this.ccButton2.Name = "ccButton2";
            this.ccButton2.Size = new System.Drawing.Size(100, 100);
            this.ccButton2.TabIndex = 2;
            this.ccButton2.Text = "ccButton2";
            this.ccButton2.UseVisualStyleBackColor = true;
            this.ccButton2.Click += new System.EventHandler(this.ccButton2_Click);
            // 
            // TextEntryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.ccButton2);
            this.Controls.Add(this.ccButton1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "TextEntryControl";
            this.Size = new System.Drawing.Size(269, 250);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private CCButton ccButton1;
        private CCButton ccButton2;
    }
}

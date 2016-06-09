namespace CameraCanvas
{
    partial class TextEntryForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ccButton1 = new CameraCanvas.CCButton();
            this.ccButton2 = new CameraCanvas.CCButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(109, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(263, 126);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ccButton1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ccButton2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 134);
            this.tableLayoutPanel1.TabIndex = 3;
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
            this.ccButton1.Location = new System.Drawing.Point(378, 3);
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
            this.ccButton2.Location = new System.Drawing.Point(3, 3);
            this.ccButton2.Name = "ccButton2";
            this.ccButton2.Size = new System.Drawing.Size(100, 100);
            this.ccButton2.TabIndex = 2;
            this.ccButton2.Text = "ccButton2";
            this.ccButton2.UseVisualStyleBackColor = true;
            this.ccButton2.Click += new System.EventHandler(this.ccButton2_Click);
            // 
            // TextEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(484, 134);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TextEntryForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private CCButton ccButton1;
        private CCButton ccButton2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

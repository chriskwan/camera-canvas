namespace CameraCanvas
{
    partial class DrawingDisplayControl
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
            this.colorBtn = new CameraCanvas.CCButton();
            this.thicknessBtn = new CameraCanvas.CCButton();
            this.shapeBtn = new CameraCanvas.CCButton();
            this.SuspendLayout();
            // 
            // colorBtn
            // 
            this.colorBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colorBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.colorBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.colorBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.colorBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.colorBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.colorBtn.Location = new System.Drawing.Point(109, 3);
            this.colorBtn.Name = "colorBtn";
            this.colorBtn.Size = new System.Drawing.Size(100, 100);
            this.colorBtn.TabIndex = 2;
            this.colorBtn.UseVisualStyleBackColor = true;
            // 
            // thicknessBtn
            // 
            this.thicknessBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.thicknessBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.thicknessBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.thicknessBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.thicknessBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.thicknessBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.thicknessBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.thicknessBtn.Location = new System.Drawing.Point(215, 3);
            this.thicknessBtn.Name = "thicknessBtn";
            this.thicknessBtn.Size = new System.Drawing.Size(100, 100);
            this.thicknessBtn.TabIndex = 1;
            this.thicknessBtn.UseVisualStyleBackColor = true;
            // 
            // shapeBtn
            // 
            this.shapeBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.shapeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.shapeBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.shapeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.shapeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.shapeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shapeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.shapeBtn.Location = new System.Drawing.Point(3, 3);
            this.shapeBtn.Name = "shapeBtn";
            this.shapeBtn.Size = new System.Drawing.Size(100, 100);
            this.shapeBtn.TabIndex = 0;
            this.shapeBtn.UseVisualStyleBackColor = true;
            // 
            // DrawingDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.colorBtn);
            this.Controls.Add(this.thicknessBtn);
            this.Controls.Add(this.shapeBtn);
            this.Name = "DrawingDisplayControl";
            this.Size = new System.Drawing.Size(318, 106);
            this.ResumeLayout(false);

        }

        #endregion

        private CCButton shapeBtn;
        private CCButton thicknessBtn;
        private CCButton colorBtn;
    }
}

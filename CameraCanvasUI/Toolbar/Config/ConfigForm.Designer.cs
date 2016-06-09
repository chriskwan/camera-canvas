namespace CameraCanvas.Toolbar.Config
{
    partial class ConfigForm
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
            this.closeBtn = new System.Windows.Forms.Button();
            this.autoSlideSpeedGroupBox = new System.Windows.Forms.GroupBox();
            this.speedFormBtn = new System.Windows.Forms.Button();
            this.iconSizeGroupBox = new System.Windows.Forms.GroupBox();
            this.iconSizeFormBtn = new System.Windows.Forms.Button();
            this.toolbarOrientationGroupBox = new System.Windows.Forms.GroupBox();
            this.protoLaunchBtn = new System.Windows.Forms.Button();
            this.autoSlideGroupBox = new System.Windows.Forms.GroupBox();
            this.autoSlideChoiceBtn = new System.Windows.Forms.Button();
            this.movementFormBtn = new System.Windows.Forms.Button();
            this.toolbarPlacementBtn = new System.Windows.Forms.Button();
            this.autoSlideSpeedGroupBox.SuspendLayout();
            this.iconSizeGroupBox.SuspendLayout();
            this.toolbarOrientationGroupBox.SuspendLayout();
            this.autoSlideGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(197, 298);
            this.closeBtn.Name = "bottomBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 8;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // autoSlideSpeedGroupBox
            // 
            this.autoSlideSpeedGroupBox.Controls.Add(this.speedFormBtn);
            this.autoSlideSpeedGroupBox.Location = new System.Drawing.Point(10, 239);
            this.autoSlideSpeedGroupBox.Name = "autoSlideSpeedGroupBox";
            this.autoSlideSpeedGroupBox.Size = new System.Drawing.Size(181, 82);
            this.autoSlideSpeedGroupBox.TabIndex = 11;
            this.autoSlideSpeedGroupBox.TabStop = false;
            this.autoSlideSpeedGroupBox.Text = "Automatic Toolbar Sliding Speed";
            // 
            // speedFormBtn
            // 
            this.speedFormBtn.Location = new System.Drawing.Point(10, 19);
            this.speedFormBtn.Name = "speedFormBtn";
            this.speedFormBtn.Size = new System.Drawing.Size(75, 47);
            this.speedFormBtn.TabIndex = 12;
            this.speedFormBtn.Text = "Speed Form";
            this.speedFormBtn.UseVisualStyleBackColor = true;
            this.speedFormBtn.Click += new System.EventHandler(this.speedFormBtn_Click);
            // 
            // iconSizeGroupBox
            // 
            this.iconSizeGroupBox.Controls.Add(this.iconSizeFormBtn);
            this.iconSizeGroupBox.Location = new System.Drawing.Point(10, 85);
            this.iconSizeGroupBox.Name = "iconSizeGroupBox";
            this.iconSizeGroupBox.Size = new System.Drawing.Size(96, 61);
            this.iconSizeGroupBox.TabIndex = 12;
            this.iconSizeGroupBox.TabStop = false;
            this.iconSizeGroupBox.Text = "Icon Size";
            // 
            // iconSizeFormBtn
            // 
            this.iconSizeFormBtn.Location = new System.Drawing.Point(10, 19);
            this.iconSizeFormBtn.Name = "iconSizeFormBtn";
            this.iconSizeFormBtn.Size = new System.Drawing.Size(75, 36);
            this.iconSizeFormBtn.TabIndex = 17;
            this.iconSizeFormBtn.Text = "Icon Size Form";
            this.iconSizeFormBtn.UseVisualStyleBackColor = true;
            this.iconSizeFormBtn.Click += new System.EventHandler(this.iconSizeFormBtn_Click);
            // 
            // toolbarOrientationGroupBox
            // 
            this.toolbarOrientationGroupBox.Controls.Add(this.toolbarPlacementBtn);
            this.toolbarOrientationGroupBox.Location = new System.Drawing.Point(12, 12);
            this.toolbarOrientationGroupBox.Name = "toolbarOrientationGroupBox";
            this.toolbarOrientationGroupBox.Size = new System.Drawing.Size(124, 67);
            this.toolbarOrientationGroupBox.TabIndex = 13;
            this.toolbarOrientationGroupBox.TabStop = false;
            this.toolbarOrientationGroupBox.Text = "Toolbar Placement";
            // 
            // protoLaunchBtn
            // 
            this.protoLaunchBtn.Location = new System.Drawing.Point(233, 31);
            this.protoLaunchBtn.Name = "protoLaunchBtn";
            this.protoLaunchBtn.Size = new System.Drawing.Size(72, 23);
            this.protoLaunchBtn.TabIndex = 14;
            this.protoLaunchBtn.Text = "Prototype";
            this.protoLaunchBtn.UseVisualStyleBackColor = true;
            this.protoLaunchBtn.Click += new System.EventHandler(this.protoLaunchBtn_Click);
            // 
            // autoSlideGroupBox
            // 
            this.autoSlideGroupBox.Controls.Add(this.autoSlideChoiceBtn);
            this.autoSlideGroupBox.Location = new System.Drawing.Point(10, 152);
            this.autoSlideGroupBox.Name = "autoSlideGroupBox";
            this.autoSlideGroupBox.Size = new System.Drawing.Size(151, 69);
            this.autoSlideGroupBox.TabIndex = 15;
            this.autoSlideGroupBox.TabStop = false;
            this.autoSlideGroupBox.Text = "Automatic Toolbar Sliding";
            // 
            // autoSlideChoiceBtn
            // 
            this.autoSlideChoiceBtn.Location = new System.Drawing.Point(10, 19);
            this.autoSlideChoiceBtn.Name = "autoSlideChoiceBtn";
            this.autoSlideChoiceBtn.Size = new System.Drawing.Size(75, 34);
            this.autoSlideChoiceBtn.TabIndex = 17;
            this.autoSlideChoiceBtn.Text = "Auto Slide Form";
            this.autoSlideChoiceBtn.UseVisualStyleBackColor = true;
            // 
            // movementFormBtn
            // 
            this.movementFormBtn.Location = new System.Drawing.Point(233, 149);
            this.movementFormBtn.Name = "movementFormBtn";
            this.movementFormBtn.Size = new System.Drawing.Size(88, 58);
            this.movementFormBtn.TabIndex = 16;
            this.movementFormBtn.Text = "Movement Form";
            this.movementFormBtn.UseVisualStyleBackColor = true;
            this.movementFormBtn.Click += new System.EventHandler(this.movementFormBtn_Click);
            // 
            // toolbarPlacementBtn
            // 
            this.toolbarPlacementBtn.Location = new System.Drawing.Point(8, 19);
            this.toolbarPlacementBtn.Name = "toolbarPlacementBtn";
            this.toolbarPlacementBtn.Size = new System.Drawing.Size(75, 37);
            this.toolbarPlacementBtn.TabIndex = 17;
            this.toolbarPlacementBtn.Text = "Toolbar Placement Form";
            this.toolbarPlacementBtn.UseVisualStyleBackColor = true;
            this.toolbarPlacementBtn.Click += new System.EventHandler(this.toolbarPlacementBtn_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 338);
            this.Controls.Add(this.movementFormBtn);
            this.Controls.Add(this.autoSlideGroupBox);
            this.Controls.Add(this.protoLaunchBtn);
            this.Controls.Add(this.toolbarOrientationGroupBox);
            this.Controls.Add(this.iconSizeGroupBox);
            this.Controls.Add(this.autoSlideSpeedGroupBox);
            this.Controls.Add(this.closeBtn);
            this.Name = "ConfigForm";
            this.Text = "Configuration";
            this.autoSlideSpeedGroupBox.ResumeLayout(false);
            this.iconSizeGroupBox.ResumeLayout(false);
            this.toolbarOrientationGroupBox.ResumeLayout(false);
            this.autoSlideGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.GroupBox autoSlideSpeedGroupBox;
        private System.Windows.Forms.GroupBox iconSizeGroupBox;
        private System.Windows.Forms.Button speedFormBtn;
        private System.Windows.Forms.GroupBox toolbarOrientationGroupBox;
        private System.Windows.Forms.Button protoLaunchBtn;
        private System.Windows.Forms.GroupBox autoSlideGroupBox;
        private System.Windows.Forms.Button movementFormBtn;
        private System.Windows.Forms.Button iconSizeFormBtn;
        private System.Windows.Forms.Button autoSlideChoiceBtn;
        private System.Windows.Forms.Button toolbarPlacementBtn;
    }
}
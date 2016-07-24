namespace CameraCanvas.Toolbox
{
     class AboutCCButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.txtBoxAck = new System.Windows.Forms.TextBox();
            this.txtBoxDev = new System.Windows.Forms.TextBox();
            this.txtBoxIVC = new System.Windows.Forms.TextBox();
            this.linkLblChrisEmail = new System.Windows.Forms.LinkLabel();
            this.linkLblMargritEmail = new System.Windows.Forms.LinkLabel();
            this.linkLblMargrit = new System.Windows.Forms.LinkLabel();
            this.linkLblChris = new System.Windows.Forms.LinkLabel();
            this.txtBoxVersion = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSendFeedback = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoxAck
            // 
            this.txtBoxAck.AcceptsReturn = true;
            this.txtBoxAck.BackColor = System.Drawing.SystemColors.Control;
            this.txtBoxAck.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxAck.Enabled = false;
            this.txtBoxAck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxAck.Location = new System.Drawing.Point(3, 273);
            this.txtBoxAck.Multiline = true;
            this.txtBoxAck.Name = "txtBoxAck";
            this.txtBoxAck.ReadOnly = true;
            this.txtBoxAck.Size = new System.Drawing.Size(577, 56);
            this.txtBoxAck.TabIndex = 5;
            this.txtBoxAck.Text = "We acknowledge support from:\r\n    Boston University Undergraduate Research Opport" +
                "unities Program\r\n    NSF, grant IIS-HCC 0713229";
            this.txtBoxAck.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxDev
            // 
            this.txtBoxDev.AcceptsReturn = true;
            this.txtBoxDev.BackColor = System.Drawing.SystemColors.Control;
            this.txtBoxDev.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxDev.Enabled = false;
            this.txtBoxDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxDev.Location = new System.Drawing.Point(129, 72);
            this.txtBoxDev.Multiline = true;
            this.txtBoxDev.Name = "txtBoxDev";
            this.txtBoxDev.ReadOnly = true;
            this.txtBoxDev.Size = new System.Drawing.Size(108, 27);
            this.txtBoxDev.TabIndex = 7;
            this.txtBoxDev.Text = "Developed by:";
            // 
            // txtBoxIVC
            // 
            this.txtBoxIVC.AcceptsReturn = true;
            this.txtBoxIVC.BackColor = System.Drawing.SystemColors.Control;
            this.txtBoxIVC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxIVC.Enabled = false;
            this.txtBoxIVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxIVC.Location = new System.Drawing.Point(123, 173);
            this.txtBoxIVC.Multiline = true;
            this.txtBoxIVC.Name = "txtBoxIVC";
            this.txtBoxIVC.ReadOnly = true;
            this.txtBoxIVC.Size = new System.Drawing.Size(329, 69);
            this.txtBoxIVC.TabIndex = 8;
            this.txtBoxIVC.Text = "Image and Video Computing Group\r\nComputer Science Department\r\nBoston University";
            this.txtBoxIVC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // linkLblChrisEmail
            // 
            this.linkLblChrisEmail.AutoSize = true;
            this.linkLblChrisEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblChrisEmail.Location = new System.Drawing.Point(270, 102);
            this.linkLblChrisEmail.Name = "linkLblChrisEmail";
            this.linkLblChrisEmail.Size = new System.Drawing.Size(188, 25);
            this.linkLblChrisEmail.TabIndex = 9;
            this.linkLblChrisEmail.TabStop = true;
            this.linkLblChrisEmail.Text = "ckwan@cs.bu.edu";
            this.linkLblChrisEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblChrisEmail_LinkClicked);
            // 
            // linkLblMargritEmail
            // 
            this.linkLblMargritEmail.AutoSize = true;
            this.linkLblMargritEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblMargritEmail.Location = new System.Drawing.Point(278, 136);
            this.linkLblMargritEmail.Name = "linkLblMargritEmail";
            this.linkLblMargritEmail.Size = new System.Drawing.Size(180, 25);
            this.linkLblMargritEmail.TabIndex = 10;
            this.linkLblMargritEmail.TabStop = true;
            this.linkLblMargritEmail.Text = "betke@cs.bu.edu";
            this.linkLblMargritEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblMargritEmail_LinkClicked);
            // 
            // linkLblMargrit
            // 
            this.linkLblMargrit.AutoSize = true;
            this.linkLblMargrit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblMargrit.Location = new System.Drawing.Point(124, 136);
            this.linkLblMargrit.Name = "linkLblMargrit";
            this.linkLblMargrit.Size = new System.Drawing.Size(140, 25);
            this.linkLblMargrit.TabIndex = 11;
            this.linkLblMargrit.TabStop = true;
            this.linkLblMargrit.Text = "Margrit Betke";
            this.linkLblMargrit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblMargrit_LinkClicked);
            // 
            // linkLblChris
            // 
            this.linkLblChris.AutoSize = true;
            this.linkLblChris.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblChris.Location = new System.Drawing.Point(124, 102);
            this.linkLblChris.Name = "linkLblChris";
            this.linkLblChris.Size = new System.Drawing.Size(121, 25);
            this.linkLblChris.TabIndex = 12;
            this.linkLblChris.TabStop = true;
            this.linkLblChris.Text = "Chris Kwan";
            this.linkLblChris.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblChris_LinkClicked);
            // 
            // txtBoxVersion
            // 
            this.txtBoxVersion.AcceptsReturn = true;
            this.txtBoxVersion.BackColor = System.Drawing.SystemColors.Control;
            this.txtBoxVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxVersion.Enabled = false;
            this.txtBoxVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxVersion.Location = new System.Drawing.Point(225, 41);
            this.txtBoxVersion.Multiline = true;
            this.txtBoxVersion.Name = "txtBoxVersion";
            this.txtBoxVersion.ReadOnly = true;
            this.txtBoxVersion.Size = new System.Drawing.Size(97, 25);
            this.txtBoxVersion.TabIndex = 14;
            this.txtBoxVersion.Text = "version 2.0.0";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.txtBoxAck, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(583, 379);
            this.tableLayoutPanel.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.lblSendFeedback);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.linkLblMargrit);
            this.panel1.Controls.Add(this.linkLblChris);
            this.panel1.Controls.Add(this.txtBoxDev);
            this.panel1.Controls.Add(this.linkLblMargritEmail);
            this.panel1.Controls.Add(this.linkLblChrisEmail);
            this.panel1.Controls.Add(this.txtBoxIVC);
            this.panel1.Controls.Add(this.txtBoxVersion);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 264);
            this.panel1.TabIndex = 17;
            // 
            // lblSendFeedback
            // 
            this.lblSendFeedback.AutoSize = true;
            this.lblSendFeedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSendFeedback.Location = new System.Drawing.Point(3, 239);
            this.lblSendFeedback.Name = "lblSendFeedback";
            this.lblSendFeedback.Size = new System.Drawing.Size(577, 25);
            this.lblSendFeedback.TabIndex = 16;
            this.lblSendFeedback.Text = "Please send us feedback to help make this software better!";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(165, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(226, 33);
            this.lblTitle.TabIndex = 15;
            this.lblTitle.Text = "Camera Canvas";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(583, 379);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Camera Canvas";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxAck;
        private System.Windows.Forms.TextBox txtBoxDev;
        private System.Windows.Forms.TextBox txtBoxIVC;
        private System.Windows.Forms.LinkLabel linkLblChrisEmail;
        private System.Windows.Forms.LinkLabel linkLblMargritEmail;
        private System.Windows.Forms.LinkLabel linkLblMargrit;
        private System.Windows.Forms.LinkLabel linkLblChris;
        private System.Windows.Forms.TextBox txtBoxVersion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSendFeedback;

    }
}
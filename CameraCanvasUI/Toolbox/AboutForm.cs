using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas.Toolbox
{
    public partial class AboutForm : Form
    {

        public AboutForm(CCToolbar ccToolbar)
        {
            InitializeComponent();

            Toolbar.Config.CloseCCButton closeBtn = new CameraCanvas.Toolbar.Config.CloseCCButton(ccToolbar, this);
            closeBtn.Size = new Size(ccToolbar.ButtonWidth, ccToolbar.ButtonHeight);
            closeBtn.Anchor = AnchorStyles.None; //center the button vertically
            this.tableLayoutPanel.Controls.Add(closeBtn,0,1);

            txtBoxVersion.Text = Application.ProductVersion;
        }


        private void linkLblChris_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://cs-people.bu.edu/ckwan/");
        }

        private void linkLblChrisEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + linkLblChrisEmail.Text);
        }

        private void linkLblMargrit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.cs.bu.edu/~betke/");
        }

        private void linkLblMargritEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + linkLblMargritEmail.Text);
        }
    }
}
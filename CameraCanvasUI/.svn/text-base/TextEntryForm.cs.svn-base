using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas
{
    public partial class TextEntryForm : Form
    {
        private CCMainForm ccMainForm;

        public TextEntryForm()
        {
            InitializeComponent();
            richTextBox1.Font = ccButton1.Font;
            ccButton2.SetIcon("Icons/Close.png");
            ccButton1.SetIcon("Icons/Apply.png");
            this.ControlBox = false;
            this.ShowInTaskbar = false;
        }

        public RichTextBox TextBox
        {
            get { return this.richTextBox1; }
        }

        public void SetForm(CCMainForm ccMainForm)
        {
            //TODO refactor
            this.ccMainForm = ccMainForm;
            ccButton2.Width = this.ccMainForm.CCToolbar.ButtonWidth;
            ccButton2.Height = this.ccMainForm.CCToolbar.ButtonHeight;

            ccButton1.Width = this.ccMainForm.CCToolbar.ButtonWidth;
            ccButton1.Height = this.ccMainForm.CCToolbar.ButtonHeight;

            //keep text entry form always on top of camera canvas, but not other programs
            this.Owner = ccMainForm;

            //center text entry form to main form
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(ccMainForm.Location.X + ccMainForm.Width / 2 - this.Width/2, 
                ccMainForm.Location.Y + ccMainForm.Height / 2 - this.Height/2);
        }


        private void ccButton1_Click(object sender, EventArgs e)
        {
            ccMainForm.DrawText(this.richTextBox1.Text);
            ccMainForm.resetState();

            //TODO refactor!
            //show move arrows to move image since image automatically scrolls down to where caption is
            //display instructions for move arrows
            ccMainForm.ShowMessage("Put mouse in arrows to move image");
            ccMainForm.State = CCMainForm.CCFormState.Moving;

            this.Close();
        }

        private void ccButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            //this.Hide();
        }
    }
}

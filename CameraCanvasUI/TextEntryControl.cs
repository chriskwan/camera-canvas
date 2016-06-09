using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas
{
    public partial class TextEntryControl : UserControl
    {
        private CCMainForm ccMainForm;

        public TextEntryControl()
        {
            InitializeComponent();
            richTextBox1.Font = ccButton1.Font;
        }

        public RichTextBox TextBox
        {
            get { return this.richTextBox1; }
        }


        public void SetForm(CCMainForm ccMainForm)
        {
            this.ccMainForm = ccMainForm;
        }



        private void ccButton1_Click(object sender, EventArgs e)
        {
            ccMainForm.DrawText(this.richTextBox1.Text);
            ccMainForm.resetState();   
        }

        private void ccButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

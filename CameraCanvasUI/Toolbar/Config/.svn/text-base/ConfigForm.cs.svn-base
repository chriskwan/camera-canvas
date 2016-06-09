using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas.Toolbar.Config
{
    public partial class ConfigForm : Form
    {
        CCMainForm ccForm;
        CCToolbar ccToolbar;

        public ConfigForm(CCMainForm ccForm, CCToolbar ccToolbar)
        {
            InitializeComponent();
            this.ccForm = ccForm;
            this.ccToolbar = ccToolbar;
        }


        private void closeBtn_Click(object sender, EventArgs e)
        {
            //Close();
            //this.Hide();
            Console.WriteLine("config form is visible: " + this.Visible);
            Hide();
            Console.WriteLine("config form is visible: " + this.Visible);
        }


        private void speedFormBtn_Click(object sender, EventArgs e)
        {
            //SpeedConfigForm speedConfigForm = new SpeedConfigForm(this.ccMainForm);
            //speedConfigForm.Show();
            SlidingSpeedChoiceForm autoSlideChoiceForm = new SlidingSpeedChoiceForm(ccToolbar);
            autoSlideChoiceForm.Show();
        }

        private void protoLaunchBtn_Click(object sender, EventArgs e)
        {
            TestForm tf = new TestForm();
            tf.Show();
        }

        private void movementFormBtn_Click(object sender, EventArgs e)
        {
            MovementForm movementForm = new MovementForm(this.ccToolbar);
            movementForm.Show();
            movementForm.TopMost = true;
        }

        private void iconSizeFormBtn_Click(object sender, EventArgs e)
        {
            ButtonSizeChoiceForm iconSizeChoiceForm = new ButtonSizeChoiceForm(ccToolbar);
            iconSizeChoiceForm.Show();
        }

        private void toolbarPlacementBtn_Click(object sender, EventArgs e)
        {
            ToolbarPlacementForm toolbarPlacementForm = new ToolbarPlacementForm(ccToolbar);
            toolbarPlacementForm.Show();
        }




    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas
{
    public partial class WelcomeForm : Form
    {
        CCMainForm ccForm;

        public WelcomeForm(CCMainForm ccForm)
        {
            InitializeComponent();
            this.ccForm = ccForm;    
        }

        private void launchBtn_Click(object sender, EventArgs e)
        {
            if (ccForm == null || ccForm.IsDisposed == true)
            {
                ccForm = new CCMainForm("config.ini");
            }
           
            ccForm.Show();
            this.Hide();
        }

        private void gameBtn_Click(object sender, EventArgs e)
        {
            ChaseGame chaseGame = new ChaseGame(this.ccForm);
            chaseGame.Show();
            this.Hide();
        }
    }
}
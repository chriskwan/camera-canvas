using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas
{
    public partial class DockTestForm : Form
    {
        public DockTestForm()
        {
            InitializeComponent();
        }

        private void topBtn_Click(object sender, EventArgs e)
        {
            //dockPanel.Dock = DockStyle.Top;
            tablePanel.Controls.Add(dockPanel,1,0);
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            //dockPanel.Dock = DockStyle.Left;
            tablePanel.Controls.Add(dockPanel, 0, 1);
        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            //dockPanel.Dock = DockStyle.Right;
            tablePanel.Controls.Add(dockPanel, 2, 1);
        }

        private void bottomBtn_Click(object sender, EventArgs e)
        {
            //dockPanel.Dock = DockStyle.Bottom;
            tablePanel.Controls.Add(dockPanel, 1, 2);
        }

        private void tablePanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
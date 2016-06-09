using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CameraCanvas.Toolbar;

namespace CameraCanvas
{
    public partial class MovementForm : Form
    {
        private CCToolbar ccToolbar;
        private CCToolbar.ToolbarOrientation orientation;
        private CCButton prevBtn;
        private CCButton nextBtn;
        private CCButton selectionBtn;

        public MovementForm()
        {
            InitializeComponent();
        }

        public MovementForm(CCToolbar ccToolbar)
            : this()
        {
            this.ccToolbar = ccToolbar;
            //this.orientation = orientation;

            this.orientation = ccToolbar.Orientation;

            prevBtn = new PrevCCButton(this.ccToolbar);
            nextBtn = new NextCCButton(this.ccToolbar);
            selectionBtn = new SelectionCCButton(this.ccToolbar);

            if (orientation == CCToolbar.ToolbarOrientation.HorizontalTop
                || orientation == CCToolbar.ToolbarOrientation.HorizontalBottom)
            {
                tableLayoutPanel.Controls.Add(prevBtn, 0, 1);
                tableLayoutPanel.Controls.Add(selectionBtn, 1, 1);
                tableLayoutPanel.Controls.Add(nextBtn, 2, 1);
            }
            else if (orientation == CCToolbar.ToolbarOrientation.VerticalLeft
                || orientation == CCToolbar.ToolbarOrientation.VerticalRight)
            {
                tableLayoutPanel.Controls.Add(prevBtn, 1, 0);
                tableLayoutPanel.Controls.Add(selectionBtn, 1, 1);
                tableLayoutPanel.Controls.Add(nextBtn, 1, 2);
            }
        }
    }
}
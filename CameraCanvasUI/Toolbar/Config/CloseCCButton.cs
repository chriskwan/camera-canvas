using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms; //for Form

namespace CameraCanvas.Toolbar.Config
{
    class CloseCCButton : CCButton
    {
        Form parentForm;
        public CloseCCButton(CCToolbar ccToolbar, Form parentForm)
            : base(ccToolbar, "&Close", "Icons/Close.png")
        {
            this.parentForm = parentForm;
            this.Click += new EventHandler(CloseCCButton_Click);
        }

        void CloseCCButton_Click(object sender, EventArgs e)
        {
            this.parentForm.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CameraCanvas.Toolbox
{
    class AboutCCButton : CCButton
    {
        //ref icon: http://www.openclipart.org/detail/25240
        public AboutCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "&About", "Icons/About.png")
        {
            this.Click += new EventHandler(AboutCCButton_Click);
        }

        void AboutCCButton_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm(ccToolbar);
            aboutForm.ShowDialog();
        }
    }
}

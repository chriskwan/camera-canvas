using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas.Toolbar
{
    /// <summary>
    /// Defines Back Button on the CCToolbar.
    /// </summary>
    class BackCCButton : CCButton
    {
        public BackCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "&Back", "Icons/Back.png", "Icons/BackGray.png")
        {
            //cannot cancel anything yet in the beginning
            this.Disable();
        }


        /// <summary>
        /// 
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            ccToolbar.RemoveCurrentToolbarButtons();
        }

    }
}

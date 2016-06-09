using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas.Toolbar
{
    /// <summary>
    /// Defines Next Button on the CCToolbar.
    /// </summary>
    public class NextCCButton : SlideToButton
    {
        public NextCCButton(CCToolbar ccToolbar)
            : base(ccToolbar,
            "&Next",
            "Icons/NextRight.png",
            "Icons/NextRightGray.png",
            "Icons/NextDown.png",
            "Icons/NextDownGray.png") { }

        /// <summary>
        /// Slide toolbar to the next button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void SlideToolbarButtons(object sender, EventArgs e)
        {
            ccToolbar.SlideToolbarButtonsToNext();
        }

    }
}

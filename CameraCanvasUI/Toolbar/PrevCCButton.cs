using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas.Toolbar
{
    /// <summary>
    /// Defines Previous Button on the CCToolbar.
    /// </summary>
    public class PrevCCButton : SlideToButton
    {   

        public PrevCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, 
            "&Previous",
            "Icons/PrevLeft.png",
            "Icons/PrevLeftGray.png",
            "Icons/PrevUp.png",
            "Icons/PrevUpGray.png"){}

        /// <summary>
        /// Slide toolbar to the previous button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void SlideToolbarButtons(object sender, EventArgs e)
        {
            ccToolbar.SlideToolbarButtonsToPrev();
        }

    }
}

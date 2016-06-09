using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas.Toolbar
{
    /// <summary>
    /// Defines MainMenu Button on the.
    /// </summary>
    class MainMenuCCButton : CCButton
    {
        public MainMenuCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "&Main Menu", "Icons/MainMenu.png", "Icons/MainMenuGray.png")
        {
            //program starts in main menu already
            this.Disable();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            ccToolbar.RemoveAllToolbarButtons();
        }

    }
}

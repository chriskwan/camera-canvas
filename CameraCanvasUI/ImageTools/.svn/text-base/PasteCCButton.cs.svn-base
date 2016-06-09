using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms; //for Clipboard

namespace CameraCanvas.ImageTools
{
    /// <summary>
    /// Defines the Paste button in the Image tools on the toolbar.
    /// </summary>
    /// ref icon: http://www.openclipart.org/detail/32347
    class PasteCCButton : CCButton
    {
        public PasteCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Paste", "Icons/paste.png")
        {
        }
        
        /// <summary>
        /// On Click: Show the image to be pasted from the clipboard.
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                this.ccMainForm.resetSelectPoints();
                this.ccMainForm.State = CCMainForm.CCFormState.Pasting;
                this.ccMainForm.ShowMessage("Click on image to paste selection");
                this.ccMainForm.getWavPlayer().Play(@"wav\click.wav");
                this.ccMainForm.MainImage.pasteImage(this.ccMainForm.MainImage.width, this.ccMainForm.MainImage.height);
                this.ccMainForm.MainImage.Invalidate();
                this.ccMainForm.getImagePanel().Invalidate();
            }
            else
            {
                this.ccMainForm.ShowMessage("No selection copied to paste");
                this.ccMainForm.getWavPlayer().Play(@"wav\click.wav");
            }
        }

    }
}

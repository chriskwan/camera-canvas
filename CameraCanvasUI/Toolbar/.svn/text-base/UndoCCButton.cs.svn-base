using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace CameraCanvas.Toolbar
{
    /// <summary>
    /// Defines Undo Button on the CCToolbar.
    /// </summary>
    public class UndoCCButton : CCButton
    {
        public UndoCCButton(CCToolbar ccToolbar) 
            : base(ccToolbar, "&Undo", "Icons/undo.png", "Icons/undoGray.png")
        {
            //in beginning, no undos exist yet
            //TODO fix
            this.Disable();
        }

        /// <summary>
        /// On Click: Undo the last change to the image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ClickAction(object sender, EventArgs e)
        {
            //if we successfully undid a change
            if (true == ccToolbar.CCMainForm.MainImage.undo())
            {
                //make sure redo button is enabled so we can redo the change
                ccToolbar.RedoButton.Enable();

                //if we just undid the last undo, disable this button
                if (false == ccToolbar.CCMainForm.MainImage.HasUndos())
                {
                    this.Disable();
                }

                //TODO refactor what is the difference between these two?
                ccToolbar.CCMainForm.MainImage.Invalidate();
                ccToolbar.CCMainForm.getImagePanel().Invalidate();
            }

            
        }
    }
}

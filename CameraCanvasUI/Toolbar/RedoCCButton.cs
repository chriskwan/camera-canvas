using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace CameraCanvas.Toolbar
{
    /// <summary>
    /// Defines Redo Button on the CCToolbar.
    /// </summary>
    public class RedoCCButton : CCButton
    {
        public RedoCCButton(CCToolbar ccToolbar) 
            : base(ccToolbar, "&Redo", "Icons/redo.png", "Icons/redoGray.png")
        {
            //in beginning, no redos exist yet
            this.Disable();
        }


        /// <summary>
        /// On Click: Redo the last undone change to the image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ClickAction(object sender, EventArgs e)
        {
            //if we successfully undid a change
            if (true == ccToolbar.CCMainForm.MainImage.redo())
            {
                //make sure undo button is enabled if we need to undo the change again
                ccToolbar.UndoButton.Enable();

                //if this was the last change to redo, disable the redo button
                if (false == ccToolbar.CCMainForm.MainImage.HasRedos())
                {
                    this.Disable();
                }

                //TODO refactor do we need both calls?
                ccToolbar.CCMainForm.MainImage.Invalidate();
                ccToolbar.CCMainForm.getImagePanel().Invalidate();
            }
        }

    }
}

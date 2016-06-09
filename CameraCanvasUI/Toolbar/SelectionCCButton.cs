using System;
using System.Collections.Generic;
using System.Text;

namespace CameraCanvas.Toolbar
{
    class SelectionCCButton : CCButton
    {
        public SelectionCCButton(CCToolbar ccToolbar)
            : base(ccToolbar)
        {
        }

        
        /// <summary>
        /// Update the appearance of the button to match the currently selected toolbar button.
        /// </summary>
        public void UpdateButtonAppearance()
        {
            this.SetTextAndTooltip(ccToolbar.SelectedButton.Text);
            this.BackgroundImage = ccToolbar.SelectedButton.BackgroundImage;
            this.BackColor = ccToolbar.SelectedButton.BackColor; //for color picker
            //TODO DELETE?
            //this.MouseHoverAction(null, null);
            //this.MouseEnterAction(null, null);
        }


        /// <summary>
        /// On Click: Perform the action of the currently selected toolbar button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ClickAction(object sender, EventArgs e)
        {
            ccToolbar.SelectedButton.ClickAction(sender, e);
        }


        /// <summary>
        /// On Mouse Hover: Perform the action of the currently selected toolbar button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MouseHoverAction(object sender, EventArgs e)
        {
            ccToolbar.SelectedButton.MouseHoverAction(sender, e);
        }


        /// <summary>
        /// On Mouse Enter: Perform the action of the currently selected toolbar button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MouseEnterAction(object sender, EventArgs e)
        {
            //stop the automatic sliding slideTimer so toolbar does not slide when user is trying to click
            ccToolbar.StopAutomaticSliding();

            ccToolbar.SelectedButton.MouseEnterAction(sender, e);
        }


        /// <summary>
        /// On Mouse Leave: Perform the action of the currently selected toolbar button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MouseLeaveAction(object sender, EventArgs e)
        {
            //resume automatic sliding if not trying to click
            ccToolbar.StartAutomaticSliding();

            ccToolbar.SelectedButton.MouseLeaveAction(sender, e);
        }
    }
}

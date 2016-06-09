using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing; //for Color

namespace CameraCanvas.ImageTools
{
    /// <summary>
    /// Defines Move button in Image Tools on toolbar.
    /// </summary>
    class MoveCCButton : CCButton
    {

        private ChoiceForm moveChoiceForm;

        //TODO refactor: use composition instead of inheritance
        /// <summary>
        /// Construct Move button.
        /// </summary>
        /// <param name="ccToolbar"></param>
        /// ref icon: http://www.openclipart.org/detail/30475
        public MoveCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Move", "Icons/move.png")
        {
            
        }

        //TODO bug, since we dont create the form from scratch each time, the button sizes dont get updated
        private ChoiceForm createChoiceForm()
        {
            if (this.moveChoiceForm == null || this.moveChoiceForm.IsDisposed == true)
            {
                this.moveChoiceForm = new ChoiceForm(this.ccToolbar, 4, false, false);

                //moveChoiceForm.TransparencyKey = Color.Magenta;
                //moveChoiceForm.BackColor = Color.Magenta;

                moveChoiceForm.TopButton.SetIcon("Icons/arrow_up.png");
                moveChoiceForm.LeftButton.SetIcon("Icons/arrow_left.png");
                moveChoiceForm.RightButton.SetIcon("Icons/arrow_right.png");
                moveChoiceForm.BottomButton.SetIcon("Icons/arrow_down.png");

                moveChoiceForm.BottomButton.Click += new EventHandler(BottomButton_Click);

            }
            return this.moveChoiceForm;
        }

        void BottomButton_Click(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// On Click: Change CCMainForm's state to moving.
        /// </summary>
        public override void  ClickAction(object sender, EventArgs e)
        {
            //TODO combine these ccform methods into one
            this.ccMainForm.resetState();
            this.ccMainForm.MainImage.pasting = false;

            //display instructions for move arrows
            ccMainForm.ShowMessage("Put mouse in arrows to move image");

            ccMainForm.State = CCMainForm.CCFormState.Moving;
            this.ccMainForm.getImagePanel().Invalidate();

            //ChoiceForm cf = this.createChoiceForm();
            ////cf.ShowDialog();
            //cf.Show();
            //cf.BringToFront();
        }

    }
}

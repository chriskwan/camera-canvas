using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms; //for PictureBox
using System.Drawing; //for Point
using CameraCanvas.Toolbar;

namespace CameraCanvas.ImageTools
{
    public class RotateChoiceForm : ChoiceForm
    {
        private float rotateAngle = 0f;

        public RotateChoiceForm(CCToolbar ccToolbar)
            : base(ccToolbar, 4, true, true)
        {
            this.label.Text = "Rotation Angle: " + rotateAngle + " degrees";
            
            //top: cancel
            this.SetButtonToCancel(this.topBtn);

            this.leftBtn.Text = "Rotate Left";
            //ref icon: http://www.openclipart.org/detail/27401
            this.leftBtn.SetIcon("Icons/RotateLeft.png");
            this.leftBtn.actionEvent += new CCButton.actionEventHandler(leftBtn_actionEvent);

            this.rightBtn.Text = "Rotate Right";
            //ref icon: http://www.openclipart.org/detail/27401
            this.rightBtn.SetIcon("Icons/RotateRight.png");
            this.rightBtn.actionEvent += new CCButton.actionEventHandler(rightBtn_actionEvent);
            
            //bottom: apply
            this.SetButtonIconToApply(this.bottomBtn);
            this.bottomBtn.actionEvent += new CCButton.actionEventHandler(applyBtn_actionEvent);
            
            previewPicBox.Image = ccToolbar.CCMainForm.MainImage.previewRotation(rotateAngle);
        }

        /// <summary>
        /// On Click: Apply the rotation to the actual image.
        /// </summary>
        void applyBtn_actionEvent()
        {
            if (rotateAngle != 0f)
            {
                //TODO refactor
                this.ccToolbar.CCMainForm.takeSnap("image rotated");
                this.ccToolbar.CCMainForm.MainImage.rotate(rotateAngle);
                this.ccToolbar.CCMainForm.centerImage();
                this.ccToolbar.CCMainForm.MainImage.Invalidate();
                this.ccToolbar.CCMainForm.getImagePanel().Invalidate();
            }
            this.ccToolbar.CCMainForm.HidePreviewBox();

            this.Close();
        }

        /// <summary>
        /// On Click: Rotate the preview 270 degrees clockwise (90 counter clockwise)
        /// </summary>
        void leftBtn_actionEvent()
        {
            changeRotationAngle(270);
        }

        /// <summary>
        /// On Click: Rotate the preview 90 degrees clockwise.
        /// </summary>
        void rightBtn_actionEvent()
        {
            changeRotationAngle(90);
        }

        /// <summary>
        /// Change the rotation angle by specified degrees.
        /// </summary>
        /// <param name="degrees"></param>
        private void changeRotationAngle(float degrees)
        {
            rotateAngle = (rotateAngle + degrees) % 360;
            this.label.Text = "Rotation Angle: " + rotateAngle + " degrees";
            previewPicBox.Image = ccToolbar.CCMainForm.MainImage.previewRotation(rotateAngle);
        }

    }//end class RotateChoiceForm

    /// <summary>
    /// Defines the Rotate button in the Image tools on the toolbar.
    /// </summary>
    /// ref icon: http://www.openclipart.org/detail/27401
    public class RotateCCButton : CCButton
    {
        public RotateCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Rotate", "Icons/rotate.png"){}

        /// <summary>
        /// On Click: Change the toolbar to rotation angles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ClickAction(object sender, EventArgs e)
        {
            //TODO refactor
            this.ccMainForm.resetState();

            RotateChoiceForm rotateChoiceForm = new RotateChoiceForm(ccToolbar);
            rotateChoiceForm.ShowDialog();

            //TODO refactor
            this.ccMainForm.State = CCMainForm.CCFormState.Etc;
        }

    }//end class RotateCCButton
}//end namespace

using System;

namespace CameraCanvas.EffectsTools.RotationColorTools
{
    public class BlueRotationCCButtonChoiceForm : ChoiceForm
    {
        private float rotationDegree = 0;

        private static int rotationDegreeStep = 5;
        public BlueRotationCCButtonChoiceForm(CCToolbar toolbar)
            : base(toolbar, 4, true, true)
        {

            this.label.Text = "Rotating Degree :" + rotationDegree;
            //top: cancel
            this.SetButtonToCancel(this.topBtn);

            this.leftBtn.Text = "Decrease";
            this.leftBtn.actionEvent += new CCButton.actionEventHandler(leftBtn_actionEvent);

            this.rightBtn.Text = "Increase";
            this.rightBtn.actionEvent += new CCButton.actionEventHandler(rightBtn_actionEvent);

            //bottom: apply
            this.SetButtonIconToApply(bottomBtn);
            this.bottomBtn.actionEvent += new CCButton.actionEventHandler(applyBtn_actionEvent);

            previewPicBox.Image = ccToolbar.CCMainForm.MainImage.BlueRotationPreview(this.rotationDegree);
        }

        private void applyBtn_actionEvent()
        {
            //TODO refactor
            this.ccToolbar.CCMainForm.takeSnap("Blue Rotation changed");
            this.ccToolbar.CCMainForm.MainImage.RotateColorFromBlue(rotationDegree);

            //TODO refactor
            this.ccToolbar.CCMainForm.centerImage();
            this.ccToolbar.CCMainForm.MainImage.Invalidate();
            this.ccToolbar.CCMainForm.getImagePanel().Invalidate();

            this.Close();
        }

        private void leftBtn_actionEvent()
        {
            changeRotationDegree(-1 * (float)rotationDegreeStep);
        }
        private void rightBtn_actionEvent()
        {
            changeRotationDegree(1 * (float)rotationDegreeStep);
        }

        private void changeRotationDegree(float value)
        {
            this.rotationDegree += value;
            this.label.Text = "Rotating Degree :" + rotationDegree;
            previewPicBox.Image = ccToolbar.CCMainForm.MainImage.BlueRotationPreview(this.rotationDegree);
        }
    }//end class BlueRotationCCButtonChoiceForm

    public class BlueRotationCCButton : CCButton
    {
        public BlueRotationCCButton(CCToolbar toolbar)
            : base(toolbar, "Blue Rotation", "Icons/brightness.png") { }

        public override void ClickAction(object sender, EventArgs e)
        {
            BlueRotationCCButtonChoiceForm BlueRotationForm = new BlueRotationCCButtonChoiceForm(ccToolbar);
            BlueRotationForm.ShowDialog();

            this.ccMainForm.State = CCMainForm.CCFormState.Etc;
        }

    }//end BlueRotationCCButton
}

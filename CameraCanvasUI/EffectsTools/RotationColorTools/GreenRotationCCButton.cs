using System;


namespace CameraCanvas.EffectsTools.RotationColorTools
{
    public class GreenRotationCCButtonChoiceForm : ChoiceForm
    {
        private float rotationDegree = 0;

        private static int rotationDegreeStep = 5;
        public GreenRotationCCButtonChoiceForm(CCToolbar toolbar)
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

            previewPicBox.Image = ccToolbar.CCMainForm.MainImage.GreenRotationPreview(this.rotationDegree);
        }

        private void applyBtn_actionEvent()
        {
            //TODO refactor
            this.ccToolbar.CCMainForm.takeSnap("Green Rotation changed");
            this.ccToolbar.CCMainForm.MainImage.RotateColorFromGreen(rotationDegree);

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
            previewPicBox.Image = ccToolbar.CCMainForm.MainImage.GreenRotationPreview(this.rotationDegree);
        }
    }//end class GreenRotationCCButtonChoiceForm

    public class GreenRotationCCButton : CCButton
    {
        public GreenRotationCCButton(CCToolbar toolbar)
            : base(toolbar, "Green Rotation", "Icons/brightness.png") { }

        public override void ClickAction(object sender, EventArgs e)
        {
            GreenRotationCCButtonChoiceForm greenRotationForm = new GreenRotationCCButtonChoiceForm(ccToolbar);
            greenRotationForm.ShowDialog();

            this.ccMainForm.State = CCMainForm.CCFormState.Etc;
        }

    }//end GreenRotationCCButton
}

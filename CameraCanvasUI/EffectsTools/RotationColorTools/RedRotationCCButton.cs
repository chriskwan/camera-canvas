
using System;

namespace CameraCanvas.EffectsTools.RotationColorTools
{
    public class RedRotationCCButtonChoiceForm : ChoiceForm
    {
        private float rotationDegree = 0;

        private static int rotationDegreeStep = 5;
        public RedRotationCCButtonChoiceForm(CCToolbar toolbar)
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

            previewPicBox.Image = ccToolbar.CCMainForm.MainImage.RedRotationPreview(this.rotationDegree);
        }

        private void applyBtn_actionEvent()
        {
            //TODO refactor
            this.ccToolbar.CCMainForm.takeSnap("Red Rotation changed");
            this.ccToolbar.CCMainForm.MainImage.RotateColorFromRed(rotationDegree);

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
            previewPicBox.Image = ccToolbar.CCMainForm.MainImage.RedRotationPreview(this.rotationDegree);
        }
    }//end class RedRotationCCButtonChoiceForm

    public class RedRotationCCButton : CCButton
    {
        public RedRotationCCButton(CCToolbar toolbar)
            : base(toolbar, "Red Rotation", "Icons/brightness.png") { }

        public override void ClickAction(object sender, EventArgs e)
        {
            RedRotationCCButtonChoiceForm redRotationForm = new RedRotationCCButtonChoiceForm(ccToolbar);
            redRotationForm.ShowDialog();

            this.ccMainForm.State = CCMainForm.CCFormState.Etc;
        }

        }//end RedRotationCCButton
    }

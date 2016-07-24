using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas.EffectsTools
{

    public class ColorThresholdingChoiceForm : ChoiceForm
    {
        private int brightnessValue = 0;
        private int brightnessChangeStep = 20;

        public ColorThresholdingChoiceForm(CCToolbar ccToolbar)
            : base(ccToolbar, 4, true, true)
        {
            this.label.Text = "Brightness: " + brightnessValue;

            //top: cancel
            this.SetButtonToCancel(this.topBtn);

            this.leftBtn.Text = "Darker";
            this.leftBtn.actionEvent += new CCButton.actionEventHandler(leftBtn_actionEvent);

            this.rightBtn.Text = "Lighter";
            this.rightBtn.actionEvent += new CCButton.actionEventHandler(rightBtn_actionEvent);

            //bottom: apply
            this.SetButtonIconToApply(bottomBtn);
            this.bottomBtn.actionEvent += new CCButton.actionEventHandler(applyBtn_actionEvent);

            previewPicBox.Image = ccToolbar.CCMainForm.MainImage.BrightenPreview(this.brightnessValue);
        }

        /// <summary>
        /// On Click: Apply the brightness change to the actual image.
        /// </summary>
        private void applyBtn_actionEvent()
        {
            //TODO refactor
            this.ccToolbar.CCMainForm.takeSnap("Brightness Changed");
            this.ccToolbar.CCMainForm.MainImage.Brighten(this.brightnessValue);

            //TODO refactor
            this.ccToolbar.CCMainForm.centerImage();
            this.ccToolbar.CCMainForm.MainImage.Invalidate();
            this.ccToolbar.CCMainForm.getImagePanel().Invalidate();

            this.Close();
        }

        /// <summary>
        /// On Click: Make the preview image darker.
        /// </summary>
        private void leftBtn_actionEvent()
        {
            changeBrightnessValue(-1 * brightnessChangeStep);
        }

        /// <summary>
        /// On Click: Make the preview image lighter.
        /// </summary>
        private void rightBtn_actionEvent()
        {
            changeBrightnessValue(brightnessChangeStep);
        }

        /// <summary>
        /// Change the brightness value darker or lighter.
        /// </summary>
        /// <param name="value"></param>
        private void changeBrightnessValue(int value)
        {
            this.brightnessValue += value;
            this.label.Text = "Brightness: " + brightnessValue;
            previewPicBox.Image = ccToolbar.CCMainForm.MainImage.BrightenPreview(this.brightnessValue);
        }
    }//end class BrightnessChoiceForm


    /// <summary>
    /// Defines the Brightness button in the Effects tools on the toolbar.
    /// </summary>
    public class ColorThresholding : CCButton
    {
        public ColorThresholding(CCToolbar ccToolbar)
            : base(ccToolbar, "Brightness", "Icons/brightness.png") { }

        /// <summary>
        /// On Click: Change toolbar buttons to brightness options.
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            BrightnessChoiceForm brightForm = new BrightnessChoiceForm(ccToolbar);
            brightForm.ShowDialog();

            this.ccMainForm.State = CCMainForm.CCFormState.Etc;
        }
    }//end class BrightnessCCButton
}

using System;


namespace CameraCanvas.EffectsTools
{
    public class ContrastChoiceForm
    {
        private CCToolbar ccToolbar;
        private ChoiceForm choiceForm;
        private int contrastValue = 0;
        private int contrastChangeStep = 20;

        public ChoiceForm ChoiceForm
        {
            get { return createChoiceForm(); }
        }

        public ContrastChoiceForm(CCToolbar ccToolbar)
        {
            this.ccToolbar = ccToolbar;
            createChoiceForm();
        }

        ChoiceForm createChoiceForm()
        {
            if (this.choiceForm == null)
            {
                choiceForm = new ChoiceForm(ccToolbar, 4, true, true);
                choiceForm.Label.Text = "Contrast: " + this.contrastValue;
                
                //top: cancel
                choiceForm.SetButtonToCancel(choiceForm.TopButton);

                choiceForm.LeftButton.Text = "Less";
                choiceForm.LeftButton.Click += new EventHandler(LeftButton_Click);

                choiceForm.RightButton.Text = "More";
                choiceForm.RightButton.Click += new EventHandler(RightButton_Click);

                //bottom: apply
                choiceForm.SetButtonIconToApply(choiceForm.BottomButton);
                choiceForm.BottomButton.Click += new EventHandler(ApplyButton_Click);

                choiceForm.PreviewImage = ccToolbar.CCMainForm.MainImage.ContrastPreview(this.contrastValue);
            }
            return choiceForm;
        }

        /// <summary>
        /// On Click: apply the contrast change to the actual image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ApplyButton_Click(object sender, EventArgs e)
        {
            //TODO refactor
            this.ccToolbar.CCMainForm.takeSnap("Contrast Changed");
            this.ccToolbar.CCMainForm.MainImage.Contrast(this.contrastValue);

            //TODO refactor
            this.ccToolbar.CCMainForm.centerImage();
            this.ccToolbar.CCMainForm.MainImage.Invalidate();
            this.ccToolbar.CCMainForm.getImagePanel().Invalidate();

            choiceForm.Close();
        }

        /// <summary>
        /// On Click: make preview image have lower contrast.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LeftButton_Click(object sender, EventArgs e)
        {
            changeContrastValue(-1 * contrastChangeStep);
        }

        /// <summary>
        /// On Click: make preview image have higher contrast.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RightButton_Click(object sender, EventArgs e)
        {
            changeContrastValue(contrastChangeStep);
        }

        void changeContrastValue(int newValue)
        {
            this.contrastValue += newValue;
            choiceForm.Label.Text = "Contrast: " + this.contrastValue;
            choiceForm.PreviewImage = ccToolbar.CCMainForm.MainImage.ContrastPreview(this.contrastValue);
        }
    }

    /// <summary>
    /// Defines the Contrast button in the Effects Tools on the toolbar.
    /// </summary>
    public class ContrastCCButton : CCButton
    {
        public ContrastCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Contrast", "Icons/contrast.png"){}

        /// <summary>
        /// On Click: change the toolbar buttons to Contrast values.
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            ContrastChoiceForm contrastForm = new ContrastChoiceForm(ccToolbar);
            contrastForm.ChoiceForm.ShowDialog();

            this.ccMainForm.State = CCMainForm.CCFormState.Etc;    
        }

    }
}

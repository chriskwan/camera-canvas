using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CameraCanvas.EffectsTools;
using CameraCanvas.Toolbar;

namespace CameraCanvas.Toolbox
{
    /// <summary>
    /// Defines Effects Button in the MainMenu in the CCToolbar.
    /// </summary>
    class EffectsCCButton : CCButton
    {
        List<CCButton> effectsToolbarButtons;

        public EffectsCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "&Effects", "Icons/Effects.png")
        {
            effectsToolbarButtons = new List<CCButton>();

            effectsToolbarButtons.Add(ccToolbar.UndoButton);
            effectsToolbarButtons.Add(ccToolbar.RedoButton);

            //TODO change these to use ChoiceForm's instead
            BrightnessCCButton brightnessBtn = new BrightnessCCButton(this.ccToolbar);
            effectsToolbarButtons.Add(brightnessBtn);

            ContrastCCButton contrastBtn = new ContrastCCButton(this.ccToolbar);
            effectsToolbarButtons.Add(contrastBtn);

            CCButton invertColorsBtn = new CCButton(this.ccToolbar, "Invert Colors", "Icons/InvertColors.png");
            invertColorsBtn.Click += new EventHandler(invertColorsBtn_Click);
            effectsToolbarButtons.Add(invertColorsBtn);

            CCButton grayscaleBtn = new CCButton(this.ccToolbar, "Grayscale", "Icons/Grayscale.png");
            grayscaleBtn.Click += new EventHandler(grayscaleBtn_Click);
            effectsToolbarButtons.Add(grayscaleBtn);
        }

        void grayscaleBtn_Click(object sender, EventArgs e)
        {
            //TODO refactor
            this.ccToolbar.CCMainForm.takeSnap("Grayscale");
            ccMainForm.MainImage.Grayscale();

            //TODO refactor
            this.ccToolbar.CCMainForm.centerImage();
            this.ccToolbar.CCMainForm.MainImage.Invalidate();
            this.ccToolbar.CCMainForm.getImagePanel().Invalidate();
        }

        void invertColorsBtn_Click(object sender, EventArgs e)
        {
            //TODO refactor
            this.ccToolbar.CCMainForm.takeSnap("Colors Inverted");
            ccMainForm.MainImage.InvertColors();

            //TODO refactor
            this.ccToolbar.CCMainForm.centerImage();
            this.ccToolbar.CCMainForm.MainImage.Invalidate();
            this.ccToolbar.CCMainForm.getImagePanel().Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            //TODO refactor
            this.ccMainForm.resetState();

            ccToolbar.AddNewToolbarButtons(effectsToolbarButtons, "Effects");
        }

    }
}

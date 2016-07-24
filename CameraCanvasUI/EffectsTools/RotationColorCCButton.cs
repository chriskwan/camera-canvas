using System;
using System.Collections.Generic;
using CameraCanvas.EffectsTools.RotationColorTools;

namespace CameraCanvas.EffectsTools
{
    class RotationColorCCButton : CCButton{

        List<CCButton> rotationColorToolbarButtons; 

        public RotationColorCCButton(CCToolbar toolbar)
            :base (toolbar, "&Rotating Color", "Icons/Effects.png")
        {
            rotationColorToolbarButtons = new List<CCButton>();

            rotationColorToolbarButtons.Add(toolbar.UndoButton);
            rotationColorToolbarButtons.Add(toolbar.RedoButton);

            RedRotationCCButton redRotBtn = new RedRotationCCButton(this.ccToolbar);
            rotationColorToolbarButtons.Add(redRotBtn);

            BlueRotationCCButton blueRotBtn = new BlueRotationCCButton(this.ccToolbar);
            rotationColorToolbarButtons.Add(blueRotBtn);

            GreenRotationCCButton greenRotBtn = new GreenRotationCCButton(this.ccToolbar);
            rotationColorToolbarButtons.Add(greenRotBtn);
        }
       
        public override void ClickAction(object sender, EventArgs e)
        {
            //TODO refactor
            this.ccMainForm.resetState();

            ccToolbar.AddNewToolbarButtons(rotationColorToolbarButtons, "Color Rotation");
        }
    }//end RotationColorCCButton
}

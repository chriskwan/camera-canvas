using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CameraCanvas.ImageTools;
using CameraCanvas.FileTools;
using CameraCanvas.Toolbar;
using CameraCanvas.DrawingTools;

namespace CameraCanvas.Toolbox
{
    /// <summary>
    /// Defines Image Button in the MainMenu in the CCToolbar.
    /// </summary>
    class ImageCCButton : CCButton
    {
        List<CCButton> imageToolbarButtons;

        public ImageCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "&Photos", "Icons/Photos.png")
        {
            imageToolbarButtons = new List<CCButton>();

            FileCCButton fileCCBtn = new FileCCButton(this.ccToolbar);
            imageToolbarButtons.Add(fileCCBtn);

            imageToolbarButtons.Add(ccToolbar.UndoButton);
            imageToolbarButtons.Add(ccToolbar.RedoButton);

            MoveCCButton moveCCBtn = new MoveCCButton(this.ccToolbar);
            imageToolbarButtons.Add(moveCCBtn);

            ZoomCCButton zoomCCBtn = new ZoomCCButton(this.ccToolbar);
            imageToolbarButtons.Add(zoomCCBtn);

            RotateCCButton rotateCCBtn = new RotateCCButton(this.ccToolbar);
            imageToolbarButtons.Add(rotateCCBtn);

            SelectCCButton selectCCBtn = new SelectCCButton(this.ccToolbar);
            imageToolbarButtons.Add(selectCCBtn);

            EffectsCCButton effectsCCBtn = new EffectsCCButton(this.ccToolbar);
            imageToolbarButtons.Add(effectsCCBtn);

            TextCCButton textCCBtn = new TextCCButton(this.ccToolbar);
            imageToolbarButtons.Add(textCCBtn);
            
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            ccToolbar.AddNewToolbarButtons(imageToolbarButtons, "Photos"); 
        
        }
    }
}

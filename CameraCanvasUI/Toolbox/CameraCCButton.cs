using System;
using System.Collections.Generic;
using CameraCaptureWithHAARCascade;
namespace CameraCanvas.Toolbox
{
    /// <summary>
    /// Defines Camera Button in the MainMenu in the CCToolbar.
    /// </summary>
    public class CameraCCButton : CCButton
    {

        List<CCButton> cameraToolbarButtons;

        public CameraCCButton(CCToolbar ccToolbar) 
            :base(ccToolbar, "&Camera", "Icons/Photos.png")
        {
            cameraToolbarButtons.Add(CameraCapture);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            ccToolbar.AddNewToolbarButtons(cameraToolbarButtons, "Camera");

        }
    }
}

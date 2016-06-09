using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CameraCanvas.DrawingTools
{
    class PencilCCButton : CCButton
    {
        //reference to main canvas

        List<CCButton> pencilToolbarButtons; //list of buttons in subtoolbar

        public PencilCCButton(CCToolbar toolbar)
            : base(toolbar, "Pencil", "Icons/pencil.png")
        {
            pencilToolbarButtons = new List<CCButton>();

            ColorCCButton colorCCBtn = new ColorCCButton(toolbar);
            pencilToolbarButtons.Add(colorCCBtn);


        }

        public override void ClickAction(object sender, EventArgs e)
        {
            //ccToolbar.AddNewToolbarButtons(pencilToolbarButtons, "Pencil");

            //ccMainForm.ShowMessage("Click on screen to draw");
            //ccMainForm.ShowMessage("Click to set start point");

            ccMainForm.Shape = CCMainForm.DrawingShape.Pencil;
            //ccMainForm.Shape = CCMainForm.DrawingShape.Circle;
            ccMainForm.State = CCMainForm.CCFormState.DrawingWait;

            //TODO refactor
            ccMainForm.UpdateDrawingDisplay();
        }

    }//end class
}//end namespace

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CameraCanvas.DrawingTools;
using CameraCanvas.FileTools;
using CameraCanvas.Toolbox;
using CameraCanvas.Toolbar;

namespace CameraCanvas.Toolbox
{
    /// <summary>
    /// Defines Drawing Button in the MainMenu in the CCToolbar.
    /// </summary>
    class DrawingCCButton : CCButton
    {
        List<CCButton> drawingToolbarButtons;

        public DrawingCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "&Drawing", "Icons/Drawing.png")
        {
            drawingToolbarButtons = new List<CCButton>();
            drawingToolbarButtons.Add(new FileCCButton(ccToolbar));
            drawingToolbarButtons.Add(ccToolbar.UndoButton);
            drawingToolbarButtons.Add(ccToolbar.RedoButton);
            drawingToolbarButtons.Add(new PencilCCButton(ccToolbar));
            drawingToolbarButtons.Add(new ShapesCCButton(ccToolbar));
            drawingToolbarButtons.Add(new ColorCCButton(ccToolbar));

            CCButton lineThickCCBtn = new CCButton(ccToolbar, "Line Thickness", "Icons/Thickness.png");
            lineThickCCBtn.actionEvent += new actionEventHandler(lineThickCCBtn_actionEvent);
            drawingToolbarButtons.Add(lineThickCCBtn);

            drawingToolbarButtons.Add(new TextCCButton(ccToolbar));
        }

        void lineThickCCBtn_actionEvent()
        {
            LineThicknessChoiceForm lineThickChoiceForm = new LineThicknessChoiceForm(ccToolbar);
            lineThickChoiceForm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {      
            ccToolbar.AddNewToolbarButtons(drawingToolbarButtons, "Drawing");

            //DrawingDisplayForm drawingDisplayControl = new DrawingDisplayForm(this.ccMainForm);
            
            //drawingDisplayControl.Show();
            
            //TODO refactor
            //ccMainForm.DrawingDisplay.Show();
            ccToolbar.CCMainForm.UpdateDrawingDisplay();

            //Reset zoom to 100% so we don't have to scale drawing helper box
            /*
             * Addresses this issue:
             * if image is zoomed in, helper box should be larger
             * if image is zoomed out, helper box should be smaller,
             * but this may be too small for user
            */
            if (ccToolbar.CCMainForm.Zoom != 1.0f)
            {
                ccToolbar.CCMainForm.Zoom = 1.0f;
                ccToolbar.CCMainForm.centerImage(); //centering will cause image to refresh
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CameraCanvas.DrawingTools
{
    class ShapesCCButton : CCButton
    {
        List<CCButton> drawingToolbarButtons;

        public ShapesCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Shapes", "Icons/Shapes.png")
        {
            drawingToolbarButtons = new List<CCButton>();

            //TODO refactor
            CCButton lineCCBtn = new CCButton(ccToolbar, "Line", "Icons/Line.png");
            lineCCBtn.actionEvent += new actionEventHandler(lineCCBtn_actionEvent);
            drawingToolbarButtons.Add(lineCCBtn);

            //TODO refactor
            CCButton rectCCBtn = new CCButton(ccToolbar, "Rectangle", "Icons/Rectangle.png");
            rectCCBtn.actionEvent += new actionEventHandler(rectCCBtn_actionEvent);
            drawingToolbarButtons.Add(rectCCBtn);

            //TODO refactor
            CCButton filledRectCCBtn = new CCButton(ccToolbar, "Filled Rectangle", "Icons/FilledRectangle.png");
            filledRectCCBtn.actionEvent += new actionEventHandler(filledRectCCBtn_actionEvent);
            drawingToolbarButtons.Add(filledRectCCBtn);

            //TODO refactor
            CCButton circleCCBtn = new CCButton(ccToolbar, "Circle", "Icons/Circle.png");
            circleCCBtn.actionEvent += new actionEventHandler(circleCCBtn_actionEvent);
            drawingToolbarButtons.Add(circleCCBtn);

            //TODO refactor
            CCButton filledCircleCCBtn = new CCButton(ccToolbar, "Filled Circle", "Icons/FilledCircle.png");
            filledCircleCCBtn.actionEvent += new actionEventHandler(filledCircleCCBtn_actionEvent);
            drawingToolbarButtons.Add(filledCircleCCBtn);

            this.Click += new EventHandler(ShapesCCButton_Click);
        }

        void ShapesCCButton_Click(object sender, EventArgs e)
        {
            ccToolbar.AddNewToolbarButtons(this.drawingToolbarButtons, "Shapes");
            
        }



        void filledCircleCCBtn_actionEvent()
        {
            shapeActionEvent(CCMainForm.DrawingShape.FilledCircle);
        }

        void filledRectCCBtn_actionEvent()
        {
            shapeActionEvent(CCMainForm.DrawingShape.FilledRectangle);
        }

        void lineThickCCBtn_actionEvent()
        {
            LineThicknessChoiceForm lineThickChoiceForm = new LineThicknessChoiceForm(ccToolbar);
            lineThickChoiceForm.ShowDialog();
        }

        void circleCCBtn_actionEvent()
        {
            shapeActionEvent(CCMainForm.DrawingShape.Circle);
        }

        void rectCCBtn_actionEvent()
        {
            shapeActionEvent(CCMainForm.DrawingShape.Rectangle);
        }

        void lineCCBtn_actionEvent()
        {
            shapeActionEvent(CCMainForm.DrawingShape.Line);
        }

        void shapeActionEvent(CCMainForm.DrawingShape shape)
        {
            //ccMainForm.ShowMessage("Click on screen to draw");
            ccMainForm.Shape = shape;
            ccMainForm.State = CCMainForm.CCFormState.DrawingWait;
            ccToolbar.RemoveCurrentToolbarButtons();

            //ccMainForm.DrawingDisplay.SetToolIcon(this.enabledIconPath);
            //TODO refactor
            ccMainForm.UpdateDrawingDisplay();
        }


    }
}

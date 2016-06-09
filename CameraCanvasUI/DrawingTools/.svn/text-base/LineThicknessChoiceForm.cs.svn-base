using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using CameraCanvas.Toolbar.Config;

namespace CameraCanvas.DrawingTools
{
    class LineThicknessChoiceForm : ChoiceForm
    {
        private Bitmap previewImage;
        private float thicknessChangeStep = 5.0f; 

        public LineThicknessChoiceForm(CCToolbar ccToolbar) 
            : base(ccToolbar, 2, true, false)
        {
            this.label.Text = "Line Thickness: " + ccToolbar.CCMainForm.LineThickness;
            this.leftBtn.Text = "Less";
            this.rightBtn.Text = "More";

            leftBtn.Click += new EventHandler(button1_Click);
            rightBtn.Click += new EventHandler(button2_Click);

            //initialize preview image and draw for first time
            previewImage = new Bitmap(previewPanel.Width, previewPanel.Height);
            DrawPreview();
        }

        /// <summary>
        /// On click: make the line thinner.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //cannot have a 0 or negative line thickness
            if (ccToolbar.CCMainForm.LineThickness > 1)
            {
                this.ccToolbar.CCMainForm.LineThickness -= thicknessChangeStep;
                this.label.Text = "Line Thickness: " + ccToolbar.CCMainForm.LineThickness;
                DrawPreview();
                
                //TODO refactor
                ccToolbar.CCMainForm.State = CCMainForm.CCFormState.DrawingWait;
                ccToolbar.CCMainForm.UpdateDrawingDisplay();
            }
        }

        /// <summary>
        /// On Click: make the line thicker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.ccToolbar.CCMainForm.LineThickness += thicknessChangeStep;
            this.label.Text = "Line Thickness: " + ccToolbar.CCMainForm.LineThickness;
            DrawPreview();

            //TODO refactor
            ccToolbar.CCMainForm.State = CCMainForm.CCFormState.DrawingWait;
            ccToolbar.CCMainForm.UpdateDrawingDisplay();
        }

        /// <summary>
        /// Draw a preview of the current line thickness.
        /// </summary>
        private void DrawPreview()
        {
            Graphics g = Graphics.FromImage(this.previewImage);
            Pen pen = new Pen(Color.Black, ccToolbar.CCMainForm.LineThickness);
            g.Clear(previewPanel.BackColor);
            g.DrawLine(pen, new Point(previewPanel.Width / 4, previewPanel.Height / 2),
               new Point(previewPanel.Width / 2 + previewPanel.Width / 4, previewPanel.Height / 2));
            pen.Dispose();
            g.Dispose();

            previewPanel.BackgroundImage = this.previewImage;
            previewPanel.Refresh();
        }
    }
}

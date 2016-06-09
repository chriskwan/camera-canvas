using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas
{
    public partial class DrawingDisplayControl : UserControl
    {
        private CCMainForm ccMainForm;
        private CCMainForm.DrawingShape shape;
        private float thickness;
        private Color color;
        private Bitmap thicknessPreviewImage;

        public DrawingDisplayControl(CCMainForm ccMainForm)
        {
            InitializeComponent();
            this.ccMainForm = ccMainForm;

            //buttons in the control only for display and should not be clickable
            foreach (CCButton button in this.Controls)
            {
                button.Disable();
            }

            this.thicknessPreviewImage = new Bitmap(this.thicknessBtn.Width, this.thicknessBtn.Height);
        }

        /// <summary>
        /// Update the icons of the Drawing Display control
        /// to match current settings of the main form.
        /// </summary>
        public void UpdateDisplay()
        {
            UpdateShape();
            UpdateThickness();
            UpdateColor();
        }

        /// <summary>
        /// Helper method: Update the icon of the Shape button.
        /// </summary>
        private void UpdateShape()
        {
            //skip updating if shape is unchanged and already has an icon
            if (this.shape == ccMainForm.Shape && this.shapeBtn.BackgroundImage != null)
            {
                return;
            }

            //change display to current shape
            this.shape = this.ccMainForm.Shape;
            string shapeIconPath = "";
            switch (this.shape)
            {
                case CCMainForm.DrawingShape.Circle:
                    shapeIconPath = "Icons/Circle.png";
                    break;
                case CCMainForm.DrawingShape.FilledCircle:
                    shapeIconPath = "Icons/FilledCircle.png";
                    break;
                case CCMainForm.DrawingShape.FilledRectangle:
                    shapeIconPath = "Icons/FilledRectangle.png";
                    break;
                case CCMainForm.DrawingShape.Line:
                    shapeIconPath = "Icons/Line.png";
                    break;
                case CCMainForm.DrawingShape.Pencil:
                    shapeIconPath = "Icons/Pencil.png";
                    break;
                case CCMainForm.DrawingShape.Rectangle:
                    shapeIconPath = "Icons/Rectangle.png";
                    break;
                default:
                    break;
            }//end switch

            if (shapeIconPath != "")
            {
                this.shapeBtn.SetIcon(shapeIconPath);
            }

            //hide the thickness display if shape does not have thickness
            if (this.shape == CCMainForm.DrawingShape.FilledCircle
                || this.shape == CCMainForm.DrawingShape.FilledRectangle)
            {
                this.thicknessBtn.Hide();
            }
            else
            {
                this.thicknessBtn.Show();
            }
        }

        /// <summary>
        /// Helper method: Update the icon of the thickness button.
        /// </summary>
        private void UpdateThickness()
        {
            if (this.thickness == this.ccMainForm.LineThickness && this.thicknessBtn.Text == "")
            {
                return;
            }

            this.thickness = this.ccMainForm.LineThickness;

            //update the thickness preview image
            Graphics g = Graphics.FromImage(thicknessPreviewImage);
            Pen pen = new Pen(Color.Black, this.ccMainForm.LineThickness);
            g.Clear(thicknessBtn.BackColor);
            g.DrawString("Thickness: " + this.thickness, this.thicknessBtn.Font, Brushes.Black, new PointF(0.0f, 0.0f));
            g.DrawLine(pen, new Point(this.thicknessBtn.Width / 4, this.thicknessBtn.Height / 2),
             new Point(this.thicknessBtn.Width / 2 + this.thicknessBtn.Width / 4, this.thicknessBtn.Height / 2));
            pen.Dispose();
            g.Dispose();
            thicknessBtn.BackgroundImage = thicknessPreviewImage;
            thicknessBtn.Invalidate();

        }

        /// <summary>
        /// Helper method: Update the icon of the color button.
        /// </summary>
        private void UpdateColor()
        {
            if (this.color == this.ccMainForm.SelectedColor)
            {
                return;
            }

            this.color = this.ccMainForm.SelectedColor;
            this.colorBtn.BackColor = this.color;
        }
    
    }
}

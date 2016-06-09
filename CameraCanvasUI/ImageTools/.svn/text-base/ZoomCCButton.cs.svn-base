using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CameraCanvas.ImageTools
{
    /// <summary>
    /// Defines Zoom button in Image tools on toolbar.
    /// </summary>
    public class ZoomCCButton : CCButton
    {
        private float zoomLevel = 1.0f;

        private ChoiceForm zoomChoiceForm;

        private ChoiceForm createChoiceForm()
        {
            if (this.zoomChoiceForm == null || this.zoomChoiceForm.IsDisposed == true)
            {
                zoomChoiceForm = new ChoiceForm(this.ccToolbar, 2, false, false);
                
                zoomChoiceForm.Label.Text = "Zoom: " + (this.zoomLevel * 100) + "%";
                zoomChoiceForm.LeftButton.Text = "Zoom Out";
                zoomChoiceForm.RightButton.Text = "Zoom In";

                //ref icon: http://www.openclipart.org/detail/38995
                zoomChoiceForm.LeftButton.SetIcon("Icons/ZoomOut.png");

                //ref icon: http://www.openclipart.org/detail/38989
                zoomChoiceForm.RightButton.SetIcon("Icons/ZoomIn.png");

                zoomChoiceForm.LeftButton.Click += new EventHandler(LeftButton_Click);
                zoomChoiceForm.RightButton.Click += new EventHandler(RightButton_Click);
                zoomChoiceForm.BottomButton.Click += new EventHandler(BottomButton_Click);
            }
            return this.zoomChoiceForm;
        }

        /// <summary>
        /// On Click: Zoom Out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftButton_Click(object sender, EventArgs e)
        {
            //if level <= 100%, zoom out = 0.5x
            //if level > 100%, zoom out = -100x
            adjustZoom(0.5f, -1.0f);
        }

        /// <summary>
        /// On Click: Zoom In
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightButton_Click(object sender, EventArgs e)
        {
            //if level <= 100%, zoom in = 2x
            //if level > 100%, zoom in = 100x
            adjustZoom(2.0f, 1.0f);
        }

        /// <summary>
        /// Helper method to adjust zoom to different level.
        /// </summary>
        /// <param name="levelAdjustment1"></param>
        /// <param name="levelAdjustment2"></param>
        private void adjustZoom(float levelAdjustment1, float levelAdjustment2)
        {
            if (zoomLevel <= 1.0f)
            {
                zoomLevel *= levelAdjustment1;
            }
            else if (zoomLevel > 1.0f)
            {
                zoomLevel += levelAdjustment2;
            }

            zoomChoiceForm.Label.Text = "Zoom: " + (this.zoomLevel * 100) + "%";
            
            this.ccMainForm.MainImage.zoom = zoomLevel;
            this.ccMainForm.centerImage();
            this.ccMainForm.MainImage.Invalidate();
            this.ccMainForm.getImagePanel().Invalidate();

        }

        /// <summary>
        /// On Click: Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BottomButton_Click(object sender, EventArgs e)
        {
            zoomChoiceForm.Close();
            ccMainForm.resetState();
        }

        //TODO refactor change from inheritance to composition
        /// <summary>
        /// Defines the Zoom button.
        /// </summary>
        /// <param name="ccToolbar"></param>
        /// ref icon: http://www.openclipart.org/detail/26947
        public ZoomCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Zoom", "Icons/zoom.png"){}

        /// <summary>
        /// On Click: Show Zoom choice form.
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            this.ccMainForm.State = CCMainForm.CCFormState.Etc;

            //TODO refactor
            this.ccMainForm.resetState();

            ChoiceForm zoomCF = createChoiceForm();
            zoomCF.Show();    //buttons will get resized
            zoomCF.TopMost = true;
            //zoomCF.ShowDialog(); //buttons will not get resized

            //TODO refactor, maybe move can be a choice form within the zoom choice form?
            //also show moving arrows so you can move around while zooming

            //display instructions for move arrows
            ccMainForm.ShowMessage("Put mouse in arrows to move image");

            ccMainForm.State = CCMainForm.CCFormState.Moving;
        }
    }
}

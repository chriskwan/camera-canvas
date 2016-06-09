using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas.Toolbar
{
    /// <summary>
    /// Button that slides the toolbar buttons.
    /// </summary>
    public abstract class SlideToButton : CCButton
    {
        //paths for button icons
        private string horizIconPath;
        private string horizGrayIconPath;
        private string vertIconPath;
        private string vertGrayIconPath;
        
        //if true toolbar slides with every click
        //if false toolbar automatically slides on a timer
        private bool isSlideOnClick = true;//false;

        private Timer autoSlideTimer; //for automatic sliding

        public SlideToButton(CCToolbar ccToolbar, string buttonName, 
            string horizIconPath, string horizGrayIconPath, 
            string vertIconPath, string vertGrayIconPath
            ) : base(ccToolbar, buttonName)
        {
            //initialize button image
            this.horizIconPath = horizIconPath;
            this.horizGrayIconPath = horizGrayIconPath;
            this.vertIconPath = vertIconPath;
            this.vertGrayIconPath = vertGrayIconPath;
            UpdateImage(); 

            //slide buttons on each click
            if (true == isSlideOnClick)
            {
                this.Click += new EventHandler(SlideToolbarButtons);
            }
            //slide buttons automatically as long as mouse is on button
            else
            {
                //initialize timer, interval is set on click
                autoSlideTimer = new Timer();
                autoSlideTimer.Tick += new EventHandler(SlideToolbarButtons);
                autoSlideTimer.Enabled = false;
                
                this.Click += new EventHandler(OnClick_ActivateSliding);
                this.MouseLeave += new EventHandler(OnMouseLeave_StopSliding);
            }    
        }

        /// <summary>
        /// Update icon of button based on toolbar orientation.
        /// </summary>
        public void UpdateImage()
        {
            if (ccToolbar.Orientation == CCToolbar.ToolbarOrientation.HorizontalTop
                || ccToolbar.Orientation == CCToolbar.ToolbarOrientation.HorizontalBottom)
            {
                enabledIconPath = horizIconPath;
                disabledIconPath = horizGrayIconPath;
            }
            else if (ccToolbar.Orientation == CCToolbar.ToolbarOrientation.VerticalLeft
                || ccToolbar.Orientation == CCToolbar.ToolbarOrientation.VerticalRight)
            {
                enabledIconPath = vertIconPath;
                disabledIconPath = vertGrayIconPath;
            }
            this.SetIcon(enabledIconPath);
            this.Text = ""; //no button text since we have an icon
        }

        /// <summary>
        /// On Click: Activate sliding if not already activated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnClick_ActivateSliding(object sender, EventArgs e)
        {
            if (false == autoSlideTimer.Enabled)
            {
                //get latest speed setting from toolbar
                autoSlideTimer.Interval = ccToolbar.AutoSlideInterval;

                //activate automatic sliding
                autoSlideTimer.Start();
            }
        }

        /// <summary>
        /// On Mouse Leave: Stop automatic sliding.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseLeave_StopSliding(object sender, EventArgs e)
        {
            autoSlideTimer.Stop();
        }

        /// <summary>
        /// Define the method of sliding the toolbar buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void SlideToolbarButtons(object sender, EventArgs e);

    }
}

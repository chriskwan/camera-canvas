using System;
using System.Collections.Generic;
using System.Text;

namespace CameraCanvas.Toolbar.Config
{
    class SlidingSpeedChoiceForm : ChoiceForm
    {
        public SlidingSpeedChoiceForm(CCToolbar ccToolbar)
            : base(ccToolbar, 2)
        {
            label.Text = "Speed: Slide every " + 0.001 * ccToolbar.AutoSlideInterval + " seconds";
            leftBtn.Text = "Slower";
            rightBtn.Text = "Faster";

            leftBtn.actionEvent += new CCButton.actionEventHandler(leftBtn_actionEvent);
            rightBtn.actionEvent += new CCButton.actionEventHandler(rightBtn_actionEvent);
            this.SetBottomButtonToClose();
        }

        /// <summary>
        /// On Click: Make the toolbar slide slower.
        /// </summary>
        private void leftBtn_actionEvent()
        {
            if (ccToolbar.AutoSlideInterval < 500)
            {
                //double the interval
                adjustSpeed(ccToolbar.AutoSlideInterval);
            }
            else
            {
                //intervals over 3000 ms are too slow
                if (ccToolbar.AutoSlideInterval < 3000)
                {
                    adjustSpeed(500);
                }
            }
        }

        /// <summary>
        /// On Click: Make the toolbar slide faster.
        /// </summary>
        private void rightBtn_actionEvent()
        {
            if (ccToolbar.AutoSlideInterval <= 500)
            {
                //intervals less than 125 ms are too fast
                if (ccToolbar.AutoSlideInterval > 125)
                {
                    //cut interval in half
                    adjustSpeed(-1 * (ccToolbar.AutoSlideInterval / 2));
                }
            }
            else
            {
                adjustSpeed(-500);
            }
        }

        /// <summary>
        /// Make the toolbar slide faster or slower.
        /// </summary>
        /// <param name="interval"></param>
        private void adjustSpeed(int interval)
        {
            ccToolbar.AutoSlideInterval += interval;
            label.Text = "Speed: Slide every " + 0.001 * ccToolbar.AutoSlideInterval + " seconds";           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CameraCanvas.Toolbar.Config
{
    class ToolbarPlacementForm : ChoiceForm
    {
        public ToolbarPlacementForm(CCToolbar ccToolbar)
            : base(ccToolbar, 4)
        {
            label.Text = "Toolbar Placement: " + ccToolbar.Orientation;

            topBtn.Text = "Top";
            topBtn.actionEvent += new CCButton.actionEventHandler(topBtn_actionEvent);

            bottomBtn.Text = "Bottom";
            bottomBtn.actionEvent += new CCButton.actionEventHandler(bottomBtn_actionEvent);

            leftBtn.Text = "Left";
            leftBtn.actionEvent += new CCButton.actionEventHandler(leftBtn_actionEvent);

            rightBtn.Text = "Right";
            rightBtn.actionEvent += new CCButton.actionEventHandler(rightBtn_actionEvent);
        }

        void rightBtn_actionEvent()
        {
            changeOrientation(CCToolbar.ToolbarOrientation.VerticalRight);
        }

        void leftBtn_actionEvent()
        {
            changeOrientation(CCToolbar.ToolbarOrientation.VerticalLeft);
        }

        void bottomBtn_actionEvent()
        {
            changeOrientation(CCToolbar.ToolbarOrientation.HorizontalBottom);
        }

        void topBtn_actionEvent()
        {
            changeOrientation(CCToolbar.ToolbarOrientation.HorizontalTop);
        }

        void changeOrientation(CCToolbar.ToolbarOrientation orientation)
        {
            ccToolbar.ChangeOrientation(orientation);
            label.Text = "Toolbar Placement: " + orientation;
            this.Close();
        }

    }
}

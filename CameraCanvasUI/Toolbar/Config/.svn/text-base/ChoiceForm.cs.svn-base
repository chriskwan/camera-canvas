using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CameraCanvas.Toolbar.Config;

namespace CameraCanvas
{
    
    public partial class ChoiceForm : Form
    {
        protected CCToolbar ccToolbar;
        protected Panel previewPanel;
        protected PictureBox previewPicBox;
        private int previewPicBoxWidth = 200;
        private int previewPicBoxHeight = 200;

        public Label Label
        {
            get { return this.label; }
        }

        public CCButton TopButton
        {
            get { return this.topBtn; }
        }

        public CCButton LeftButton
        {
            get { return this.leftBtn; }
        }

        public CCButton RightButton
        {
            get { return this.rightBtn; }
        }

        public CCButton BottomButton
        {
            get { return this.bottomBtn; }
        }

        public System.Drawing.Image PreviewImage
        {
            get { return this.previewPicBox.Image; }
            set { this.previewPicBox.Image = value; }
        }

        /// <summary>
        /// Construct a choice form.
        /// Choice forms begin in the center of the screen on top of all windows.
        /// </summary>
        public ChoiceForm()
        {
            InitializeComponent();
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.BackColor = Color.Transparent;
            //this.TransparencyKey = this.BackColor;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;            
            this.Text = "";
            this.ControlBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.BringToFront();
        }

        /// <summary>
        /// Construct a choice form with button sizes based on the toolbar.
        /// </summary>
        /// <param name="ccToolbar"></param>
        public ChoiceForm(CCToolbar ccToolbar, int numChoices)
            : this()
        {
            this.ccToolbar = ccToolbar;

            //center the choice form on top of the toolbar for easiest access
            this.StartPosition = FormStartPosition.Manual;
            int x = 0;
            int y = 0;

            switch (ccToolbar.Orientation)
            {
                case CCToolbar.ToolbarOrientation.HorizontalTop:
                    x = ccToolbar.Width / 2 - this.Width / 2;
                    y = Math.Abs(ccToolbar.Height / 2 - this.Height / 2);
                    break;
                case CCToolbar.ToolbarOrientation.HorizontalBottom:
                    x = ccToolbar.Width / 2 - this.Width / 2;
                    y = ccToolbar.Location.Y - this.Height / 2;
                    break;
                case CCToolbar.ToolbarOrientation.VerticalLeft:
                    x = ccToolbar.Width - this.Width / 2;
                    y = ccToolbar.Height / 2 - this.Height / 2;
                    break;
                case CCToolbar.ToolbarOrientation.VerticalRight:
                    x = ccToolbar.Location.X - this.Width / 2;
                    y = ccToolbar.Height / 2 - this.Height / 2;
                    break;
                default:
                    break;
            }

            //offset choiceform location by main form's location to account for main form moving
            x = this.ccToolbar.CCMainForm.Location.X + x;
            y = this.ccToolbar.CCMainForm.Location.Y + y;

            this.Location = new Point(x, y);

            this.topBtn.Size = new Size(ccToolbar.ButtonWidth, ccToolbar.ButtonHeight);
            this.leftBtn.Size = new Size(ccToolbar.ButtonWidth, ccToolbar.ButtonHeight);
            this.rightBtn.Size = new Size(ccToolbar.ButtonWidth, ccToolbar.ButtonHeight);
            this.bottomBtn.Size = new Size(ccToolbar.ButtonWidth, ccToolbar.ButtonHeight);

            switch (numChoices)
            {
                case 2:
                    this.buttonTableLayout.Controls.Remove(topBtn);
                    //when only 2 choices, bottom button becomes close button
                    this.SetBottomButtonToClose();
                    break;
                case 4: 
                    //do nothing for now
                    break;
                default:
                    //do nothing for now
                    break;
            }
        }

        /// <summary>
        /// Set a button's icon to the Apply icon.  You must implement the behavior yourself.
        /// </summary>
        public void SetButtonIconToApply(CCButton button)
        {
            button.Text = "Apply";
            //ref icon: http://www.openclipart.org/detail/26557
            button.SetIcon("Icons/Apply.png");
        }

        /// <summary>
        /// Set the bottom button to have the Close icon and behavior.
        /// </summary>
        public void SetBottomButtonToClose()
        {
            this.bottomBtn.Text = "Close";
            //ref icon: http://www.openclipart.org/detail/16982
            this.bottomBtn.SetIcon("Icons/Close.png");
            this.bottomBtn.actionEvent += new CCButton.actionEventHandler(closeBtn_actionEvent);   
        }

        /// <summary>
        /// Set a button to have Cancel icon and behavior.
        /// </summary>
        public void SetButtonToCancel(CCButton button)
        {
            button.Text = "Cancel";
            //ref icon: http://www.openclipart.org/detail/19676
            button.SetIcon("Icons/Cancel.png");
            button.actionEvent += new CCButton.actionEventHandler(closeBtn_actionEvent);
        }

        /// <summary>
        /// On Click: close the form when clicking the bottom button.
        /// </summary>
        private void closeBtn_actionEvent()
        {
            this.Close();
        }

        /// <summary>
        /// Construct a choice form with a preview area.
        /// </summary>
        /// <param name="ccToolbar"></param>
        /// <param name="previewEnabled"></param>
        public ChoiceForm(CCToolbar ccToolbar, int numChoices, bool previewEnabled, bool hasPreviewImage)
            : this(ccToolbar, numChoices)
        {
            //enable the preview panel
            if (previewEnabled == true)
            {
                previewPanel = new Panel();
                previewPanel.AutoSize = true;
                previewPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                previewPanel.Dock = DockStyle.Fill;
                previewPanel.BackColor = Color.White;

                //preview is in the middle of the form
                this.buttonTableLayout.Controls.Add(previewPanel, 1, 1);

                //create a label for the preview w/ same settings as form label
                Label previewLabel = new Label();
                previewLabel.AutoSize = true;
                previewLabel.BackColor = this.label.BackColor;
                previewLabel.Font = this.label.Font;
                previewLabel.Text = "Preview:";
                this.previewPanel.Controls.Add(previewLabel);
            }

            //enable the picture box in the preview panel
            if (hasPreviewImage == true)
            {
                previewPicBox = new PictureBox();
                previewPicBox.BackColor = Color.DarkGray;
                previewPicBox.Width = previewPicBoxWidth;
                previewPicBox.Height = previewPicBoxHeight;
                previewPicBox.SizeMode = PictureBoxSizeMode.Zoom;
                this.previewPanel.Controls.Add(previewPicBox);
            }
        }
    }
}
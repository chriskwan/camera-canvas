using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace CameraCanvas
{
    /// <summary>
    /// Defines a button on the CCToolbar.
    /// </summary>
    public class CCButton : DoubleBufferButton//Button
    {
        //button appearance members
        //TODO: these should read from config
        public const int MIN_WIDTH = 100;//80;
        public const int MIN_HEIGHT = 100;//80;
        public const int MAX_WIDTH = 1000;//200; //TODO: change back, maximums changed for Rick's visit 11/17/2010
        public const int MAX_HEIGHT = 1000;//200;

        private static int btnWidth = 100;
        private static int btnHeight = 100;
        private int btnUnclickedBrdrSize = 1;//0;//2;
        private int btnClickingBrdrSize = 10;//5;
        private Color unclickedColor = Color.WhiteSmoke;
        private Color btnUnclickedBrdrColor = Color.Black;//Color.Red;
        private Color btnClickingBrdrColor = Color.Gold;//Color.Yellow;
        private Color btnClickedBrdrColor = Color.Green;

        private Color unselectedColor = Color.White;

        //dwell clicking members
        private int SEC_FOR_DWELL_CLICK = 3;
        private Timer dwellClickTimer;
        private Label dwellClickTimerLabel;   //displays how many sec left until click
        private int secUntilDwellClick;

        protected CCToolbar ccToolbar;
        protected CCMainForm ccMainForm;    //just a shortcut for ccToolbar.CCMainForm

        protected string enabledIconPath;
        protected string disabledIconPath;

        public delegate void actionEventHandler();
        public event actionEventHandler actionEvent;

        private Timer flashTimer;
        private int flashInterval = 250;

        Font font;

        /// <summary>
        /// Construct a CCButton without a CCToolbar.
        /// Initialize general button appearance, dwell click slideTimer, and attach event handlers.
        /// </summary>
        public CCButton()
        {
            //initialize general button appearance
            this.Width = btnWidth;
            this.Height = btnHeight;
            this.BackColor = unclickedColor;//Color.WhiteSmoke;//Color.AntiqueWhite;
            font = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold);
            this.Font = font;
            this.TextAlign = ContentAlignment.MiddleCenter;

            //selected border size is based on button size
            //TODO need to update this when button size changes
            //btnClickingBrdrSize = (int)(0.1 * btnWidth);

            //this.Font = new Font(FontFamily.GenericMonospace, 7);
            this.BackgroundImageLayout = ImageLayout.Zoom;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = btnUnclickedBrdrColor;
            this.FlatAppearance.BorderSize = btnUnclickedBrdrSize;
            this.FlatAppearance.MouseOverBackColor = btnClickingBrdrColor;
            this.FlatAppearance.MouseDownBackColor = btnClickedBrdrColor;

            //initialize dwell click slideTimer
            dwellClickTimer = new Timer();
            dwellClickTimer.Interval = 1000; //1 sec
            
            //TODO turned off for now
            //dwellClickTimer.Tick += new EventHandler(dwellClickTimer_Tick);

            //initialize dwell click label
            secUntilDwellClick = SEC_FOR_DWELL_CLICK;
            dwellClickTimerLabel = new Label();
            dwellClickTimerLabel.BackColor = Color.Wheat;
            dwellClickTimerLabel.Text = "Click in: " + secUntilDwellClick;
            dwellClickTimerLabel.AutoSize = true;
            this.Controls.Add(dwellClickTimerLabel);
            dwellClickTimerLabel.BringToFront();
            dwellClickTimerLabel.Hide();

            //attach CCButton event handlers so behavior is always invoked by derived CCButtons
            this.Click += new EventHandler(CCButton_Click);
            this.MouseEnter += new EventHandler(CCButton_MouseEnter);
            this.MouseLeave += new EventHandler(CCButton_MouseLeave);

            //attach placeholder (virtual) event handlers for derived CCButtons
            this.Click += new EventHandler(ClickAction);
            this.MouseHover += new EventHandler(MouseHoverAction);
            this.MouseEnter += new EventHandler(MouseEnterAction);
            this.MouseLeave += new EventHandler(MouseLeaveAction);

            flashTimer = new Timer();
            flashTimer.Interval = flashInterval;
            flashTimer.Tick += new EventHandler(flashTimer_Tick);

        }

        void flashTimer_Tick(object sender, EventArgs e)
        {
            if (this.BackColor == btnClickingBrdrColor)
            {
                this.BackColor = unclickedColor;
            }
            else if (this.BackColor == unclickedColor)
            {
                this.BackColor = btnClickingBrdrColor;
            }
        }//end CCButton()


        /// <summary>
        /// Construct a CCButton given the CCToolbar it will go on.
        /// </summary>
        /// <param name="ccToolbar"></param>
        public CCButton(CCToolbar ccToolbar) : this()
        {
            this.ccToolbar = ccToolbar;
            this.ccMainForm = ccToolbar.CCMainForm; //shortcut
        }


        /// <summary>
        /// Construct a CCButton given the CCToolbar and its text.
        /// </summary>
        /// <param name="ccToolbar"></param>
        /// <param name="text"></param>
        public CCButton(CCToolbar ccToolbar, string text)
            : this(ccToolbar)
        {
            SetTextAndTooltip(text);
        }


        /// <summary>
        /// Construct a CCButton given the CCToolbar, its text,
        /// and the path of its icon.
        /// </summary>
        /// <param name="ccToolbar"></param>
        /// <param name="text"></param>
        /// <param name="enabledIconPath"></param>
        public CCButton(CCToolbar ccToolbar, string text, string enabledIconPath)
            : this(ccToolbar, text)
        {
            this.SetIcon(enabledIconPath);
        }



        /// <summary>
        /// Construct a CCButton given the CCToolbar, its text, 
        /// the path of its icon (when enabled), and path of its icon (when disabled).
        /// </summary>
        /// <param name="ccToolbar"></param>
        /// <param name="text"></param>
        /// <param name="enabledIconPath"></param>
        /// <param name="disabledIconPath"></param>
        public CCButton(CCToolbar ccToolbar, string text, string enabledIconPath, string disabledIconPath) 
            : this(ccToolbar, text, enabledIconPath)
        {
            this.disabledIconPath = disabledIconPath;
        }

        /// <summary>
        /// Set the icon image of the button.
        /// </summary>
        /// <param name="enabledIconPath"></param>
        public void SetIcon(string enabledIconPath)
        {
            this.enabledIconPath = enabledIconPath;

            if (File.Exists(enabledIconPath))
            {
                //create a copy of the icon image to use as the background image
                //if we don't do this, the original image is locked and cannot be saved
                //bug fix thanks to: http://blog.vishalon.net/index.php/bitmapsave-a-generic-error-occurred-in-gdi/
                Bitmap iconImage = new Bitmap(enabledIconPath);
                Bitmap tempImage = new Bitmap(iconImage.Width, iconImage.Height);

                //copy contents of icon image to the new image
                Graphics g = Graphics.FromImage(tempImage);
                g.DrawImage(iconImage, 0, 0, iconImage.Width, iconImage.Height);
                g.Dispose();

                //release the lock on the old image
                iconImage.Dispose();

                //point the icon image reference to our new image
                iconImage = tempImage;

                this.BackgroundImage = iconImage;

                //since button has an image, we don't need the text
                this.Text = "";
            }
        }


        /// <summary>
        /// Set the Text property and Tooltip for the button.
        /// </summary>
        /// <param name="text"></param>
        public void SetTextAndTooltip(string text)
        {
            this.Text = text;

            //remove '&' (Alt-key function) from Text and make into Tooltip
            string toolTipString = text;
            //windows uses the last '&' as the Alt-key function
            int ampersandIndex = toolTipString.LastIndexOf('&');
            if (ampersandIndex >= 0)
            {
                toolTipString = toolTipString.Remove(ampersandIndex, 1);
            }
            ccMainForm.CCToolTip.SetToolTip(this, toolTipString);
        }


        /// <summary>
        /// Enable the button for clicking.
        /// </summary>
        public void Enable()
        {
            this.Enabled = true;

            if (File.Exists(enabledIconPath))
            {
                this.BackgroundImage = System.Drawing.Image.FromFile(enabledIconPath);
            }
        }


        /// <summary>
        /// Disable the button from being clicked.
        /// </summary>
        public void Disable()
        {
            this.Enabled = false;

            if (File.Exists(disabledIconPath))
            {
                this.BackgroundImage = System.Drawing.Image.FromFile(disabledIconPath);
            }
        }


        /// <summary>
        /// Mark this button as being selected.
        /// </summary>
        public void MarkAsSelected()
        {
            this.FlatAppearance.BorderSize = btnClickingBrdrSize;
            this.FlatAppearance.BorderColor = btnClickingBrdrColor;
            
            
            //this.BackColor = this.FlatAppearance.MouseOverBackColor;

            //Graphics g = this.CreateGraphics();
            //g.DrawEllipse(Pens.Red, 0, 0, 25, 25);
            //g.Dispose();
        }


        /// <summary>
        /// Mark this button as being unselected.
        /// </summary>
        public void MarkAsUnselected()
        {
            this.FlatAppearance.BorderSize = btnUnclickedBrdrSize;
            this.FlatAppearance.BorderColor = btnUnclickedBrdrColor;
            
            
            //this.BackColor = this.unselectedColor;
        }

        
        public void FlashOn()
        {
            flashTimer.Start();
        }

        public void FlashOff()
        {
            flashTimer.Stop();
        }


        /// <summary>
        /// On Tick: Every second, decrease seconds left until dwell click
        /// and check if enough time has passed for a dwell click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dwellClickTimer_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Dwell slideTimer tick");
            secUntilDwellClick--;
            dwellClickTimerLabel.Text = "Click in: " + secUntilDwellClick.ToString();

            //enough time for a click
            if(secUntilDwellClick <= 0)
            {
                //perform action event
                stopDwellTimer();
                
                dwellClickTimerLabel.Text = "Click!";
                this.FlatAppearance.BorderColor = btnClickedBrdrColor;
                dwellClickTimerLabel.Show();
                this.Refresh();

                ClickAction(sender, e);

                startDwellTimer();
            }
        }

        protected void OnActionEvent()
        {
            //check if any subscribers
            if (actionEvent != null)
            {
                //call the event
                actionEvent();
            }
        }


        /// <summary>
        /// On Mouse Enter: Restart slideTimer in prep for a new dwell click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CCButton_MouseEnter(object sender, EventArgs e)
        {
            startDwellTimer();

            MarkAsSelected();

            //TODO remove?
            //stop the automatic sliding slideTimer so toolbar does not slide when user is trying to click
            if (ccToolbar != null)
            {
                ccToolbar.StopAutomaticSliding();
            }
        }


        private void startDwellTimer()
        {
            //this is turned off for now

            //this.MarkAsSelected();

            //secUntilDwellClick = SEC_FOR_DWELL_CLICK;
            //dwellClickTimerLabel.Text = "Click in: " + secUntilDwellClick;
            //dwellClickTimerLabel.Show();

            //dwellClickTimer.Start();
        }


        /// <summary>
        /// On Mouse Leave: Void the current dwell click attempt by stopping the slideTimer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CCButton_MouseLeave(object sender, EventArgs e)
        {
            stopDwellTimer();

            this.MarkAsUnselected();   

            //TODO remove?
            //resume automatic sliding if not trying to click
            if (ccToolbar != null)
            {
                ccToolbar.StartAutomaticSliding();
            }
        }


        private void stopDwellTimer()
        {
            dwellClickTimer.Stop();

            dwellClickTimerLabel.Hide();
        }


        /// <summary>
        /// On Click: Update label and slideTimer to signify that a click event has occurred
        /// and then restart slideTimer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CCButton_Click(object sender, EventArgs e)
        {
            //Give visual feedback that a click has occured
            //dwellClickTimerLabel.Text = "Click!";
            this.FlatAppearance.BorderColor = btnClickedBrdrColor;
            //dwellClickTimerLabel.Show();
            //this.Refresh();

            //TODO: play a sound here
            //commented out b/c its kind of annoying
            //ccMainForm.getWavPlayer().Play(@"wav\click.wav");

            //restart slideTimer after the click
            stopDwellTimer();
            startDwellTimer();

            //refresh the form and image (recenter, etc)
            //also removes artifacts from moving, cropping
            if (ccMainForm != null) //check in case button is not attached to ccform, ie for choice forms
            {
                //TODO uncomment
                //ccMainForm.resetState();

                //TODO delete
                Console.WriteLine("form state: " + ccMainForm.State);
            }
        }


        /// <summary>
        /// On Click: Perform action defined by deriving CCButton.
        /// NOTE: Overridding methods should NOT call this base method
        /// otherwise, the action will be performed twice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ClickAction(object sender, EventArgs e) 
        {
            OnActionEvent();
        }


        /// <summary>
        /// On Mouse Hover: Perform action defined by deriving CCButton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void MouseHoverAction(object sender, EventArgs e) { }


        /// <summary>
        /// On Mouse Enter: Perform action defined by deriving CCButton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void MouseEnterAction(object sender, EventArgs e) { }


        /// <summary>
        /// On Mouse Leave: Perform action defined by deriving CCButton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void MouseLeaveAction(object sender, EventArgs e) { }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CCButton
            // 
            //this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            //this.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            //this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            //this.ResumeLayout(false);

        }


    }//end class

}//end namespace

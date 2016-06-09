using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas
{
    public partial class PopGame : Form
    {
        private CCMainForm ccForm;

        private string startMsg = "Hold the mouse on the balloon to pop it!";
        private string endMsg = "BOOM! Good job!";

        private Point triggerPoint = new Point(150, 128); //based on center of balloon image
        private int triggerRadius = 25;
        private bool drawMousePath = false;
        
        private Timer popTimer;
        private int popTimerInterval = 1000;
        private int TIME_FOR_POP = 5;
        private int timeUntilPop;

        private int minX = 9000;
        private int minY = 9000;
        private int maxX = -1;
        private int maxY = -1;


        private int minSize;// = 50;
        private int sizeBuffer = 5;
        private int actualSize;
        private int recSize;

        public PopGame(CCMainForm ccForm)
        {
            InitializeComponent();

            Size screenSize = Screen.PrimaryScreen.WorkingArea.Size;
            ClientSize = Screen.PrimaryScreen.WorkingArea.Size; //prevent form from getting resized
            this.triggerPoint = new Point(screenSize.Width / 2, screenSize.Height / 2);

            this.ccForm = ccForm;

            //TODO refactor
            //this.playAgainBtn.Width = ccForm.CCToolbar.ButtonWidth;
            //this.playAgainBtn.Height = ccForm.CCToolbar.ButtonHeight;
            //this.launchBtn.Width = ccForm.CCToolbar.ButtonWidth;
            //this.launchBtn.Height = ccForm.CCToolbar.ButtonHeight;

            minSize = Math.Min(CCButton.MIN_WIDTH, CCButton.MIN_HEIGHT);
            recSize = minSize;

            //minSize = Math.Min(CCButton.MIN_WIDTH, CCButton.MIN_HEIGHT);

            //initialize slideTimer
            popTimer = new Timer();
            popTimer.Interval = popTimerInterval;
            popTimer.Tick += new EventHandler(popTimer_Tick);
            timeUntilPop = TIME_FOR_POP;

        }

        void popTimer_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Time until pop: " + timeUntilPop);
            this.msgLabel.Text = "Time until pop: " + timeUntilPop;

            //if no time left until pop
            if (timeUntilPop <= 0)
            {
                //stop slideTimer
                popTimer.Stop();
                
                //stop drawing mouse path
                drawMousePath = false;

                //pop!
                Console.WriteLine("Pop!");
                msgLabel.Text = endMsg;

                //output actual radius
                Graphics g = this.CreateGraphics();
                System.Drawing.Image explosionImage = System.Drawing.Image.FromFile("Icons/explosionSquare.jpg");
                g.DrawImage(explosionImage, (triggerPoint.X - explosionImage.Width/2),
                    (triggerPoint.Y - explosionImage.Height/2));
                g.DrawEllipse(Pens.Purple, minX, minY, 5, 5);
                g.DrawEllipse(Pens.Purple, maxX, maxY, 5, 5);
                g.DrawRectangle(Pens.PowderBlue, minX, minY, (maxX - minX), (maxY - minY));
                g.Dispose();
                sizeLabel.Text = (maxX - minX) + " by " + (maxY - minY);
                actualSize = Math.Max((maxX - minX), (maxY - minY));
                recSize = Math.Max(minSize, (actualSize + sizeBuffer));
                //Console.WriteLine("Actual Size: " + actualSize);
                //Console.WriteLine("Reccommended Size: " + recSize);
                recSizeLabel.Text = "Reccommended Button Size: " + recSize;

                //send radius info to main form
                Console.WriteLine("recsize: " + recSize);
                ccForm.CCToolbar.ChangeButtonSize(recSize, recSize);

                //make sure this form is still visible
                this.BringToFront();
            }
            //still time left before pop
            else
            {
                timeUntilPop--;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //TODO delete
            Graphics g = this.CreateGraphics();
            g.DrawEllipse(Pens.Blue, triggerPoint.X, triggerPoint.Y, 5, 5);
            g.DrawEllipse(Pens.Green, triggerPoint.X-triggerRadius, 
                triggerPoint.Y-triggerRadius, triggerRadius*2, triggerRadius*2);
        }

        private void PopGame_MouseMove(object sender, MouseEventArgs e)
        {
            //if mouse pos within radius of trigger point and slideTimer not started yet
            if(Math.Abs(e.X - triggerPoint.X) <= triggerRadius
                && Math.Abs(e.Y - triggerPoint.Y) <= triggerRadius 
                && timeUntilPop >= TIME_FOR_POP)
            {
                //start slideTimer
                popTimer.Start();

                //draw mouse path
                drawMousePath = true;
            }

            //otherwise just draw the mouse path
            else if (drawMousePath == true)
            {
                Graphics g = this.CreateGraphics();
                g.DrawEllipse(Pens.Red, e.X, e.Y, 5, 5);
                g.Dispose();

                //check for new mins and/or max's
                if (e.X < minX)
                {
                    minX = e.X;
                }
                else if (e.X > maxX)
                {
                    maxX = e.X;
                }

                if (e.Y < minY)
                {
                    minY = e.Y;
                }
                else if (e.Y > maxY)
                {
                    maxY = e.Y;
                }
            }

        }

        private void playAgainBtn_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);
            g.Dispose();

            this.BackgroundImage = System.Drawing.Image.FromFile("Icons/purple_balloon_small50.jpg");
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;

            this.msgLabel.Text = startMsg;

            minX = 9000;
            minY = 9000;
            maxX = -1;
            maxY = -1;

            popTimer.Stop();
            timeUntilPop = TIME_FOR_POP;
            drawMousePath = false;
        }

        private void launchBtn_Click(object sender, EventArgs e)
        {
            if (ccForm == null || ccForm.IsDisposed == true)
            {
                ccForm = new CCMainForm("config.ini");
            }
            ccForm.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

    }//end class
}//end namespace
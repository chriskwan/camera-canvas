using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas
{
    public partial class ChaseGame : Form
    {
        private Point startPoint;
        private int distToBee = 300;
        private List<Point> beePathPoints;
        private int curBeePointIndex = 0;
        private Timer beeMoveTimer;

        private DateTime startTime;
        private DateTime endTime;
        private List<TimeSpan> elapsedTimes;

        private string startMsg = "Catch the Butterfly!";
        private string endMsg = "You caught it! Good Job!";

        private bool drawMousePath = false;
        CCMainForm ccForm;

        private CCToolbar.ToolbarOrientation reccOrient = CCToolbar.ToolbarOrientation.HorizontalTop;

        public ChaseGame(CCMainForm ccForm)
        {
            InitializeComponent();

            ClientSize = Screen.PrimaryScreen.WorkingArea.Size; //prevent form from getting resized

            this.ccForm = ccForm;

            //TODO refactor
            //playAgainBtn.Width = ccForm.CCToolbar.ButtonWidth;
            //playAgainBtn.Height = ccForm.CCToolbar.ButtonHeight;
            //camCanBtn.Width = ccForm.CCToolbar.ButtonWidth;
            //camCanBtn.Height = ccForm.CCToolbar.ButtonHeight;
            //nextBtn.Width = ccForm.CCToolbar.ButtonWidth;
            //nextBtn.Height = ccForm.CCToolbar.ButtonHeight;

            //start in the middle of the form
            this.StartPosition = FormStartPosition.CenterScreen;
            
            startPoint = new Point(ClientSize.Width / 2 - beeBtn.Width/2, ClientSize.Height / 2 - beeBtn.Height/2);
            
            distToBee = Math.Min(ClientSize.Width / 2, ClientSize.Height / 2);   
            
            beePathPoints = new List<Point>();
            setBeePathPoints(startPoint, distToBee);

            elapsedTimes = new List<TimeSpan>();

            beeMoveTimer = new Timer();
            beeMoveTimer.Interval = 50;
            beeMoveTimer.Tick += new EventHandler(beeMoveTimer_Tick);

            beeBtn.MouseEnter += new EventHandler(beeBtn_Click);

            beeBtn.Location = beePathPoints[0];
        }

        void beeMoveTimer_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine(DateTime.Now + "." + DateTime.Now.Millisecond);

            if (curBeePointIndex >= beePathPoints.Count)
            {
                beeMoveTimer.Stop();
            }
        }

        private void beeBtn_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("current index: " + curBeePointIndex);

            //if the current location is in the list
            if(curBeePointIndex < beePathPoints.Count)
            {
                //ok to draw the mouse path
                drawMousePath = true;

                //if not the first point
                if (curBeePointIndex > 0)
                {
                    //stop slideTimer
                    beeMoveTimer.Stop();
                    
                    //add elapsed time to list
                    endTime = DateTime.Now;
                    TimeSpan timeSpan = endTime - startTime;

                    //Console.WriteLine("From: " + (curBeePointIndex-1)  + " to " + curBeePointIndex + ": " + timeSpan);

                    elapsedTimes.Add(timeSpan);
                }//end if

                //update location to next
                curBeePointIndex++;

                //if new location is a valid location
                if (curBeePointIndex < beePathPoints.Count)
                {
                    //move bee to next location
                    beeBtn.Location = beePathPoints[curBeePointIndex];

                    //draw guide line to next location
                    Graphics g = this.CreateGraphics();
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Pen pen = new Pen(Color.Green, 10);
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                    
                    
                    int widthMod = beeBtn.Width / 2;
                    int heightMod = beeBtn.Height / 2;
                    Point prevPoint = beePathPoints[curBeePointIndex - 1];
                    Point curPoint = beePathPoints[curBeePointIndex];

                    g.DrawLine(pen, new Point(prevPoint.X + widthMod, prevPoint.Y + heightMod)
                        , new Point(curPoint.X + widthMod, curPoint.Y + heightMod));
                    g.Dispose(); //TODO do we have to dispose manually?
                    pen.Dispose();

                    //start slideTimer
                    startTime = DateTime.Now;
                    beeMoveTimer.Start();

                }//end if
                else
                {
                    //stop drawing the mouse path
                    drawMousePath = false;

                    //print out times
                    printElapsedTimes();

                    //hide the bee
                    beeBtn.Hide();

                    //display message
                    msgLabel.Text = endMsg;

                    //update camera canvas setting
                    ccForm.CCToolbar.ChangeOrientation(this.reccOrient);

                    //make sure this form is still visible
                    this.BringToFront();

                }//end else

            }//end if

        }//end method

        

        private void printElapsedTimes()
        {
            updateLabel(leftTimeLabel, elapsedTimes[0], elapsedTimes[1]);
            //updateLabel(rightTimeLabel, elapsedTimes[2], elapsedTimes[3]);
            //updateLabel(topTimeLabel, elapsedTimes[4], elapsedTimes[5]);

            updateLabel(topTimeLabel, elapsedTimes[2], elapsedTimes[3]);
            updateLabel(rightTimeLabel, elapsedTimes[4], elapsedTimes[5]);

            updateLabel(bottomTimeLabel, elapsedTimes[6], elapsedTimes[7]);

            //find min of left,right,top,bottom
            Label min = minLabel(minLabel(leftTimeLabel, rightTimeLabel), minLabel(topTimeLabel, bottomTimeLabel));

            min.BackColor = Color.PowderBlue;
            Label otherMin = leftTimeLabel; //arbitrary

            if (min == leftTimeLabel || min == rightTimeLabel)
            {
                otherMin = minLabel(topTimeLabel, bottomTimeLabel);
                if (otherMin == topTimeLabel)
                {
                    reccOrient = CCToolbar.ToolbarOrientation.HorizontalTop;
                    toolbarOrientationLabel.Text = reccOrient.ToString();
                }
                else if (otherMin == bottomTimeLabel)
                {
                    reccOrient = CCToolbar.ToolbarOrientation.HorizontalBottom;
                    toolbarOrientationLabel.Text = reccOrient.ToString();
                }
                

            }
            else if (min == topTimeLabel || min == bottomTimeLabel)
            {
                otherMin = minLabel(leftTimeLabel, rightTimeLabel);

                if (otherMin == leftTimeLabel)
                {
                    reccOrient = CCToolbar.ToolbarOrientation.VerticalLeft;
                    toolbarOrientationLabel.Text = reccOrient.ToString();
                }
                else if (otherMin == rightTimeLabel)
                {
                    reccOrient = CCToolbar.ToolbarOrientation.VerticalRight;
                    toolbarOrientationLabel.Text = reccOrient.ToString();
                }
                
            }
            otherMin.BackColor = Color.Peru;

        }

        //TODO rewrite to not use labels!
        Label minLabel(Label label1, Label label2)
        {
            if (Double.Parse(label1.Text) < Double.Parse(label2.Text))
            {
                return label1;
            }
            else
            {
                return label2;
            }
        }


        private void updateLabel(Label label, TimeSpan timeSpan1, TimeSpan timeSpan2)
        {
            TimeSpan total = timeSpan1.Add(timeSpan2);

            double ave = total.TotalSeconds / 2.0;

            label.BackColor = Color.White;
            label.Text = ave.ToString();
            label.Show();

        }

        private void setBeePathPoints(Point start, int dist)
        {
            Point center = start;
            Point left = new Point(start.X - dist, start.Y);
            Point right = new Point(start.X + dist, start.Y);
            Point up = new Point(start.X, start.Y - dist);
            Point down = new Point(start.X, start.Y + dist);
            
            beePathPoints.Add(center);
            beePathPoints.Add(left);

            beePathPoints.Add(center);
            //beePathPoints.Add(right);
            beePathPoints.Add(up);

            beePathPoints.Add(center);
            //beePathPoints.Add(up);
            beePathPoints.Add(right);
            
            beePathPoints.Add(center);
            beePathPoints.Add(down);
            
            beePathPoints.Add(center);
        }

        private void ChaseGame_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawMousePath == true)
            {
                Graphics g = this.CreateGraphics();
                Pen pen = new Pen(Color.Red);
                g.DrawEllipse(pen, e.X, e.Y, 10, 10);
                g.Dispose();
                pen.Dispose();
            }

            
        }


        private void playAgainBtn_Click(object sender, EventArgs e)
        {
            reset();
        }


        private void reset()
        {
            //move bee back to start
            curBeePointIndex = 0;
            beeBtn.Location = beePathPoints[curBeePointIndex];
            beeBtn.Show();
            drawMousePath = false;

            //clear out elapsed times
            beeMoveTimer.Stop();
            elapsedTimes.Clear();
        
            //hide labels
            msgLabel.Text = startMsg;
            leftTimeLabel.Hide();
            rightTimeLabel.Hide();
            topTimeLabel.Hide();
            bottomTimeLabel.Hide();
            
            //clear drawings
            this.Invalidate();
        
        }


        private void camCanBtn_Click(object sender, EventArgs e)
        {
            if (ccForm == null || ccForm.IsDisposed == true)
            {
                ccForm = new CCMainForm("config.ini");
            }
            //ccMainForm.CCToolbar.ChangeOrientation(this.reccOrient);
            ccForm.Show();
            this.Hide();
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            PopGame popGame = new PopGame(this.ccForm);
            popGame.ShowDialog();
            this.Hide();
        }
    }
}
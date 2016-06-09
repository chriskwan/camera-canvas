using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas
{
    public partial class TestForm : Form
    {
        CCToolbar toolbar;


        private bool TOOLBAR_FOLLOW_ENABLED = false;//true; //toolbar automatically follows where mouse dwells
        private int TOOLBAR_FOLLOW_OFFSET_X = 0;    //offsets so toolbar is near mouse but not exactly on it
        private int TOOLBAR_FOLLOW_OFFSET_Y = -100;

        private Point mouseDwellStartPos = new Point(0, 0);
        private Point mouseDwellCurrentPos;
        private int MOUSE_DWELL_RADIUS = 50;    //radius of the dwell circle
        private Pen mouseDwellPen;
        private int mouseDwellBrushStartOpacity;    //opacity in new dwell circle
        private int mouseDwellBrushEndOpacity;      //opacity once dwell click is registered
        private Color mouseDwellBrushBaseColor;
        private Brush mouseDwellBrush;
        private Graphics mouseDwellGraphics;


        private int MOUSE_DWELL_MIN_TIME = 5;//3;//2;   //min time for mouse dwell: 2 seconds
        private int secondsLeftUntilDwell;
        private Timer mouseDwellTimer;          //dwellClickTimer necessary to trigger dwell events if mouse does not move 
        private DateTime mouseDwellStartTime;
        private DateTime mouseDwellCurrentTime;
        private TimeSpan mouseDwellTimeSinceStart;

        public TestForm()
        {
            InitializeComponent();

            

            //use built in double buffering in .NET 2
            //TODO: implement own double buffering
            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            #region Initialization for Sliding Toolbar

            //TODO DELETE
            this.toolbar = new CCToolbar(this, CCToolbar.ToolbarOrientation.HorizontalTop);
            this.toolbar.Location = new Point(250, 50);
            this.Controls.Add(this.toolbar);
            this.toolbar.Show();
            

            #endregion

            #region Initialization for Mouse Dwell Clicking

            //check every second if a mouse dwell click has occurred
            secondsLeftUntilDwell = this.MOUSE_DWELL_MIN_TIME;
            mouseDwellTimer = new Timer();
            mouseDwellTimer.Interval = 1000;
            mouseDwellTimer.Tick += new EventHandler(mouseDwellTimer_Tick);

            mouseDwellPen = new Pen(Color.AntiqueWhite);
            mouseDwellBrushStartOpacity = 64;
            mouseDwellBrushEndOpacity = 128;
            mouseDwellBrushBaseColor = Color.GreenYellow;
            mouseDwellBrush = new SolidBrush(Color.FromArgb(mouseDwellBrushStartOpacity,mouseDwellBrushBaseColor));
            mouseDwellGraphics = mouseDwellPanel.CreateGraphics();

            
            ////mouseDwellGraphics.DrawImage(System.Drawing.Image.FromFile("Icons/undo.png"), new Point(0, 0));

            #endregion
        }

        public TextBox TextBox1
        {
            get { return this.textbox1; }
        }

        #region Mouse Dwell Methods

        public bool Toolbar_Following_Enabled
        {
            get { return this.TOOLBAR_FOLLOW_ENABLED; }
            set { this.TOOLBAR_FOLLOW_ENABLED = Toolbar_Following_Enabled; }
        }

        /// <summary>
        /// redraw image and dwell circle
        /// </summary>
        private void redrawGraphics()
        {
            //mouseDwellGraphics.Clear(mouseDwellPanel.BackColor);
            //this.OnPaintBackground(null);
            //mouseDwellGraphics.DrawImage(this.mouseDwellPanel.BackgroundImage, new Point(0, 0));

            mouseDwellGraphics.DrawImage(System.Drawing.Image.FromFile("Icons/kermodebear.jpg"), new Point(0, 0));

            mouseDwellGraphics.FillEllipse(mouseDwellBrush,
                mouseDwellStartPos.X - MOUSE_DWELL_RADIUS, mouseDwellStartPos.Y - MOUSE_DWELL_RADIUS,
                2 * MOUSE_DWELL_RADIUS, 2 * MOUSE_DWELL_RADIUS);
        }

        /// <summary>
        /// make the dwell circle more opaque to show that a dwell-click occured
        /// </summary>
        private void makeDwellCircleMoreOpaque()
        {
            //make the brush more opaque
            mouseDwellBrush = new SolidBrush(Color.FromArgb(mouseDwellBrushEndOpacity, mouseDwellBrushBaseColor));
            redrawGraphics();
        }

        /// <summary>
        /// check if dwell time is enough to move toolbar to mouse location
        /// </summary>
        /// <returns></returns>
        private void checkDwellTime()
        {
            mouseDwellCurrentTime = DateTime.Now;
            mouseDwellTimeSinceStart = mouseDwellCurrentTime - mouseDwellStartTime;
            
            //enough time for a dwell-click
            if (mouseDwellTimeSinceStart.Seconds >= MOUSE_DWELL_MIN_TIME)
            {
                //prepare for the next dwell-click
                mouseDwellStartTime = DateTime.Now;

                //restart dwellClickTimer for next dwell-click
                this.mouseDwellTimer.Stop();
                
                //right now im not going to allow repeated clicks without moving the mouse
                //this.mouseDwellTimer.Start();

                this.makeDwellCircleMoreOpaque();

                //move the toolbar near the mouse location
                //Console.WriteLine("Dwell Click!");
                moveToolbarToMouse();
            }
        }

        /// <summary>
        /// Move toolbar to a new location
        /// </summary>
        /// <param name="newLocation"></param>
        private void moveToolbarTo(Point newLocation)
        {
            //create toolbar if not created yet
            if (this.toolbar == null || this.toolbar.IsDisposed == true)
            {
                this.toolbar = new CCToolbar(this, CCToolbar.ToolbarOrientation.HorizontalTop);
            }

            //offset the new location so that toolbar is centered over the dwell circle
            newLocation.X = newLocation.X - toolbar.Width / 2;

            //update toolbar location
            this.toolbar.Location = newLocation;
            this.toolbar.Show();
            //this.toolbar.Activate();
            this.toolbar.Focus();
        }

        /// <summary>
        /// Move toolbar to location near the mouse cursor
        /// </summary>
        private void moveToolbarToMouse()
        {
            if (TOOLBAR_FOLLOW_ENABLED == true)
            {
                //calculate mouse location
                Point mouseLocation = this.mouseDwellStartPos;
                Point formLocation = this.Location; //need this in case form is not fullscreen
                Point newLocation = new Point(mouseLocation.X + formLocation.X + TOOLBAR_FOLLOW_OFFSET_X,
                    mouseLocation.Y + formLocation.Y + TOOLBAR_FOLLOW_OFFSET_Y);

                this.moveToolbarTo(newLocation);

                //this.testFollowPanel.BackColor = Color.Transparent;//Color.FromArgb(128, Color.SpringGreen);
                this.testFollowPanel.Location = newLocation;
                this.testFollowPanel.BringToFront();
                this.testFollowPanel.Show();
            }

            ////TODO DELETE
            ////calculate mouse location
            //Point mouseLocation = this.mouseDwellStartPos;
            //Point formLocation = this.Location; //need this in case form is not fullscreen
            //Point newLocation = new Point(mouseLocation.X + formLocation.X + TOOLBAR_FOLLOW_OFFSET_X,
            //    mouseLocation.Y + formLocation.Y + TOOLBAR_FOLLOW_OFFSET_Y);

            ////this.testFollowPanel.BackColor = Color.Transparent;//Color.FromArgb(128, Color.SpringGreen);
            //this.testFollowPanel.Location = newLocation;
            //this.testFollowPanel.BringToFront();
            //this.testFollowPanel.Show();
        }

        #region Mouse Dwell Event Handlers

        /// <summary>
        /// on mouse move: check if dwell time is enough to move toolbar to mouse or if the dwell circle has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseDwellPanel_MouseMove(object sender, MouseEventArgs e)
        {

            //compare current mouse location with starting mouse location
            mouseDwellCurrentPos = e.Location;

            //sqrt[ (x2-x1)^2 + (y2-y1)^2 ] < radius
            int distFromBasePos = (int)(Math.Sqrt(
                                    Math.Pow((mouseDwellCurrentPos.X - mouseDwellStartPos.X), 2)
                                    + Math.Pow((mouseDwellCurrentPos.Y - mouseDwellStartPos.Y), 2)));
           //is within dwell radius of the current dwell circle
            if (distFromBasePos < MOUSE_DWELL_RADIUS)
            {
                //this.checkDwellTime();
            }
            //is not within radius, make a new dwell circle around the new starting location
            else
            {
                //Console.WriteLine("new dwell circle");

                //store the new starting info
                mouseDwellStartPos = mouseDwellCurrentPos;
                mouseDwellStartTime = DateTime.Now;

                //reset brush opacity
                mouseDwellBrush = new SolidBrush(Color.FromArgb(mouseDwellBrushStartOpacity, mouseDwellBrushBaseColor));

                //redraw the new dwell circle
                redrawGraphics();

                //reset seconds
                secondsLeftUntilDwell = MOUSE_DWELL_MIN_TIME;
            
                //restart dwell dwellClickTimer
                mouseDwellTimer.Stop();
                mouseDwellTimer.Start();
            }//end else
        }

        /// <summary>
        /// on tick: check if dwell time is enough to move toolbar to mouse
        /// note: i do this rather than setting tick interval to the dwell time
        /// because the ticks actually go much faster than what i specify
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseDwellTimer_Tick(Object sender, EventArgs e)
        {
            //Console.WriteLine("Tick " + secondsLeftUntilDwell + " " + DateTime.Now);
            if (secondsLeftUntilDwell <= 0)
            {
                this.checkDwellTime();
            }
            else
            {
                Font font = new Font("Arial",16);
                this.redrawGraphics();
                mouseDwellGraphics.DrawString(secondsLeftUntilDwell.ToString(),font,new SolidBrush(Color.Black),mouseDwellStartPos);
                secondsLeftUntilDwell--;
            }

        }

        /// <summary>
        /// on left-click:
        /// on right-click: move toolbar near mouse location 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseDwellPanel_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Click");
            mouseDwellBrush = new SolidBrush(Color.FromArgb(128, Color.GreenYellow));
            
            //move toolbar on right-click
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                //Console.WriteLine("Right-Click");
                this.makeDwellCircleMoreOpaque();
                this.moveToolbarToMouse();
            }
        }

        /// <summary>
        /// on resize: reinitialize graphics for the new window size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseDwellPanel_Resize(object sender, EventArgs e)
        {
            this.mouseDwellGraphics = mouseDwellPanel.CreateGraphics();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
           
        }



        #endregion

        private void mouseDwellPanel_MouseEnter(object sender, EventArgs e)
        {

        }

        private void mouseDwellPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            //mouseDwellGraphics.DrawImage(System.Drawing.Image.FromFile("Icons/undo.png"), new Point(0, 0));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Controls.Remove(toolbar);
            //toolbar = new CCToolbar(this, ToolbarOrientation.HorizontalBottom);
            //this.Controls.Add(toolbar);
            //this.Controls.SetChildIndex(toolbar, 0);
            toolbar.ChangeOrientation(CCToolbar.ToolbarOrientation.HorizontalBottom);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //this.Controls.Remove(toolbar);
            //toolbar = new CCToolbar(this, ToolbarOrientation.HorizontalTop);
            //this.Controls.Add(toolbar);
            toolbar.ChangeOrientation(CCToolbar.ToolbarOrientation.HorizontalTop);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Controls.Remove(toolbar);
            //toolbar = new CCToolbar(this);
            //this.Controls.Add(toolbar);

            toolbar.ChangeOrientation(CCToolbar.ToolbarOrientation.VerticalLeft);
            //this.contentPanel.Dock = DockStyle.Right;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.Controls.Remove(toolbar);
            //toolbar = new CCToolbar(this, ToolbarOrientation.VerticalRight);
            //this.Controls.Add(toolbar);
            toolbar.ChangeOrientation(CCToolbar.ToolbarOrientation.VerticalRight);

        }

        private void dockTestBtn_Click(object sender, EventArgs e)
        {
            DockTestForm dtf = new DockTestForm();
            dtf.Show();
        }

    }//end class TestForm
}//end namespace CameraCanvas
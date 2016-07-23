using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CameraCanvas
{
    public delegate void ClickEventHandler();
    public delegate void SelectEventHandler();
  
    public class SlidingPanel : DoubleBufferPanel
    {
        private Timer animateTimer;

        private int iconHeight = 0;
        private int iconWidth = 0;
        private int numIcons;
        private int selectedIndex;

        public float barPosition;
        private int mouseYbefore = 0;
        private enum AnimationState { Rest, FrameUp, FrameDown };
        private int frameNumber = 32; // how many frames btn moving? 100 = slow, 10 = fast
        private int currentFrame = 0;
        private int nextIndex = 0;
        private AnimationState state;

        private List<Bitmap> icons;
        private List<Delegate> clickEvent;
        private List<Delegate> selectEvent;

        /// <summary>
        /// Is the panel allowed to move?
        /// </summary>
        public bool canMove = true;

        public delegate void IndexEventHandler(object source);
        public event IndexEventHandler IndexChange;

        private Pen mypen = new Pen(Color.FromArgb(100, Color.Black), 0);

        // Movement speed of the toolbar:
        // Ranges from 0.25f (slowest) to 1.75f (fastest) in increments of 0.25f
        public float TOOLBAR_MIN_SPEED = 0.25f;
        public float TOOLBAR_MAX_SPEED = 1.75f;
        private float toolbarSpeed = 1.0f;

        // The parent form that contains this panel
        protected Form parentForm;

        // Needed for derived classes
        protected CCMainForm ccForm; 

        public SlidingPanel()
        {
            // Required for Windows Form Designer support
            InitializeComponent();

            ////trying out some built in double buffering
            ////ref: http://www.bobpowell.net/doublebuffer.htm
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint |
            //    ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            // TODO: Add any constructor code after InitializeComponent call
            this.animateTimer = new Timer();
            this.animateTimer.Interval = 5;
            this.animateTimer.Tick += new EventHandler(this.animate_tick);
            this.state = AnimationState.Rest;
            this.numIcons = 0;
            //barVel = 0.2f;
            //barPosition = 0;
            icons = new List<Bitmap>();
            clickEvent = new List<Delegate>();
            selectEvent = new List<Delegate>();
            //LoadIcons(iconList);
        }

        public SlidingPanel(Form parentForm)
        {
            this.parentForm = parentForm;
        }

        public SlidingPanel(CCMainForm _ccForm) : this()
        {
            this.ccForm = _ccForm;
        }

        public float getToolBarSpeed()
        {
            return this.toolbarSpeed;
        }

        public void setToolBarSpeed(float _toolbarSpeed)
        {
            this.toolbarSpeed = _toolbarSpeed;
        }

        /// <summary>
        /// Set the dimensions of the scrolling toolbar selection box and icons
        /// </summary>
        /// <param name="iconWidth"></param>
        /// <param name="iconHeight"></param>
        public void setIconDim(int iconWidth, int iconHeight)
        {
            this.iconHeight = iconHeight;
            this.iconWidth = iconWidth;
            this.mypen = new Pen(Color.FromArgb(100, Color.Black), (float)iconHeight);
        }

        public int frameSpeed
        {
            get
            {
                return this.frameNumber;
            }

            set
            {
                frameNumber = Math.Min(128, Math.Max(0, value));
            }
        }

        public int barHeight
        {
            get
            {
                return this.Height;
            }
        }

        private int listHeight
        {
            get
            {
                return this.numIcons * this.iconHeight;
            }
        }

        private int marginHeight
        {
            get
            {
                return (this.barHeight - this.iconHeight)/2;
            }
        }

        private int pixelIndex(int iconIndex)
        {
            return -(iconIndex * this.iconHeight - this.marginHeight);
        }

        private int absPixelIndex(int relPixel)
        {
            return relPixel - (int)barPosition;
        }

        private int iconPixelIndex(int relPixel)
        {
            return Math.Min(this.numIcons-1, absPixelIndex(relPixel) / this.iconHeight);
        }

        public int iconIndex
        {
            get
            {
                return selectedIndex;
            }

            set
            {
                if (value >= 0 && value < numIcons)
                {
                    selectedIndex = value;
                    this.barPosition = this.pixelIndex(selectedIndex);
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        /// <summary>
        /// On Paint event
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = this.getBackBuffer();
            //Graphics g = this.CreateGraphics(); //causes panel to look gray in between movement

            if (numIcons > 0)
            {

                int xorigin = this.Width / 2 - icons[0].Width / 2;
                int yorigin = (int)barPosition;
                g.Clear(this.BackColor);


                
                for (int i = 0; i < icons.Count; i++)
                {
                    g.DrawImage(icons[i], new Rectangle(xorigin, yorigin + i * icons[i].Height, icons[i].Width, icons[i].Height));
                    if (yorigin + i * icons[i].Width > this.Height - i * icons[0].Height)
                        continue;
                }
                //this is the gray box!
                //it's just a thick line
                g.DrawLine(mypen, new Point(0, this.Height / 2), new Point(this.Width, this.Height / 2));

            }

            g.Dispose();


            base.OnPaint(e); // raise the OnPaint event
        }

        protected override void OnResize(EventArgs eventargs)
        {
            this.iconIndex = this.iconIndex;
            this.Invalidate();
            base.OnResize(eventargs);
        }

        private void InitializeComponent()
        {
        }

        /// <summary>
        /// Loads an Icon onto the Toolbar
        /// </summary>
        /// <param name="IconPath">Path to the icon image</param>
        /// <param name="onSelect">Action when icon is selected</param>
        /// <param name="onClick">Action when icon is clicked</param>
        public void loadIcon(string IconPath, SelectEventHandler onSelect, ClickEventHandler onClick)
        {
            if (System.IO.File.Exists(IconPath))
            {
                loadIcon((System.Drawing.Image.FromFile(IconPath) as Bitmap), onSelect, onClick);
                /*
                try
                {
                    icons.Add(System.Drawing.Image.FromFile(IconPath) as Bitmap);
                    selectEvent.Add(onSelect);
                    clickEvent.Add(onClick);
                    numIcons++;
                    this.iconIndex = 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                 */
            }
        }

        /// <summary>
        /// Loads an Icon onto the Toolbar
        /// </summary>
        /// <param name="IconPath">Icon image</param>
        /// <param name="onSelect">Action when icon is selected</param>
        /// <param name="onClick">Action when icon is clicked</param>
        public void loadIcon(Bitmap icon, SelectEventHandler onSelect, ClickEventHandler onClick)
        {
            if (null != icon)
            {
                try
                {
                    icons.Add(icon);
                    selectEvent.Add(onSelect);
                    clickEvent.Add(onClick);
                    numIcons++;
                    this.iconIndex = 0;
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!this.canMove) return;
            if (e.Delta > 0)
            {
                this.iconIndex--;
                OnIndexChange(this);

            }
            else if (e.Delta < 0)
            {
                this.iconIndex++;
                OnIndexChange(this);
            }

            base.OnMouseWheel(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Focus();
            if (!this.canMove) return;

            if (!this.animateTimer.Enabled)
            {
                if (iconPixelIndex(e.Y) > this.selectedIndex && this.selectedIndex < this.numIcons - 1 && e.Y - this.mouseYbefore > 2)
                {
                    this.animateTimer.Start();
                    this.state = AnimationState.FrameDown;
                    this.nextIndex = this.selectedIndex + 1;
                }
                else if (iconPixelIndex(e.Y) < this.selectedIndex && this.selectedIndex > 0 && e.Y - this.mouseYbefore < -2)
                {
                    this.animateTimer.Start();
                    this.state = AnimationState.FrameUp;
                    this.nextIndex = this.selectedIndex - 1;
                }
            }
            mouseYbefore = e.Y;
 	        base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //if (!this.Enabled) return;

            ClickEventHandler handler = null;

            if (iconPixelIndex(e.Y) == this.iconIndex && null != (handler = (ClickEventHandler)this.clickEvent[iconPixelIndex(e.Y)]))
            {
                handler();
            }
            
            /* disable animation dwellClickTimer */
            //this.currentFrame = 0;
            //this.animateTimer.Stop();
            //this.iconIndex = this.nextIndex;
            //this.state = AnimationState.Rest;

            /* move to index location */
            //this.iconIndex = iconPixelIndex(e.Y);
            
            base.OnMouseClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnDoubleClick(e);
        }

        protected void OnIndexChange(object sender)
        {
            SelectEventHandler handler2;
            if (null != (handler2 = (SelectEventHandler)this.selectEvent[this.iconIndex]))
            {
                handler2();
            }

            if (IndexChange != null) IndexChange(sender);

        }

        //Animation of toolbar is here!
        private void animate_tick(object sender, EventArgs e)
        {
            if (this.currentFrame++ >= (this.frameNumber / this.toolbarSpeed))
            {
                this.currentFrame = 0;
                this.animateTimer.Stop();
                this.iconIndex = this.nextIndex;
                this.state = AnimationState.Rest;

                OnIndexChange(this);
            }
            else
            {
                //when the frame moves up
                //lower numerator for slower, higher numerator for faster
                //float numerSpeed = 1.0f; //ranges from 0.25f to 1.75f in increments of 0.25f
                if (this.state == AnimationState.FrameUp)
                {
                    //this.barPosition += 1.0f / (float)this.frameNumber * this.iconHeight;
                    //this.barPosition += 5.0f / (float)this.frameNumber * this.iconHeight;
                    //this.barPosition += 0.5f / (float)this.frameNumber * this.iconHeight;
                    //this.barPosition += 0.75f / (float)this.frameNumber * this.iconHeight;
                    //this.barPosition += 0.25f / (float)this.frameNumber * this.iconHeight;
                    //this.barPosition += 0.1f / (float)this.frameNumber * this.iconHeight;
                    this.barPosition += toolbarSpeed / (float)this.frameNumber * this.iconHeight;

                    this.Invalidate();
                }
                else if (this.state == AnimationState.FrameDown)
                {
                    this.barPosition -= toolbarSpeed / (float)this.frameNumber * this.iconHeight;
                    //this.barPosition -= 5.0f / (float)this.frameNumber * this.iconHeight;
                    this.Invalidate();
                }
            }
            this.Invalidate();
        }

        public virtual void begin()
        {
            Console.WriteLine("Default beginning action for sliding toolbar.");
        }

        public virtual void end()
        {
            Console.WriteLine("Default end action for sliding toolbar.");
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        #endregion
    }
}

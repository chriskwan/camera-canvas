using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsApplication1
{
    public class toolPanel : DoubleBufferPanel
    {
        private System.Windows.Forms.Timer timer1;
        private float barVel;
        private float barMaxAccel = 1.25f;
        private float barAccel = 0.2f;
    

        public float barPosition;
        private bool barGoingUp;
        private bool mouseOut;
        private List<Bitmap> icons;


        public toolPanel()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.timer1 = new System.Windows.Forms.Timer();
            this.timer1.Interval = 5;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            barVel = 0.2f;
            barPosition = 0;
            icons = new List<Bitmap>();
            //LoadIcons(iconList);
            

        }

        public float toolbarMaxSpeed
        {
            get
            {
                return barMaxAccel;
            }
            set
            {
                barMaxAccel = value;
            }
        }


        public float toolbarAccel
        {
            get
            {
                return barAccel;
            }
            set
            {
                barAccel = value;
            }
        }


        public void LoadIcons(string[] iconList)
        {
            for (int i = 0; i < iconList.Length; i++)
                loadIcon(iconList[i]);
            
        }

        public void LoadIcons(List<Bitmap> iconList)
        {
            for (int i = 0; i < iconList.Count; i++)
                loadIcon(iconList[i]);
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

            if (icons.Count > 0)
            {

                int xorigin = this.Width / 2 - icons[0].Width / 2;
                int yorigin = (int)barPosition;
                g.Clear(Color.White);
                for (int i = 0; i < icons.Count; i++)
                {
                    g.DrawImage(icons[i], new Rectangle(xorigin, yorigin + i * icons[i].Height, icons[i].Width, icons[i].Height));
                    if (yorigin + i * icons[i].Width > this.Height - i * icons[0].Height)
                        continue;
                }

                Pen mypen = new Pen(Color.FromArgb(100, Color.Black), (float)icons[0].Height);
                g.DrawLine(mypen, new Point(0, this.Height / 2), new Point(this.Width, this.Height / 2));
            }

            else
            {

            }

            g.Dispose();


            base.OnPaint(e); // raise the OnPaint event
        }

        protected override void OnMove(EventArgs e)
        {
            this.Invalidate();
            base.OnMove(e);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            this.Invalidate();
            base.OnResize(eventargs);
        }


        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        private void loadIcon(string IconPath)
        {
            icons.Add(System.Drawing.Image.FromFile(IconPath) as Bitmap);
        }

        private void loadIcon(Bitmap icon)
        {
            icons.Add(icon);
        }



        protected override void OnMouseEnter(EventArgs e)
        {
            mouseOut = false;
            timer1.Start();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            mouseOut = true;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            Point absPos = this.PointToScreen(new Point(0, 0));
            //Console.WriteLine(barVel);
            int barHeight = icons.Count * icons[0].Height;
            if (MousePosition.Y > (absPos.Y + this.Height / 2 + icons[0].Height / 2))  {
                int barMin = Math.Max(-barHeight, this.Height / 2 - barHeight + icons[0].Height/2);
                this.barPosition = Math.Max(barMin, (this.barPosition - this.barVel));
                barVel = Math.Min(barMaxAccel, barVel + barAccel);
                barGoingUp = true;
            }
            else if (MousePosition.Y < absPos.Y + this.Height / 2 - icons[0].Height / 2)
            
            {
                int barMax = Math.Min(this.Height, this.Height / 2 - icons[0].Height/2);
                this.barPosition = Math.Min(barMax, (this.barPosition + this.barVel));
                barVel = Math.Min(barMaxAccel, barVel + barAccel);
                barGoingUp = false;
            }
            else if (Math.Abs(((float)Math.Round(getIndex()) - getIndex())) > 0.01f)
            {
                if (barGoingUp)
                {
                    int barMin = Math.Max(-barHeight, this.Height / 2 - barHeight + icons[0].Height / 2);
                    this.barPosition = Math.Max(barMin, ((float)Math.Floor(this.barPosition) - 1));
                    //this.barPosition = Math.Max(barMin, (float)Math.Floor(getIndexPix(float)this.barPosition));
                }
                else
                {
                    int barMax = Math.Min(this.Height, this.Height / 2 - icons[0].Height / 2);
                    this.barPosition = Math.Min(barMax, ((float)Math.Ceiling(this.barPosition) + 1));
                    //this.barPosition = Math.Min(barMax, (float)Math.Ceiling((float)this.barPosition));
                }
                
            }
            else
            {
                //barPosition = getIndexPix((int)Math.Round(getIndex()));
                barVel = 0.2f;
            }
            if (mouseOut && Math.Abs(((float)Math.Round(getIndex()) - getIndex())) < 0.01f)
            {
                timer1.Stop();
            }
                

            //Console.WriteLine(getIndex());
            this.Invalidate();
           

        }

        public int getIconIndex()
        {
            if (Math.Abs(((float)Math.Round(getIndex()) - getIndex())) > 0.01f)
            {
                return -1;
            }
            else
            {
                return (int)Math.Round(getIndex())-1;
            }
            
            
        }

        public float getIndex()
        {
            return ((float)this.Height / 2 + (float)icons[0].Height/2 - barPosition) / (float)icons[0].Height;
        }

        private int getIndexPix(int index)
        {
            
            return icons.Count - this.Height/ 2 - icons[0].Height/2 - index * icons[0].Height;
        }


        #endregion
    }
}

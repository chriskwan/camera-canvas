using System;
using System.Windows.Forms;
using System.Drawing;

namespace CameraCanvas
{
    /// <summary>
    /// Panel that uses the double buffering graphics technique to reduce flicker during animation
    /// All changes are first drawn to a back buffer that is swapped with the buffer that the user sees
    /// </summary>
    public class DoubleBufferPanel : Panel
    {
        public Bitmap _backBuffer;

        public DoubleBufferPanel()
        {
            // Required for Windows Form Designer support
            InitializeComponent();

            // TODO: Add any constructor code after InitializeComponent call
            //this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint |
            ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
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
            base.OnPaint(e); // raise the OnPaint event

            if (_backBuffer == null)
            {
                // TODO BUG 
                // Argument Exception was unhandled
                _backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            }

            /* draw the backbuffer to the screen */
            //e.Graphics.Clear(SystemColors.ControlDark);
            //e.Graphics.Clear(this.BackColor);
            e.Graphics.DrawImageUnscaled(_backBuffer, 0, 0);
        }

        //this was empty before
        //so it overrode the OnPaintBackground by drawing nothing!
        //protected override void OnPaintBackground(PaintEventArgs e)
        //{

        //}

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_backBuffer != null)
            {
                _backBuffer.Dispose();

                _backBuffer = null;

            }
            base.OnSizeChanged(e);
        }

        public Graphics getBackBuffer()
        {
            if (_backBuffer == null)
            {
                _backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            }
            Graphics g = Graphics.FromImage(_backBuffer);
            //g.Clear(Color.White);
            return g;
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
    
}

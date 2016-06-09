using System; //for EventArgs
using System.Collections.Generic; //for List<>
//using System.ComponentModel;
//using System.Data;
using System.Drawing; //for Brush, Color
//using System.Text;
//using System.Threading;
using System.Windows.Forms; //for Form
using Nini.Config; //for IConfigSource
using CameraCanvas.Toolbar.Config;

namespace CameraCanvas
{
    public partial class CCMainForm : Form
    {
        CCToolbar ccToolbar;

        //configuration stuff
        ConfigForm configForm;

        /* application state */
        public enum CCFormState { Etc, Pasting, Selecting, DrawingWait, Drawing, Moving };
        private CCFormState state = CCFormState.Etc;

        //drawing shapes
        public enum DrawingShape { Pencil, Line, Rectangle, FilledRectangle, Circle, FilledCircle, Fill };
        private DrawingShape shape = DrawingShape.Pencil;
        private float lineThickness = 5.0f;


        /* settings */
        string configFile;
        string myPicturesFolder;
        string defaultFolder = "/"; /* starting working directory */
        string currentFolder;

        int defaultwidth = 300;
        int defaultheight = 300;

        bool showminmap = false;
        bool arrowInverted = false;

        /// <summary>
        /// Has the image been saved?
        /// </summary>
        private bool isSaved = false;

        private PictureBox previewBox;

        /// <summary>
        /// Get or set the CCFormState of the CCMainForm.
        /// </summary>
        public CCFormState State
        {
            get{return this.state;}
            set{this.state = value;}
        }


        /// <summary>
        /// Get or set the current drawing shape of the form.
        /// </summary>
        public DrawingShape Shape
        {
            get { return this.shape; }
            set { this.shape = value; }
        }

        /// <summary>
        /// Get or set the drawing line thickness of the form.
        /// </summary>
        public float LineThickness
        {
            get { return this.lineThickness; }
            set { this.lineThickness = value; }
        }

        /// <summary>
        /// Get whether or not all changes have been saved.
        /// </summary>
        public bool IsSaved
        {
            get { return this.isSaved; }
            set { this.isSaved = value; }
        }

        public string CurrentFolder
        {
            get { return this.currentFolder; }
            set { this.currentFolder = value; }
        }

        public string DefaultFolder
        {
            get { return this.defaultFolder; }
            set { this.defaultFolder = value; }
        }

        int movespeed = 1;

        /* image data */
        private Image mainImage;
        private SolidBrush msgbrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));

        /* states for drawing tool */
        Color selectedColor = Color.Black;
        Point drawingOldPoint;

        /* related to seletion */
        Point selectStart;
        Point selectEnd;

        /* select box */
        private List<Point> selectboxes;
        
        /* mouse control */
        private Point mouseOldPoint;

        /* msg box */
        private string msg;
        private Font msgfont = new Font("Times New Roman", 50);
        private bool showmsg = false;

        /* sound */
        private sound wavplayer = new sound();

        /* arrows for navigating around */
        Bitmap arrowup;
        Bitmap arrowleft;
        Bitmap arrowright;
        Bitmap arrowdown;
        Bitmap selres;
        Bitmap selmove;

        /* common font */
        private Font commonfont = new Font("Times New Roman", 20);

        //TODO delete or refactor
        public TextBox letterBox;
        //public TextEntryControl textEntryControl;
        public TextEntryForm textEntryControl;
        
        private DrawingDisplayControl drawingDisplayControl;



        public CCMainForm(string configFile)
        {
            InitializeComponent();

            //if window is resized, recenter the image
            this.Resize += new EventHandler(OnResizeCenterImage);

            //this.BackColor = Color.MidnightBlue;

            this.configFile = configFile;
            Environment.CurrentDirectory = Application.StartupPath + "\\";
            
            loadSettings(configFile);

            
            ccToolbar = new CCToolbar(this);
            this.Controls.Add(ccToolbar);
            
            //initialize config form
            configForm = new ConfigForm(this, this.ccToolbar);
        
            //initialize preview box
            previewBox = new PictureBox();
            previewBox.BackColor = Color.DarkGray;

            //preview is twice the size of a button
            previewBox.Width = 200;
            previewBox.Height = 200;
            previewBox.SizeMode = PictureBoxSizeMode.Zoom;

            this.contentPanel.Controls.Add(previewBox);

            //TODO refactor or delete
            letterBox = new TextBox();
            letterBox.Width = 200;
            letterBox.Height = 100;
            this.contentPanel.Controls.Add(letterBox);
            letterBox.Hide();
            letterBox.BringToFront();

            drawingDisplayControl = new DrawingDisplayControl(this);
            this.contentPanel.Controls.Add(drawingDisplayControl);
            drawingDisplayControl.Hide();
            drawingDisplayControl.BringToFront();
        }



        public DrawingDisplayControl DrawingDisplay
        {
            get
            {
                if (this.drawingDisplayControl == null 
                    || this.drawingDisplayControl.IsDisposed == true)
                {
                    this.drawingDisplayControl = new DrawingDisplayControl(this);
                }
                return this.drawingDisplayControl;
            }


        }

        public CCToolbar CCToolbar
        {
            get
            {
                return this.ccToolbar;
            }
        }

        /// <summary>
        /// Get the portion of the form where content is displayed.
        /// </summary>
        public Panel ContentPanel
        {
            get { return this.contentPanel; }
        }

        public Image MainImage
        {
            get{ return mainImage; }
            set{ this.mainImage = value; }
        }

        public PictureBox PreviewBox
        {
            get { return this.previewBox; }
        }

        public ToolTip CCToolTip
        {
            get { return this.ccToolTip; }
        }

        public Color SelectedColor
        {
            get{return selectedColor;}
            set{selectedColor = value;}
        }

        public Font getCommonFont()
        {
            return this.commonfont;
        }

        public sound getWavPlayer()
        {
            return this.wavplayer;
        }

        public DoubleBufferPanel getImagePanel()
        {
            return this.imagePanel;
        }

        public Control StatusPanel
        {
            get { return this.drawingDisplayControl; }
        }

        private void loadSettings(string configFile)
        {
            try
            {
                IConfigSource config = new IniConfigSource(configFile);

                if (config.Configs["Directories"].Contains("default"))
                {
                    //TODO delete?
                    defaultFolder = config.Configs["Directories"].GetString("default");
                }
                if (config.Configs["UI"].Contains("defaultwidth"))
                {
                    defaultwidth = config.Configs["UI"].GetInt("defaultwidth");
                }

                if (config.Configs["UI"].Contains("defaultheight"))
                {
                    defaultheight = config.Configs["UI"].GetInt("defaultheight");
                }
                if (config.Configs["UI"].Contains("arrowInverted"))
                {
                    arrowInverted = config.Configs["UI"].GetBoolean("arrowInverted");
                }
                if (config.Configs["UI"].Contains("movespeed"))
                {
                    movespeed = config.Configs["UI"].GetInt("movespeed");
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

           
            //default folder is <My Pictures>/Camera Canvas Images
            this.myPicturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/";
            string imageFolderPath = System.IO.Path.Combine(myPicturesFolder, "Camera Canvas Images");
            
            //create the folder inside my pictures if it does not exist
            if (false == System.IO.Directory.Exists(imageFolderPath))
            {
                System.IO.Directory.CreateDirectory(imageFolderPath);

                //copy the sample images to it
                string samplesDir = System.IO.Path.GetFullPath("samples");
                if (System.IO.Directory.Exists(samplesDir))
                {
                    string srcFile;
                    string dstFile;
                    string[] samplesFiles = System.IO.Directory.GetFiles(samplesDir);
                    foreach (string sf in samplesFiles)
                    {
                        srcFile = System.IO.Path.GetFileName(sf);
                        dstFile = System.IO.Path.Combine(imageFolderPath, srcFile);
                        System.IO.File.Copy(sf, dstFile, false);
                    }
                }
            }

            this.defaultFolder = imageFolderPath + "/"; //need the slash otherwise imageFolderPath will get appended to file name

        }//end loadsettings

        //TODO refactor state pattern
        private void imagepanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = imagePanel.getBackBuffer();
            g.Clear(SystemColors.ControlDark);
            mainImage.draw(g, imagePanel.Width, imagePanel.Height);

            //Moving
            if (mainImage.loaded && this.state == CCFormState.Moving)
            {
                draw_movearrows(g);
            }

            //All: Min Map?
            if (mainImage.loaded && showminmap)
            {
                draw_minmap(g);
            }

            //Selecting
            if (this.selectboxes != null && this.state == CCFormState.Selecting)
            {
                drawSelection(g);
            }

            //All: Showing a message
            if (this.showmsg)
            {
                drawMsgBox(g);
            }

            //All: Drawing
            if (this.state == CCFormState.Drawing && this.shape != DrawingShape.Pencil)
            {
                drawInvalidShape(g);
            }

            //All: DrawingWait
            //draw double click radius for drawing
            if (drawingFeedback == true
                && (this.state == CCFormState.DrawingWait || this.state == CCFormState.Drawing)
                )
            {
                //DrawDrawingHelperBox(g, this.drawingFeedbackText);
                DrawDrawingHelperBox(g, this.drawingFeedbackIsStart);
            }

            g.Dispose();
            
        }

        /// <summary>
        /// Draw the drawing helper box
        /// </summary>
        /// <param name="g"></param>
        /// <param name="boxText"></param>
        private void DrawDrawingHelperBox(Graphics g, bool isStart)
        {
            string boxText;
            int boxTextFontSize = 22; //based on 100x100 buttons and "Click to Start" and "Click to Stop" as text
            //int boxTextFontSize = 28; //based on 100x100 buttons and "Start" and "Stop" as text
            Brush brush;

            if (isStart == true)
            {
                boxText = "Click to Start";
                brush = new SolidBrush(Color.FromArgb(100, Color.Green));
            }
            else
            {
                boxText = "Click to Stop";
                brush = new SolidBrush(Color.FromArgb(100, Color.Red));
            }
            
            Point lastPoint = mainImage.getScreenPoint(imagePanelLastPointClicked);
            Rectangle radiusRect = new Rectangle(lastPoint.X-ccToolbar.ButtonWidth/2,
                lastPoint.Y-ccToolbar.ButtonHeight/2,
                ccToolbar.ButtonWidth,
                ccToolbar.ButtonHeight);

            //fill blue box
            g.FillRectangle(brush,radiusRect);
           
            //draw outline
            Pen outlinePen = new Pen(Color.Black, 5.0f);
            outlinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            g.DrawRectangle(outlinePen, radiusRect);

            //draw text to show in the box
            ////text is positioned somewhat in the center of the box
            //g.DrawString(boxText, 
            //    new Font(FontFamily.GenericSansSerif,boxTextFontSize,FontStyle.Bold), 
            //    Brushes.Black, 
            //    new Rectangle(radiusRect.X,radiusRect.Y+radiusRect.Height/4,radiusRect.Width,radiusRect.Height)
            //    );

            //text is positioned from the top of the box
            g.DrawString(boxText,
                new Font(FontFamily.GenericSansSerif, boxTextFontSize, FontStyle.Bold),
                Brushes.Black,
                new Rectangle(radiusRect.X, radiusRect.Y, radiusRect.Width, radiusRect.Height)
                );
 
            outlinePen.Dispose();
            brush.Dispose();
        }

        //draw invalid shape for rubberbanding effect feedback when drawing
        //TODO BUG: shape gets drawn slightly lower than where the last rubber band shape is
        private void drawInvalidShape(Graphics g)
        {
            Pen pen = new Pen(this.selectedColor, this.lineThickness);
            Brush brush = new SolidBrush(this.selectedColor);
            Pen dashPen = new Pen(Color.Black, 5.0f);
            dashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            Point startPoint = mainImage.getScreenPoint(drawStartPoint);
            Point endPoint = mainImage.getScreenPoint(drawEndPoint);
            int startX = Math.Min(startPoint.X, endPoint.X);
            int startY = Math.Min(startPoint.Y, endPoint.Y);
            int width = Math.Abs(endPoint.X - startPoint.X);
            int height = Math.Abs(endPoint.Y - startPoint.Y);

            switch (this.shape)
            {
                case DrawingShape.Pencil:
                    break;
                case DrawingShape.Line:
                    g.DrawLine(pen,startPoint,endPoint);
                    break;
                case DrawingShape.Circle:
                    g.DrawEllipse(pen, startX, startY, width, height);

                    //draw a rectangle outline around the invalid circle to show how the start and end points affect the drawing
                    g.DrawRectangle(dashPen, startX, startY, width, height);

                    break;
                case DrawingShape.FilledCircle:
                    g.FillEllipse(brush, startX, startY, width, height);

                    //draw a rectangle outline around the invalid circle to show how the start and end points affect the drawing
                    g.DrawRectangle(dashPen, startX, startY, width, height);

                    break;
                case DrawingShape.Rectangle:
                    g.DrawRectangle(pen, startX, startY, width, height);
                    break;
                case DrawingShape.FilledRectangle:
                    g.FillRectangle(brush, startX, startY, width, height);
                    break;
                default:
                    break;
            }

            dashPen.Dispose();
            brush.Dispose();
            pen.Dispose();
        }

        /* section for managing mouse input on image panel */

        private Point drawStartPoint;
        private Point drawEndPoint;

        private int imagePanelClickCount = 0;
        private Point imagePanelLastPointClicked;

        private bool drawingFeedback = false; //show invalid shapes and drawing helper box?
        private bool drawingFeedbackIsStart = false; //did you just put down start point?

        private bool isCloseToLastPoint(Point newPoint)
        {
            Point lastPoint = imagePanelLastPointClicked;

            //radius is the same as the button size
            int radius = Math.Max(this.ccToolbar.ButtonWidth/2, this.ccToolbar.ButtonHeight/2);

            Console.WriteLine("Last: " + lastPoint);
            Console.WriteLine("New: " + newPoint);

            if (Math.Abs(newPoint.X - lastPoint.X) < radius
                && Math.Abs(newPoint.Y - lastPoint.Y) < radius)
            {
                return true;
            }
            return false;
        }


        private void imagepanel_MouseClick(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("mouse click on {0},{1}", mainImage.getPicturePoint(e.Location).X, mainImage.getPicturePoint(e.Location).Y);
            switch (this.state)
            {
                case CCFormState.Etc:
                    Console.WriteLine("default!");
                    break;
                case CCFormState.Selecting:
                    Console.WriteLine("cancel selection!");
                    //resetSelectPoints();
                    break;
                case CCFormState.Pasting:
                    endPaste(e.Location);
                    this.state = CCFormState.Etc;
                    break;
                case CCFormState.DrawingWait:
                    
                    imagePanelClickCount++;

                    //one click means user wants to start drawing
                    if (imagePanelClickCount == 1)
                    {
                        drawingFeedback = true;
                        this.drawingFeedbackIsStart = true;
                        
                        //refresh so start box is shown
                        this.imagePanel.Invalidate();

                        //store last point clicked
                        imagePanelLastPointClicked = mainImage.getPicturePoint(e.Location);
                    }

                    //two clicks means user has pressed start and is currently drawing a shape
                    else if (imagePanelClickCount == 2)
                    {
                        drawingFeedback = true;

                        //if the second click is close enough to the first click
                        if (isCloseToLastPoint(mainImage.getPicturePoint(e.Location)) == true)
                        {
                            this.state = CCFormState.Drawing;

                            //TODO delete?
                            drawingFeedback = false; //hide the radius box
                            this.imagePanel.Invalidate(); //refresh the panel to hid the drawing helper box

                            this.takeSnap("drawing");
                            
                            //set start point of drawing
                            this.drawStartPoint = mainImage.getPicturePoint(e.Location);

                            this.wavplayer.Play(@"wav\click.wav");
                            drawingOldPoint = mainImage.getPicturePoint(e.Location);  
                        }
                        //second click is too far from the first click, store a new last position
                        else
                        {
                            //force image panel to redraw radius
                            drawingFeedback = true;
                            this.imagePanel.Invalidate(); 

                            this.HideMessage();
                            this.drawingFeedbackIsStart = true;
                                                        
                            //refresh so start box is shown
                            this.imagePanel.Invalidate();

                            //store last point clicked
                            imagePanelLastPointClicked = mainImage.getPicturePoint(e.Location);
                        }

                        //reset click count in both cases
                        imagePanelClickCount = 1;//0;
                    }

                    break;
                case CCFormState.Drawing:
                    imagePanelClickCount++;

                    //one click means user wants to stop drawing
                    if (imagePanelClickCount == 1)
                    {
                        drawingFeedback = true;
                        this.drawingFeedbackIsStart = false;
                        
                        //refresh so stop box is shown
                        this.imagePanel.Invalidate();

                        //store last point clicked
                        imagePanelLastPointClicked = mainImage.getPicturePoint(e.Location);
                    }

                    //two clicks means user has just hit stop button
                    else if (imagePanelClickCount == 2)
                    {
                        drawingFeedback = true;
                        //if the second click is close enough to the first click
                        if (isCloseToLastPoint(mainImage.getPicturePoint(e.Location)) == true)
                        {
                            //draw the final shape
                            if (this.shape != DrawingShape.Pencil)
                            {
                                //TODO BUG: shape gets drawn slightly lower than where the last rubber band shape is
                                drawShape(mainImage.getPicturePoint(e.Location));
                            }

                            drawingFeedback = false; //drawing helper box should be hidden now
                            this.imagePanel.Invalidate(); //refresh the panel to hide the drawing helper box

                            //TODO here

                            this.wavplayer.Play(@"wav\click.wav");
                            this.state = CCFormState.DrawingWait;

                            imagePanelClickCount = 0;

                           
                        }
                        //second click is too far from the first click, store a new last position
                        else
                        {
                            drawingFeedback = true;

                            //force image panel to redraw radius
                            this.imagePanel.Invalidate(); 

                            this.HideMessage();
                            this.drawingFeedbackIsStart = false;
                                                        
                            //refresh so stop box is shown
                            this.imagePanel.Invalidate();

                            //store last point clicked
                            imagePanelLastPointClicked = mainImage.getPicturePoint(e.Location);

                            imagePanelClickCount = 1;//0;
                        }

                        //reset click count in both cases
                        //imagePanelClickCount = 1;//0;
                    }
                    break;
                default:
                    break;
            }
        }

        void imagepanel_MouseEnter(object sender, System.EventArgs e)
        {
            this.mouseOldPoint = this.imagePanel.PointToClient(Cursor.Position);
        }

        private void imagepanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.state == CCFormState.Moving)
            {
                this.imagePanel.Invalidate();
                checkArrow_tick(this, null);
                //if (!arrowTimer.Enabled) arrowTimer.Start();
            }

            if (this.state == CCFormState.Selecting)
            {
                this.imagePanel.Invalidate();
                selectTimer_Tick(this, null);
                //if (!selectTimer.Enabled) selectTimer.Start();
            }
            
            
            switch (this.state)
            {
                case CCFormState.Selecting:
                    //selectEndPoint(e.Location);
                    //handleSelect(e.Location); 
                    break;
                case CCFormState.Pasting:
                    updatePastePosition(e.Location);
                    break;
                
                case CCFormState.Drawing:
                    //pencil: actually drawing the line
                    //call to drawing should be here for path drawing
                    if (this.shape == DrawingShape.Pencil)
                    {
                        //keep drawing the pencil path
                        drawShape(mainImage.getPicturePoint(e.Location));
                        CheckIfHideHelperBox(e);
                        this.imagePanel.Invalidate();
                    }

                    //shapes: adjusting the size of the shape without putting it down
                    else
                    {
                        //TODO refactor, just invalidate the union of the previous invalid shape and current shape
                        //draw the invalid shape to achieve a rubberbanding effect for feedback
                        //TODO BUG: shape gets drawn slightly lower than where the last rubber band shape is
                        
                        this.drawEndPoint = mainImage.getPicturePoint(e.Location);
                        CheckIfHideHelperBox(e);
                        this.imagePanel.Invalidate();
                    }

                    break;

                //before starting to draw, deciding where to put the start point
                case CCFormState.DrawingWait:
                    CheckIfHideHelperBox(e);
                    this.imagePanel.Invalidate();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Check if the current drawing helper box should be hidden based on current mouse position,
        /// ie: if mouse moves outside of helper box, hide and reset it.
        /// </summary>
        /// <param name="e"></param>
        private void CheckIfHideHelperBox(MouseEventArgs e)
        {
            //if you move cursor out of the helper box
            if (false == isCloseToLastPoint(mainImage.getPicturePoint(e.Location)))
            {
                //hide the helper box
                this.drawingFeedback = false;

                //reset number of clicks
                //so that if you happen to click again where the previous helper box was, 
                //it will show another helper box instead of just drawing there
                this.imagePanelClickCount = 0;
            }
        }

        /* section for managing mouse input on image panel over */

        public void DrawText(string text)
        {
            //mainImage.DrawText(text, this.drawingOldPoint);
            this.takeSnap("Text added");
            mainImage.DrawText(text);
        }

        private void drawShape(Point newpoint)
        {
            switch (this.shape)
            {
                case DrawingShape.Pencil:
                    mainImage.DrawShape(selectedColor, lineThickness, drawingOldPoint, newpoint, DrawingShape.Line);
                    break;
                case DrawingShape.Line:
                    mainImage.DrawShape(selectedColor, lineThickness, drawingOldPoint, newpoint, DrawingShape.Line);    
                    break;
                case DrawingShape.Circle:
                    mainImage.DrawShape(selectedColor, lineThickness, drawingOldPoint, newpoint, DrawingShape.Circle);    
                    break;
                case DrawingShape.FilledCircle:
                    mainImage.DrawShape(selectedColor, lineThickness, drawingOldPoint, newpoint, DrawingShape.FilledCircle);
                    break;
                case DrawingShape.Rectangle:
                    mainImage.DrawShape(selectedColor, lineThickness, drawingOldPoint, newpoint, DrawingShape.Rectangle);
                    break;
                case DrawingShape.FilledRectangle:
                    mainImage.DrawShape(selectedColor, lineThickness, drawingOldPoint, newpoint, DrawingShape.FilledRectangle);
                    break;
                case DrawingShape.Fill:
                    mainImage.DrawShape(selectedColor, lineThickness, drawingOldPoint, newpoint, DrawingShape.Fill);
                    break;
                default:
                    mainImage.DrawShape(selectedColor, lineThickness, drawingOldPoint, newpoint, DrawingShape.Line);
                    break;
            }

            //TODO refactor
            //we want to leave the message when path drawing
            if (this.shape != DrawingShape.Pencil)
            {
                this.HideMessage();
            }
            
            mainImage.Invalidate();
            imagePanel.Invalidate();

            if (this.shape == DrawingShape.Pencil)
            {
                drawingOldPoint = newpoint;
            }
        }


        /// <summary>
        /// Update the preview box with the image and show it on the form.
        /// </summary>
        public void ShowPreviewBox(System.Drawing.Image previewImage)
        {
            this.previewBox.Image = previewImage;

            //change location of preview box based on toolbar orientation
            //horizontal layouts
            if (ccToolbar.Orientation == CCToolbar.ToolbarOrientation.HorizontalTop
                || ccToolbar.Orientation == CCToolbar.ToolbarOrientation.HorizontalBottom)
            {
                int formMidPt = this.ClientSize.Width / 2;
                this.previewBox.Left = formMidPt - this.previewBox.Width / 2;

                //top
                if (ccToolbar.Orientation == CCToolbar.ToolbarOrientation.HorizontalTop)
                {
                    this.previewBox.Top = 0;
                }

                //bottom
                else if (ccToolbar.Orientation == CCToolbar.ToolbarOrientation.HorizontalBottom)
                {
                    this.previewBox.Top = this.Height - (this.previewBox.Height + ccToolbar.Height);
                }
            }
            //vertical layouts
            else if (ccToolbar.Orientation == CCToolbar.ToolbarOrientation.VerticalLeft
                || ccToolbar.Orientation == CCToolbar.ToolbarOrientation.VerticalRight)
            {
                int formMidPt = this.ClientSize.Height / 2;
                this.previewBox.Top = formMidPt - this.previewBox.Height / 2;

                //left
                if (ccToolbar.Orientation == CCToolbar.ToolbarOrientation.VerticalLeft)
                {
                    this.previewBox.Left = 0;
                }

                //right
                else if (ccToolbar.Orientation == CCToolbar.ToolbarOrientation.VerticalRight)
                {
                    this.previewBox.Left = this.Width - (this.previewBox.Width + ccToolbar.Width);
                }
            }

            //Write "Preview"
            Graphics g = Graphics.FromImage(this.previewBox.Image);
            g.DrawString("Preview", this.commonfont, Brushes.Black, new PointF(0, 0));
            g.Dispose();

            this.previewBox.Invalidate();

            this.previewBox.Show();
            this.previewBox.BringToFront();
        }


        /// <summary>
        /// Hide the preview box on the form.
        /// </summary>
        public void HidePreviewBox()
        {
            this.previewBox.Hide();
        }

        /* section for managing selecting areas on image */

        private void selectStartPoint(Point startpoint)
        {
            
            selectStart = startpoint;
            selectStart.X = Math.Max(0, selectStart.X);
            selectStart.Y = Math.Max(0, selectStart.Y);
            selectStart.X = Math.Min(mainImage.width, selectStart.X);
            selectStart.Y = Math.Min(mainImage.height, selectStart.Y);
            
            mainImage.selecting = true;
            mainImage.Invalidate();
            imagePanel.Invalidate();
            
        }

        private void selectEndPoint(Point startpoint)
        {
            
            selectEnd = startpoint;
            selectEnd.X = Math.Max(0, selectEnd.X);
            selectEnd.Y = Math.Max(0, selectEnd.Y);
            selectEnd.X = Math.Min(mainImage.width, selectEnd.X);
            selectEnd.Y = Math.Min(mainImage.height, selectEnd.Y);
             
            mainImage.selectedArea = new Rectangle(selectStart, new Size(selectEnd.X - selectStart.X, selectEnd.Y - selectStart.Y));
            mainImage.selecting = true;
            mainImage.Invalidate();
            imagePanel.Invalidate();
             
        }

        public void resetSelectPoints()
        //private void resetSelectPoints()
        {
            mainImage.resetSelection();
            mainImage.selecting = false;
            mainImage.Invalidate();
            imagePanel.Invalidate();
        }
        /* section over */



        /* section for managing nav arrows */

        private void initArrows()
        {
            //TODO
            arrowup = System.Drawing.Image.FromFile("Icons/arrow_up.png") as Bitmap;
            //arrowup = System.Drawing.Image.FromFile("Icons/zoom.png") as Bitmap;
            arrowleft = System.Drawing.Image.FromFile("Icons/arrow_left.png") as Bitmap;
            arrowright = System.Drawing.Image.FromFile("Icons/arrow_right.png") as Bitmap;
            arrowdown = System.Drawing.Image.FromFile("Icons/arrow_down.png") as Bitmap;
            selres = System.Drawing.Image.FromFile("Icons/resizesel.png") as Bitmap;
            selmove = System.Drawing.Image.FromFile("Icons/movesel.png") as Bitmap;

        }

        private void draw_movearrows(Graphics g)
        {
            int origin_x = imagePanel.Width / 2 - 132;
            int origin_y = imagePanel.Height / 2 - 132;

            g.DrawImage(arrowup, new Rectangle(origin_x + 85, origin_y, 75, 75));
            g.DrawImage(arrowright, new Rectangle(origin_x + 170, origin_y + 85, 75, 75));
            g.DrawImage(arrowdown, new Rectangle(origin_x + 85, origin_y + 170, 75, 75));
            g.DrawImage(arrowleft, new Rectangle(origin_x, origin_y + 85, 75, 75));
        }

        private void draw_selectarrows(Graphics g)
        {
            int origin_x = imagePanel.Width / 2 - 264;
            int origin_y = imagePanel.Height / 2 - 264;

            //TODO change to this
            //at top of selection box right of move box arrows
            //but arrow checks are not working correctly at the moment
            //int origin_x2 = imagePanel.Width / 2;
            //int origin_y2 = imagePanel.Height / 2 - 264;

            //TODO change to above
            //at bottom right corner of selection box
            int origin_x2 = imagePanel.Width / 2;
            int origin_y2 = imagePanel.Height / 2;

            g.DrawImage(selmove, new Rectangle(origin_x + 85, origin_y + 85, 75, 75));
            g.DrawImage(arrowup, new Rectangle(origin_x + 85, origin_y, 75, 75));
            g.DrawImage(arrowright, new Rectangle(origin_x + 170, origin_y + 85, 75, 75));
            g.DrawImage(arrowdown, new Rectangle(origin_x + 85, origin_y + 170, 75, 75));
            g.DrawImage(arrowleft, new Rectangle(origin_x, origin_y + 85, 75, 75));


            g.DrawImage(selres, new Rectangle(origin_x2 + 85, origin_y2 + 85, 75, 75));
            g.DrawImage(arrowup, new Rectangle(origin_x2 + 85, origin_y2, 75, 75));
            g.DrawImage(arrowright, new Rectangle(origin_x2 + 170, origin_y2 + 85, 75, 75));
            g.DrawImage(arrowdown, new Rectangle(origin_x2 + 85, origin_y2 + 170, 75, 75));
            g.DrawImage(arrowleft, new Rectangle(origin_x2, origin_y2 + 85, 75, 75));

        }

        private void draw_minmap(Graphics g)
        {
            int minWidth = imagePanel.Width/2 * (imagePanel.Width/mainImage.height);
            int minHeight = imagePanel.Height/2 * (imagePanel.Width / mainImage.height);
            int origin_x = (int)(-(float)mainImage.imageX / (float)(mainImage.width) * ((float)imagePanel.Width/2));
            int origin_y = (int)(-(float)mainImage.imageY / (float)(mainImage.height) * ((float)imagePanel.Height/2));
            Console.WriteLine(":"+origin_x);
            g.DrawRectangle(Pens.Black, new Rectangle(imagePanel.Width / 4, imagePanel.Height / 4, minWidth, minHeight));
            g.DrawRectangle(Pens.Black, new Rectangle(origin_x + imagePanel.Width / 4, origin_y + imagePanel.Height / 4, minWidth, minHeight));
        }

        private int checkArrow(Point pos)
        {
            int origin_x = imagePanel.Width / 2 - 132;
            int origin_y = imagePanel.Height / 2 - 132;

            if (origin_x + 85 <= pos.X && pos.X <= origin_x + 85 + 75 &&
                origin_y <= pos.Y && pos.Y <= origin_y + 75)
            {
                return 1;
            }
            else if (origin_x + 170 <= pos.X && pos.X <= origin_x + 170 + 75 &&
                    origin_y + 85 <= pos.Y && pos.Y <= origin_y + 85 + 75)
            {
                return 2;
            }
            else if (origin_x + 85 <= pos.X && pos.X <= origin_x + 85 + 75 &&
                     origin_y + 170 <= pos.Y && pos.Y <= origin_y + 170 + 75)
            {
                return 3;
            }
            else if (origin_x <= pos.X && pos.X <= origin_x + 75 &&
                    origin_y + 85 <= pos.Y && pos.Y <= origin_y + 85 + 75)
            {
                return 4;
            }
            else if (origin_x <= pos.X && pos.X <= origin_x + 245 &&
                     origin_y <= pos.Y && pos.Y <= origin_y + 245)
            {
                return 5;
            }

            return 0;
        }

        private int checkArrow(Point pos, int origin_x, int origin_y)
        {
            if (origin_x + 85 <= pos.X && pos.X <= origin_x + 85 + 75 &&
                origin_y <= pos.Y && pos.Y <= origin_y + 75)
            {
                return 1;
            }
            else if (origin_x + 170 <= pos.X && pos.X <= origin_x + 170 + 75 &&
                    origin_y + 85 <= pos.Y && pos.Y <= origin_y + 85 + 75)
            {
                return 2;
            }
            else if (origin_x + 85 <= pos.X && pos.X <= origin_x + 85 + 75 &&
                     origin_y + 170 <= pos.Y && pos.Y <= origin_y + 170 + 75)
            {
                return 3;
            }
            else if (origin_x <= pos.X && pos.X <= origin_x + 75 &&
                    origin_y + 85 <= pos.Y && pos.Y <= origin_y + 85 + 75)
            {
                return 4;
            }
            return 5;
        }

        private void checkArrow_tick(object sender, EventArgs e)
        {
            
            if (arrowInverted)
            {
                switch (checkArrow(imagePanel.PointToClient(MousePosition)))
                {
                    case 1:
                        mainImage.imageY = Math.Min((int)(3 * imagePanel.Height / 4), mainImage.imageY + (int)(movespeed));
                        //mainImage.moveUp((int)vel);
                        if (!arrowTimer.Enabled) arrowTimer.Start();
                        break;
                    case 2:
                        mainImage.imageX = Math.Max((int)(-mainImage.width + imagePanel.Width / 4), mainImage.imageX - (int)(movespeed));
                        //mainImage.moveRight((int)vel);
                        if (!arrowTimer.Enabled) arrowTimer.Start();
                        break;
                    case 3:
                        mainImage.imageY = Math.Max((int)(-mainImage.height + imagePanel.Height / 4), mainImage.imageY - (int)(movespeed));
                        //mainImage.moveDown((int)vel);
                        if (!arrowTimer.Enabled) arrowTimer.Start();
                        break;
                    case 4:
                        mainImage.imageX = Math.Min((int)(3 * imagePanel.Width / 4), mainImage.imageX + (int)(movespeed));
                        //mainImage.moveLeft((int)vel);
                        if (!arrowTimer.Enabled) arrowTimer.Start();
                        
                        break;
                    case 5:
                        if (arrowTimer.Enabled)
                        {
                            arrowTimer.Stop();
                            Console.WriteLine("t stop");
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (checkArrow(imagePanel.PointToClient(MousePosition)))
                {
                    case 3:
                        mainImage.imageY = Math.Min((int)(3 * imagePanel.Height / 4), mainImage.imageY + (int)(movespeed));
                        //mainImage.moveUp((int)vel);
                        if (!arrowTimer.Enabled) arrowTimer.Start();
                        break;
                    case 4:
                        mainImage.imageX = Math.Max((int)(-mainImage.width + imagePanel.Width / 4), mainImage.imageX - (int)(movespeed));

                        if (!arrowTimer.Enabled) arrowTimer.Start();
                        //mainImage.moveRight((int)vel);
                        break;
                    case 1:
                        mainImage.imageY = Math.Max((int)(-mainImage.height + imagePanel.Height / 4), mainImage.imageY - (int)(movespeed));
                        //mainImage.moveDown((int)vel);
                        if (!arrowTimer.Enabled) arrowTimer.Start();
                        break;
                    case 2:
                        mainImage.imageX = Math.Min((int)(3 * imagePanel.Width / 4), mainImage.imageX + (int)(movespeed));
                        //mainImage.moveLeft((int)vel);
                        if (!arrowTimer.Enabled) arrowTimer.Start();
                        break;
                    case 5:
                        if (arrowTimer.Enabled)
                        {
                            arrowTimer.Stop();
                            Console.WriteLine("t stop");
                        }
                        break;
                    default:
                        break;
                }
            }
            imagePanel.Invalidate();
            if (this.state != CCFormState.Moving)
            {
                arrowTimer.Stop();
            }
            //Console.WriteLine("inside arrow! {0} {1}", mainImage.imageX, mainImage.imageY);
        }

        /* section for managing nav arrows over */

        private void CCForm_Load(object sender, EventArgs e)
        {
            //hide the control box(minimize, maximize, exit)
            //this.ControlBox = false;
            
            //TODO clean this up

            //hide this in the beginning and show choice game first
            //this.Visible = false;
            
            //Splash Screen
            //TODO uncomment
            //hiding for Pathways visit
            //this.Hide();
            //WelcomeForm welcomeForm = new WelcomeForm(this);
            //welcomeForm.Show();

            /* init tool bar */
            //loadToolBar();
            //this.toolsSldPnl.loadToolBar();

            createNewImage(defaultwidth, defaultheight);
            centerImage();
            //Console.WriteLine(mainImage.width);
            //mainImage.clearImage();
            mainImage.zoom = 1f;
            initArrows();
            
            //Console.WriteLine(imageX + "," + imageY);
            drawingOldPoint = new Point(0, 0);

            //make the form always take up the entire screen (not including taskbar)
            //this nullifies accidental resizing and moving of the window
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.Location = new Point(0, 0);
        }

        /// <summary>
        /// Display confirmation when closing the form to catch accidental presses
        /// Ref: http://stackoverflow.com/questions/1669318/override-standard-close-x-button-in-a-windows-form
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                return;
            }

            String exitMessage = "";

            if (this.isSaved)
            {
                exitMessage = "Exit Camera Canvas?";
            }
            else
            {
                exitMessage = "There are unsaved changes. Exit Camera Canvas without saving?";
            }

            switch (MessageBox.Show(this, exitMessage, "Exit Camera Canvas", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }


        public float Zoom
        {
            get
            {
                return mainImage.zoom;
            }

            set
            {
                mainImage.zoom = value;
            }
        }

        //private void resetState()
        public void resetState()
        {
            this.state = CCFormState.Etc;
            this.mainImage.pasting = false;
            this.mainImage.selecting = false;
            mainImage.Invalidate();
            imagePanel.Invalidate();
            this.HideMessage();

            //TODO refactor
            this.UpdateDrawingDisplay();
        }

        public void createNewImage(int width, int height)
        {      
            int i= 0;
            while (System.IO.File.Exists(this.defaultFolder + "newimage" + i.ToString() + ".png")) i++;
            mainImage = new Image(width, height, "newimage"+i.ToString()+".png");
            mainImage.fileFormat = System.Drawing.Imaging.ImageFormat.Png;
            mainImage.imagePath = this.defaultFolder;
            mainImage.clearImage();
            this.isSaved = true;
            initSelection();
            setAppStatus();
            mainImage.Invalidate();
            imagePanel.Invalidate();        
        }

        public void beginSelecting()
        {
            this.state = CCFormState.Selecting;
            this.ShowMessage("Selection started");
            this.mainImage.pasting = false;
            this.setSelection();
            mainImage.Invalidate();
            imagePanel.Invalidate();
        }



        public void updatePastePosition(Point Location)
        {
            //Console.WriteLine("updating paste");
            mainImage.pastePosition = mainImage.getPicturePoint(Location);
            mainImage.pasting = true;
            mainImage.Invalidate();
            imagePanel.Invalidate();
            
        }

        public void endPaste(Point Location)
        {
            //Console.WriteLine("pasted");
            this.ShowMessage("Selection Pasted");
            this.wavplayer.Play(@"wav\click.wav");
            this.takeSnap("image pasted");
            mainImage.pastePosition = mainImage.getPicturePoint(Location);
            mainImage.pasteImage(mainImage.getPicturePoint(Location).X, mainImage.getPicturePoint(Location).Y);
            mainImage.pasting = false;
            mainImage.Invalidate();
            imagePanel.Invalidate();
        }

        /// <summary>
        /// On Resize: recenter the image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnResizeCenterImage(object sender, EventArgs e)
        {
            this.centerImage();
        }

        /// <summary>
        /// Centers on the current image.
        /// </summary>
        public void centerImage()
        //private void centerImage()
        {
            //TODO BUG if image is larger than the window, the edges will not be shown
            if (null != mainImage)
            {
                mainImage.imageX = (int)((this.imagePanel.Width - mainImage.width * mainImage.zoom) / (2 * mainImage.zoom));
                mainImage.imageY = (int)((this.imagePanel.Height - mainImage.height * mainImage.zoom) / (2 * mainImage.zoom));
                //mainImage.Invalidate();
                this.imagePanel.Invalidate(); //force image panel to redraw mainImage in the center
            }
        }
        
        public void handleSelect(int xvector, int yvector, int box)
        {

            /* handle moving boxes */
            if (box == 0)
            {
                Point newBox1 = new Point(this.selectboxes[0].X + xvector, this.selectboxes[0].Y + yvector);
                this.selectboxes[0] = newBox1;
                Point newBox2 = new Point(this.selectboxes[1].X + xvector, this.selectboxes[1].Y + yvector);
                this.selectboxes[1] = newBox2;
            }
            else
            {
                Point newBox2 = new Point(this.selectboxes[1].X + xvector, this.selectboxes[1].Y + yvector);
                this.selectboxes[1] = newBox2;
            }

            /* update selected area on image */

            setSelection();
            
            imagePanel.Invalidate();
            
        }

        /// <summary>
        /// Sets what portion of image is being selected?   
        /// </summary>
        public void initSelection()
        //void initSelection()
        {
            selectboxes = new List<Point>();
            this.selectboxes.Add(new Point(mainImage.width / 3, mainImage.height / 3));
            this.selectboxes.Add(new Point(mainImage.width * 2 / 3, mainImage.height * 2 / 3));

            setSelection();

            resetSelectPoints();
        }

        /// <summary>
        /// Sets Application Status Indicators like the title bar
        /// </summary>
        public void setAppStatus()
        {
            //TODO
            //this.statLabel.Text =  this.mainImage.fileFormat.ToString() + " : " + this.mainImage.width + " x " + this.mainImage.height;
            this.Text = "Camera Canvas modified - " + this.mainImage.name;
        }

        void setSelection()
        {
            selectStartPoint(selectboxes[0]);
            selectEndPoint(selectboxes[1]);
            mainImage.Invalidate();
            this.imagePanel.Invalidate();
        }

        void drawSelection(Graphics g)
        {
            /*
            foreach (Rectangle rec in this.selectboxes)
            {
                g.FillRectangle(boxbrush, rec);
            }
             */
            draw_selectarrows(g);
            /*
            g.DrawImage(this.selMove, this.selectboxes[0]);
            g.DrawImage(this.selResize, this.selectboxes[1]);
            */
        }

        void drawMsgBox(Graphics g)
        {
            //draw message box on the bottom of the screen
            g.FillRectangle(msgbrush, new Rectangle(0, imagePanel.Height - 90, imagePanel.Width, 90));
            SizeF stringsize = g.MeasureString(this.msg, this.msgfont);
            g.DrawString(this.msg, this.msgfont, Brushes.White, (float)((imagePanel.Width - stringsize.Width)/2), (float)(imagePanel.Height - 90));

            //draw message box on the top of the screen too
            g.FillRectangle(msgbrush, new Rectangle(0, 0, imagePanel.Width, 90));
            stringsize = g.MeasureString(this.msg, this.msgfont);
            g.DrawString(this.msg, this.msgfont, Brushes.White, (float)((imagePanel.Width - stringsize.Width) / 2), 0.0f);
        }

        public void UpdateDrawingDisplay()
        {
            if (this.state == CCFormState.Drawing || this.state == CCFormState.DrawingWait)
            {
                if (this.drawingDisplayControl == null || this.drawingDisplayControl.IsDisposed == true)
                {
                    this.drawingDisplayControl = new DrawingDisplayControl(this);
                }
                this.drawingDisplayControl.UpdateDisplay();
                this.drawingDisplayControl.Show();
            }
            else
            {
                if (drawingDisplayControl != null && drawingDisplayControl.IsDisposed == false)
                {
                    this.drawingDisplayControl.Hide();
                }
            }
        }

        public void ShowAndFocusTextEntry()
        {
            if (textEntryControl == null || textEntryControl.IsDisposed == true)
            {
                textEntryControl = new TextEntryForm();
                textEntryControl.SetForm(this);
            }
            textEntryControl.Show();
            textEntryControl.Focus();
        }

        /// <summary>
        /// Display a notification message on the CCMainForm.
        /// </summary>
        /// <param name="msg"></param>
        public void ShowMessage(string msg)
        {
            this.msg = msg;
            this.showmsg = true;
            this.imagePanel.Invalidate();
            this.msgTimer.Start();
        }

        public void HideMessage()
        {
            this.msg = "";
            this.showmsg = false;
            this.imagePanel.Invalidate();
            this.msgTimer.Stop();
        }

        private void msgTimer_Tick(object sender, EventArgs e)
        {
            HideMessage();
        }

        public void takeSnap(string tag)
        //private void takeSnap(string tag)
        {
            mainImage.takeSnap(tag);

            //TODO QUICK
            //this.ccToolbar.RedoButton.Disable();

            //TODO QUICK
            this.ccToolbar.UndoButton.Enable();
            
            isSaved = false;
        }


        private void selectTimer_Tick(object sender, EventArgs e)
        {
            int origin_x = imagePanel.Width / 2 - 264;
            int origin_y = imagePanel.Height / 2 - 264;
            int origin_x2 = imagePanel.Width / 2;
            int origin_y2 = imagePanel.Height / 2;

            bool outside1 = false;
            bool outside2 = false;

            switch (checkArrow(imagePanel.PointToClient(MousePosition), origin_x, origin_y))
            {
                case 1:
                    handleSelect(0, -1, 0);
                    if (!selectTimer.Enabled)
                    {
                        selectTimer.Start();
                    }
                    break;
                case 2:
                    handleSelect(1, 0, 0);
                    if (!selectTimer.Enabled)
                    {
                        selectTimer.Start();
                    }
                    break;
                case 3:
                    handleSelect(0, 1, 0);
                    if (!selectTimer.Enabled)
                    {
                        selectTimer.Start();
                    }
                    break;
                case 4:
                    handleSelect(-1, 0, 0);
                    if (!selectTimer.Enabled)
                    {
                        selectTimer.Start();
                    }
                    break;
                case 5:
                    outside1 = true;
                    break;
                default:

                    break;
            }

            switch (checkArrow(imagePanel.PointToClient(MousePosition), origin_x2, origin_y2))
            {
                case 1:
                    handleSelect(0, -1, 1);
                    if (!selectTimer.Enabled)
                    {
                        selectTimer.Start();
                    }
                    break;
                case 2:
                    handleSelect(1, 0, 1);
                    if (!selectTimer.Enabled)
                    {
                        selectTimer.Start();
                    }
                    break;
                case 3:
                    handleSelect(0, 1, 1);
                    if (!selectTimer.Enabled)
                    {
                        selectTimer.Start();
                    }
                    break;
                case 4:
                    handleSelect(-1, 0, 1);
                    if (!selectTimer.Enabled)
                    {
                        selectTimer.Start();
                    }
                    break;
                case 5:
                    outside2 = true;
                    break;
                default:
                    
                    break;
            }
            //Console.WriteLine("selecting");
            imagePanel.Invalidate();
            if (this.state != CCFormState.Selecting || (this.selectTimer.Enabled && outside1 && outside2))
            {
                selectTimer.Stop();
            }

        }

        private void chaseGameBtn_Click(object sender, EventArgs e)
        {
            ChaseGame chaseGame = new ChaseGame(this);
            chaseGame.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopGame popGame = new PopGame(this);
            popGame.Show();
        }

        private void welcomeBtn_Click(object sender, EventArgs e)
        {
            WelcomeForm welcomeForm = new WelcomeForm(this);
            welcomeForm.Show();
        }

        private void CCMainForm_ResizeBegin(object sender, EventArgs e)
        {
            //Dont
        }

    }//end class

}//end namespace

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CameraCanvas.Toolbar;

namespace CameraCanvas
{
    /// <summary>
    /// Defines a CCToolbar (Camera Canvas Toolbar).
    /// </summary>
    public partial class CCToolbar : DoubleBufferPanel
    {        
        /// <summary>
        /// Defines possible orientations for the CCToolbar.
        /// </summary>
        public enum ToolbarOrientation
        {
            HorizontalTop,
            VerticalRight,
            HorizontalBottom,
            VerticalLeft
        }

        /// <summary>
        /// Defines possible directions that the CCToolbar can slide
        /// when sliding in place is enabled.
        /// </summary>
        public enum SlideDirection
        {
            Previous,
            Next
        }

        private Form ccMainForm;

        private ToolbarOrientation orientation = ToolbarOrientation.HorizontalTop;

        // Toolbar buttons
        private DoubleBufferPanel toolbarButtonsPanel; // container for the buttons on the toolbar
        private int toolbarButtonsIndex = 0;
        private Stack<int> prevToolbarIndex;
        private List<CCButton> toolbarButtons;
        private Stack<List<CCButton>> prevToolbarButtons;
        private Label toolbarLabel; 
        private Stack<string> prevToolbarLabels;

        // Main menu buttons
        public CCButton fileCCButton;
        public CCButton imageCCButton;
        public CCButton drawingCCButton;
        public CCButton settingsCCButton;
        public CCButton aboutCCButton;

        // Navigation buttons
        private DoubleBufferPanel navButtonsPanel; // for grouping standard buttons together
        private PrevCCButton prevCCBtn; // ie: left / up
        private NextCCButton nextCCBtn; //ie: right / down
        private BackCCButton backCCBtn;
        private SettingsCCButton settingsCCBtn;
        private MainMenuCCButton mainMenuCCBtn;

        private UndoCCButton undoBtn;
        private RedoCCButton redoBtn;

        private int SPACE_BTWN_BTNS = 20; //pixels between buttons
        private int buttonWidth = 100;
        private int buttonHeight = 100;

        private bool autoSlide = false;
        private Timer autoSlideTimer;
        private int autoSlideInterval = 500;
        private SlideDirection lastSlideDirection = SlideDirection.Next;

        CCButton morePrevBtn;
        CCButton moreNextBtn;

        Color toolbarColor = Color.DarkSlateBlue;

        /// <summary>
        /// Construct the CCToolbar by initializing and adding all the buttons.
        /// Its default orientation is HorizontalTop.
        /// </summary>
        /// <param name="ccMainForm"></param>
        public CCToolbar(Form sourceForm)
        {
            InitializeComponent();

            this.ccMainForm = sourceForm;

            morePrevBtn = new PrevCCButton(this);
            //morePrevBtn.SetIcon("Icons/MoreLeft.png");
            morePrevBtn.FlashOn();

            moreNextBtn = new NextCCButton(this);
            //moreNextBtn.SetIcon("Icons/MoreRight.png");
            moreNextBtn.FlashOn();

            this.BackColor = this.toolbarColor;//Color.DarkBlue;
            //this.BackColor = Color.MidnightBlue;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.BringToFront();

            // Re-center the toolbar (reset the button scrolling) when its size changes
            this.SizeChanged += new EventHandler(OnResizeCenterToolbar);

            SetToolbarDockLocation();

            InitializeAllToolbarItems();

            ShowMoreArrows();

            // Initialize auto slide slideTimer
            autoSlideTimer = new Timer();
            autoSlideTimer.Interval = autoSlideInterval;
            autoSlideTimer.Tick += new EventHandler(autoSlideTimer_Tick);
            autoSlideTimer.Stop();
        }

        private void ShowMoreArrows()
        {
            switch (this.orientation)
            {
                case ToolbarOrientation.HorizontalTop:
                    //same as HorizontalBottom
                case ToolbarOrientation.HorizontalBottom:
                    //morePrevBtn.SetIcon("Icons/MoreLeft.png");
                    
                    morePrevBtn.Location = new Point(0, toolbarButtonsPanel.Location.Y);
                    //moreNextBtn.SetIcon("Icons/MoreRight.png");
                    moreNextBtn.Location = new Point(this.Width - moreNextBtn.Width, toolbarButtonsPanel.Location.Y);
                    break;
                case ToolbarOrientation.VerticalLeft:
                    //morePrevBtn.SetIcon("Icons/MoreUp.png");
                    morePrevBtn.Location = new Point(0, 0);
                    //moreNextBtn.SetIcon("Icons/MoreDown.png");
                    moreNextBtn.Location = new Point(0, this.Height - moreNextBtn.Height);
                    break;
                case ToolbarOrientation.VerticalRight:
                    //morePrevBtn.SetIcon("Icons/MoreUp.png");
                    morePrevBtn.Location = new Point(toolbarButtonsPanel.Location.X, 0);
                    //moreNextBtn.SetIcon("Icons/MoreDown.png");
                    moreNextBtn.Location = new Point(toolbarButtonsPanel.Location.X, this.Height - moreNextBtn.Height);
                    break;
                default:
                    break;
            }
            ((PrevCCButton)morePrevBtn).UpdateImage();
            ((NextCCButton)moreNextBtn).UpdateImage();

            if (toolbarButtonsPanel.Width > 0 && toolbarButtonsPanel.Height > 0)
            {
                if (toolbarButtonsPanel.Location.X < 0 || toolbarButtonsPanel.Location.Y < 0)
                {
                    if (this.Controls.Contains(morePrevBtn) == false)
                    {
                        this.Controls.Add(morePrevBtn);
                        morePrevBtn.BringToFront();
                        toolbarLabel.BringToFront();
                    }
                    morePrevBtn.Show();
                }
                else
                {
                    morePrevBtn.Hide();
                }

                if (toolbarButtonsPanel.Location.X + toolbarButtonsPanel.Width > this.Width
                    || toolbarButtonsPanel.Location.Y + toolbarButtonsPanel.Height > this.Height)
                {
                    if (this.Controls.Contains(moreNextBtn) == false)
                    {
                        this.Controls.Add(moreNextBtn);
                        moreNextBtn.BringToFront();
                        this.toolbarLabel.BringToFront();
                    }
                    moreNextBtn.Show();
                }
                else
                {
                    moreNextBtn.Hide();
                }
            }
        }

        /// <summary>
        /// On Tick: Automatically slide toolbar towards prev or next depending on
        /// what direction was last chosen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoSlideTimer_Tick(object sender, EventArgs e)
        {
            if (autoSlide == true)
            {
                if (lastSlideDirection == SlideDirection.Previous)
                {
                    this.SlideToolbarButtonsToPrev();
                }
                else
                {
                    this.SlideToolbarButtonsToNext();
                }
            }
        }

        /// <summary>
        /// Get or set automatic sliding.
        /// </summary>
        public bool AutoSlide
        {
            get { return this.autoSlide; }
            set { this.autoSlide = value; }
        }

        /// <summary>
        /// Turn on automatic sliding of the toolbar.
        /// </summary>
        public void StartAutomaticSliding()
        {
            if (autoSlide == true)
            {
                autoSlideTimer.Start();
            }
        }

        public void StartAutomaticSliding(SlideDirection direction)
        {
            lastSlideDirection = direction;
            StartAutomaticSliding();
        }

        /// <summary>
        /// Turn off automatic sliding of the toolbar.
        /// </summary>
        public void StopAutomaticSliding()
        {
            autoSlideTimer.Stop();
        }

        /// <summary>
        /// Get or set the interval of the autoslidetimer
        /// (affects how fast the toolbar moves)
        /// </summary>
        public int AutoSlideInterval
        {
            get { return this.autoSlideTimer.Interval; }
            set { if (value > 0) {this.autoSlideTimer.Interval = value; } }
        }

        /// <summary>
        /// Get the name of the current set of toolbar buttons.
        /// </summary>
        public string ToolbarName
        {
            get { return toolbarLabel.Text; }
        }

        /// <summary>
        /// Construct the CCToolbar of a given orientation.
        /// </summary>
        /// <param name="ccMainForm"></param>
        /// <param name="orientation"></param>
        public CCToolbar(Form sourceForm, ToolbarOrientation orientation) : this(sourceForm)
        {
            ChangeOrientation(orientation);
        }

        /// <summary>
        /// Get the form that the toolbar is inside of.
        /// </summary>
        public CCMainForm CCMainForm
        {
            get { return (CCMainForm)ccMainForm; }
        }

        /// <summary>
        /// Get or set the toolbar button currently selected.
        /// </summary>
        public CCButton SelectedButton
        {
            get { return toolbarButtons[toolbarButtonsIndex]; }
        }

        /// <summary>
        /// Get the program's Undo button.
        /// </summary>
        public CCButton UndoButton
        {
            get
            {
                if (null == undoBtn || undoBtn.IsDisposed)
                {
                    undoBtn = new UndoCCButton(this);
                }
                return undoBtn;
            }
        }

        /// <summary>
        /// Get the program's Redo button.
        /// </summary>
        public CCButton RedoButton
        {
            get
            {
                if (null == redoBtn || redoBtn.IsDisposed)
                {
                    redoBtn = new RedoCCButton(this);
                }
                return redoBtn;
            }
        }

        public int ButtonWidth
        {
            get { return this.buttonWidth; }
        }

        public int ButtonHeight
        {
            get { return this.buttonHeight; }
        }

        /// <summary>
        /// Get the toolbar's orientation.
        /// </summary>
        public ToolbarOrientation Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        /// <summary>
        /// Set the Dock property of the toolbar based on its orientation.
        /// </summary>
        private void SetToolbarDockLocation()
        {
            switch (this.orientation)
            {
                case ToolbarOrientation.HorizontalTop:
                    this.Dock = DockStyle.Top;
                    break;
                case ToolbarOrientation.HorizontalBottom:
                    this.Dock = DockStyle.Bottom;
                    break;
                case ToolbarOrientation.VerticalLeft:
                    this.Dock = DockStyle.Left;
                    break;
                case ToolbarOrientation.VerticalRight:
                    this.Dock = DockStyle.Right;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Initialize everything on the toolbar.
        /// </summary>
        private void InitializeAllToolbarItems()
        {
            InitializeToolbarLabel();
            InitializeNavButtons();
            InitializeToolbarButtons();
            AddMainMenuButtons();
        }

        /// <summary>
        /// Initialize label on top of the toolbar.
        /// </summary>
        private void InitializeToolbarLabel()
        {
            toolbarLabel = new Label();
            toolbarLabel.BackColor = Color.Cornsilk;
            toolbarLabel.AutoSize = true;
            toolbarLabel.TextAlign = ContentAlignment.MiddleCenter;
            Font toolbarLabelFont = new Font(FontFamily.GenericSansSerif, 21, FontStyle.Bold);
            toolbarLabel.Font = toolbarLabelFont;

            this.Controls.Add(toolbarLabel);

            SetToolbarLabelLocation();
        }

        /// <summary>
        /// Set the location of the toolbar label based on the toolbar's orientation.
        /// </summary>
        private void SetToolbarLabelLocation()
        {
            // For now, all orientations have label at the top
            toolbarLabel.Location = new Point(0, 0);
        }

        /// <summary>
        /// Initialize the toolbar buttons.
        /// </summary>
        private void InitializeToolbarButtons()
        {
            toolbarButtons = new List<CCButton>();
            prevToolbarButtons = new Stack<List<CCButton>>();
            prevToolbarLabels = new Stack<string>();
            prevToolbarIndex = new Stack<int>();

            toolbarButtonsPanel = new DoubleBufferPanel();
            toolbarButtonsPanel.Location = new Point(0, 0);
            toolbarButtonsPanel.Size = new Size(0, 50);
            toolbarButtonsPanel.AutoSize = true;
            toolbarButtonsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            toolbarButtonsPanel.BackColor = this.toolbarColor;//Color.BlueViolet;
            //toolbarButtonsPanel.BackColor = Color.MidnightBlue;

            this.Controls.Add(toolbarButtonsPanel);

            SetToolbarButtonsPanelLocation();
        }

        /// <summary>
        /// Set the location of the toolbar buttons panel based on the toolbar's orientation.
        /// </summary>
        private void SetToolbarButtonsPanelLocation()
        {
            int toolbarButtonsPanelX = 0;
            int toolbarButtonsPanelY = 0;

            switch (this.orientation)
            {
                case ToolbarOrientation.HorizontalTop:
                    toolbarButtonsPanelX = 0;
                    toolbarButtonsPanelY = toolbarLabel.Height; //under the label 
                    break;
                case ToolbarOrientation.HorizontalBottom:
                    toolbarButtonsPanelX = 0;
                    toolbarButtonsPanelY = toolbarLabel.Height + buttonHeight + SPACE_BTWN_BTNS; //under the label and the navbuttons
                    break;
                case ToolbarOrientation.VerticalLeft:
                    toolbarButtonsPanelX = 0;
                    toolbarButtonsPanelY = 0;
                    break;
                case ToolbarOrientation.VerticalRight:
                    toolbarButtonsPanelX = buttonWidth + SPACE_BTWN_BTNS; //right of navbuttons
                    toolbarButtonsPanelY = 0;
                    break;
                default:
                    break;
            }
            
            toolbarButtonsPanel.Location = new Point(toolbarButtonsPanelX, toolbarButtonsPanelY);
        }

        /// <summary>
        /// Initialize and add the navigation buttons to the toolbar.
        /// </summary>
        private void InitializeNavButtons()
        {
            navButtonsPanel = new DoubleBufferPanel();
            navButtonsPanel.AutoSize = true;
            navButtonsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            navButtonsPanel.BackColor = this.toolbarColor;//Color.ForestGreen;
            //navButtonsPanel.BackColor = Color.MidnightBlue;

            this.Controls.Add(navButtonsPanel);

            SetNavButtonsPanelLocation();

            prevCCBtn = new PrevCCButton(this); // ie: left / up
            nextCCBtn = new NextCCButton(this); // ie: right / down
            backCCBtn = new BackCCButton(this);
            settingsCCBtn = new SettingsCCButton(this);
            mainMenuCCBtn = new MainMenuCCButton(this);

            // Actually draw the buttons in the panel
            AddNavButtonsToPanel();
        }

        /// <summary>
        /// Set the location of the navigation buttons panel based on the toolbar's orientation.
        /// </summary>
        private void SetNavButtonsPanelLocation()
        {
            int navButtonsPanelX = 0;
            int navButtonsPanelY = 0;

            switch (this.orientation)
            {
                case ToolbarOrientation.HorizontalTop:
                    navButtonsPanelX = 0;
                    navButtonsPanelY = toolbarLabel.Height + buttonHeight + SPACE_BTWN_BTNS; // under tool btns
                    break;
                case ToolbarOrientation.HorizontalBottom:
                    navButtonsPanelX = 0;
                    navButtonsPanelY = toolbarLabel.Height; // under label, above tool btns
                    break;
                case ToolbarOrientation.VerticalLeft:
                    navButtonsPanelX = buttonWidth + SPACE_BTWN_BTNS; // right of tool btns
                    navButtonsPanelY = 0;
                    break;
                case ToolbarOrientation.VerticalRight:
                    navButtonsPanelX = 0; // left of tool btns
                    navButtonsPanelY = 0;
                    break;
                default:
                    break;
            }

            navButtonsPanel.Location = new Point(navButtonsPanelX, navButtonsPanelY);
        }

        /// <summary>
        /// Add navigation buttons to the toolbar.
        /// </summary>
        private void AddNavButtonsToPanel()
        {
            // Clear out controls in case orientation was just changed and buttons are re-added
            navButtonsPanel.Controls.Clear(); 

            AddNavButtonToPanelAtIndex(backCCBtn, 0);
            AddNavButtonToPanelAtIndex(prevCCBtn, 1);
            // Index 2 is left empty to give users some resting space
            AddNavButtonToPanelAtIndex(nextCCBtn, 3);
            AddNavButtonToPanelAtIndex(mainMenuCCBtn, 4);
        }

        /// <summary>
        /// Helper method: Add a navigation button to the toolbar at a given index.
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="index"></param>
        private void AddNavButtonToPanelAtIndex(CCButton navButton, int index)
        {
            int x = 0;
            int y = 0;

            if (orientation == ToolbarOrientation.HorizontalTop
                || orientation == ToolbarOrientation.HorizontalBottom)
            {
                x = index * (buttonWidth + SPACE_BTWN_BTNS);
                y = 0;
            }
            else if (orientation == ToolbarOrientation.VerticalLeft
                || orientation == ToolbarOrientation.VerticalRight)
            {
                x = 0;
                y = index * (buttonHeight + SPACE_BTWN_BTNS);
            }

            navButton.Location = new Point(x, y);

            navButtonsPanel.Controls.Add(navButton);
        }

        /// <summary>
        /// Add the MainMenu buttons to the toolbar.
        /// </summary>
        private void AddMainMenuButtons()
        {
            List<CCButton> mainMenuButtons = new List<CCButton>();

            fileCCButton = new Toolbox.FileCCButton(this);
            imageCCButton = new Toolbox.ImageCCButton(this);
            drawingCCButton = new Toolbox.DrawingCCButton(this);
            settingsCCButton = new Toolbar.SettingsCCButton(this);
            aboutCCButton = new Toolbox.AboutCCButton(this);

            mainMenuButtons.Add(fileCCButton);
            mainMenuButtons.Add(imageCCButton);
            mainMenuButtons.Add(drawingCCButton);
            mainMenuButtons.Add(settingsCCButton);
            mainMenuButtons.Add(aboutCCButton);

            AddNewToolbarButtons(mainMenuButtons, "Main Menu");
        }

        /// <summary>
        /// Add a list of buttons to the toolbar.
        /// </summary>
        /// <param name="buttons"></param>
        private void AddButtonsToToolbar(List<CCButton> buttons)
        {
            // Use a double-buffering like method 
            // to reduce screen twitch when changing buttons
            // - create a copy of the toolbar panel to put the new buttons on
            DoubleBufferPanel tempPanel = new DoubleBufferPanel();
            tempPanel.Location = toolbarButtonsPanel.Location;
            tempPanel.Size = toolbarButtonsPanel.Size;
            tempPanel.AutoSize = toolbarButtonsPanel.AutoSize;
            tempPanel.AutoSizeMode = toolbarButtonsPanel.AutoSizeMode;
            tempPanel.BackColor = toolbarButtonsPanel.BackColor;

            // Add new buttons to the copy panel
            for (int i = 0; i < buttons.Count; i++)
            {
                CCButton currentButton = buttons[i];

                // Make sure button is the current desired size
                currentButton.Width = buttonWidth;
                currentButton.Height = buttonHeight;

                int x = 0;
                int y = 0;

                switch (this.orientation)
                {
                    case ToolbarOrientation.HorizontalTop:
                        x = i * (currentButton.Width + SPACE_BTWN_BTNS);
                        y = 0;
                        break;
                    case ToolbarOrientation.HorizontalBottom:
                        goto case ToolbarOrientation.HorizontalTop;
                    case ToolbarOrientation.VerticalLeft:
                        x = 0;
                        y = i * (currentButton.Height + SPACE_BTWN_BTNS);
                        break;
                    case ToolbarOrientation.VerticalRight:
                        goto case ToolbarOrientation.VerticalLeft;
                    default:
                        break;
                }

                currentButton.Location = new Point(x, y);

                tempPanel.Controls.Add(currentButton);
            }

            // Swap the toolbar panel and the copy
            // -- the order of operations is very important
            this.Controls.Add(tempPanel);
            toolbarButtonsPanel.Hide();
            toolbarButtonsPanel.Controls.Clear();
            toolbarButtonsPanel = tempPanel;
            
            // Reset toolbar panel with starting settings
            // since this is a new set of buttons
            SetToolbarButtonsPanelLocation();
            toolbarButtonsIndex = 0;
            lastSlideDirection = SlideDirection.Next;
            CenterizeToolbar();
        }

        /// <summary>
        /// Re-center the toolbar in relation to the window
        /// wrapper to give to event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnResizeCenterToolbar(object sender, EventArgs e)
        {
            // Center the toolbar panel to the screen
            CenterizeToolbar();

            // Slide the toolbar buttons over so the middle button is in the center
            this.SlideToolbarButtonsToCenter();
        }

        /// <summary>
        /// Re-center the toolbar in relation to the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CenterizeToolbar()
        {
            // Freeze the layout engine so user does not see an ugly intermediate state
            this.SuspendLayout();

            if (orientation == ToolbarOrientation.HorizontalTop
                || orientation == ToolbarOrientation.HorizontalBottom)
            {
                int windowHorizMidpt = this.ClientSize.Width / 2;
                
                // Before updating each control's location, we hide the control
                // since layout engine is frozen, user will still see old version
                toolbarLabel.Hide();
                toolbarLabel.Left = windowHorizMidpt - toolbarLabel.Width / 2;
                toolbarLabel.Show();

                // Align start of toolbar buttons with space in between prev and next arrows
                toolbarButtonsPanel.Hide();
                toolbarButtonsPanel.Left = (windowHorizMidpt - (int)(0.5 * buttonWidth));
                toolbarButtonsIndex = 0;
                toolbarButtonsPanel.Show();

                navButtonsPanel.Hide();
                navButtonsPanel.Left = windowHorizMidpt - navButtonsPanel.Width / 2;
                navButtonsPanel.Show();

            }
            else if (orientation == ToolbarOrientation.VerticalLeft
                || orientation == ToolbarOrientation.VerticalRight)
            {
                int windowVertMidpt = this.ClientSize.Height / 2;
                int windowHorizMidpt = this.ClientSize.Width / 2;

                // Before updating each control's location we hide the control
                // since layout engine is frozen, user will still see old version
                toolbarLabel.Hide();
                toolbarLabel.Left = windowHorizMidpt - toolbarLabel.Width / 2;
                toolbarLabel.Show();
                
                // Align start of toolbar buttons with space in between prev and next arrows
                toolbarButtonsPanel.Hide();
                toolbarButtonsPanel.Top = windowVertMidpt - (int)(0.5 * buttonHeight);
                toolbarButtonsIndex = 0;
                toolbarButtonsPanel.Show();

                navButtonsPanel.Hide();
                navButtonsPanel.Top = windowVertMidpt - navButtonsPanel.Height / 2;
                navButtonsPanel.Show();
            }

            //TODO refactor
            this.ShowMoreArrows();
            
            // Unfreeze layout engine and show user updated toolbar
            this.ResumeLayout();
        }

        /// <summary>
        /// Redraw everything on the toolbar.
        /// </summary>
        private void RedrawToolbar()
        {
            // Freeze the parent form's layout engine
            // so user does not see ugly intermediate state
            // we freeze the parent because we may be changing the dock location
            this.Parent.SuspendLayout();
            
            // Redraw the toolbar behind the scenes:
            
            // Update the locations of all the controls
            SetToolbarDockLocation();
            SetToolbarLabelLocation();
            SetNavButtonsPanelLocation();
            SetToolbarButtonsPanelLocation();

            // Add the buttons back to panels with possibly updated properties
            AddNavButtonsToPanel();
            AddButtonsToToolbar(this.toolbarButtons);

            // Re-center because change in properties may have caused toolbar to be off-center
            CenterizeToolbar();

            UpdateDrawingDisplayPanelLocation();

            // Unfreeze the layout engine and let user see redrawn toolbar
            this.Parent.ResumeLayout();
        }

        /// <summary>
        /// Change the orientation of the toolbar to a new orientation.
        /// </summary>
        /// <param name="newOrientation"></param>
        public void ChangeOrientation(ToolbarOrientation newOrientation)
        {
            // Don't bother changing if newOrientation is the same as current
            if (newOrientation != this.orientation)
            {
                this.orientation = newOrientation;

                RedrawToolbar();

                // Arrows may need to point in a different direction now
                prevCCBtn.UpdateImage();
                nextCCBtn.UpdateImage();

                // Image may need to be recentered in screen since toolbar moved
                // TODO refactor
                //((CCMainForm)ccMainForm).resetState();
                this.CCMainForm.centerImage();
                this.CCMainForm.MainImage.Invalidate();
                this.CCMainForm.getImagePanel().Invalidate();
            }
        }

        private void UpdateDrawingDisplayPanelLocation()
        {
            // TODO refactor
            Control statusPanel = ((CCMainForm)ccMainForm).StatusPanel;
            CCMainForm mainForm = ((CCMainForm)ccMainForm);

            switch (this.orientation)
            {
                case ToolbarOrientation.HorizontalTop:
                case ToolbarOrientation.VerticalLeft:
                    statusPanel.Top = 0;
                    statusPanel.Left = 0;
                    break;
                case ToolbarOrientation.VerticalRight:
                    statusPanel.Top = 0;
                    statusPanel.Left = 0;
                    break;
                case ToolbarOrientation.HorizontalBottom:
                    statusPanel.Top = 0;
                    statusPanel.Left = 0;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Change the size of all the buttons on the toolbar to a new size.
        /// </summary>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        public void ChangeButtonSize(int newWidth, int newHeight)
        {
            // Do not let new values go below min or above max sizes
            newWidth = Math.Max(CCButton.MIN_WIDTH, Math.Min(CCButton.MAX_WIDTH, newWidth));
            newHeight = Math.Max(CCButton.MIN_HEIGHT, Math.Min(CCButton.MAX_HEIGHT, newHeight));

            // Don't bother redrawing if new size is same as current size
            if (newWidth == buttonWidth && newHeight == buttonHeight)
            {
                return;
            }

            // Update and store new dimensions
            buttonWidth = newWidth;
            buttonHeight = newHeight;

            // Change and update nav buttons
            foreach (CCButton navBtn in navButtonsPanel.Controls)
            {
                navBtn.Width = newWidth;
                navBtn.Height = newHeight;
            }

            // Change and update toolbar buttons
            foreach (CCButton toolbarBtn in toolbarButtonsPanel.Controls)
            {
                toolbarBtn.Width = newWidth;
                toolbarBtn.Height = newHeight;
            }

            // Change and update "more" buttons
            morePrevBtn.Size = new Size(newWidth, newHeight);
            moreNextBtn.Size = new Size(newWidth, newHeight);

            RedrawToolbar();
        }

        /// <summary>
        /// Add a new set of buttons to the toolbar, replacing the last set.
        /// </summary>
        /// <param name="newToolbarButtons"></param>
        /// <param name="newToolbarName"></param>
        public void AddNewToolbarButtons(List<CCButton> newToolbarButtons, string newToolbarName)
        {
            if (toolbarButtons.Count > 0)
            {
                // Backup previous toolbar, label, and index
                prevToolbarButtons.Push(toolbarButtons);
                prevToolbarLabels.Push(toolbarLabel.Text);
                prevToolbarIndex.Push(toolbarButtonsIndex);

                // Enable back/cancel button so you can go back
                backCCBtn.Enable();

                // Enable main menu button so you can go back
                mainMenuCCBtn.Enable();
            }

            // Update current toolbar variables
            toolbarButtons = newToolbarButtons;
            toolbarLabel.Text = newToolbarName;

            // Actually add the new buttons to the toolbar
            AddButtonsToToolbar(toolbarButtons);

            // TODO refactor make clearer

            SlideToolbarButtonsToCenter();
        }

        /// <summary>
        /// Slide until the center button is in the center of the screen.
        /// </summary>
        private void SlideToolbarButtonsToCenter()
        {
            // if the toolbar is larger than the screen: slide the first icon to the left most position
            while (
                (toolbarButtonsIndex < toolbarButtons.Count / 2)
                &&
                !(toolbarButtonsPanel.Location.X < 0 || toolbarButtonsPanel.Location.Y < 0)
                )
            {
                this.SlideToolbarButtonsToNext();
            }

            // if at this point the first icon will be just off screen so slide it back on screen
            if ((toolbarButtonsPanel.Location.X < 0 || toolbarButtonsPanel.Location.Y < 0))
            {
                this.SlideToolbarButtonsToPrev();
            }
        }

        /// <summary>
        /// Remove the current set of toolbar buttons, restoring toolbar to the previous set.
        /// </summary>
        public void RemoveCurrentToolbarButtons()
        {
            if (prevToolbarButtons.Count > 0)
            {
                // Update current variables to previous from the stacks
                toolbarButtons = prevToolbarButtons.Pop();
                toolbarLabel.Text = prevToolbarLabels.Pop();
                
                // Actually add the buttons to the toolbar
                AddButtonsToToolbar(toolbarButtons);

                // Slide the toolbar back to the previous index
                toolbarButtonsIndex = prevToolbarIndex.Pop();
                UpdateToolbarButtonsLocation(toolbarButtonsPanel, toolbarButtonsIndex, SlideDirection.Next);

                // We are now at the top most level of buttons
                if (prevToolbarButtons.Count == 0)
                {
                    // Back/Cancel button cannot go back anymore
                    backCCBtn.Disable();

                    // Reset the form state
                    ((CCMainForm)ccMainForm).resetState();

                    // Main menu button should not work since we are at main menu
                    mainMenuCCBtn.Disable();
                }
            }
        }

        /// <summary>
        /// Remove all sets of toolbar buttons added so far, restoring toolbar to the Main Menu.
        /// </summary>
        public void RemoveAllToolbarButtons()
        {
            // The Main Menu buttons are the first thing (bottommost) in the stack
            while (prevToolbarButtons.Count > 0)
            {
                RemoveCurrentToolbarButtons();
            }
        }

        /// <summary>
        /// Slide the toolbar to the previous button.
        /// </summary>
        public void SlideToolbarButtonsToPrev()
        {
            // Do not slide before the first button
            if (toolbarButtonsIndex > 0)
            {
                UpdateToolbarButtonsLocation(toolbarButtonsPanel, 1, SlideDirection.Previous);
                toolbarButtonsIndex--;
            }

            // If autosliding and at first button, wrap around to the last button
            else if (toolbarButtonsIndex == 0 && this.autoSlide == true)
            {
                UpdateToolbarButtonsLocation(toolbarButtonsPanel, toolbarButtons.Count - 1, SlideDirection.Next);
                toolbarButtonsIndex = toolbarButtons.Count - 1;
            }

            lastSlideDirection = SlideDirection.Previous;
        }

        /// <summary>
        /// Slide the toolbar to the next button.
        /// </summary>
        public void SlideToolbarButtonsToNext()
        {
            // Do not slide after the last button
            if (toolbarButtonsIndex < toolbarButtons.Count - 1)
            {
                UpdateToolbarButtonsLocation(toolbarButtonsPanel, 1, SlideDirection.Next);
                toolbarButtonsIndex++;
            }

            // If autosliding and at last button, wrap around to first button
            else if (toolbarButtonsIndex == toolbarButtons.Count - 1 && this.autoSlide == true)
            {
                UpdateToolbarButtonsLocation(toolbarButtonsPanel, toolbarButtons.Count - 1, SlideDirection.Previous);
                toolbarButtonsIndex = 0;
            }

            lastSlideDirection = SlideDirection.Next;
        }

        /// <summary>
        /// Helper method to update the physical location of the toolbar buttons.
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="buttonPlacesToSlide"></param>
        /// <param name="direction"></param>
        private void UpdateToolbarButtonsLocation(Panel panel, int buttonPlacesToSlide, SlideDirection direction)
        {
            // If sliding towards next, we are moving the toolbar in the negative direction
            if (direction == SlideDirection.Next)
            {
                buttonPlacesToSlide = -1 * buttonPlacesToSlide;
            }

            Point newLocation = panel.Location;

            switch (this.orientation)
            {
                case ToolbarOrientation.HorizontalTop:
                    // same as HorizontalBottom
                case ToolbarOrientation.HorizontalBottom:
                    newLocation.X = panel.Location.X + buttonPlacesToSlide * (buttonWidth + SPACE_BTWN_BTNS);
                    break;
                case ToolbarOrientation.VerticalLeft:
                    // same as VerticalRight
                case ToolbarOrientation.VerticalRight:
                    newLocation.Y = panel.Location.Y + buttonPlacesToSlide * (buttonHeight + SPACE_BTWN_BTNS);
                    break;
                default:
                    break;
            }

            panel.Location = newLocation;

            ShowMoreArrows();
        }

    } // end class
} // end namespace

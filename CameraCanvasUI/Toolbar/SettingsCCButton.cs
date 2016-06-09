using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CameraCanvas.Toolbar.Config;

namespace CameraCanvas.Toolbar
{
    /// <summary>
    /// Defines Settings Button in the MainMenu on the CCToolbar.
    /// </summary>
    class SettingsCCButton : CCButton
    {
        private List<CCButton> settingsToolbarButtons;
        private string name = "Settings";

        public SettingsCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "&Settings", "Icons/Settings.png")
        {
            settingsToolbarButtons = new List<CCButton>();

            CCButton toolbarPlacementBtn = new CCButton(ccToolbar, "Toolbar Placement", "Icons/ToolbarPlacement.png");
            toolbarPlacementBtn.actionEvent += new actionEventHandler(toolbarPlacementBtn_actionEvent);
            settingsToolbarButtons.Add(toolbarPlacementBtn);

            CCButton buttonSizeBtn = new CCButton(ccToolbar, "Button Size", "Icons/ButtonSize.png");
            buttonSizeBtn.actionEvent += new actionEventHandler(buttonSizeBtn_actionEvent);
            settingsToolbarButtons.Add(buttonSizeBtn);

            CCButton slideSpeedBtn = new CCButton(ccToolbar, "Sliding Speed", "Icons/SlidingSpeed.png");
            slideSpeedBtn.actionEvent += new actionEventHandler(slideSpeedBtn_actionEvent);
            settingsToolbarButtons.Add(slideSpeedBtn);

            CCButton welcomeBtn = new CCButton(ccToolbar, "Play Games", "Icons/PlayGames.png");
            welcomeBtn.actionEvent += new actionEventHandler(welcomeBtn_actionEvent);
            settingsToolbarButtons.Add(welcomeBtn);

            //TODO delete/uncomment
            //hiding before the Pathways visit
            //CCButton movementPanelBtn = new CCButton(ccToolbar, "Movement Panel");
            //movementPanelBtn.actionEvent += new actionEventHandler(movementPanelBtn_actionEvent);
            //settingsToolbarButtons.Add(movementPanelBtn);        
        }

        void movementPanelBtn_actionEvent()
        {
            MovementForm movementForm = new MovementForm(this.ccToolbar);
            movementForm.StartPosition = FormStartPosition.CenterScreen;
            movementForm.ShowDialog();
            movementForm.TopMost = true;
        }

        void welcomeBtn_actionEvent()
        {
            //WelcomeForm welcomeForm = new WelcomeForm(this.ccMainForm);
            //welcomeForm.Show();

            ChaseGame chaseGame = new ChaseGame(this.ccMainForm);
            chaseGame.ShowDialog();
        }

        void slideSpeedBtn_actionEvent()
        {
            SlidingSpeedChoiceForm scf = new SlidingSpeedChoiceForm(ccToolbar);
            scf.ShowDialog();
        }

        void buttonSizeBtn_actionEvent()
        {
            ButtonSizeChoiceForm iscf = new ButtonSizeChoiceForm(ccToolbar);
            iscf.ShowDialog();
        }

        void toolbarPlacementBtn_actionEvent()
        {
            ToolbarPlacementForm tpf = new ToolbarPlacementForm(ccToolbar);
            tpf.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            //change toolbar buttons to settings buttons if not already changed
            if (ccToolbar.ToolbarName != this.name)
            {
                ccToolbar.AddNewToolbarButtons(settingsToolbarButtons, this.name);
            }
        }
    }
}

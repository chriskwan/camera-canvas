using System;
using System.Collections.Generic;
using System.Text;

namespace CameraCanvas.FileTools
{
    /// <summary>
    /// Defines the Exit button in the File tools on the toolbar.
    /// </summary>
    class ExitCCButton : CCButton
    {
        /// <summary>
        /// Defines a button representing an option upon trying to exit.
        /// </summary>
        public class ExitOptionsCCButton : CCButton
        {
            private bool saveAndExit = false;

            /// <summary>
            /// Construct a exit option button.
            /// </summary>
            /// <param name="ccToolbar"></param>
            public ExitOptionsCCButton(CCToolbar ccToolbar, string text, bool saveAndExit)
                : base(ccToolbar, text)
            {
                this.saveAndExit = saveAndExit;
            }


            /// <summary>
            /// On Click: Save and Exit or Exit without Saving.
            /// </summary>
            public override void ClickAction(object sender, EventArgs e)
            {
                if (saveAndExit == true)
                {
                    //change toolbar back to previous so user will be presented
                    //with "File Tools" after choosing a save option
                    ccToolbar.RemoveCurrentToolbarButtons();

                    //present save options
                    SaveCCButton saveBtn = new SaveCCButton(this.ccToolbar);
                    saveBtn.ClickAction(sender, e);
                }
                //exit without saving
                else
                {
                    ccMainForm.Close();
                }
    
            }

        }


        private List<CCButton> exitOptionsButtons;

        /// <summary>
        /// Construct an Exit button on the toolbar.
        /// </summary>
        /// <param name="ccToolbar"></param>
        /// ref icon: http://www.openclipart.org/detail/20476
        public ExitCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Exit", "Icons/exit.png")
        {
            exitOptionsButtons = new List<CCButton>();

            //Save and Exit
            ExitOptionsCCButton saveAndExitBtn
                = new ExitOptionsCCButton(this.ccToolbar, "Save and Exit", true);
            exitOptionsButtons.Add(saveAndExitBtn);

            //Exit without Saving
            ExitOptionsCCButton exitWithoutSavingBtn 
                = new ExitOptionsCCButton(this.ccToolbar, "Exit Without Saving", false);
            exitOptionsButtons.Add(exitWithoutSavingBtn);
        }


        /// <summary>
        /// On Click: Change toolbar buttons to Exit options.
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            //unsaved changes, give option to save and exit
            if (ccMainForm.IsSaved == false)
            {
                ccToolbar.AddNewToolbarButtons(exitOptionsButtons, "Exit");
                this.ccMainForm.State = CCMainForm.CCFormState.Etc;
            }
            //no changes made just exit
            else
            {
                ccMainForm.Close();
            }

        }

    }
}

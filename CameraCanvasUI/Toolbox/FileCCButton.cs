using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CameraCanvas.FileTools;
using CameraCanvas.Toolbar;

namespace CameraCanvas.Toolbox
{
    /// <summary>
    /// Defines File Button in the MainMenu in the CCToolbar.
    /// </summary>
    class FileCCButton : CCButton
    {
        List<CCButton> fileToolbarButtons;

        public FileCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "&File", "Icons/File.png")
        {
            fileToolbarButtons = new List<CCButton>();
            
            NewCCButton newCCButton = new NewCCButton(this.ccToolbar);
            fileToolbarButtons.Add(newCCButton);

            OpenCCButton openCCButton = new OpenCCButton(this.ccToolbar);
            fileToolbarButtons.Add(openCCButton);

            SaveCCButton saveCCButton = new SaveCCButton(this.ccToolbar);
            fileToolbarButtons.Add(saveCCButton);

            ExitCCButton exitCCButton = new ExitCCButton(this.ccToolbar);
            fileToolbarButtons.Add(exitCCButton);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            ccToolbar.AddNewToolbarButtons(fileToolbarButtons, "File");
        }
    }
}

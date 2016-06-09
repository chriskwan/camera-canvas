using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO; //for File and Directory

namespace CameraCanvas.FileTools
{
    /// <summary>
    /// Button that displays list of drives on the computer.
    /// Similar functionality to "My Computer".
    /// </summary>
    public class DriveListCCButton : CCButton
    {
        private List<CCButton> drives;

        public DriveListCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Up One Level")
        {
            drives = new List<CCButton>();
            foreach (string drive in Directory.GetLogicalDrives())
            {
                DirectoryInfo driveDirInfo = new DirectoryInfo(drive);
                if (driveDirInfo.Exists)
                {
                    drives.Add(new FolderCCButton(ccToolbar, driveDirInfo.Name, driveDirInfo.FullName));
                }
            }
            this.Click += new EventHandler(DriveCCBtn_Click);
        }

        /// <summary>
        /// On Click: Display the accessible drives on this computer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DriveCCBtn_Click(object sender, EventArgs e)
        {
            ccToolbar.AddNewToolbarButtons(drives, "Drives");
        }
    }//end class DriveListCCButton

    /// <summary>
    /// Button representing an image file.
    /// </summary>
    public class ImageFileCCButton : CCButton
    {
        public ImageFileCCButton(CCToolbar ccToolbar, string text, string iconPath)
            : base(ccToolbar, text, iconPath)
        {
            this.Click += new EventHandler(ImageFileCCBtn_Click);
        }

        /// <summary>
        /// On Click: Open the image associated with this button in the main form.
        /// </summary>
        private void ImageFileCCBtn_Click(object sender, EventArgs e)
        {
            //TODO: refactor, compare with resetState()
            this.ccMainForm.MainImage = new Image(this.enabledIconPath);
            this.ccMainForm.MainImage.zoom = 1f;
            this.ccMainForm.setAppStatus();
            this.ccMainForm.centerImage();
            this.ccMainForm.initSelection();
            this.ccMainForm.IsSaved = true;

            //no changes to undo yet
            this.ccToolbar.UndoButton.Disable();
            this.ccToolbar.RedoButton.Disable();

            //update the main form's current directory to the directory of the file that was just clicked on
            //ie: next time the open button is pressed, the directory will be the directory of this image
            FileInfo file = new FileInfo(this.enabledIconPath);
            ccMainForm.DefaultFolder = file.Directory + "\\";
        }
    }//end class ImageFileCCButton

    /// <summary>
    /// Button representing a folder.
    /// </summary>
    public class FolderCCButton : CCButton
    {
        protected string directory;
        protected List<CCButton> directoryBtnList;

        /// <summary>
        /// Defines a button representing a folder.
        /// </summary>
        /// <param name="ccToolbar"></param>
        public FolderCCButton(CCToolbar ccToolbar, string text, string directory)
            : base(ccToolbar, text, directory)
        {
            directoryBtnList = new List<CCButton>();
            this.directory = directory;
            this.Click += new EventHandler(FolderCCButton_Click);
        }

        /// <summary>
        /// On Click: Generate buttons for all the folders and image files in the directory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void FolderCCButton_Click(object sender, EventArgs e)
        {
            CreateButtonsFromDirectory(this.directory);
            DirectoryInfo dir = new DirectoryInfo(this.directory);
            ccToolbar.AddNewToolbarButtons(directoryBtnList, "Folder: " + dir.Name);
        }

        /// <summary>
        /// Create toolbar buttons representing the folders and image files in the directory.
        /// </summary>
        protected void CreateButtonsFromDirectory(string dir)
        {
            //clear out previous buttons to prevent duplicates
            this.directoryBtnList.Clear();

            //create a button to go to the directory's parent (up one level)
            DirectoryInfo currentDir = new DirectoryInfo(dir);
            DirectoryInfo parentDir = currentDir.Parent;

            //add Up One Level button to get to parent directory
            if (currentDir.FullName != currentDir.Root.FullName) 
            {
                try
                {
                    FolderCCButton parentBtn = new FolderCCButton(this.ccToolbar, "Up One Level", parentDir.FullName);
                    directoryBtnList.Add(parentBtn);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid parent directory.");
                    Console.WriteLine(e.ToString());
                }
            }

            //if at drive root, add a Drive List button to allow changing of drives
            else if (currentDir.FullName == currentDir.Root.FullName)
            {
                directoryBtnList.Add(new DriveListCCButton(ccToolbar));
            }

            //create buttons for all the subdirectories in the directory
            foreach (DirectoryInfo subdir in currentDir.GetDirectories())
            {
                try
                {
                    //don't check hidden directories, 
                    //ref: http://stackoverflow.com/questions/1288975/how-to-test-if-directory-is-hidden-in-c
                    if ((subdir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        FolderCCButton folderBtn = new FolderCCButton(this.ccToolbar, "Folder: " + subdir.Name, subdir.FullName);
                        directoryBtnList.Add(folderBtn);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(subdir.Name + " is an invalid subdirectory.");
                    Console.WriteLine(e.ToString());
                }
            }//end foreach

            //create buttons for all the image files in the directory
            foreach (FileInfo file in currentDir.GetFiles())
            {
                string extension = file.Extension.ToLower();
                if (extension == ".jpg"
                     || extension == ".jpeg"
                     || extension == ".png"
                     || extension == ".bmp")
                {
                    try
                    {
                        ImageFileCCButton imageButton = new ImageFileCCButton(this.ccToolbar, file.Name, file.FullName);
                        directoryBtnList.Add(imageButton);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(file.Name + " is an invalid image.");
                        Console.WriteLine(e.ToString());
                    }
                }//end if
            }//end foreach
        }//end CreateButtonsFromDirectory
    }//end class FolderCCButton

    /// <summary>
    /// The Open button on the toolbar: Opens to the MainForm's current default directory.
    /// </summary>
    public class OpenCCButton : FolderCCButton
    {
        public OpenCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Open", new DirectoryInfo(ccToolbar.CCMainForm.DefaultFolder).FullName)
        {
            //ref icon: http://www.openclipart.org/detail/13738
            this.SetIcon("Icons/Open.png");
        }

        /// <summary>
        /// On Click: Update the directory associated with this button to the current
        /// default directory of the main form, then create buttons for the directory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void FolderCCButton_Click(object sender, EventArgs e)
        {
            //first, update this button's directory to what the main form's current default directory is
            //the default directory is the directory of the last image that was opened
            this.directory = new DirectoryInfo(ccMainForm.DefaultFolder).FullName;
            
            //the FolderCCButton base class takes care of the button generation
            base.FolderCCButton_Click(sender, e);
        }
    }//end class OpenCCButton

}//end namespace

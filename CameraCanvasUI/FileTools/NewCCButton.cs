using System;
using System.Collections.Generic;
using System.Text;
using System.IO; //for checking if a file exists
using System.Windows.Forms; //for Clipboard

namespace CameraCanvas.FileTools
{
    /// <summary>
    /// Defines the New button in the File Tools on the toolbar.
    /// </summary>
    public class NewCCButton : CCButton
    {

        /// <summary>
        /// Defines a button specifying a new image of a given size or from the clipboard.
        /// </summary>
        public class NewSizeCCButton : CCButton
        {
            private int newImageWidth = 300;
            private int newImageHeight = 300;
            private bool isNewFromClipboard = false;


            /// <summary>
            /// Construct a new image from a given width and height.
            /// </summary>
            /// <param name="ccToolbar"></param>
            /// <param name="newImageWidth"></param>
            /// <param name="newImageHeight"></param>
            public NewSizeCCButton(CCToolbar ccToolbar, int newImageWidth, int newImageHeight) 
                : base(ccToolbar)
            {
                this.newImageWidth = newImageWidth;
                this.newImageHeight = newImageHeight;

                SetTextAndTooltip(newImageWidth + " by " + newImageHeight);

                //set background image depending on width and height
                //string iconPath = "Icons/new_" + newImageWidth + newImageHeight + ".png";
                //if (File.Exists(iconPath))
                //{
                //    this.BackgroundImage = System.Drawing.Image.FromFile(iconPath);

                //    //TODO refactor
                //    //dont show text ontop of the picture
                //    this.Text = "";
                //}

                

                
            }


            /// <summary>
            /// Construct a new image from the image on the system clipboard.
            /// </summary>
            /// <param name="ccToolbar"></param>
            /// <param name="isNewFromClipboard"></param>
            public NewSizeCCButton(CCToolbar ccToolbar, bool isNewFromClipboard)
                : base(ccToolbar)
            {

                this.isNewFromClipboard = isNewFromClipboard;
   
                //string iconPath = "Icons/new_clip.png";
                //if (File.Exists(iconPath))
                //{
                //    this.BackgroundImage = System.Drawing.Image.FromFile(iconPath);
                //}

                SetTextAndTooltip("From Clipboard");

            }


            /// <summary>
            /// On Click: Create a new image either from the clipboard or of a given width and height.
            /// </summary>
            public override void ClickAction(object sender, EventArgs e)
            {
                //create a new image from the image in the clipboard
                if (isNewFromClipboard == true && Clipboard.ContainsImage() == true)
                {
                    ccMainForm.MainImage = new Image();
                    
                    ccMainForm.MainImage.pasteImage(0, 0);


                    ccMainForm.MainImage.Invalidate();
                    ccMainForm.getImagePanel().Invalidate();

                    ccMainForm.centerImage();
                    ccMainForm.initSelection();
                }
                //create a new image from width and height
                else
                {
                    this.ccMainForm.createNewImage(newImageWidth, newImageHeight);

                    this.ccMainForm.MainImage.Invalidate();
                    this.ccMainForm.getImagePanel().Invalidate();

                    //new image has no changes to undo yet
                    this.ccToolbar.UndoButton.Disable();
                    this.ccToolbar.RedoButton.Disable();

                    this.ccMainForm.centerImage();
                    this.ccMainForm.initSelection();
                }

                //ccToolbar.RemoveAllToolbarButtons();
                //ccToolbar.RemoveCurrentToolbarButtons();
            }
        }


        private List<CCButton> newSizeButtonList;


        /// <summary>
        /// Construct the New Size button on the toolbar.
        /// </summary>
        /// <param name="ccToolbar"></param>
        /// ref icon: http://www.openclipart.org/detail/32137
        public NewCCButton(CCToolbar ccToolbar) : base(ccToolbar, "New", "Icons/new.png")
        {
            //create the new size buttons and add them to the toolbar
            newSizeButtonList = new List<CCButton>();

            NewSizeCCButton newSizeFromClipboard = new NewSizeCCButton(this.ccToolbar, true);
            newSizeButtonList.Add(newSizeFromClipboard);
            
            NewSizeCCButton newSize100x100 = new NewSizeCCButton(this.ccToolbar, 100,100);
            newSizeButtonList.Add(newSize100x100);
            
            NewSizeCCButton newSize300x300 = new NewSizeCCButton(this.ccToolbar, 300, 300);
            newSizeButtonList.Add(newSize300x300);
            
            NewSizeCCButton newSize600x600 = new NewSizeCCButton(this.ccToolbar,600, 600);
            newSizeButtonList.Add(newSize600x600);
            
            NewSizeCCButton newSize800x800 = new NewSizeCCButton(this.ccToolbar, 800, 800);
            newSizeButtonList.Add(newSize800x800);
        }


        /// <summary>
        /// On Click: Display the new size buttons on the toolbar.
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            ccToolbar.AddNewToolbarButtons(newSizeButtonList, "New Sizes");
           
            ccMainForm.State = CCMainForm.CCFormState.Etc;
        }

    }
}

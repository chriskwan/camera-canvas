using System;
using System.Collections.Generic;
using System.Text;

namespace CameraCanvas.FileTools
{
    //TODO BUG with saving external exception with GDI?

    /// <summary>
    /// Defines the Save button in the File tools on the toolbar.
    /// </summary>
    class SaveCCButton : CCButton
    {
        /// <summary>
        /// Defines a button representing a save option.
        /// </summary>
        public class SaveOptionsCCButton : CCButton
        {
            bool isSaveAs = false;

            /// <summary>
            /// Construct a new save option button given the option text
            /// and flag whether it is "Save As"
            /// </summary>
            /// <param name="ccToolbar"></param>
            /// <param name="text"></param>
            /// <param name="isSaveAs"></param>
            public SaveOptionsCCButton(CCToolbar ccToolbar, string text, bool isSaveAs)
                : base(ccToolbar, text)
            {
                this.isSaveAs = isSaveAs;
            }


            /// <summary>
            /// Save changes to the current image.
            /// </summary>
            private void save()
            {
                if (ccMainForm.MainImage.imagePath != null)
                {
                    if (ccMainForm.MainImage.SaveImage())
                        ccMainForm.ShowMessage("Image saved as " + ccMainForm.MainImage.imageName);
                    ccMainForm.IsSaved = true;
                }
            }


            /// <summary>
            /// Save changes to a new copy of the current image.
            /// </summary>
            public void saveas()
            {
                int i = 0;
                int lastIndex = ccMainForm.MainImage.imageName.LastIndexOf(".");
                string nameBody = ccMainForm.MainImage.imageName.Substring(0, lastIndex);
                string nameExt = ccMainForm.MainImage.imageName.Substring(lastIndex + 1, ccMainForm.MainImage.imageName.Length - lastIndex - 1);
                while (System.IO.File.Exists(ccMainForm.MainImage.imagePath + nameBody + "_copy" + i.ToString() + "." + nameExt)) i++;
                ccMainForm.MainImage.name = nameBody + "_copy" + i.ToString() + "." + nameExt;
                ccMainForm.setAppStatus();
                if (ccMainForm.MainImage.imagePath != null)
                {
                    if (ccMainForm.MainImage.SaveImage())
                        ccMainForm.ShowMessage("Image saved as " + ccMainForm.MainImage.imageName);
                    //Console.WriteLine("save to {0}{1}", this.mainImage.imagePath, this.mainImage.imageName);
                    ccMainForm.IsSaved = true;
                }
            }


            /// <summary>
            /// On Click: execute either Save or Save As.
            /// </summary>
            public override void ClickAction(object sender, EventArgs e)
            {
                if (isSaveAs == true)
                {
                    saveas();
                }
                else
                {
                    save();   
                }

                ccToolbar.RemoveCurrentToolbarButtons();
            }

        }


        List<CCButton> saveOptionButtons;

        /// <summary>
        /// Construct a new Save button in the File tools on the toolbar.
        /// </summary>
        /// <param name="ccToolbar"></param>
        /// ref icon: http://www.openclipart.org/detail/2206
        public SaveCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Save", "Icons/save.png")
        {
            saveOptionButtons = new List<CCButton>();

            //"Save"
            SaveOptionsCCButton saveBtn = new SaveOptionsCCButton(this.ccToolbar, "Save", false);
            saveOptionButtons.Add(saveBtn);

            //"Save As"
            //TODO Bug
            //1. New image from clipboard
            //2. SaveBtn -> Save
            //3. SaveBtn -> Save As
            //4. Exception!
            SaveOptionsCCButton saveAsBtn = new SaveOptionsCCButton(this.ccToolbar, "Save A Copy", true);
            saveOptionButtons.Add(saveAsBtn);
        }





        /// <summary>
        /// On Click: Change toolbar to Save options.
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            ccToolbar.AddNewToolbarButtons(saveOptionButtons, "Save");
            this.ccMainForm.State = CCMainForm.CCFormState.Etc;
        }

    }
}

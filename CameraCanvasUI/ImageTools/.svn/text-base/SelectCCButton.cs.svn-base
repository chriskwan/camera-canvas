using System;
using System.Collections.Generic;
using System.Text;
using CameraCanvas.Toolbar;

namespace CameraCanvas.ImageTools
{
    /// <summary>
    /// Defines the Select Button.
    /// </summary>
    public class SelectCCButton : CCButton
    {
        //TODO switch other classes to use enums instead of booleans
        /// <summary>
        /// Defines the different options to do after choosing Select.
        /// </summary>
        public enum SelectOptions
        {
            Copy,
            Crop
        }

        /// <summary>
        /// Defines a button representing an option after choosing Select.
        /// </summary>
        public class SelectOptionsCCButton : CCButton
        {
            private SelectOptions option;

            /// <summary>
            /// Create a Select Option button (ie: Copy, Crop)
            /// </summary>
            /// <param name="ccToolbar"></param>
            /// <param name="text"></param>
            /// <param name="iconPath"></param>
            /// <param name="option"></param>
            public SelectOptionsCCButton(CCToolbar ccToolbar, String text, String iconPath, SelectOptions option)
                : base(ccToolbar, text, iconPath)
            {
                this.option = option;
                //this.MouseEnter += new EventHandler(SelectOptionsCCButton_MouseEnter);
            }

            //TODO Refactor Hacky
            /// <summary>
            /// On Mouse Enter: Show the selection box and arrows again to give user feedback
            /// about what will be copied/cropped
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void SelectOptionsCCButton_MouseEnter(object sender, EventArgs e)
            {
                ccMainForm.MainImage.selecting = true;
                ccMainForm.State = CCMainForm.CCFormState.Selecting;
                ccMainForm.MainImage.Invalidate(); //force main image to be redrawn with arrows
                ccMainForm.getImagePanel().Invalidate(); //force image panel to be redrawn with box
            }

            /// <summary>
            /// On Click: Using the selected area, either Copy or Crop
            /// </summary>
            public override void ClickAction(object sender, EventArgs e)
            {
                switch (option)
                {
                    case SelectOptions.Copy:
                        this.ccMainForm.ShowMessage("Selection Copied");
                        this.ccMainForm.getWavPlayer().Play(@"wav\click.wav");
                        this.ccMainForm.MainImage.copyImage();
                        this.ccMainForm.getImagePanel().Invalidate();

                        //TODO delete?
                        this.ccMainForm.resetState();
                        break;
                    case SelectOptions.Crop:
                        if (this.ccMainForm.MainImage.selectedArea.Width > 0)
                        {
                            this.ccMainForm.MainImage.takeSnap("cropped");
                            this.ccMainForm.MainImage.cropImage();
                            this.ccMainForm.ShowMessage("Image Cropped");
                            this.ccMainForm.getWavPlayer().Play(@"wav\click.wav");
                            this.ccMainForm.MainImage.resetSelection();
                            this.ccMainForm.centerImage();
                            this.ccMainForm.MainImage.Invalidate();
                            this.ccMainForm.getImagePanel().Invalidate();

                            //TODO delete?
                            this.ccMainForm.resetState();
                        }
                        break;
                    default:
                        //do nothing
                        break;
                }
            }

        }//end class

        private List<CCButton> selectOptionsBtns; 

        public SelectCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Select", "Icons/Select.png")
        {
            //initialize select options buttons
            selectOptionsBtns = new List<CCButton>();

            selectOptionsBtns.Add(ccToolbar.UndoButton);
            selectOptionsBtns.Add(ccToolbar.RedoButton);

            //ref icon: http://www.openclipart.org/detail/32335
            SelectOptionsCCButton copyBtn
                = new SelectOptionsCCButton(this.ccToolbar, "Copy", "Icons/copy.png", SelectOptions.Copy);
            selectOptionsBtns.Add(copyBtn);

            PasteCCButton pasteBtn = new PasteCCButton(this.ccToolbar);
            selectOptionsBtns.Add(pasteBtn);

            SelectOptionsCCButton cropBtn
                = new SelectOptionsCCButton(this.ccToolbar, "Crop", "Icons/crop.png", SelectOptions.Crop);
            selectOptionsBtns.Add(cropBtn);
        }

        /// <summary>
        /// On Click: Change the toolbar to select options and show selection box.
        /// </summary>
        public override void ClickAction(object sender, EventArgs e)
        {
            //TODO BUG
            //Selection box does not go away if you pick another icon

            //TODO refactor
            this.ccMainForm.resetState();

            //TODO should there be a close button?
            ccToolbar.AddNewToolbarButtons(selectOptionsBtns, "Select Options");
            ccMainForm.beginSelecting();    
        }
    }
}

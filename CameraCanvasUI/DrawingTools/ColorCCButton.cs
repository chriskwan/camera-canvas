using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CameraCanvas.DrawingTools
{
    class ColorCCButton : CCButton
    {
        class ShadeCCButton : CCButton
        {
            protected Color color;

            public ShadeCCButton(CCToolbar ccToolbar, Color color)
                : base(ccToolbar)
            {
                this.color = color;
                this.BackColor = color;

                //since the backcolor of these buttons represents their color values
                //do not change the backcolor on mouse events
                this.FlatAppearance.MouseOverBackColor = this.BackColor;
                this.FlatAppearance.MouseDownBackColor = this.BackColor;
            }

            public override void ClickAction(object sender, EventArgs e)
            {
                ccMainForm.SelectedColor = color;

                //ccMainForm.resetState();
                ccMainForm.State = CCMainForm.CCFormState.DrawingWait;
                ccMainForm.MainImage.Invalidate();
                ccMainForm.getImagePanel().Invalidate();

                ccMainForm.ShowMessage("Color selected");
                ccToolbar.RemoveCurrentToolbarButtons();//back to primary color level
                ccToolbar.RemoveCurrentToolbarButtons();//back to drawing level

                //TODO refactor
                ccMainForm.UpdateDrawingDisplay();
            }
        }


        class PrimaryColorCCButton : CCButton//: ShadeCCButton
        {
            protected Color color;
            private Color[] shadeColors;
            private int shadeIncrement = 38;
            private List<CCButton> shadeColorBtns;
            private int numColors = 9; //TODO refactor

            public PrimaryColorCCButton(CCToolbar ccToolbar, Color color)
                : base(ccToolbar)
            {
                this.color = color;
                this.BackColor = color;

                //since the backcolor of these buttons represents their color values
                //do not change the backcolor on mouse events
                this.FlatAppearance.MouseOverBackColor = this.BackColor;
                this.FlatAppearance.MouseDownBackColor = this.BackColor;


                shadeColors = new Color[numColors];
                shadeColorBtns = new List<CCButton>();
            }

            public override void ClickAction(object sender, EventArgs e)
            {
                //set the main form to this primary color
                //in case user clicks "Back" before choosing a shade
                ccMainForm.SelectedColor = color;



                //generate more shades
                setShadeColors(this.color);

                if (shadeColorBtns.Count <= 0)
                {
                    foreach (Color shadeColor in shadeColors)
                    {
                        ShadeCCButton shadeBtn = new ShadeCCButton(this.ccToolbar, shadeColor);
                        shadeColorBtns.Add(shadeBtn);
                    }
                }

                ccToolbar.AddNewToolbarButtons(shadeColorBtns, "Shades of " + color.Name);
                ccMainForm.State = CCMainForm.CCFormState.DrawingWait;

                //TODO refactor
                ccMainForm.UpdateDrawingDisplay();

                //perform the same action as a shade
                //base.ClickAction(sender, e);
            }

            private void setShadeColors(Color baseColor)
            {
                int baseR = baseColor.R;
                int baseG = baseColor.G;
                int baseB = baseColor.B;

                int shadeR = baseColor.R;
                int shadeG = baseColor.G;
                int shadeB = baseColor.B;

                int midpoint = shadeColors.Length / 2;

                for (int i = midpoint - 1; i >= 0; i--)
                {
                    shadeR = shadeR + shadeIncrement;
                    if (shadeR > 255) { shadeR = 255; }

                    shadeG = shadeG + shadeIncrement;
                    if (shadeG > 255) { shadeG = 255; }

                    shadeB = shadeB + shadeIncrement;
                    if (shadeB > 255) { shadeB = 255; }

                    shadeColors[i] = Color.FromArgb(shadeR, shadeG, shadeB);
                }

                shadeColors[midpoint] = baseColor;


                shadeR = baseColor.R;
                shadeG = baseColor.G;
                shadeB = baseColor.B;

                for (int i = midpoint + 1; i < shadeColors.Length; i++)
                {
                    shadeR = shadeR - shadeIncrement;
                    if (shadeR < 0) { shadeR = 0; }

                    shadeG = shadeG - shadeIncrement;
                    if (shadeG < 0) { shadeG = 0; }

                    shadeB = shadeB - shadeIncrement;
                    if (shadeB < 0) { shadeB = 0; }

                    shadeColors[i] = Color.FromArgb(shadeR, shadeG, shadeB);
                }
            }

        }


        List<CCButton> buttonList; //list of buttons in subtoolbar
        Color[] primaryColors;

        public ColorCCButton(CCToolbar toolbar)
            : base(toolbar, "Color", "Icons/Color.png")
        {
            buttonList = new List<CCButton>();

            primaryColors = new Color[] 
            {
                Color.Black, Color.White, Color.Brown, Color.Red, Color.Orange, 
                Color.Yellow, Color.Green, Color.Blue, Color.Purple
            };

            foreach (Color color in primaryColors)
            {
                PrimaryColorCCButton shadeBtn = new PrimaryColorCCButton(ccToolbar, color);
                buttonList.Add(shadeBtn);
            }
        }


        //TODO fix interfaces
        public override void ClickAction(object sender, EventArgs e)
        {
            ccToolbar.AddNewToolbarButtons(buttonList, "Primary Colors");
        }

    }//end class
}//end namespace

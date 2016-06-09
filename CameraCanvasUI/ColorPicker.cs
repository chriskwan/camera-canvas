using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas
{
    public partial class ColorPicker : Form
    {
        public ColorPicker()
        {
            InitializeComponent();
            initialize();
        }

        Color chosenColor = Color.Black;

        public Color ChosenColor
        {
            get
            {
                return C1.BackColor;
            }
        }


        Color[] primaryColors = new Color[8];
        Color[] shadeColors = new Color[8];
        int shadeIncrement = 38;

        //C Button variables
        int activeColor = 1;
        Color selectedColor = Color.Black;
        Color unselectedColor = Color.LightGray;



        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            //this.Hide();
        }

        protected void initialize()
        {
            setPrimaryButtons();
            OKButton.BackColor = Color.SeaShell;

            //Set Color 1 and 2 to Black and White
            activeColor = 1;
            SetSelectedColor(Color.Black);

            activeColor = 2;
            SetSelectedColor(Color.White);

            //Start with Color1 selected
            activeColor = 1;
            Color1Panel.BackColor = Color.Black; //highlight C1
        }

        protected void setPrimaryButtons()
        {
            setPrimaryColors();
            Control temp = PrimaryColorBox;
            for (int i = 0; i < primaryColors.Length; i++)
            {
                if (PrimaryColorBox.HasChildren)
                {
                    temp = PrimaryColorBox.GetNextControl(temp, true);
                    if (temp != null)
                    {
                        temp.BackColor = primaryColors[i];
                        temp.Text = "";
                    }
                }

            }
        }

        private void setPrimaryColors()
        {
            primaryColors[0] = Color.Black;
            primaryColors[1] = Color.White;
            primaryColors[2] = Color.Red;
            primaryColors[3] = Color.Orange;
            primaryColors[4] = Color.Yellow;
            primaryColors[5] = Color.Green;
            primaryColors[6] = Color.Blue;
            primaryColors[7] = Color.Purple;
        }

        protected void setShadeColors(Color baseColor)
        {
            int baseR = baseColor.R;
            int baseG = baseColor.G;
            int baseB = baseColor.B;

            int shadeR = baseColor.R;
            int shadeG = baseColor.G;
            int shadeB = baseColor.B;

            int midpoint = shadeColors.Length / 2;

            for (int i = midpoint-1; i >= 0; i--)
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

            for (int i = midpoint+1; i<shadeColors.Length; i++)
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

        protected void setShadeButtons(Color baseColor)
        {
            setShadeColors(baseColor);
            //BlackButton.BackColor = primaryColors[0];
            Control temp = ShadeBox;
            for (int i = 0; i < shadeColors.Length; i++)
            {
                if (ShadeBox.HasChildren)
                {
                    temp = ShadeBox.GetNextControl(temp, true);
                    if (temp != null)
                    {
                        temp.BackColor = shadeColors[i];
                        //temp.Text = temp.BackColor.Name;
                        temp.Text = "";
                    }
                }

            }
        }


        private void C1_Click(object sender, EventArgs e)
        {
            //OKButton.BackColor = Color.Pink;
            activeColor = 1;
            Color1Panel.BackColor = selectedColor;
            //Color2Panel.BackColor = unselectedColor;
            //MixPanel.BackColor = unselectedColor;
            chosenColor = selectedColor;
        }

        /*
        private void C2_Click(object sender, EventArgs e)
        {
            //OKButton.BackColor = Color.Azure;
            activeColor = 2;
            Color1Panel.BackColor = unselectedColor;
            //Color2Panel.BackColor = selectedColor;
            //MixPanel.BackColor = unselectedColor;
        }
        */

        /*
        private void CMix_Click(object sender, EventArgs e)
        {
            //OKButton.BackColor = Color.SeaShell;
            activeColor = 3;
            Color1Panel.BackColor = unselectedColor;
            //Color2Panel.BackColor = unselectedColor;
            //MixPanel.BackColor = selectedColor;
        }
         */

        /*
        protected void CMixRefresh()
        {
            int mixR = (C1.BackColor.R + C2.BackColor.R) / 2;
            int mixG = (C1.BackColor.G + C2.BackColor.G) / 2;
            int mixB = (C1.BackColor.B + C2.BackColor.B) / 2;

            CMix.BackColor = Color.FromArgb(mixR, mixG, mixB);
            CMix.Text = "";
            CMixLabel.Text = ("Mix of 1 and 2:\n R: "
                    + mixR + "\n G: " + mixG + "\n B: " + mixB);
        }
        */
        protected void SetSelectedColor(Color newColor)
        {
            if (activeColor == 1)
            {
                C1.BackColor = newColor;
                C1.Text = "";
                C1Label.Text = ("RGB Values: \n R: " 
                    + newColor.R + "\n G: " + newColor.G + "\n B: " + newColor.B);
            }
            else if (activeColor == 2)
            {
                //C2.BackColor = newColor;
                //C2.Text = "";
                //C2Label.Text = ("Color 2: \n R: "
                   // + newColor.R + "\n G: " + newColor.G + "\n B: " + newColor.B);

            }

            //CMixRefresh();

        }

        protected void MButtonAction(Color myColor)
        {
            setShadeButtons(myColor);
            SetSelectedColor(myColor);
        }

        private void MC0_Click(object sender, EventArgs e)
        {
            MButtonAction(MC0.BackColor);
        }

        private void MC1_Click(object sender, EventArgs e)
        {
            MButtonAction(MC1.BackColor);
        }

        private void MC2_Click(object sender, EventArgs e)
        {
            MButtonAction(MC2.BackColor);
        }

        private void MC3_Click(object sender, EventArgs e)
        {
            MButtonAction(MC3.BackColor);
        }

        private void MC4_Click(object sender, EventArgs e)
        {
            MButtonAction(MC4.BackColor);
        }

        private void MC5_Click(object sender, EventArgs e)
        {
            MButtonAction(MC5.BackColor);
        }

        private void MC6_Click(object sender, EventArgs e)
        {
            MButtonAction(MC6.BackColor);
        }

        private void MC7_Click(object sender, EventArgs e)
        {
            MButtonAction(MC7.BackColor);
        }


        protected void SButtonAction(Color myColor)
        {
            SetSelectedColor(myColor);
        }

        private void S0_Click(object sender, EventArgs e)
        {
            SButtonAction(S0.BackColor);
        }

        private void S1_Click(object sender, EventArgs e)
        {
            SButtonAction(S1.BackColor);
        }

        private void S2_Click(object sender, EventArgs e)
        {
            SButtonAction(S2.BackColor);
        }

        private void S3_Click(object sender, EventArgs e)
        {
            SButtonAction(S3.BackColor);
        }

        private void S4_Click(object sender, EventArgs e)
        {
            SButtonAction(S4.BackColor);
        }

        private void S5_Click(object sender, EventArgs e)
        {
            SButtonAction(S5.BackColor);
        }

        private void S6_Click(object sender, EventArgs e)
        {
            SButtonAction(S6.BackColor);
        }

        private void S7_Click(object sender, EventArgs e)
        {
            SButtonAction(S7.BackColor);
        }


    }
}
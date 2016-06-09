using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CameraCanvas.Toolbar.Config
{
    class ButtonSizeChoiceForm : ChoiceForm
    {
        //Panel topPanel;
        TableLayoutPanel topPanel;
        TextBox textBox;
        Button updateButton;

        public ButtonSizeChoiceForm(CCToolbar ccToolbar)
            : base(ccToolbar, 2)
        {
            //formTableLayout.ColumnCount = 3;

            this.topPanel = new TableLayoutPanel();//new Panel();
            topPanel.RowCount = 1;
            topPanel.ColumnCount = 3;
            this.topPanel.AutoSize = true;
            this.topPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.formTableLayout.Controls.Add(this.topPanel,0,0);
            this.topPanel.Controls.Add(this.label,0,0);

            textBox = new TextBox();
            int size = Math.Min(ccToolbar.ButtonWidth, ccToolbar.ButtonHeight);
            textBox.Text = size.ToString();
            topPanel.Controls.Add(textBox,1,0);
            textBox.Left = label.Width;

            updateButton = new Button();
            updateButton.Text = "Update";
            updateButton.BackColor = System.Drawing.Color.White;
            updateButton.Click += new EventHandler(updateButton_Click);
            topPanel.Controls.Add(updateButton, 2, 0);

            this.label.Text = "Button Size: " + ccToolbar.ButtonWidth + " by " + ccToolbar.ButtonHeight;
            this.leftBtn.Text = "Smaller";
            this.rightBtn.Text = "Larger";

            leftBtn.actionEvent += new CCButton.actionEventHandler(leftBtn_actionEvent);
            rightBtn.actionEvent += new CCButton.actionEventHandler(rightBtn_actionEvent);
        }

        void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                int newSize = Int32.Parse(textBox.Text);
                ccToolbar.ChangeButtonSize(newSize, newSize);

                this.label.Text = "Button Size: " + ccToolbar.ButtonWidth + " by " + ccToolbar.ButtonHeight;

                resizeFormButtons();
            }
            catch
            {
                int size = Math.Min(ccToolbar.ButtonWidth, ccToolbar.ButtonHeight);
                textBox.Text = size.ToString();
            }
        }

        void closeBtn_actionEvent()
        {
            this.Close();
        }

        void rightBtn_actionEvent()
        {
            int newWidth = ccToolbar.ButtonWidth + 10;
            int newHeight = ccToolbar.ButtonHeight + 10;

            ccToolbar.ChangeButtonSize(newWidth, newHeight);

            this.label.Text = "Button Size: " + ccToolbar.ButtonWidth + " by " + ccToolbar.ButtonHeight;

            resizeFormButtons();
        }

        void leftBtn_actionEvent()
        {
            int newWidth = ccToolbar.ButtonWidth - 10;
            int newHeight = ccToolbar.ButtonHeight - 10;

            if(newWidth > 0 && newHeight > 0)
            {
                ccToolbar.ChangeButtonSize(newWidth, newHeight);
                this.label.Text = "Button Size: " + ccToolbar.ButtonWidth + " by " + ccToolbar.ButtonHeight;
            }

            resizeFormButtons();
            
        }

        //resize the buttons in this form too
        void resizeFormButtons()
        {
            this.leftBtn.Width = ccToolbar.ButtonWidth;
            this.leftBtn.Height = ccToolbar.ButtonHeight;

            this.rightBtn.Width = ccToolbar.ButtonWidth;
            this.rightBtn.Height = ccToolbar.ButtonHeight;

            this.bottomBtn.Width = ccToolbar.ButtonWidth;
            this.bottomBtn.Height = ccToolbar.ButtonHeight;
        }



    }
}

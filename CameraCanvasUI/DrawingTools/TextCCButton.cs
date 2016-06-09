using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms; //for Keys

namespace CameraCanvas.DrawingTools
{
    public class BackspaceCCButton : CCButton
    {
        public BackspaceCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Backspace")
        {
            this.Click += new EventHandler(BackspaceCCButton_Click);
        }

        void BackspaceCCButton_Click(object sender, EventArgs e)
        {
            //TODO refactor!
            ccMainForm.ShowAndFocusTextEntry();

            //delete the last character
            string text = ccMainForm.textEntryControl.TextBox.Text;
            if (text.Length > 0)
            {
                text = text.Remove(text.Length - 1, 1);
            }
            ccMainForm.textEntryControl.TextBox.Text = text;

            //scroll the carot back to the new last character
            ccMainForm.textEntryControl.TextBox.SelectionStart = ccMainForm.textEntryControl.TextBox.TextLength;
            ccMainForm.textEntryControl.TextBox.ScrollToCaret();
        }
    }//end class BackspaceCCButton

    //TODO: Implement Shift functionality
    public class LetterCCButton : CCButton
    {
        private char letter;
       
        public LetterCCButton(CCToolbar ccToolbar, string text, char letter)
            : base(ccToolbar, text)
        {
            this.letter = letter;
            this.Click += new EventHandler(LetterCCButton_Click);
        }

        void LetterCCButton_Click(object sender, EventArgs e)
        {
            this.letter = char.ToLower(this.letter);

            //TODO refactor!
            ccMainForm.ShowAndFocusTextEntry();
            ccMainForm.textEntryControl.TextBox.AppendText(this.letter.ToString());
        }
    }//end class LetterCCButton

    public class LetterRangeCCButton : CCButton
    {
        private List<CCButton> letters;

        public LetterRangeCCButton(CCToolbar ccToolbar, string text)
            : base(ccToolbar, text)
        {
            letters = new List<CCButton>();
            
            letters.Add(new BackspaceCCButton(ccToolbar));
            letters.Add(new LetterCCButton(ccToolbar, "Space", ' '));

            //add all letters in the letter range
            foreach (char letter in text)
            {
                letters.Add(new LetterCCButton(ccToolbar, letter.ToString(), letter));
            }
            
            letters.Add(new LetterCCButton(ccToolbar, "Space", ' '));
            letters.Add(new BackspaceCCButton(ccToolbar));

            this.Click += new EventHandler(LetterRangeCCButton_Click);
        }

        void LetterRangeCCButton_Click(object sender, EventArgs e)
        {
            ccToolbar.AddNewToolbarButtons(letters, "Letters");
            ccMainForm.ShowAndFocusTextEntry();
        }
    }//end class LetterRangeCCButton

    public class TextCCButton : CCButton
    {
        private List<CCButton> letterRanges;

        public TextCCButton(CCToolbar ccToolbar)
            : base(ccToolbar, "Text", "Icons/Text.png")
        {
            //using Rick Hoyt spelling method
            letterRanges = new List<CCButton>();
            letterRanges.Add(new LetterRangeCCButton(ccToolbar, "ABCD"));
            letterRanges.Add(new LetterRangeCCButton(ccToolbar, "EFGH"));
            letterRanges.Add(new LetterRangeCCButton(ccToolbar, "IJKLMN"));
            letterRanges.Add(new LetterRangeCCButton(ccToolbar, "OPQRST"));
            letterRanges.Add(new LetterRangeCCButton(ccToolbar, "UVWXYZ"));
            letterRanges.Add(new LetterRangeCCButton(ccToolbar, "1234567890"));

            this.Click += new EventHandler(TextCCButton_Click);
        }

        void TextCCButton_Click(object sender, EventArgs e)
        {
            ccToolbar.AddNewToolbarButtons(letterRanges, "Letter Ranges");
            ccMainForm.ShowAndFocusTextEntry();
        }
    }//end class TextCCButton
}//end namespace

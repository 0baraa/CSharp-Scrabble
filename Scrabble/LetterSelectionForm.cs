using System;
using System.Drawing;
using System.Windows.Forms;

namespace Scrabble
{
    //Form that allows user to pick a tile for the blank tile
    class LetterSelectionForm : Form
    {
        public string selectedLetter;
        private string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N",
                                    "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        public LetterSelectionForm()
        {
            AutoSize = true;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;

            FlowLayoutPanel outerPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true
            };


            for (int p = 0; p < 5; ++p)
            {
                FlowLayoutPanel innerPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true
                };

                outerPanel.Controls.Add(innerPanel);
            }


            for(int i = 0; i < alphabet.Length; ++i) {


                Panel img = new Panel
                {
                    BackgroundImage = Image.FromFile(@"AppData\" + alphabet[i] + ".jpg"),
                    Size = new Size(Board.BUTTONWIDTH, Board.BUTTONWIDTH),
                    BackgroundImageLayout = ImageLayout.Stretch
                };
                img.Click += new EventHandler(newLetterSelected);

                Label val = new Label
                {
                    Text = alphabet[i],
                    Visible = false
                };
                img.Controls.Add(val);
                


                outerPanel.Controls[i / 6].Controls.Add(img);


            }

            Controls.Add(outerPanel);

        }

        private void newLetterSelected(object sender, EventArgs e)
        {
            Panel img = (Panel)sender;

            //close the parent
            Console.WriteLine("Selected letter :" + img.Controls[0].Text);
            selectedLetter = img.Controls[0].Text;
            img.FindForm().Close();
        }
    }
}

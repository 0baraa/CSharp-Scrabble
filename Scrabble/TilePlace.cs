using System;
using System.Drawing;
using System.Windows.Forms;

namespace Scrabble
{
    //A TilePlace is a button that holds a tile.
    //The Board is made up of a 15x15 grid of TilePlaces.
    //TilePlaces can have multipliers that give bonus points.
    //TilePlace inherits from the Button class
    public class TilePlace : Button
    {
        //Determines in which state clicking and dropping is.
        //If ClickState is 1, the player has yet to click on a TilePlace.
        //Once a tile is clicked, the value of ClickState is updated to 2, indicating that the player has chosen a tile.
        private static int ClickState = 1;
        //Stores TilePlace which has been clicked.
        private static TilePlace SelectedTilePlace;
        public Tile varTile;
        // XY location on board
        public int X { get; }
        public int Y { get; }
        public int LetterMultiplier { get; set; }
        public int WordMultiplier { get; set; }
        public Tile Tile { get; set; }
        //Decides if tiles on board can be moved
        public bool isLocked = false;

        //Constructor for TilePlaces with no multiplier
        public TilePlace(int x, int y)
        {
            Size = new Size(Board.BUTTONWIDTH, Board.BUTTONWIDTH);
            Font = new Font(Button.DefaultFont.FontFamily, 8);
            BackColor = Color.LightGray;
            this.X = x;
            this.Y = y;
            this.LetterMultiplier = 1;
            this.WordMultiplier = 1;
        }

        // Displays image of the Tile if a Tile has been placed on a TilePlace.
        // Otherwise, displays word/letter multiplier and sets the correct colour.
        public void UpdateButtonTextOrTile()
        {
            Text = "";
            Font = DefaultFont;

            //If TilePlace has a Tile
            if (this.Tile != null)
            {
                BackgroundImageLayout = ImageLayout.Stretch;

                //If the TilePlace's Tile's AltLabel property is not null (If TilePlace has a blank Tile)
                if (this.Tile.AltLabel != (char)0)
                {
                    //Sets image of blank tile
                    BackgroundImage = Image.FromFile(@"AppData\" + Tile.AltLabel.ToString() + ".jpg");
                    Text = this.Tile.Label.ToString();
                    Font = new Font(Button.DefaultFont.FontFamily, 34);
                }
                else
                    //Sets image of Tile based on the label
                    BackgroundImage = Image.FromFile(@"AppData\" + Tile.Label.ToString() + ".jpg");
                FlatStyle = FlatStyle.Popup;
            }

            else
            {
                //Setting text and colour of multiplier TilePlaces
                BackgroundImage = null;
                if (X == 7 && Y == 7)
                {
                    Text = "START (2W)";
                }
                else if (WordMultiplier == 2)
                {
                    Text = "2W";
                    BackColor = Color.FromArgb(60, Color.Red); 
                }
                else if (WordMultiplier == 3)
                {
                    Text = "3W";
                    BackColor = Color.FromArgb(50, Color.Orange); 
                }
                else if (LetterMultiplier == 2)
                {
                    Text = "2L";
                    BackColor = Color.FromArgb(20, Color.Blue); 
                }
                else if (LetterMultiplier == 3)
                {
                    Text = "3L";
                    BackColor = Color.FromArgb(50, Color.Green); 
                }
            }
        }

        //Converts the contents of a TilePlace array to a string.
        public static string TilePlaceArrayToString(TilePlace[] newWord)
        {
            string returnString = "";
            foreach (TilePlace tp in newWord)
            {
                returnString += tp.Tile.Label;
            }
            return returnString;
        }

        //Runs when a TilePlace is clicked. Used in gameplay for moving tiles.
        public void TilePlace_Click(object sender, EventArgs e)
        {
            //If player has yet to click a TilePlace
            if (TilePlace.ClickState == 1)
            {
                //If there is a Tile in the TilePalce and it is not locked
                if (this.Tile != null && !isLocked)
                {
                    Console.WriteLine("tile selected from " + X + "," + Y + ": " + Tile);
                    TilePlace.ClickState = 2;
                    TilePlace.SelectedTilePlace = this;
                }
            }

            //If a TilePlace has been selected
            else if (TilePlace.ClickState == 2)
            {
                //If the location where the user wants to place the Tile does not already have a tile
                if (this.Tile == null)
                {
                    //moving the tile from the first selected tile place to the second selection drop TilePlace
                    this.Tile = TilePlace.SelectedTilePlace.Tile;
                    //removing the reference of time from the previous location
                    TilePlace.SelectedTilePlace.Tile = null;

                    //updating button visuals
                    TilePlace.SelectedTilePlace.UpdateButtonTextOrTile();
                    this.UpdateButtonTextOrTile();

                    //removing selection
                    TilePlace.SelectedTilePlace = null;
                    Console.WriteLine("tile dropped at " + X + "," + Y + ": " + Tile);

                    //reset state, ready to select next tile
                    TilePlace.ClickState = 1;
                }
                //If the user has already selected a Tile but wants to select a different Tile
                else if (!this.isLocked)
                {
                    Console.WriteLine("tile selected from " + X + "," + Y + ": " + Tile);
                    TilePlace.ClickState = 2;
                    TilePlace.SelectedTilePlace = this;
                }
            }
        }

        //Runs when user clicks on a TilePlace.
        public void TilePlace_RightClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Clicks);

            //If the TilePlace is locked or it has no Tile then clicking will do nothing.
            if (this.isLocked == true || this.Tile == null)
            {
                return;
            }
            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N",
                                    "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            ComboBox blankTileChoice = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(0, 0)
            };
            blankTileChoice.Items.AddRange(alphabet);
            blankTileChoice.Size = new Size(Board.BUTTONWIDTH, Board.BUTTONWIDTH);
            blankTileChoice.MaxDropDownItems = 10;
            blankTileChoice.Font = DefaultFont;
            blankTileChoice.SelectedIndexChanged += new EventHandler(BlankTileChoice_SelectedIndexChanged);

            //If the user right clicked
            if (e.Button == MouseButtons.Right)
            {
                //If the label of the Tile in the TilePlace is '.' (the label for a blank tile)
                //or the AltLabel is '.'
                if (this.Tile.Label == '.' || this.Tile.AltLabel == '.')
                {
                    //Create and display a form with images of each Tile for the user to choose.
                    LetterSelectionForm popupform = new LetterSelectionForm
                    {
                        Location = new Point(FindForm().Location.X + 100, FindForm().Location.Y + 100)
                    };
                    popupform.ShowDialog();

                    if (popupform.selectedLetter != null)
                    {   //handle change of letter
                        Tile.AltLabel = '.';
                        Tile.Label = Convert.ToChar(popupform.selectedLetter);
                        UpdateButtonTextOrTile();
                    }
                }
            }
        }
        //Once user has selected an image, set AltLabel to '.' and the Label to the chosen tile.
        private void BlankTileChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox blankTileChoice = (ComboBox)sender;
            Console.WriteLine(blankTileChoice.Text);
            blankTileChoice.Hide();
            Tile.AltLabel = '.';
            Tile.Label = Convert.ToChar(blankTileChoice.Text);
            UpdateButtonTextOrTile();
        }
    }
}

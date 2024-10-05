using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Scrabble
{
    //Form where user inputs desired name for saved game.
    public partial class SaveNameForm : Form
    {
        public SaveNameForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (saveNameTxtBox.Text.Length == 0)
            {
                infoLabel.Text = "Text box cannot be empty.";
                return;
            }

            char[] invalidFileChars = Path.GetInvalidFileNameChars();
            //Ensuring user cannot enter invalid file name characters
            for (int i = 0; i < saveNameTxtBox.Text.Length; i++)
            {
                if (invalidFileChars.Contains(saveNameTxtBox.Text[i]))
                {
                    infoLabel.Text = "Invalid file character used.";
                    return;
                }
            }

            string[] savedGames = System.IO.Directory.GetFiles(@"SavedGames\", "*.scrabble");
            //Checking if a game with the name provided already exists
            foreach (string existingGame in savedGames)
            {
                if (existingGame == "SavedGames\\" + saveNameTxtBox.Text + ".scrabble")
                {
                    infoLabel.Text = "A game called \"" + saveNameTxtBox.Text + "\" already exists.";
                    return;
                }
            }
            this.Hide();
            //Save the game with the file name given.
            GameForm.GameFormState gameFormState = new GameForm.GameFormState(GameSetupForm.Game);
            SaveLoadSystem.SaveGameState(gameFormState, saveNameTxtBox.Text); //GameSetupForm.Game
        }
    }
}

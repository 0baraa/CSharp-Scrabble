using System;
using System.Windows.Forms;

namespace Scrabble
{
    //Form that provides user with list of saved games to choose from
    public partial class LoadGameForm : Form
    {
        string selectedSaveGame;
        public LoadGameForm()
        {
            InitializeComponent();
            //Getting the names of the saved games
            string[] savedGames = System.IO.Directory.GetFiles(@"SavedGames\", "*.scrabble");
            for (int i = 0; i < savedGames.Length; i++)
            {
                //Removing part of the file name so that only the name of the file appears e.g. game.scrabble appears as game in combobox.
                savedGames[i] = savedGames[i].Substring(0, savedGames[i].Length - 9);
                savedGames[i] = savedGames[i].Remove(0, 11);

                checkedListBox1.Items.Add(savedGames[i]);
            }
        }

        
        private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //Ensuring only one item in the combobox can be checked.
            if (e.NewValue == CheckState.Checked && checkedListBox1.CheckedItems.Count > 0)
            {
                checkedListBox1.ItemCheck -= CheckedListBox1_ItemCheck;
                checkedListBox1.SetItemChecked(checkedListBox1.CheckedIndices[0], false);
                checkedListBox1.ItemCheck += CheckedListBox1_ItemCheck;
            }
        }

        private void BtReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BtLoadGame_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                //Getting user's chosen save
                if (checkedListBox1.GetItemChecked(i))
                {
                    selectedSaveGame = (string)checkedListBox1.Items[i];
                    break;
                }
            }

            if(selectedSaveGame == null)
            {
                MessageBox.Show("Select a saved game.");
                return;
            }

            //Creating a GameState object by loading the file with selectedSaveGame name.
            GameState loadedGameState = SaveLoadSystem.LoadGame(selectedSaveGame);
            //Creating and loading game based on save file.
            GameForm LoadedGame = new GameForm(loadedGameState.noOfPlayers, null, loadedGameState.secondsPerTurn, loadedGameState.isCustomDB, loadedGameState, true);
            LoadedGame.ShowDialog();
            this.Show();
        }

        //Allows user to search for a saved game
        //Runs when the value of the textbox.text is changed
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string myString = textBox1.Text;
            for (int i = 0; i <= checkedListBox1.Items.Count - 1; i++)
            {
                if (checkedListBox1.Items[i].ToString().Contains(myString))
                {
                    checkedListBox1.SetSelected(i, true);
                    break;
                }
            }
        }
    }
}

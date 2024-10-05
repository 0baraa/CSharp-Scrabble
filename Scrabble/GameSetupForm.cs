using System;
using System.Windows.Forms;

namespace Scrabble
{
    //This form provides the user with different options that will alter how the game begins e.g number of players, original/custom database.
    public partial class GameSetupForm : Form
    {
        public static GameForm Game;
        private int noOfPlayers = 2;
        public GameSetupForm()
        {
            InitializeComponent();
        }

        private void BtReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Runs when a radio button that determines the number of player is selected
        //Shows the appropriate number of name entry text boxes based on the radio button selected.
        private void NoOfPlayersCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2P.Checked)
            {
                p3TextBox.Hide();
                p4TextBox.Hide();
                noOfPlayers = 2;
            }
            else if (rb3P.Checked)
            {
                p4TextBox.Hide();
                p3TextBox.Show();
                noOfPlayers = 3;
            }
            else if (rb4P.Checked)
            {
                p3TextBox.Show();
                p4TextBox.Show();
                noOfPlayers = 4;
            }
        }

        //Runs when the Confirm button is clicked
        private void BtConfirm_Click(object sender, EventArgs e)
        {
            //Determines if custom words by users are accepted during gameplay.
            bool isCustomDb = false;

            int timeChosen = 0;
            //get selected time limit
            if (rb60Seconds.Checked)
            {
                timeChosen = 60;
            }
            else if (rb120Seconds.Checked)
            {
                timeChosen = 120;
            }
            else if (rbCustomTime.Checked)
            {
                timeChosen = Convert.ToInt32(numericUpDownCustom.Value);
            }
     
            if (rbCustomDB.Checked)
                isCustomDb = true;

            //Setting player names
            string[] playerNames;
            playerNames = new string[noOfPlayers];
            playerNames[0] = p1TextBox.Text;
            playerNames[1] = p2TextBox.Text;

            //Checking if player name entered is alphanumeric
            if (!System.Text.RegularExpressions.Regex.IsMatch(p1TextBox.Text, "^[a-zA-Z\\s0-9]+$") && p1TextBox.Text != "")
            {
                p1TextBox.Text = "";
                MessageBox.Show("Player name can only include letters and numbers.");
                return;
            }
            //Checking if player name entered is alphanumeric
            if (!System.Text.RegularExpressions.Regex.IsMatch(p2TextBox.Text, "^[a-zA-Z\\s0-9]+$") && p2TextBox.Text != "")
            {
                p2TextBox.Text = "";
                MessageBox.Show("Player name can only include letters and numbers.");
                return;
            }

            if (noOfPlayers > 2)
            {
                playerNames[2] = p3TextBox.Text;
                //Checking if player name entered is alphanumeric
                if (!System.Text.RegularExpressions.Regex.IsMatch(p3TextBox.Text, "^[a-zA-Z\\s0-9]+$") && p3TextBox.Text != "")
                {
                    p3TextBox.Text = "";
                    MessageBox.Show("Player name can only include letters and numbers.");
                    return;
                }
            }
            if (noOfPlayers > 3)
            {
                playerNames[3] = p4TextBox.Text;
                //Checking if player name entered is alphanumeric
                if (!System.Text.RegularExpressions.Regex.IsMatch(p4TextBox.Text, "^[a-zA-Z\\s0-9]+$") && p4TextBox.Text != "")
                {
                    p4TextBox.Text = "";
                    MessageBox.Show("Player name can only include letters and numbers.");
                    return;
                }
            }

            //Checking if player names given are unique
            if(p1TextBox.Text != "")
            {
                if(p1TextBox.Text == p2TextBox.Text || p1TextBox.Text == p3TextBox.Text || p1TextBox.Text == p4TextBox.Text)
                {
                    MessageBox.Show("Player names must be unique.");
                    return;
                }
            }
            if (p2TextBox.Text != "")
            {
                if (p2TextBox.Text == p3TextBox.Text || p2TextBox.Text == p4TextBox.Text)
                {
                    MessageBox.Show("Player names must be unique.");
                    return;
                }
            }
            if (p3TextBox.Text != "")
            {
                if (p3TextBox.Text == p4TextBox.Text)
                {
                    MessageBox.Show("Player names must be unique.");
                    return;
                }
            }

            //Creating and displaying the game with the user's selected conditions.
            Game = new GameForm(noOfPlayers, playerNames, timeChosen, isCustomDb);
            this.Hide();
            Game.ShowDialog();
            this.Show();
        }

        //Enables or disables the numeric up down control based on the selected radio button
        private void TimeLimitCheck_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (rbCustomTime.Checked)
            {
                numericUpDownCustom.Enabled = true;
            }
            else
            {
                numericUpDownCustom.Enabled = false;
            }

        }
    }
}

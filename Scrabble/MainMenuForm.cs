using System;
using System.Windows.Forms;

namespace Scrabble
{
    //Starting point of the program.
    //Provides user with five options New Game, Load Game, Statistics, Add New Words, and Exit Game.
    //Shows rules of Scrabble in text box.
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btNewGame_Click(object sender, EventArgs e)
        {
            GameSetupForm Game = new GameSetupForm();
            this.Hide();
            Game.ShowDialog();
            this.Show();
        }

        private void btLoadGame_Click(object sender, EventArgs e)
        {
            LoadGameForm loadGame = new LoadGameForm();
            loadGame.ShowDialog();
            this.Show();
        }

        private void btNewWords_Click(object sender, EventArgs e)
        {
            CustomWordsForm addWords = new CustomWordsForm();
            addWords.ShowDialog();
            this.Show();
        }

        private void btStats_Click(object sender, EventArgs e)
        {
            StatisticsForm stats = new StatisticsForm();
            this.Hide();
            stats.ShowDialog();
            this.Show();
        }
    }
}

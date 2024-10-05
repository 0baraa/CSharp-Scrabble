using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Scrabble
{
    //Represents a player
    //Inherits from Panel
    //Each Player object contains information about the player like the player's score, player's name and player's current Tiles.
    //The exchange tray is also a player but with a slightly different look
    public class Player : Panel, IComparable
    {
        const int PLAYERLABELHEIGHT = 20;
        public string playerName;
        public int score;
        public int skipCount;
        private Label scoreLabel;
        public Button exchangeButton;
        //7 TilePlaces to represent a tray where 7 Tiles can be placed
        public TilePlace[] tilePlaces = new TilePlace[7];
        public bool hasWon = false;
        //Stores database values like their highscores.
        public PlayerData dbRow;


        public Player(string playerName, bool turn, Point location, bool isLoadedPlayer = false, bool isExchangeTray = false)
        {
            //If user has not placed any text in the text box, the player is given a guest name (Guest + number between 0,1000)
            if (playerName == "")
            {
                //Guid.NewGuid().GetHashCode() is used to increase how random the numbers generated are.
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                this.playerName = "Guest" + rand.Next(0, 1000); ;
            }
            else
            {
                this.playerName = playerName;
            }

            //Finding if a player exists in the database.
            //If player exists then a PlayerData object dbRow is created which includes the stats of the player.
            dbRow = DatabaseConnection.GetPlayerByName(this.playerName);
            //no existing data found in DB. Create new player and add to database.
            //If the player is loaded, don't add their name to the database to avoid adding guest names.
            if (dbRow == null && !isLoadedPlayer &&!isExchangeTray)
            {
                dbRow = new PlayerData();//default 
                DatabaseConnection.CreateNewPlayer(playerName, dbRow);
            }
            SetTurn(turn);

            //Creates buttons that can hold tiles for every tile place in a tray 
            for (int i = 0; i < tilePlaces.Length; i++)
            {
                tilePlaces[i] = new TilePlace(i, 0)
                {
                    Location = new Point(i * Board.BUTTONWIDTH, PLAYERLABELHEIGHT)
                };
                tilePlaces[i].Click += new EventHandler(tilePlaces[i].TilePlace_Click);
                tilePlaces[i].MouseDown += new MouseEventHandler(tilePlaces[i].TilePlace_RightClick);
                Controls.Add(tilePlaces[i]);
            }

            BorderStyle = BorderStyle.Fixed3D;
            Location = location;
            Size = new Size(tilePlaces.Length * Board.BUTTONWIDTH, Board.BUTTONWIDTH + 20);
            BackColor = Color.FromArgb(50, Color.White);

            //If Player object being created is not an exchange tray 
            if (!isExchangeTray)
            {
                scoreLabel = new Label
                {
                    Text = "Score: " + score,
                    Location = new Point(Width - 60, 0),
                    Size = new Size(60, PLAYERLABELHEIGHT),
                    Anchor = AnchorStyles.Right
                };
                Controls.Add(scoreLabel);
            }

            //Player object being created is an exchange tray
            else
            {
                exchangeButton = new Button
                {
                    Text = "EXCHANGE",
                    Location = new Point(Width - 95, 0),
                    Size = new Size(90, PLAYERLABELHEIGHT),
                    Anchor = AnchorStyles.Right
                };
                Controls.Add(exchangeButton);
            }

            //Creates label for players, e.g. Player 1
            Label playerLabel = new Label
            {
                Text = this.playerName,
                Location = new Point(0, 0),
                Size = new Size(Board.BUTTONWIDTH * tilePlaces.Length, PLAYERLABELHEIGHT)
            };
            Controls.Add(playerLabel);
        }

        // Checks if player can receive a tile
        public bool NeedsTile()
        {
            //For every tile place in a player's tray
            for (int i = 0; i < tilePlaces.Length; i++)
            {
                if (tilePlaces[i].Tile == null)
                    return true;
            }
            return false;
        }

        // Fills an empty slot in the player's tray with the tile provided
        public void FillTiles(Tile tile)
        {
            //For every tile place in a player's tray
            for (int i = 0; i < tilePlaces.Length; i++)
            {
                if (tilePlaces[i].Tile == null)
                {
                    tilePlaces[i].Tile = tile;
                    tilePlaces[i].UpdateButtonTextOrTile();
                    return;
                }
            }
        }

        // Changes colour of player tray to indicate the turn and disables trays of other players
        public void SetTurn(bool turn)
        {
            if (turn)
            {
                Enabled = true;
                BackColor = Color.FromArgb(20, Color.Blue);
            }

            else
            {
                BackColor = Color.Transparent;
                Enabled = false;
            }
        }

        // Adds the score from this turn to the total score of the player
        public int AddToScore(int thisRoundScore)
        {
            score += thisRoundScore;
            scoreLabel.Text = "Score: " + score;
            return score;
        }


        //Checks if a player has any tiles
        public bool PlayerHasTiles()
        {
            foreach (TilePlace tp in tilePlaces)
            {
                if (tp.Tile != null)
                    return true;
            }
            return false;
        }

        // Calculates the value of the tiles a player currently holds
        public int CalculateTrayTilesValue()
        {
            int score = 0;
            foreach (TilePlace tp in tilePlaces)
            {
                if (tp.Tile != null)
                    score += tp.Tile.ScoreValue;
            }
            return score;
        }

        public int CompareTo(object obj)
        {
            //descending order soroting
            Player toCompare = (Player)obj;
            if (this.score > toCompare.score)
                return -1;
            else if (this.score == toCompare.score)
                return 0;
            else return 1;
        }
    }

    public class PlayerData
    {
        //DB variables
        public DateTime lastActive;
        public int highScore;
        public DateTime dateOfHighScore;
        public int noOfGamesPlayed;
        public int noOfGamesWon;
        public int longestWordPlayedScore;

        public PlayerData()
        {
            dateOfHighScore = new DateTime();
            highScore = 0;
            lastActive = DateTime.Now;
            longestWordPlayedScore = 0;
            noOfGamesPlayed = 0;
            noOfGamesWon = 0;
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Scrabble
{
    // The form on which gameplay visuals and controls are dislpayed.
    public partial class GameForm : Form
    {
        // Instance of Board which will contain a grid of tileplaces used in the game. 
        private Board scrabbleBoard;
        //Tiles loaded from database.
        private Tile[] loadedDBTiles;
        private Player[] players;
        // Instance of player that represents the exchange tray in game play.
        private Player exchangeTray;
        private int noOfPlayers;
        // Decides how many tiles have been taken from the bag.
        private int nextTileIndex = 0;
        // Decides which player has the turn.
        private int turnPlayer = 0;
        private Button newBag;
        //Text box that updates throughout the game with gameplay info e.g. definitions of words played.
        private TextBox gameInfoTxtBox;
        //Decides if custom words will be accepted during game play.
        private bool isCustomDB = false;
        //Stores the time at which a game starts
        private DateTime startTime;
        //Stores the highest score acheived in one play/one turn.
        private int highestScoreWord = 0;
        private Timer turnTimer;
        //Determines how many seconds a turn lasts.
        private int TimePerTurn;
        //Stores how many seconds are left. Decremented every second.
        private int timeLeft;
        private Label timeLabel;

        public GameForm(int nPlayers, string[] playerNames, int secondsPerTurn, bool customDB, GameState loadedGameState = null, bool isLoadedGame = false)
        {
            InitializeComponent();
            this.TopMost = true;
            startTime = DateTime.Now;
            //initialising the timer for each move
            TimePerTurn = secondsPerTurn;
            //initialise remaining time for the first turn.
            timeLeft = TimePerTurn;
            isCustomDB = customDB;
            noOfPlayers = nPlayers;
            players = new Player[noOfPlayers];

            if (isLoadedGame)
            {
                //Setting up loaded game
                startTime = loadedGameState.startTime;
                nextTileIndex = loadedGameState.nextTileIndex;
                turnPlayer = loadedGameState.turnPlayer;
                loadedDBTiles = loadedGameState.loadedDBTiles;
                scrabbleBoard = new Board(boardPanel, true, loadedGameState.scrabbleBoard);
            }

            else
            {
                //Setting up new game
                scrabbleBoard = new Board(boardPanel, false);
                loadedDBTiles = DatabaseConnection.LoadAllTiles();
                //Shuffles tiles in loadedTiles array
                Tile.ShuffleTiles(loadedDBTiles);
            }

            //Setting up player
            int playerLocationX = boardPanel.Location.X + boardPanel.Width + 20;
            int playerLocationY = boardPanel.Location.Y;
            //For every player
            for (int i = 0; i < players.Length; ++i)
            {
                //Creates player with the correct name label
                if (isLoadedGame)
                {
                    //Create players with names from save file
                    players[i] = new Player(loadedGameState.players.playersSerializable[i].name, false, new Point(playerLocationX, playerLocationY), isLoadedGame);
                    //Updates the score label for players when games are loaded
                    players[i].AddToScore(loadedGameState.players.playersSerializable[i].score);
                    //Placing saved tiles into player's tray
                    for (int x = 0; x < players[i].tilePlaces.Length; x++)
                    {
                        players[i].tilePlaces[x].Tile = loadedGameState.players.playersSerializable[i].tilePlacesSerializable[x].Tile;
                        //Updating image of TilePlace with correct tile image
                        players[i].tilePlaces[x].UpdateButtonTextOrTile();
                    }
                }
                else
                {
                    //Create players with names entered by user
                    players[i] = new Player(playerNames[i], false, new Point(playerLocationX, playerLocationY));
                }

                //While the index is less than 100 AND the player can receive a tile
                while (nextTileIndex < loadedDBTiles.Length && players[i].NeedsTile())
                {
                    players[i].FillTiles(GetNextTile());
                }
                //If it's this player's turn, change their colour to indicate this.
                if (i == turnPlayer)
                    players[i].SetTurn(true);
                //Adds the player panel at correct location
                Controls.Add(players[i]);
                //pushes panel down 20 pixels + panel's height to move to the next player's starting position
                playerLocationY += players[i].Height + 20;
            }

            //Creating exchange tray
            exchangeTray = new Player("Place the tiles you would like to exchange here ", true, new Point(playerLocationX, playerLocationY), false, true);
            playerLocationY += exchangeTray.Height + 20;
            exchangeTray.exchangeButton.Click += new EventHandler(ConfirmExchange);
            Controls.Add(exchangeTray);
            exchangeTray.Hide();
            //player setup complete

            //Adding end turn button
            Button endTurnButton = new Button
            {
                Height = 70,
                Width = 150,
                Text = "End turn"
            };
            endTurnButton.Click += new EventHandler(EndTurn);
            endTurnButton.Location = new Point(playerLocationX, this.ClientRectangle.Height - 20 - endTurnButton.Height);
            Controls.Add(endTurnButton);
            endTurnButton.Enabled = true;

            //Adding exchange tiles button
            Button exchangeTilesButton = new Button
            {
                Height = 70,
                Width = 150,
                Text = "Exchange tiles"
            };
            exchangeTilesButton.Click += new EventHandler(ExchangeTiles);
            exchangeTilesButton.Location = new Point(endTurnButton.Location.X + endTurnButton.Width + 20, this.ClientRectangle.Height - 20 - exchangeTilesButton.Height);
            Controls.Add(exchangeTilesButton);

            //Adding save game button
            Button saveGameButton = new Button
            {
                Height = 70,
                Width = 150,
                Text = "Save Game"
            };
            saveGameButton.Click += new EventHandler(SaveGame);
            saveGameButton.Location = new Point(playerLocationX, this.ClientRectangle.Height - 30 - endTurnButton.Height - saveGameButton.Height);
            Controls.Add(saveGameButton);

            //Adding exit game button
            Button exitGameButton = new Button
            {
                Height = 70,
                Width = 150,
                Text = "Exit Game"
            };
            exitGameButton.Click += new EventHandler(ExitGame);
            exitGameButton.Location = new Point(endTurnButton.Location.X + endTurnButton.Width + 20, this.ClientRectangle.Height - 30 - exchangeTilesButton.Height - exitGameButton.Height);
            Controls.Add(exitGameButton);

            //Adding new bag image with how many tiles are left 
            newBag = new Button
            {
                Size = new Size(80, 80),
                BackgroundImageLayout = ImageLayout.Stretch,
                Location = new Point(playerLocationX, this.ClientRectangle.Height - 160 - endTurnButton.Height - saveGameButton.Height),
                Font = new Font(Button.DefaultFont.FontFamily, 20),
                Text = Convert.ToString(100 - nextTileIndex),
                BackgroundImage = Image.FromFile("bag.png")
            };
            newBag.FlatAppearance.BorderSize = 0;
            newBag.FlatStyle = FlatStyle.Flat;
            newBag.TabStop = false;
            Controls.Add(newBag);

            //Adding game info text box
            gameInfoTxtBox = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Size = new Size(players[0].Width, players[0].Height + 20),
                Location = new Point(playerLocationX, this.ClientRectangle.Height - 270 - endTurnButton.Height - saveGameButton.Height)
            };
            Controls.Add(gameInfoTxtBox);
            if (!isLoadedGame)
                gameInfoTxtBox.AppendText("Game has started." + System.Environment.NewLine);
            if (isLoadedGame)
                gameInfoTxtBox.Text = loadedGameState.gameInfoText;

            //Adding time left in turn label
            timeLabel = new Label
            {
                Location = new Point(playerLocationX, this.ClientRectangle.Height - 70 - endTurnButton.Height - saveGameButton.Height),
                Size = new Size(180, 50),
                Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            };

            //If user has chosen any time limit other than 0
            if (secondsPerTurn != 0)
            {

                Controls.Add(timeLabel);
                turnTimer = new Timer
                {
                    Interval = 1000
                };
                turnTimer.Tick += new EventHandler(TurnTimer_Tick);
                turnTimer.Enabled = true;
            }
        }

        //Runs when user clicks "EXCHANGE" button on exchange tray.
        //Swaps tiles in the exchange tray with randomly selected tiles from the bag, which are given to the user's tray
        private void ConfirmExchange(object sender, EventArgs e)
        {
            //Guid.NewGuid().GetHashCode() is used to increase how random the numbers generated are.
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (TilePlace tp in exchangeTray.tilePlaces)
            {
                if (tp.Tile != null)
                {
                    int randLocation = rand.Next(nextTileIndex, 100);
                    Console.WriteLine("RANDOM INDEX : " + randLocation);
                    players[turnPlayer].FillTiles(loadedDBTiles[randLocation]);
                    loadedDBTiles[randLocation] = tp.Tile;
                    tp.Tile = null;
                    tp.UpdateButtonTextOrTile();
                }
            }
            exchangeTray.Hide();
            
            gameInfoTxtBox.AppendText("Turn ended. " + System.Environment.NewLine);
            Console.WriteLine("Turn ended.");
            //Changing turn of player
            players[turnPlayer].SetTurn(false);

            if (turnPlayer < noOfPlayers - 1)
                turnPlayer++;

            else
                turnPlayer = 0;

            Console.WriteLine(turnPlayer);
            players[turnPlayer].SetTurn(true);
            gameInfoTxtBox.AppendText(players[turnPlayer].playerName + "'s turn ." + System.Environment.NewLine);
        }

        // Runs when "Exchange Tiles" button is clicked. Displays or hides exchange tray. 
        // Does not show exchange tray if there are tiles in it.
        // Does not show exchange tray if bag has no tiles.
        private void ExchangeTiles(object sender, EventArgs e)
        {
            foreach (TilePlace tp in exchangeTray.tilePlaces)
            {

                if (tp.Tile != null && exchangeTray.Visible == true)
                {
                    MessageBox.Show("There are tiles in the exchange tray.");
                    return;
                }
            }

            if (nextTileIndex == 100)
            {
                MessageBox.Show("No more tiles in the bag", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (exchangeTray.Visible)
                    exchangeTray.Hide();
                else
                    exchangeTray.Show();
            }
        }

        // Runs when the "End Turn" button is clicked.
        // Checks if tiles are in valid position.
        // Calculates points.
        // Locks tiles on board.
        // Replaces empty spaces in tray.
        // Changes turn.
        private void EndTurn(object sender, EventArgs e)
        {
            //If user has placed tiles on the board and timer runs out and the tiles are not placed correctly, it will count as a skip
            bool timerExpired = false;
            if (sender.GetType() == typeof(Timer))
                timerExpired = true;

            if (timerExpired)
                gameInfoTxtBox.AppendText("Ran out of time." + System.Environment.NewLine);

            bool exchangeTrayEmpty = true;
            foreach(TilePlace tp in exchangeTray.tilePlaces)
            {
                if (tp.Tile != null)
                {
                    exchangeTrayEmpty = false;
                    break;
                }
            }

            //Return tiles in exchange tray to player's tray if it is not empty
            if(!exchangeTrayEmpty)
                scrabbleBoard.ReturnPlayerTiles(players[turnPlayer].tilePlaces, exchangeTray);

            exchangeTray.Hide();

            //If tiles are placed illegally and time in turn has ended, return tiles to player
            //If there is still time in the turn do not return tiles.
            if (!scrabbleBoard.CheckTilePlacement())
            {
                if (timerExpired)
                {
                    //no holding tactics, turn ends by returning tiles. 
                    scrabbleBoard.ReturnPlayerTiles(players[turnPlayer].tilePlaces);

                }
                else
                {
                    //theres still time left
                    Console.WriteLine("Incorrect placement.");
                    gameInfoTxtBox.AppendText("Incorrect tile placement. " + System.Environment.NewLine);
                    return;
                }
            }

            //Calculating score from turn and storing in thisTurnScore
            int thisTurnScore = scrabbleBoard.CheckWordForCorrectnessAndScore(isCustomDB);


            if (thisTurnScore > 0)
            {
                //Reset skipcount
                players[turnPlayer].skipCount = 0;
                //Add thisTurnScore to the player's total score.
                players[turnPlayer].AddToScore(thisTurnScore);
                //Display definitions of words played
                gameInfoTxtBox.AppendText(DatabaseConnection.GetDefinitions(scrabbleBoard));

                //If score from this turn is larger than the highest score made in one turn this game
                if (thisTurnScore > highestScoreWord)
                    highestScoreWord = thisTurnScore;

                if (scrabbleBoard.IsFirstTurn)
                {
                    scrabbleBoard.IsFirstTurn = false;
                }
            }

            //Player has scored 0, meaning turn has been skipped.
            else if (thisTurnScore == 0)
            {
                players[turnPlayer].skipCount++;
            }

            else
            {
                //Reset skipcount
                players[turnPlayer].skipCount = 0;
                //value return is -1, word played is not valid
                //no score, turn is lost and tiles are returned to the player
                scrabbleBoard.ReturnPlayerTiles(players[turnPlayer].tilePlaces);
                gameInfoTxtBox.AppendText("One or more words incorrect, tiles returned and turn lost. " + System.Environment.NewLine);
            }

            //Lock tiles played in this turn
            scrabbleBoard.LockTurnTiles();
            //Check if this is a new highest score played in one turn for the player.
            //Update database if it is a new highscore.
            DatabaseConnection.CheckAndUpdateLongestWordPlayedScore(thisTurnScore, players[turnPlayer]);

            //While the index is less than 100 (bag is not empty) AND the player can receive a tile
            while (nextTileIndex < loadedDBTiles.Length && players[turnPlayer].NeedsTile())
            {
                players[turnPlayer].FillTiles(GetNextTile());
            }
            //updating remaining tiles label
            newBag.Text = Convert.ToString(loadedDBTiles.Length - nextTileIndex);


            //Checking if game has ended
            //Game ends if a player skips their turn twice in a row.
            //Or if a player plays all their tiles and there are no more tiles in the bag

            bool skippedTwice = false;

            //If player has skipped twice in a row
            if (players[turnPlayer].skipCount == 2)
            {
                skippedTwice = true;
                Console.WriteLine("Player " + turnPlayer + " skipped turn twice");
                gameInfoTxtBox.AppendText("Player " + players[turnPlayer].playerName + " skipped turn twice." + System.Environment.NewLine);
                EndGame(skippedTwice);
            }

            //If player has no tiles in tray
            if (!players[turnPlayer].PlayerHasTiles())
            {
                Console.WriteLine("Player " + turnPlayer + " ran out of tiles");
                gameInfoTxtBox.AppendText("Player " + players[turnPlayer].playerName + " played all their tiles." + System.Environment.NewLine);
                EndGame(skippedTwice);
            }
            
            Console.WriteLine("Turn ended.");
            gameInfoTxtBox.AppendText("Turn ended." + System.Environment.NewLine);
            players[turnPlayer].SetTurn(false);

            //changes turn of player
            if (turnPlayer < noOfPlayers - 1)
                turnPlayer++;

            else
                turnPlayer = 0;

            Console.WriteLine(turnPlayer);
            gameInfoTxtBox.AppendText(players[turnPlayer].playerName + "'s turn ." + System.Environment.NewLine);
            players[turnPlayer].SetTurn(true);

            //reset timer
            timeLeft = TimePerTurn;
        }

        //Runs when the Save Game button is clicked.
        //Displays a SaveNameForm where the user can enter their desired file name for the save file.
        private void SaveGame(object sender, EventArgs e)
        {
            SaveNameForm saveName = new SaveNameForm();
            saveName.ShowDialog();
        }

        //Runs when Exit Game button is clicked.
        //Confirms if user wants to exit before closing the form.
        private void ExitGame(Object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you would like to exit?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        //Runs when the EndTurn method detects that the game has ended.
        private void EndGame(bool skippedTwice)
        {
            //Stores index of player who has ran out of tiles 
            int playerRanOutOfTiles = -1;
            //Stores the value of the tiles of players who have not won. This will be added to the score of the player who finished their tiles.
            int otherPlayerTileValues = 0;
            Console.WriteLine("Game ended");
            gameInfoTxtBox.AppendText("Game has ended." + System.Environment.NewLine);
            //Disabling controls on game form
            foreach (Control c in Controls)
            {
                c.Enabled = false;
            }
            if(turnTimer != null)
                turnTimer.Enabled = false;

            if (!skippedTwice)
            {
                //Calculating the value of tiles of players who have not won and adding to winner's score
                //The value of the tiles in each player's tray is calculated and subtracted from their score. 
                for (int i = 0; i < noOfPlayers; ++i)
                {
                    Player p = players[i];

                    if (p.PlayerHasTiles())
                    {
                        p.AddToScore(p.CalculateTrayTilesValue() * -1);

                        otherPlayerTileValues += p.CalculateTrayTilesValue();
                    }
                    else
                    {
                        playerRanOutOfTiles = i;
                    }
                }

                if (playerRanOutOfTiles >= 0)
                {
                    //Adding other players' tiles value to score of player who finished 
                    players[playerRanOutOfTiles].AddToScore(otherPlayerTileValues);
                }
            }

            //Display message box with rankings of players based on score.
            MessageBox.Show(CalculatePlayerRankings());

            //For every player, update their stats e.g. number of games played, number of games won.
            for (int i = 0; i < players.Length; i++)
            {
                DatabaseConnection.CheckAndUpdateEndGameStats(players[i].hasWon, players[i]);
            }


            //Adding game statistics to database
            if (noOfPlayers == 4)
            {
                DatabaseConnection.SaveGameData(startTime, highestScoreWord, noOfPlayers, players[0].playerName, players[0].score, players[1].playerName, players[1].score, players[2].playerName, players[2].score, players[3].playerName, players[3].score);

            }
            else if (noOfPlayers == 3)
            {
                DatabaseConnection.SaveGameData(startTime, highestScoreWord, noOfPlayers, players[0].playerName, players[0].score, players[1].playerName, players[1].score, players[2].playerName, players[2].score);

            }
            else
            {
                DatabaseConnection.SaveGameData(startTime, highestScoreWord, noOfPlayers, players[0].playerName, players[0].score, players[1].playerName, players[1].score);
            }
        }


        //Returns the next available tile from the loaded tiles and moves the nextTileIndex 
        //Returns null if all tiles have been used
        private Tile GetNextTile()
        {
            if (loadedDBTiles.Length <= nextTileIndex)
            {
                return null;
            }

            nextTileIndex++;
            return loadedDBTiles[nextTileIndex - 1];
        }

        //Calculates the rankings of players based on their score
        //A string containing the player rankings is returned.
        private string CalculatePlayerRankings()
        {
            string rankingsString = "";
            if (players[turnPlayer].skipCount == 2)
            {
                rankingsString += players[turnPlayer].playerName + " skipped their turn twice in a row." + System.Environment.NewLine;
            }

            //sorting players in an array. 
            Array.Sort(players);

            int prevScore = players[0].score;
            int prevIndex = 0;
            rankingsString += 1 + ". " + players[0].playerName + "   " + players[0].score + System.Environment.NewLine;
            players[0].hasWon = true;

            for (int i = 1; i < noOfPlayers; i++)
            {
                if (prevScore == players[i].score)
                {
                    rankingsString += prevIndex + 1 + ". " + players[i].playerName + "   " + players[i].score + System.Environment.NewLine;

                    //if first position is being shared:
                    if (players[prevIndex].hasWon)
                        players[i].hasWon = true;

                }
                else
                {
                    rankingsString += i + 1 + ". " + players[i].playerName + "   " + players[i].score + System.Environment.NewLine;
                    //update previous score and index for next iteration
                    prevScore = players[i].score;
                    prevIndex = i;
                }
            }
            return rankingsString;
        }
  
        //Runs every 1000ms
        //Updates timer value
        private void TurnTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                timeLabel.Text = "Time left: " + timeLeft;
            }
            //If time in turn has ended, end turn
            else
            {
                //call the end turn method. 
                EndTurn(sender, e);
            }
        }

        //Stores fields from GameForm required to save and load a game
        public class GameFormState
        {
            public int noOfPlayers;
            public int secondsPerTurn;
            public int nextTileIndex;
            public int turnPlayer;
            public Tile[] loadedDBTiles;
            public bool isCustomDB;
            public string gameInfoText;
            public DateTime startTime;
            public Board scrabbleBoard;
            public Player[] players;

            public GameFormState(GameForm game)
            {
                noOfPlayers = game.noOfPlayers;
                secondsPerTurn = game.TimePerTurn;
                nextTileIndex = game.nextTileIndex;
                turnPlayer = game.turnPlayer;
                loadedDBTiles = game.loadedDBTiles;
                isCustomDB = game.isCustomDB;
                gameInfoText = game.gameInfoTxtBox.Text;
                startTime = game.startTime;
                scrabbleBoard = game.scrabbleBoard;
                players = game.players;
            }
        }
    }
}

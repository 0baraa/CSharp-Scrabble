using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Scrabble
{
    //This class includes methods that read from and update a Microsoft Access database.
    class DatabaseConnection
    {
        static DataTable results;
        public const string connString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = 'Scrabble.accdb'";

        // Loads tiles from MS Access database and returns them as a Tile array.
        public static Tile[] LoadAllTiles()
        {
            OleDbConnection Conn = new OleDbConnection(connString);
            Conn.Open();
            OleDbCommand Cmd = new OleDbCommand
            {
                Connection = Conn,
                CommandText = "SELECT * FROM Tile"
            };
            OleDbDataAdapter adapter = new OleDbDataAdapter(Cmd);
            results = new DataTable();
            adapter.Fill(results);
            Conn.Close();
            Tile[] tiles = new Tile[100];

            for (int i = 0; i < results.Rows.Count && i < 100; i++)
            {
                DataRow dataRow = results.Rows[i];
                Tile tile = new Tile(Convert.ToInt32(dataRow["ID"])
                    , Convert.ToInt32(dataRow["ScoreValue"])
                    , Convert.ToChar(dataRow["Label"]));
                Console.WriteLine(tile.ID);
                tiles[i] = tile;
            }
            return tiles;
        }

        //Inserts a new game into the database with information about the game e.g. the names of players in the game, time when game started.
        public static void SaveGameData(DateTime startTime, int HSWord, int NoOfPlayers, string P1PlayerName, int p1score, 
            string p2playerName, int p2score, string p3PlayerName = "", int p3score = 0, string p4playerName = "", int p4score = 0)
        {


            OleDbConnection Conn = new OleDbConnection(connString);
            Conn.Open();
            OleDbCommand Cmd = new OleDbCommand
            {
                Connection = Conn,
                CommandText = "INSERT INTO Game (StartTime, EndTime, HSWord, NoOfPlayers) " +
                        "Values ( '" + startTime.ToString() + "', '" + DateTime.Now.ToString() + "', " + HSWord + ", " + NoOfPlayers + "  )"
            };
            OleDbDataAdapter adapter = new OleDbDataAdapter(Cmd);
            int numRows = Cmd.ExecuteNonQuery();
            if (numRows == 0)
                Console.WriteLine("No Data saved");

            Cmd.CommandText = "Select @@Identity";
            int latestRowID = Convert.ToInt32(Cmd.ExecuteScalar());


            if (latestRowID == 0) Console.WriteLine("Nothing Saved In DB");
            Conn.Close();
            

            //get Game's ID
            for(int i = 1; i <= NoOfPlayers; ++i)
            {
                int playernumber = i;
                string playername="";
                int score = 0;
                switch (i)
                {
                    case 1:
                        playername = P1PlayerName;
                        score = p1score;
                        break;
                    case 2:
                        playername = p2playerName;
                        score = p2score;
                        break;
                    case 3:
                        playername = p3PlayerName;
                        score = p3score;
                        break;
                    case 4:
                        playername = p4playerName;
                        score = p4score;
                        break;
                    default:
                        break;

                }

                if (playername.StartsWith("Guest"))
                    playername = "Guest";

                //use insert command to insert rows into game player link table. gameplayerlink table : playername, latestrowid, score, i, 


                Conn.Open();
                OleDbCommand PlayerCmd = new OleDbCommand
                {
                    Connection = Conn,
                    CommandText = "INSERT INTO PlayerGameLinkTable (PlayerName, GameID, PlayerNumber, Score) Values ('" + playername + "', " + latestRowID + ", " + i + ", " + score + ") "
                };
                OleDbDataAdapter playeradapter = new OleDbDataAdapter(PlayerCmd);
                numRows = PlayerCmd.ExecuteNonQuery();
                if (numRows == 0)
                    Console.WriteLine("No Data saved in playergamelinktable");

                Conn.Close();



            }

        }

        //Finds if a player exists in the database.
        //If player exists then a PlayerData object is created which includes the statistics of the player. This is returned.
        public static PlayerData GetPlayerByName(string playerName)
        {
            OleDbConnection Conn = new OleDbConnection(connString);
            Conn.Open();
            OleDbCommand Cmd = new OleDbCommand
            {
                Connection = Conn,
                CommandText = "SELECT * FROM Player where PlayerName='" + playerName + "'"
            };
            OleDbDataAdapter adapter = new OleDbDataAdapter(Cmd);
            results = new DataTable();
            adapter.Fill(results);
            Conn.Close();
            if (results.Rows.Count == 0)
                return null;

            DataRow dataRow = results.Rows[0];
            PlayerData data = new PlayerData
            {
                lastActive = Convert.ToDateTime(dataRow["LastActive"]),
                noOfGamesPlayed = Convert.ToInt32(dataRow["NoOfGames"]),
                noOfGamesWon = Convert.ToInt32(dataRow["GamesWon"]),
                longestWordPlayedScore = Convert.ToInt32(dataRow["LongestWordPlayedScore"]),
                highScore = Convert.ToInt32(dataRow["HighScore"]),
                dateOfHighScore = Convert.ToDateTime(dataRow["DateOfHighScore"])
            };
            return data;
        }

        //Inserts a new player into the database 
        public static void CreateNewPlayer(string playerName, PlayerData dbRow)
        {
            if (playerName == "")
            {
                return;
            }
            OleDbConnection Conn = new OleDbConnection(connString);
            Conn.Open();
            OleDbCommand Cmd = new OleDbCommand
            {
                Connection = Conn,
                CommandText = "INSERT INTO Player Values('" + playerName + "', '" + dbRow.lastActive.ToString() + "', '" + dbRow.lastActive.ToString() + "', '" + dbRow.dateOfHighScore.ToString() + "', "
                + dbRow.highScore + ", " + dbRow.noOfGamesPlayed + ", " + dbRow.noOfGamesWon + ", " + dbRow.longestWordPlayedScore + "  )"
            };
            OleDbDataAdapter adapter = new OleDbDataAdapter(Cmd);
            Cmd.ExecuteNonQuery();
            Conn.Close();
        }

        //Adds user's custom word to database with definition.
        public static void AddCustomWord(TextBox customwordTxtBox, TextBox definitionTxtBox, Label infoLabel)
        {
            string input = customwordTxtBox.Text.ToUpper();

            if (customwordTxtBox.Text.Length < 2)
            {
                infoLabel.Text = "Words must be two or more letters long.";
                return;
            }
            //If the user has used non-alphabetical characters into the text box, do not add to database.
            else if (!System.Text.RegularExpressions.Regex.IsMatch(input, "^[a-zA-Z ]"))
            {
                infoLabel.Text = "Only alphabetical characters are accepted.";
                return;
            }
            OleDbConnection Conn = new OleDbConnection(DatabaseConnection.connString);
            Conn.Open();
            OleDbCommand Cmd = new OleDbCommand
            {
                Connection = Conn,
                CommandText = "SELECT * FROM Dictionary WHERE Word = '" + input + "'"
            };
            OleDbDataAdapter adapter = new OleDbDataAdapter(Cmd);
            DataTable results = new DataTable();
            adapter.Fill(results);

            //If the word input is already in the database
            if (results.Rows.Count != 0)
            {
                infoLabel.Text = input + " already exists in the database.";
            }
            else
            {
                Cmd.CommandText = "INSERT INTO Dictionary VALUES ('" + input + "',  '" + definitionTxtBox.Text + "'  ,True)";
                Cmd.ExecuteNonQuery();
                infoLabel.Text = input + " was added to the database.";
                customwordTxtBox.Text = "";
            }       
            Conn.Close();
        }

        //Checks if score given is greater than the player's current highest score in one turn highscore.
        public static void CheckAndUpdateLongestWordPlayedScore(int score, Player player)
        {
            //If player is a guest, do not check high score
            if(player.dbRow == null)
            {
                return;
            }

            OleDbConnection Conn = new OleDbConnection(DatabaseConnection.connString);
            Conn.Open();
            OleDbCommand Cmd = new OleDbCommand
            {
                Connection = Conn
            };
            if (player.dbRow.longestWordPlayedScore < score)
            {
                Cmd.CommandText = "UPDATE Player SET LongestWordPlayedScore = " + score + " WHERE PlayerName= '" + player.playerName + "'";
                Cmd.ExecuteNonQuery();
                player.dbRow.longestWordPlayedScore = score;
            }
            Conn.Close();
        }


        //update player's stats e.g. number of games played, number of games won.
        public static void CheckAndUpdateEndGameStats(bool hasWon, Player player)
        {
            OleDbConnection Conn = new OleDbConnection(DatabaseConnection.connString);
            Conn.Open();
            OleDbCommand Cmd = new OleDbCommand
            {
                Connection = Conn,
                CommandText = "UPDATE Player SET NoOfGames = NoOfGames+1 WHERE PlayerName='" + player.playerName + "'"
            };
            Cmd.ExecuteNonQuery();
            Cmd.CommandText = "SELECT * FROM Player where PlayerName='" + player.playerName + "'";
            Cmd.ExecuteNonQuery();
            Cmd.CommandText = "UPDATE Player SET LastActive = '" + DateTime.Now.ToString() + "' WHERE PlayerName= '" + player.playerName + "'";
            Cmd.ExecuteNonQuery();
            if (hasWon)
            {
                Cmd.CommandText = "UPDATE Player SET GamesWon = GamesWon+1 WHERE PlayerName= '" + player.playerName + "'";
                Cmd.ExecuteNonQuery();
            }
            if (player.dbRow.highScore < player.score)
            {
                Cmd.CommandText = "UPDATE Player SET HighScore = " + player.score + " WHERE PlayerName= '" + player.playerName + "'";
                Cmd.ExecuteNonQuery();
                Cmd.CommandText = "UPDATE Player SET DateOfHighScore = '" + DateTime.Now.ToString() + "' WHERE PlayerName= '" + player.playerName + "'";
                Cmd.ExecuteNonQuery();
            }
            Conn.Close();
        }

        //Retreives definitions of word/s played and returns them as a string
        public static string GetDefinitions(Board scrabbleBoard)
        {
            string definitions = "";
            OleDbConnection Conn = new OleDbConnection(DatabaseConnection.connString);
            Conn.Open();
            OleDbCommand Cmd = new OleDbCommand
            {
                Connection = Conn
            };
            OleDbDataAdapter adapter = new OleDbDataAdapter(Cmd);
            DataTable results = new DataTable();

            List<TilePlace[]> newlyFormedWords = scrabbleBoard.GetNewlyFormedWords();
            List<string> newlyFormedWordStrings = new List<string>();

            if (newlyFormedWords.Count == 0)
                return "";

            for (int i = 0; i < newlyFormedWords.Count; i++)
            {
                string newWordString = TilePlace.TilePlaceArrayToString(newlyFormedWords[i]);
                Cmd.CommandText = "SELECT Definition FROM Dictionary WHERE Word = '" + newWordString + "'";
                adapter.Fill(results);
                DataRow dataRow = results.Rows[i];
                if (Convert.ToString(dataRow["Definition"]) != "")
                {
                    definitions += newWordString + " definition: " + Convert.ToString(dataRow["Definition"]) + System.Environment.NewLine;
                }
            }
            return definitions;
        }



        public static DataTable[] loadDataGrid(bool firstChange, int mode, string playerSelection, string gameSelection)
        {

            DataTable playertable = new DataTable();
            DataTable gametable = new DataTable();


            OleDbConnection Conn = new OleDbConnection(connString);
            Conn.Open();
            OleDbCommand Cmd = new OleDbCommand
            {
                Connection = Conn
            };
            string playerSQL = "";
            string gameSQL = "";

            if (firstChange == true)
            {
                playerSQL = "SELECT PlayerName, HighScore, DateOfHighScore FROM Player ORDER BY HighScore DESC";
                gameSQL = "Select * from PlayerGameData ORDER BY EndTime DESC";

                Cmd.CommandText = playerSQL;
                OleDbDataAdapter playerdata = new OleDbDataAdapter(Cmd);
                
                playerdata.Fill(playertable);

                Cmd.CommandText = gameSQL;
                OleDbDataAdapter gamedata = new OleDbDataAdapter(Cmd);
                gamedata.Fill(gametable);
                Conn.Close();
                firstChange = false;
            }

            if (mode == 1)
            {
                switch (playerSelection)
                {
                    case "Highest score in one game":
                        playerSQL = "SELECT PlayerName, HighScore, DateOfHighScore FROM Player ORDER BY HighScore DESC";
                        break;
                    case "Number of games played":
                        playerSQL = "SELECT PlayerName, NoOfGames FROM Player ORDER BY NoOfGames DESC";
                        break;
                    case "Number of games won":
                        playerSQL = "SELECT PlayerName, GamesWon FROM Player ORDER BY GamesWon DESC";
                        break;
                    case "Highest score in one turn":
                        playerSQL = "SELECT PlayerName, LongestWordPlayedScore FROM Player ORDER BY LongestWordPlayedScore DESC";
                        break;
                }
                Cmd.CommandText = playerSQL;
                OleDbDataAdapter playerdata = new OleDbDataAdapter(Cmd);
                playerdata.Fill(playertable);
            }

            if (mode == 2)
            {
                switch (gameSelection)
                {
                    case "Most recent":
                        gameSQL = "Select * from PlayerGameData ORDER BY EndTime DESC";
                        break;
                    case "Least recent":
                        gameSQL = "Select * from PlayerGameData ORDER BY EndTime";
                        break;
                    case "Highest score in one turn":
                        gameSQL = "Select * from PlayerGameData ORDER BY HSWord DESC";
                        break;
                }
                Cmd.CommandText = gameSQL;
                OleDbDataAdapter gamedata = new OleDbDataAdapter(Cmd);
                gamedata.Fill(gametable);
                Conn.Close();
            }

            DataTable[] returnable = {playertable, gametable };
            return returnable;
        }

    }
}

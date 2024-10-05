using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Scrabble
{
    //A Board contains a 15x15 grid of TilePlaces where Tiles can be placed.
    public class Board
    {
        //Two dimensional array of TilePlaces that represent the Scrabble board.
        public TilePlace[,] boardData = new TilePlace[BOARDWIDTH, BOARDWIDTH];
        //Determines how many TilePlaces make up the Board. By default it is 15x15.
        private static int BOARDWIDTH = 15;
        //Determines the width of the TilePlaces.
        public static int BUTTONWIDTH = 60;
        public bool IsFirstTurn = true;

        public Board(Panel boardPanel, bool isLoadedBoard, BoardState loadedBoard = null)
        {
            //Creating a 15x15 grid of TilePlaces.
            for (int x = 0; x < BOARDWIDTH; x++)
            {
                for (int y = 0; y < BOARDWIDTH; y++)
                {
                    boardData[x, y] = new TilePlace(x, y);

                }
            }

            //Sets word/letter multipliers for TilePlaces.
            boardData[0, 0].WordMultiplier = 3;
            boardData[0, 3].LetterMultiplier = 2;
            boardData[0, 7].WordMultiplier = 3;
            boardData[0, 11].LetterMultiplier = 2;
            boardData[0, 14].WordMultiplier = 3;

            boardData[1, 1].WordMultiplier = 2;
            boardData[1, 5].LetterMultiplier = 3;
            boardData[1, 9].LetterMultiplier = 3;
            boardData[1, 13].WordMultiplier = 2;

            boardData[2, 2].WordMultiplier = 2;
            boardData[2, 6].LetterMultiplier = 2;
            boardData[2, 8].LetterMultiplier = 2;
            boardData[2, 12].WordMultiplier = 2;

            boardData[3, 0].LetterMultiplier = 2;
            boardData[3, 3].WordMultiplier = 2;
            boardData[3, 7].LetterMultiplier = 2;
            boardData[3, 11].WordMultiplier = 2;
            boardData[3, 14].LetterMultiplier = 2;

            boardData[4, 4].WordMultiplier = 2;
            boardData[4, 10].WordMultiplier = 2;

            boardData[5, 1].LetterMultiplier = 3;
            boardData[5, 5].LetterMultiplier = 3;
            boardData[5, 9].LetterMultiplier = 3;
            boardData[5, 13].LetterMultiplier = 3;

            boardData[6, 2].LetterMultiplier = 2;
            boardData[6, 6].LetterMultiplier = 2;
            boardData[6, 8].LetterMultiplier = 2;
            boardData[6, 12].LetterMultiplier = 2;

            boardData[7, 0].WordMultiplier = 3;
            boardData[7, 3].LetterMultiplier = 2;
            boardData[7, 7].WordMultiplier = 2;
            boardData[7, 11].LetterMultiplier = 2;
            boardData[7, 14].WordMultiplier = 3;

            boardData[8, 2].LetterMultiplier = 2;
            boardData[8, 6].LetterMultiplier = 2;
            boardData[8, 8].LetterMultiplier = 2;
            boardData[8, 12].LetterMultiplier = 2;

            boardData[9, 1].LetterMultiplier = 3;
            boardData[9, 5].LetterMultiplier = 3;
            boardData[9, 9].LetterMultiplier = 3;
            boardData[9, 13].LetterMultiplier = 3;

            boardData[10, 4].WordMultiplier = 2;
            boardData[10, 10].WordMultiplier = 2;

            boardData[11, 0].LetterMultiplier = 2;
            boardData[11, 3].WordMultiplier = 2;
            boardData[11, 7].LetterMultiplier = 2;
            boardData[11, 11].WordMultiplier = 2;
            boardData[11, 14].LetterMultiplier = 2;

            boardData[12, 2].WordMultiplier = 2;
            boardData[12, 6].LetterMultiplier = 2;
            boardData[12, 8].LetterMultiplier = 2;
            boardData[12, 12].WordMultiplier = 2;

            boardData[13, 1].WordMultiplier = 2;
            boardData[13, 5].LetterMultiplier = 3;
            boardData[13, 9].LetterMultiplier = 3;
            boardData[13, 13].WordMultiplier = 2;

            boardData[14, 0].WordMultiplier = 3;
            boardData[14, 3].LetterMultiplier = 2;
            boardData[14, 7].WordMultiplier = 3;
            boardData[14, 11].LetterMultiplier = 2;
            boardData[14, 14].WordMultiplier = 3;

            //If the board is being loaded from a saved file
            if (isLoadedBoard)
            {
                //Sets up a Board based on its state when it was saved.
                IsFirstTurn = loadedBoard.IsFirstTurn;
                for (int y = 0; y < BOARDWIDTH; y++)
                {
                    for (int x = 0; x < BOARDWIDTH; x++)
                    {
                        if (loadedBoard.tpSerializables[x, y].Tile != null)
                        {
                            boardData[x, y].Tile = loadedBoard.tpSerializables[x, y].Tile;
                            boardData[x, y].isLocked = true;
                        }
                    }
                }
            }

            //Placing TilePlaces in the correct position
            PrintBoard(boardPanel);
        }

        //Returns the Tiles placed by the player to their tray.
        //If the exchange tray is passed to the method then Tile in the exchange tray are return to the player tray.
        //else Tile placed on the Board are return to the player tray.
        public void ReturnPlayerTiles(TilePlace[] playerTray, Player exchangeTray = null)
        {
            TilePlace[] newlyPlacedTiles;
            if (exchangeTray == null)
                newlyPlacedTiles = FindTurnTiles();
            else
                newlyPlacedTiles = FindExchangeTrayTiles(exchangeTray);
            int j = 0;

            for (int i = 0; i < playerTray.Length; i++)
            {
                if (playerTray[i].Tile == null)
                {
                    playerTray[i].Tile = newlyPlacedTiles[j].Tile;
                    newlyPlacedTiles[j].Tile = null;
                    //updating button visuals
                    playerTray[i].UpdateButtonTextOrTile();
                    newlyPlacedTiles[j].UpdateButtonTextOrTile();
                    j++;
                    if (j >= newlyPlacedTiles.Length)
                        break;
                }
            }
        }

        //Finds and returns latest tiles placed by player.
        //The size of the array returned will depend on the number of tiles added by player (0 to 7)
        private TilePlace[] FindTurnTiles()
        {
            int tileIndex = 0;
            TilePlace[] newTiles = new TilePlace[7];

            for (int y = 0; y < BOARDWIDTH; y++)
            {
                for (int x = 0; x < BOARDWIDTH; x++)
                {
                    //If TilePlace is not locked AND TilePlace has a Tile
                    if (!boardData[x, y].isLocked && boardData[x, y].Tile != null)
                    {
                        newTiles[tileIndex] = boardData[x, y];
                        tileIndex++;
                    }
                }
            }

            TilePlace[] finalResult = new TilePlace[tileIndex];

            for (int i = 0; i < finalResult.Length; ++i)
            {
                finalResult[i] = newTiles[i];
            }

            return finalResult;
        }


        //Finds and returns Tiles placed in the exchange tray.
        private TilePlace[] FindExchangeTrayTiles(Player exchangeTray)
        {
            int tileIndex = 0;
            TilePlace[] exchangeTrayTiles = new TilePlace[7];

            for (int i = 0; i < exchangeTray.tilePlaces.Length; i++)
            {
                if (exchangeTray.tilePlaces[i].Tile != null)
                {
                    exchangeTrayTiles[tileIndex] = exchangeTray.tilePlaces[i];
                    tileIndex++;
                }
            }
            TilePlace[] finalResult = new TilePlace[tileIndex];

            for (int i = 0; i < finalResult.Length; ++i)
            {
                finalResult[i] = exchangeTrayTiles[i];
            }
            return finalResult;
        }

        //Locks newly placed tiles.
        //Ensures next player is unable to manipulate Tiles from previous turns.
        public void LockTurnTiles()
        {
            foreach (TilePlace tp in FindTurnTiles())
            {
                if (tp != null)
                {
                    tp.isLocked = true;
                }
            }
        }

        //Creates and places all buttons required to represent a 15x15 Scrabble board
        private void PrintBoard(Panel boardPanel)
        {
            for (int y = 0; y < BOARDWIDTH; y++)
            {
                for (int x = 0; x < BOARDWIDTH; x++)
                {
                    boardData[x, y].Location = new Point(x * BUTTONWIDTH, y * BUTTONWIDTH);
                    boardData[x, y].UpdateButtonTextOrTile();
                    //binding
                    boardData[x, y].Click += new EventHandler(boardData[x, y].TilePlace_Click);
                    boardData[x, y].MouseDown += new MouseEventHandler(boardData[x, y].TilePlace_RightClick);
                    boardData[x, y].MouseUp += new MouseEventHandler(boardData[x, y].TilePlace_RightClick);
                    boardPanel.Controls.Add(boardData[x, y]);
                }
            }
        }

        //Returns true if all newly placed Tiles have the same X coordinate / are in the same column.
        private bool IsInXDirection()
        {
            TilePlace[] newtiles = FindTurnTiles();

            if (newtiles.Length <= 1)
                return true;

            else
            {
                int startX = newtiles[0].X;
                bool XDirection = true;
                foreach (TilePlace tp in newtiles)
                {
                    if (tp.X != startX)
                    {
                        Console.WriteLine("X direction test failed");
                        XDirection = false;
                        break;
                    }
                }
                return XDirection;
            }
        }

        //This method checks if the Tiles placed by the user are in a valid position.
        //All tiles must only be in one row or one column.
        //All Tiles must be connected together 
        //The new Tiles must connect to an existing Tile.
        //If this is the first turn of the game one Tile must be placed on the middle TilePlace.
        public bool CheckTilePlacement()
        {
            //TilePlace array with Tiles played in the latest turn.
            TilePlace[] newtiles = FindTurnTiles();

            if (newtiles.Length > 0)
            {
                int startX = newtiles[0].X;
                int startY = newtiles[0].Y;
                bool XDirection = true;
                bool YDirection = true;
                //Checking if all Tiles have the same X coordinate (Tiles are in the same column)
                foreach (TilePlace tp in newtiles)
                {
                    if (tp.X != startX)
                    {
                        Console.WriteLine("X direction test failed");
                        XDirection = false;
                        break;
                    }
                }

                //If not all Tiles have the same X coordinate, check if they all have the same Y coordinate.
                if (!XDirection)
                {
                    foreach (TilePlace tp in newtiles)
                    {
                        if (tp.Y != startY)
                        {
                            Console.WriteLine("Y direction test failed");
                            YDirection = false;
                            break;
                        }
                    }
                }

                //1. XDirection is true if x coordinate of all TilePlaces match. 
                //2. Otherwise, YDirection is true if y coordinate of all TilePlaces match
                //3. Both XDirection and YDirection are false, meaning the Tiles are neither placed in one row nor one column.
                if (XDirection == false && YDirection == false)
                    return false;

                //If this is the first turn of the game
                if (IsFirstTurn)
                {
                    //Check if a Tile is placed on the middle TilePlace
                    bool tileOn77 = false;
                    if (newtiles.Length < 2)
                    {
                        //Cannot use only one Tile in the first turn
                        Console.WriteLine("Less than two tiles placed in first turn.");
                        return false;
                    }
                    //For every TilePlace, if any are placed in the middle then tileOn77 is true
                    foreach (TilePlace tp in newtiles)
                    {
                        if (tp.X == 7 && tp.Y == 7)
                        {
                            tileOn77 = true;
                            break;
                        }
                    }

                    if (!tileOn77)
                    {
                        Console.WriteLine("No tile found on 7,7.");
                        return false;
                    }

                }
                else
                {
                    //Indicates if the TilePlace has another TilePlace adjacent to it.
                    bool hasNeighbour = false;
                    foreach (TilePlace tp in newtiles)
                    {
                        //checking the right neighbour
                        if (tp.X < 14 && boardData[tp.X + 1, tp.Y].isLocked)
                        {
                            hasNeighbour = true;
                            break;
                        }
                        //checking the left neighbour
                        if (tp.X > 0 && boardData[tp.X - 1, tp.Y].isLocked)
                        {
                            hasNeighbour = true;
                            break;
                        }
                        //checking the bottom neighbour

                        if (tp.Y < 14 && boardData[tp.X, tp.Y + 1].isLocked)
                        {
                            hasNeighbour = true;
                            break;
                        }
                        //checking the top neighbour
                        if (tp.Y > 0 && boardData[tp.X, tp.Y - 1].isLocked)
                        {
                            hasNeighbour = true;
                            break;
                        }
                    }

                    //If no adjacent Tile found
                    if (!hasNeighbour)
                    {
                        Console.WriteLine("No existing neighbour found");
                        return false;
                    }
                }

                //This code is only reached if the tiles are in a single row or column
                //Checks that either there are no gaps in the new tiles, or the gaps have existing tiles.
                Console.WriteLine("Checking for gaps");
                if (XDirection) //all x coordinates are same
                {               //traversing y coordinates

                    //Check if it has a neighbouring tile
                    for (int i = 1; i < newtiles.Length; ++i)
                    {

                        if (newtiles[i].Y == (newtiles[i - 1].Y + 1))
                        {
                            //they're next to each other
                        }

                        else
                        {
                            //If there is a gap in the given range of tile coordinates
                            if (!RangeHasTiles(newtiles[i - 1].X, newtiles[i - 1].Y, newtiles[i].Y - newtiles[i - 1].Y - 1, true))
                            {
                                Console.WriteLine("X constant, gap in y direction detected");
                                return false;
                            }
                        }
                    }
                }
                else if (YDirection)
                {//y direction
                    for (int i = 1; i < newtiles.Length; ++i)
                    {
                        if (newtiles[i].X == newtiles[i - 1].X + 1)
                        {
                            //they're next to each other
                        }

                        else
                        {
                            //If there is a gap in the given range of tile coordinates
                            if (!RangeHasTiles(newtiles[i - 1].X, newtiles[i - 1].Y, newtiles[i].X - newtiles[i - 1].X - 1, false))
                            {
                                Console.WriteLine("Y constant, gap in x direction detected");
                                return false;
                            }
                        }
                    }
                }
            }
            //Tiles have a legal position.
            return true;
        }

        //This method checks if there are existing old tiles in the given range on the board
        //Parameters:
        //x coordinate of the newly placed tile for starting position
        //y cordinate of the newly placed tile for starting position
        //No of tile places to be checked for old tiles (not including the x, y provided)
        //Bool set to true if X coordinate is constant for all these new tiles
        private bool RangeHasTiles(int x, int y, int noOfTiles, bool isXConstant)
        {
            if (isXConstant)
            {
                for (int i = 1; i <= noOfTiles; ++i)
                {
                    if (boardData[x, y + i].Tile == null)
                        return false;
                }
            }

            else
            {
                for (int i = 1; i <= noOfTiles; ++i)
                {
                    if (boardData[x + i, y].Tile == null)
                        return false;
                }
            }
            return true;
        }

        // This function applies rules regarding validity of words and calculates scores. 
        // It searches the word in dictionary and it searches the neighboring word that is being extended in dictionary
        // If both words are found, the score is calculated based on word and letter multipliers
        // Collects score from extending the existing words
        // returns -1 if word is invalid or score value if word is valid
        // If player skips turn, no score, 0 returned
        public int CheckWordForCorrectnessAndScore(bool isCustomDB)
        {
            Console.WriteLine("Check For Word Correctness And Score: finding words:");
            //need the words
            List<TilePlace[]> newlyFormedWords = GetNewlyFormedWords();
            List<string> newlyFormedWordStrings = new List<string>();

            //if no word is found award 0 score
            if (newlyFormedWords.Count == 0)
                return 0;

            foreach (TilePlace[] newword in newlyFormedWords)
            {
                string newWordString = TilePlace.TilePlaceArrayToString(newword);
                Console.WriteLine("possible word: " + newWordString);
                newlyFormedWordStrings.Add(newWordString);
            }

            
            string query = "select * from dictionary where Word in (";
            //Creating SQL query
            foreach (string word in newlyFormedWordStrings)
            {
                query += "'" + word + "',";
            }
            query = query.Remove(query.Length - 1);
            query += ")";

            //If user has chosen the original dictionary in the game setup form then only allow non-custom words.
            if (!isCustomDB)
            {
                query += " AND IsCustom = False";
            }
            Console.WriteLine(query);
            OleDbConnection Conn = new OleDbConnection(DatabaseConnection.connString);
            Conn.Open();
            OleDbCommand Cmd = new OleDbCommand
            {
                Connection = Conn,
                CommandText = query
            };
            OleDbDataReader reader = Cmd.ExecuteReader();//Runs the query & allows results to be read.

            while (reader.Read())
            {
                Console.WriteLine("Word found: " + reader["Word"].ToString());
                //Remove word found from list
                newlyFormedWordStrings.Remove(reader["Word"].ToString());
            }

            Conn.Close();

            //If newlyFormedWordStrings is not empty then one or more words are invalid.
            if (newlyFormedWordStrings.Count > 0)
            {
                //one or more words were not found in dictionary, -1 returned as error code
                foreach (string word in newlyFormedWordStrings)
                {
                    Console.WriteLine("Incorrect word: " + word);
                }
                return -1;
            }


            //If the list of newlyformedstring is empty at this poistion, all the words are correct. move to scoring.
            //Score for words played is calculated.
            //for each word, get the total sum based on rules.
            //each tile has its own score
            //each tileplace has its own multiplier of two types (word multiplier, letter multiplier)
            //keep track of total score, keep adding to total score for the turn after each word score is calcualted.
            int totalScore = 0;
            //50 point bonus for using all 7 tiles in tray and word played is correct
            if (FindTurnTiles().Length == 7)
            {
                totalScore += 50;
            }

            //Calculating score for every word
            foreach (TilePlace[] newWord in newlyFormedWords)
            {
                int wordScore = 0;
                int wordMultiplier = 1;

                foreach (TilePlace tp in newWord)
                {
                    wordScore += tp.Tile.ScoreValue * tp.LetterMultiplier;
                    wordMultiplier *= tp.WordMultiplier;
                }

                wordScore *= wordMultiplier;

                //add this to total score and print score for the word..
                totalScore += wordScore;
                //if user has played 7 tiles in one turn


                Console.WriteLine("Score for " + TilePlace.TilePlaceArrayToString(newWord) + " is " + wordScore);
            }

            //Change the letter multipliers for TilePlaces where tiles have been placed to 1 as the multiplier has been used.
            for (int i = 0; i < newlyFormedWords.Count; i++)
            {
                for (int j = 0; j < newlyFormedWords[i].Length; j++)
                {
                    newlyFormedWords[i][j].WordMultiplier = 1;
                    newlyFormedWords[i][j].LetterMultiplier = 1;
                }
            }
            return totalScore;
        }

        //This method finds all words created in one turn
        //Returns a list of TilePlace arrays that contains all newly formed words.
        public List<TilePlace[]> GetNewlyFormedWords()
        {
            bool xDirection = IsInXDirection();
            bool wordInDirectionOfPlayAdded = false;
            List<TilePlace[]> newWords = new List<TilePlace[]>();
            TilePlace[] newTiles = FindTurnTiles();

            for (int i = 0; i < newTiles.Length; ++i)
            {
                TilePlace current = newTiles[i];

                //check x direction where y is constant 
                int startX = current.X;
                int endX = current.X;

                //get the direction of the word, in that direction, detect new word only once to avoid repetition of the word
                while (startX > 0 && boardData[startX - 1, current.Y].Tile != null)
                {
                    startX--;
                }

                while (endX < BOARDWIDTH - 1 && boardData[endX + 1, current.Y].Tile != null)
                {
                    endX++;
                }


                Console.WriteLine("CurrentX: " + current.X + "StartX: " + startX + " - endX:" + endX);

                //Copy from StartX till EndX as one word. 
                if (startX < endX)//connecting letters found, add this as a new word
                {
                    TilePlace[] newWord = new TilePlace[endX - startX + 1];
                    for (int j = startX; j <= endX; ++j)
                    {
                        newWord[j - startX] = boardData[j, current.Y];
                    }

                    //Direction of play is whether tiles are placed horizontally or vertically (x or y direction)
                    //This code avoids adding a word multiple times if the tiles are in the same direction of play
                    if (!xDirection)
                    {
                        if (!wordInDirectionOfPlayAdded)
                        {
                            newWords.Add(newWord);

                            wordInDirectionOfPlayAdded = true;
                        }
                    }
                    else
                        newWords.Add(newWord);
                }

                //y direction, x is constant:
                int startY = current.Y;
                int endY = current.Y;

                while (startY > 0 && boardData[current.X, startY - 1].Tile != null)
                {
                    startY--;
                }

                while (endY < BOARDWIDTH - 1 && boardData[current.X, endY + 1].Tile != null)
                {
                    endY++;
                }

                //Copy from StartX till EndX as one word. 
                if (startY < endY)//connecting letters found, add this as a new word
                {
                    TilePlace[] newWord = new TilePlace[endY - startY + 1];

                    for (int j = startY; j <= endY; ++j)
                    {
                        newWord[j - startY] = boardData[current.X, j];
                    }

                    //Direction of play is whether tiles are placed horizontally or vertically (x or y direction)
                    //This code avoids adding a word multiple times if the tiles are in the same direction of play
                    if (xDirection)
                    {
                        if (!wordInDirectionOfPlayAdded)
                        {
                            newWords.Add(newWord);

                            wordInDirectionOfPlayAdded = true;
                        }
                    }
                    else
                        newWords.Add(newWord);
                }
            }
            return newWords;
        }
    }
}

using System;

namespace Scrabble
{
    //This class stores all variables required to save and load a game.
    [Serializable]
    public class GameState
    {
        public int noOfPlayers;
        public int nextTileIndex;
        public int secondsPerTurn;
        public int turnPlayer;
        public Tile[] loadedDBTiles;
        public bool isCustomDB;
        public string gameInfoText;
        public DateTime startTime;
        public BoardState scrabbleBoard;
        public PlayerStates players;
        
        public GameState(GameForm.GameFormState game) 
        {
            noOfPlayers = game.noOfPlayers;
            secondsPerTurn = game.secondsPerTurn;
            nextTileIndex = game.nextTileIndex;
            turnPlayer = game.turnPlayer;
            loadedDBTiles = game.loadedDBTiles;
            isCustomDB = game.isCustomDB;
            gameInfoText = game.gameInfoText;
            startTime = game.startTime;

            //Creating a BoardState from the Board of the game passed.
            scrabbleBoard = new BoardState(game.scrabbleBoard);
            //Creating PlayerStates from the players of the game passed.
            players = new PlayerStates(game.players);
        }
    }

    //TilePlaceSerializable is the same as the TilePlace class however only fields that must be serialized are included.
    //This was created because TilePlace inherits from Button, which cannot be serialized.
    [Serializable]
    public class TilePlaceSerializable
    {
        public int X;
        public int Y;
        public Tile Tile;

        public TilePlaceSerializable(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    //Contains the state of the board when it is saved with the values of all 15x15 TilePlaces 
    [Serializable]
    public class BoardState
    {
        public static int BOARDWIDTH = 15;
        public TilePlaceSerializable[,] tpSerializables = new TilePlaceSerializable[BOARDWIDTH, BOARDWIDTH];
        public bool IsFirstTurn { get; set; }

        public BoardState(Board scrabbleBoard)
        {
            IsFirstTurn = scrabbleBoard.IsFirstTurn;
            for (int x = 0; x < BOARDWIDTH; x++)
            {
                for (int y = 0; y < BOARDWIDTH; y++)
                {
                    tpSerializables[x, y] = new TilePlaceSerializable(x, y);

                }
            }
            for (int y = 0; y < BOARDWIDTH; y++)
            {
                for (int x = 0; x < BOARDWIDTH; x++)
                {
                    if (scrabbleBoard.boardData[x, y].isLocked && scrabbleBoard.boardData[x, y].Tile != null)
                    {
                        tpSerializables[x, y].Tile = scrabbleBoard.boardData[x, y].Tile;
                    }
                }
            }
        }
    }
    //PlayerSerializable is the same as the Player class however only fields that must be serialized are included.
    //This was created because Player inherits from Panel, which cannot be serialized.
    [Serializable]
    public class PlayerSerializable
    {
        public int score;
        public int skipcount;
        public string name;
        public TilePlaceSerializable[] tilePlacesSerializable = new TilePlaceSerializable[7];
        public PlayerSerializable(int Score, int Skipcount, string Name)
        {
            score = Score;
            skipcount = Skipcount;
            name = Name;
            for (int i = 0; i < tilePlacesSerializable.Length; i++)
            {
                tilePlacesSerializable[i] = new TilePlaceSerializable(i, 0);
            }
        }
    }

    //Includes an array of serializable players that are stored in a GameState object.
    [Serializable]
    public class PlayerStates
    {
        public PlayerSerializable[] playersSerializable = new PlayerSerializable[4];
        public PlayerStates(Player[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                playersSerializable[i] = new PlayerSerializable(0, 0, "");
            }

            for (int i = 0; i < players.Length; i++)
            {
                playersSerializable[i].score = players[i].score;
                playersSerializable[i].skipcount = players[i].skipCount;
                playersSerializable[i].name = players[i].playerName;
                for (int x = 0; x < players[i].tilePlaces.Length; x++)
                {
                    playersSerializable[i].tilePlacesSerializable[x].Tile = players[i].tilePlaces[x].Tile;
                }
            }
        }
    }

}

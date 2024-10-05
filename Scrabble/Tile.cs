using System;

namespace Scrabble
{
    //Tile objects represent Scrabble tiles with different score values.
    //The tile with label 'X' would have a score value of 8 while the tile with label 'E' would have a score value of 1.
    //The Tile class is serializable as Tiles are a component of each game that must be stored in order to load the game.
    [Serializable]
    public class Tile
    {
        //Each tile has an ID which is determined by an AutoNumber in a Microsoft Access database.
        public int ID { get; }
        //Determines the score provided by playing this tile.
        public int ScoreValue { get; }
        //Determines the image the TilePlace with this Tile will have.
        public char Label { get; set; }
        //Initially set to null.
        //Blank tiles will not have a null AltLabel.
        public char AltLabel { get; set; }

        public Tile(int iD, int scoreValue, char label)
        {
            ID = iD;
            ScoreValue = scoreValue;
            Label = label;
            AltLabel = (char)0;
        }


        //Shuffles tiles in a Tile array.
        //Swaps the position of the Nth tile with a different randomly chosen tile 100 times.
        public static void ShuffleTiles(Tile[] tiles)
        {
            //Guid.NewGuid().GetHashCode() is used to increase how random the numbers generated are.
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < tiles.Length; i++)
            {
                int a = i;
                int b = rand.Next(0, tiles.Length);
                Tile table = tiles[a];
                tiles[a] = tiles[b];
                tiles[b] = table;
            }
        }
    }
}
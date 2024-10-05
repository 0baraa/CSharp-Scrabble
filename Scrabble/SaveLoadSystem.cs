using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Scrabble
{
    //Contains methods for loading and saving a game using serialization and deserialization.
    class SaveLoadSystem
    {
        //Creates a save file with a file name provided by the user.
        public static void SaveGameState(GameForm.GameFormState gameForm, string fileName)
        {
            string path = @"SavedGames\" + fileName + ".scrabble";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            GameState gameData = new GameState(gameForm);
            formatter.Serialize(stream, gameData);
            stream.Close();
        }

        //Loads the game chosen by the user if the file exists.
        public static GameState LoadGame(string fileName)
        {
            string path = @"SavedGames\" + fileName + ".scrabble";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                GameState game = (GameState)formatter.Deserialize(stream);
                stream.Close();
                return game;
            }
            else
            {
                Console.WriteLine("File not found in " + path);
                return null;
            }
        }
    }
}

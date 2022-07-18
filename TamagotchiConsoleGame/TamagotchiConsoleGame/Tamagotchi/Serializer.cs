using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TamagotchiConsoleGame.Tamagotchi
{
    class Serializer
    {
        private const string CHARACTER_BIN = "Character.bin";
        private const string SETTINGS_BIN = "Settings.bin";

        private IFormatter formatter;
        public Serializer()
        {
            formatter = new BinaryFormatter();
        }

        public void Serialize(Character character)
        {
            Stream stream = new FileStream(CHARACTER_BIN, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, character);
            stream.Close();
        }

        public Character Deserialize()
        {
            Stream stream = new FileStream(CHARACTER_BIN, FileMode.Open, FileAccess.Read, FileShare.Read);
            Character character = (Character)formatter.Deserialize(stream);
            stream.Close();

            return character;
        }

        public bool Exists()
        {
            return File.Exists(CHARACTER_BIN);
        }

        public void Delete()
        {
            if (Exists())
                File.Delete(CHARACTER_BIN);
        }


        public void SerializeSettings(GameSettings settings)
        {
            Stream stream = new FileStream(SETTINGS_BIN, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, settings);
            stream.Close();
        }

        public GameSettings DeserializeSettings()
        {
            Stream stream = new FileStream(SETTINGS_BIN, FileMode.Open, FileAccess.Read, FileShare.Read);
            GameSettings settings = (GameSettings)formatter.Deserialize(stream);
            stream.Close();

            return settings;
        }

        public bool SettingsExists()
        {
            return File.Exists(SETTINGS_BIN);
        }

    }
}

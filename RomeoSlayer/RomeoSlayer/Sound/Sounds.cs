using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace RomeoSlayer.Sound
{
    class Sounds
    {
        public static Song menuMusic { get { return Content.Load<Song>(Directories.MUSIC_PATH + Directories.MENU_MUSIC); } }
        public static Song gameMusic { get { return Content.Load<Song>(Directories.MUSIC_PATH + Directories.MENU_MUSIC); } }
        public static Song crowd { get { return Content.Load<Song>(Directories.MUSIC_PATH + "Applause"); } }

        public static SoundEffect flamethrowerOn;
        public static SoundEffect trapDoorOpen;

        public static SoundEffect grunt;
        public static SoundEffect scream;

        private static ContentManager Content;

        public static void LoadSounds(ContentManager content)
        {
            Content = content;

            // Sounds
            flamethrowerOn = content.Load<SoundEffect>(Directories.SOUND_EFFECTS_PATH + Directories.LIGHTER_SOUND);
            trapDoorOpen = content.Load<SoundEffect>(Directories.SOUND_EFFECTS_PATH + Directories.DOOR_OPEN_SOUND);

            grunt = content.Load<SoundEffect>(Directories.SOUND_EFFECTS_PATH + Directories.GRUNT);
            scream = content.Load<SoundEffect>(Directories.SOUND_EFFECTS_PATH + Directories.SCREAM);
        }
    }

    class Directories
    {
        // Paths
        public const string MUSIC_PATH = "Music\\";
        public const string SOUND_EFFECTS_PATH = "SoundEffects\\";

        // Music Files
        public const string MENU_MUSIC = "MenuMusic";
        
        public const string LIGHTER_SOUND = "Lighter";
        public const string DOOR_OPEN_SOUND = "DoorOpen";

        public const string GRUNT = "grunt";
        public const string SCREAM = "scream";

    }
}

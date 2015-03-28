using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace RomeoSlayer.Sound
{
    class SoundManager
    {
        public static float volume;

        public SoundManager()
        {
            volume = 1f;
        }

        public void PlaySound(SoundEffect sound)
        {
            sound.Play(volume, 0, 0);
        }

        public void PlayMusic(Song song)
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(song);
        }

        public void StopMusic()
        {
            MediaPlayer.Stop();
        }
    }
}

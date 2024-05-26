using System;
using JetBrains.Annotations;
using osu.Framework.Audio;
using osu.Framework.Graphics.Audio;
using osu.Framework.Logging;

namespace GentrysQuest.Game.Audio
{
    public static class AudioManager
    {
        private static readonly Volume gameVolume = new Volume(0.5);
        private static readonly Volume musicVolume = new Volume(0.25);
        private static readonly Volume soundVolume = new Volume(0.25);

        [CanBeNull]
        private static DrawableTrack gameMusic;

        private const int FADE_TIME = 5000;

        public static void ChangeMusic(DrawableTrack track)
        {
            track.Looping = true;

            Action modifyTrack = () =>
            {
                gameMusic = track;
                gameMusic.Start();
                gameMusic.VolumeTo(musicVolume.Amount, FADE_TIME);
            };

            Logger.Log($"music volume is {musicVolume.Amount}");

            if (gameMusic == null)
            {
                gameMusic = track;
                gameMusic.VolumeTo(0);
                gameMusic.Start();
                gameMusic.VolumeTo(musicVolume.Amount, FADE_TIME);
            }
            else
            {
                gameMusic.VolumeTo(0, FADE_TIME).Then().Finally(_ => modifyTrack());
            }
        }

        public static void PlaySound(DrawableSample sample)
        {
            Logger.Log("playing sound");
            sample.VolumeTo(soundVolume.Amount);
            sample.Play();
        }

        public static void ChangeMusicVolume(int percent) { musicVolume.Amount = (double)percent / 100 / gameVolume.Amount; }

        private static void adjustMusicVolume() { ChangeMusicVolume((int)musicVolume.Amount * 100); }

        public static void ChangeSoundVolume(int percent) { soundVolume.Amount = (double)percent / 100 / gameVolume.Amount; }

        private static void adjustSoundVolume() { ChangeSoundVolume((int)soundVolume.Amount * 100); }

        public static void ChangeGameVolume(int percent)
        {
            gameVolume.Amount = (double)percent / 100;
            adjustMusicVolume();
            adjustSoundVolume();
        }
    }
}

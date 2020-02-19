using System;
using System.Threading;
using NAudio.Wave;

namespace TheSyndicate.SoundEffects
{
    class Sounds
    {
        internal static AudioFileReader audioFile;
        internal static WaveOutEvent outputDevice;

        internal static void PlaySound(string musicFile, int milliSeconds = 0)
        {
            // Takes in an audio file & controls the wait time to start
            audioFile = new AudioFileReader(musicFile);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
            Thread.Sleep(milliSeconds);
        }

        internal static void DisposeAudio()
        {
            // Ends audio playback
            audioFile.Dispose();
            outputDevice.Dispose();
        }
    }
}

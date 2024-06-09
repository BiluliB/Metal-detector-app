using Magnetify.Common;
using Plugin.Maui.Audio;
using System.Diagnostics;

namespace Magnetify.Services
{
    /// <summary>
    /// Service for handling sound.
    /// </summary>
    public class SoundService : BaseNotifyHandler, ISoundService
    {
        /// <summary>
        /// Global flag to disable sound.
        /// Other code using this service should disable sound if this is true.
        /// </summary>
        public bool DisableSound { get; private set; } = false;

        /// <summary>
        /// The audio player for the beep sound.
        /// </summary>
        public IAudioPlayer BeepPlayer { get; private set; } = null!;

        /// <summary>
        /// The stream for the beep sound file.
        /// </summary>
        public Stream? BeepStream { get; private set; } = null!;

        public SoundService()
        {
            Debug.WriteLine("Sound service created");
        }

        /// <summary>
        /// Initialize the sound service.
        /// loads the beep sound file.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            BeepStream = await FileSystem.OpenAppPackageFileAsync("beep.mp3");
            BeepPlayer = AudioManager.Current.CreatePlayer(BeepStream);
            Debug.WriteLine("beep.mp3 loaded!");
        }

        /// <summary>
        /// Disable sound.
        /// Does nothing, only sets the DisableSound flag.
        /// </summary>
        public void Disable()
        {
            DisableSound = true;
            Debug.WriteLine("Sound disabled");
        }

        /// <summary>
        /// Enable sound.
        /// Does nothing, only sets the DisableSound flag.
        /// </summary>
        public void Enable()
        {
            DisableSound = false;
            Debug.WriteLine("Sound enabled");
        }

    }
}

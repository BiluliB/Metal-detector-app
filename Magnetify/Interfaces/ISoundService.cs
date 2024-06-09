using Magnetify.Interfaces;
using Plugin.Maui.Audio;

namespace Magnetify.Services
{
    public interface ISoundService : IBaseNotifyHandler
    {
        bool DisableSound { get; }
        public IAudioPlayer BeepPlayer { get; }
        public Stream? BeepStream { get; }

        void Disable();
        void Enable();
        Task InitializeAsync();
    }
}
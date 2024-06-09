namespace Magnetify.Interfaces
{
    public interface IVibrationService : IBaseNotifyHandler
    {
        bool DisableVibration { get; }

        void Disable();
        void Enable();
        void Vibrate(TimeSpan duration);
    }
}
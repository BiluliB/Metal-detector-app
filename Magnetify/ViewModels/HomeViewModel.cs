using Magnetify.Common;
using Magnetify.Interfaces;
using Magnetify.Services;
using PropertyChanged;
using System.ComponentModel;
using System.Diagnostics;

namespace Magnetify.ViewModels
{
    /// <summary>
    /// View model for the home page.
    /// </summary>
    public class HomeViewModel : BaseNotifyHandler
    {
        /// <summary>
        /// local link to the magnetometer service
        /// </summary>
        private readonly IMagnetometerService _magnetometerService;

        /// <summary>
        /// local link to the sound service
        /// </summary>
        private readonly ISoundService _soundService;

        /// <summary>
        /// local link to the vibration service
        /// </summary>
        private readonly IVibrationService _vibrationService;

        /// <summary>
        /// local cancellation token source
        /// </summary>
        private CancellationTokenSource? _cts;

        /// <summary>
        /// Tracker for the playing state of the beep sound
        /// </summary>
        public bool IsPlaying { get; set; } = false;

        /// <summary>
        /// The current value of the magnetometer
        /// </summary>
        public double CurrentValue { get; set; } = 0.0;

        /// <summary>
        /// Current label for the magnetometer value, displaying the average value
        /// Updated when the CurrentValue changes, formatted to 2 decimal places
        /// </summary>
        [DependsOn(nameof(CurrentValue))]
        public string Label => $"{_magnetometerService.CurrentAverage:F2} µT";

        /// <summary>
        /// Current width of the bar, based on the CurrentValue
        /// Updated when the CurrentValue changes, scaled to 300
        /// </summary>
        [DependsOn(nameof(CurrentValue))]
        public int BarWidth => (int)(CurrentValue * 300);

        /// <summary>
        /// Current color of the bar, based on the CurrentValue
        /// linearly interpolated between green and red
        /// </summary>
        [DependsOn(nameof(CurrentValue))]
        public Color BarColor => GetColorFromGradient(CurrentValue);


        /// <summary>
        /// Initializes a new instance of the <see cref="HomeViewModel"/> class.
        /// </summary>
        /// <param name="magnetometerService">Injected magnetometer service</param>
        /// <param name="soundService">Injected sound service</param>
        /// <param name="vibrationService">Injected vibration service</param>
        public HomeViewModel(IMagnetometerService magnetometerService, ISoundService soundService, IVibrationService vibrationService)
        {
            _magnetometerService = magnetometerService;
            _soundService = soundService;
            _vibrationService = vibrationService;

            _magnetometerService.PropertyChanged += OnMagnetometerServicePropertyChanged;
            _soundService.PropertyChanged += OnSoundServicePropertyChanged;
            Debug.WriteLine("Home view model created");
        }

        /// <summary>
        /// Listener for the sound service property changed event.
        /// Helps to disable sound if the DisableSound flag is set.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void OnSoundServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISoundService.DisableSound))
            {
                if (_soundService.DisableSound && IsPlaying)
                {
                    Stop();
                }
                else if (!_soundService.DisableSound)
                {
                    _cts?.Cancel();
                    Debug.WriteLine("Sound enabled, checking and acting");
                    CheckAndAct();
                }
            }
        }

        /// <summary>
        /// Listener for the magnetometer service property changed event.
        /// Core listener, runs the check and act.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void OnMagnetometerServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMagnetometerService.CurrentAverage))
            {
                CurrentValue = _magnetometerService.NormalizedValue;
                CheckAndAct();
            }
        }

        /// <summary>
        /// Check the magnetometer value and act accordingly.
        /// Plays a sound and vibrates if the value is above the detection threshold.
        /// </summary>
        public async void CheckAndAct()
        {
            _cts = new CancellationTokenSource();
            if (_magnetometerService.CurrentAverage > _magnetometerService.DetectionThreshold && !IsPlaying)
            {
                IsPlaying = true;
                Debug.WriteLine("Playing sound and vibrating");
                await PlayAndVibrateAsync(_cts.Token);
            }
            else if (_magnetometerService.CurrentAverage <= _magnetometerService.DetectionThreshold)
            {
                IsPlaying = false;
                _cts?.Cancel();
            }
        }

        /// <summary>
        /// Stop the sound and vibration.
        /// </summary>
        public void Stop()
        {
            IsPlaying = false;
            _cts?.Cancel();
            Debug.WriteLine("Stopping sound and vibration");
        }

        /// <summary>
        /// Task to play the sound and vibrate.
        /// Listens to the cancellation token to stop the task.
        /// The duration of the sound and vibration is based on the magnetometer value.
        /// </summary>
        /// <param name="cancellation">The cancellation token to listen to</param>
        /// <returns></returns>
        private async Task PlayAndVibrateAsync(CancellationToken cancellation)
        {
            while (IsPlaying && !cancellation.IsCancellationRequested)
            {
                var duration = Math.Max(50, 800 - (int)(Math.Pow(_magnetometerService.NormalizedValue, 2) * 550));
                // SPAM
                //Debug.WriteLine($"Normalized: {_magnetometerService.NormalizedValue}\nAverage: {_magnetometerService.CurrentAverage}\nPlaying beep for {duration} ms");
                _soundService.BeepPlayer.Seek(0);
                _soundService.BeepPlayer.Play();
                _vibrationService.Vibrate(TimeSpan.FromMilliseconds(duration / 2));
                await Task.Delay(duration);
            }
        }

        /// <summary>
        /// Get the color from a gradient based on the value.
        /// Interpolates between green and red.
        /// </summary>
        /// <param name="value">The value to get the color for</param>
        /// <returns>The color for the value</returns>
        Color GetColorFromGradient(double value)
        {
            return new Color((float)value, (float)(1 - value), 0);
        }
    }
}

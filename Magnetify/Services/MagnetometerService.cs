using Magnetify.Common;
using Magnetify.Interfaces;
using PropertyChanged;
using System.Diagnostics;

namespace Magnetify.Services
{
    /// <summary>
    /// Magnetometer service.
    /// Main Communication with the magnetometer sensor. 
    /// Also Tracks values and calculates averages.
    /// </summary>
    public class MagnetometerService : BaseNotifyHandler, IMagnetometerService
    {
        /// <summary>
        /// local link to the alert service
        /// </summary>
        private readonly IAlertService _alertService;

        /// <summary>
        /// When this is true, the magnetometer is not supported on the device
        /// </summary>
        public bool Disabled { get; private set; } = false;

        /// <summary>
        /// True while the magnetometer is running
        /// </summary>
        public bool Running { get; private set; } = false;

        /// <summary>
        /// The detection threshold for the magnetometer
        /// </summary>
        public double DetectionThreshold { get; set; } = 90.0;

        /// <summary>
        /// The minimum value for the magnetometer
        /// </summary>
        public double MinMagnetometerValue { get; set; } = 50.0;

        /// <summary>
        /// The maximum value for the magnetometer
        /// </summary>
        public double MaxMagnetometerValue { get; set; } = 200.0;

        /// <summary>
        /// The Queue of values for the magnetometer
        /// </summary>
        public Queue<double> Values { get; private set; } = new Queue<double>();

        /// <summary>
        /// The current value of the magnetometer
        /// </summary>
        public double CurrentValue { get; private set; } = 0.0;

        /// <summary>
        /// The current value of the magnetometer normalized between 0 and 1
        /// </summary>
        public double NormalizedValue { get; private set; } = 0.0;

        /// <summary>
        /// The current average of the magnetometer values
        /// </summary>
        public double CurrentAverage { get; private set; } = 0.0;

        /// <summary>
        /// The current max of the magnetometer values
        /// </summary>
        public double CurrentMax { get; private set; } = 0.0;

        /// <summary>
        /// The current min of the magnetometer values
        /// </summary>
        public double CurrentMin { get; private set; } = 0.0;

        public MagnetometerService(IAlertService alertService)
        {
            _alertService = alertService;
        }

        /// <summary>
        /// Initialize the magnetometer service
        /// Register the ReadingChanged event, alert if not supported
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {

           if (Magnetometer.Default.IsSupported)
            {
                Magnetometer.Default.ReadingChanged += OnChange;
                Debug.WriteLine("Magnetometer supported");
            }
            else
            {
                Debug.WriteLine("Magnetometer not supported");
                await _alertService.ShowAlertAsync("Error", "Magnetometer not supported", "OK");
                Disabled = true;
            }
        }

        /// <summary>
        /// Event handler for the magnetometer ReadingChanged event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The data of the magnetometer</param>
        private void OnChange(object? sender, MagnetometerChangedEventArgs e)
        {
            var data = e.Reading;
            var strength = Math.Sqrt(Math.Pow(data.MagneticField.X, 2) +
                                     Math.Pow(data.MagneticField.Y, 2) +
                                     Math.Pow(data.MagneticField.Z, 2));

            AddValue(strength);
        }

        /// <summary>
        /// Start the magnetometer
        /// </summary>
        public void Start()
        {
            if (Disabled)
            {
                return;
            }
            try { Magnetometer.Default.Start(SensorSpeed.UI); }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error starting magnetometer: {ex.Message}");
                return;
            }
            Running = true;
            Debug.WriteLine("Magnetometer started");
        }

        /// <summary>
        /// Stop the magnetometer
        /// </summary>
        public void Stop()
        {
            if (Disabled)
            {
                return;
            }
            try { Magnetometer.Default.Stop(); }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error stopping magnetometer: {ex.Message}");
                return;
            }
            Running = false;
            Debug.WriteLine("Magnetometer stopped");
        }

        /// <summary>
        /// Add a value to the magnetometer values
        /// Also calculates the average, max, min, and normalized value
        /// </summary>
        /// <param name="value">The value to add</param>
        public void AddValue(double value)
        {
            CurrentValue = value;
            Values.Enqueue(value);
            if (Values.Count > 20)
            {
                Values.Dequeue();
            }

            NormalizedValue = Math.Min(Math.Max((CurrentAverage - MinMagnetometerValue) / (MaxMagnetometerValue - MinMagnetometerValue), 0.0), 1.0);
            CurrentAverage = Values.Average(); 
            CurrentMax = Values.Max();
            CurrentMin = Values.Min();

            OnPropertyChanged(nameof(CurrentValue));
            // SPAM
            //Debug.WriteLine($"Magnetometer value: {value}");
        }

    }
}

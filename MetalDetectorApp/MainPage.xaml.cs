using Plugin.Maui.Audio;

namespace MetalDetectorApp
{
    public partial class MainPage : ContentPage
    {
        const double MaxMagnetometerValue = 200.0;
        const double DetectionThreshold = 50.0;
        const double MinMagnetometerValue = 50.0;

        Queue<double> _recentReadings = new Queue<double>();
        const int ReadingCount = 20;

        IAudioPlayer player;
        bool isPlaying = false;
        double beepInterval = 1000;

        public MainPage()
        {
            InitializeComponent();
            StartMagnetometer();
            InitializeAudioPlayer();
        }

        void StartMagnetometer()
        {
            if (Magnetometer.Default.IsSupported)
            {
                Magnetometer.Default.ReadingChanged += Magnetometer_ReadingChanged;
                Magnetometer.Default.Start(SensorSpeed.UI);
            }
            else
            {
                DisplayAlert("Error", "Magnetometer not supported on this device", "OK");
            }
        }

        async void InitializeAudioPlayer()
        {
            var audioManager = AudioManager.Current;
            var beepStream = await FileSystem.OpenAppPackageFileAsync("beep.mp3");
            player = audioManager.CreatePlayer(beepStream);
        }

        async void PlayBeepAndVibrateContinuously()
        {
            while (isPlaying)
            {
                if (App.IsAppInBackground)
                {
                    isPlaying = false;
                    break;
                }

                player.Seek(0);
                player.Play();
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(beepInterval / 2));
                await Task.Delay((int)beepInterval);
            }
        }

        void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
        {
            if (App.IsAppInBackground)
            {
                return;
            }

            var data = e.Reading;
            var strength = Math.Sqrt(Math.Pow(data.MagneticField.X, 2) +
                                     Math.Pow(data.MagneticField.Y, 2) +
                                     Math.Pow(data.MagneticField.Z, 2));

            // Update the readings queue
            _recentReadings.Enqueue(strength);
            if (_recentReadings.Count > ReadingCount)
            {
                _recentReadings.Dequeue();
            }

            // Calculate the average reading
            var averageStrength = _recentReadings.Average();

            // Update the Label with the current magnetic field strength
            ValueLabel.Text = "Magnetic Field:";
            NumericValueLabel.Text = $"{averageStrength:F2} µT";

            // Normalize the magnetic field strength for the progress bar (0 to 1)
            var normalizedStrength = Math.Min(Math.Max((averageStrength - MinMagnetometerValue) / (MaxMagnetometerValue - MinMagnetometerValue), 0.0), 1.0);

            // Set the width of the BoxView
            MagnetometerBar.WidthRequest = normalizedStrength * 300; // Example max width of 300

            // Set the color of the BoxView from green to red
            MagnetometerBar.Color = GetColorFromGradient(normalizedStrength);

            // Adjust the beep interval based on the magnetic field strength
            beepInterval = Math.Max(50, 500 - (int)(normalizedStrength * 450)); // Faster peeping

            // Start or stop the beeping sound and vibration based on the threshold
            if (averageStrength > DetectionThreshold && !isPlaying)
            {
                isPlaying = true;
                PlayBeepAndVibrateContinuously();
            }
            else if (averageStrength <= DetectionThreshold && isPlaying)
            {
                isPlaying = false;
            }
        }

        Color GetColorFromGradient(double value)
        {
            // Linearly interpolate between green and red
            return new Color((float)value, (float)(1 - value), 0);
        }
    }
}

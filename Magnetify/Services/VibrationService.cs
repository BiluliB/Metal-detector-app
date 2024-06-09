using Magnetify.Common;
using Magnetify.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnetify.Services
{
    /// <summary>
    /// Vibration service.
    /// </summary>
    public class VibrationService : BaseNotifyHandler, IVibrationService
    {
        /// <summary>
        /// flag to disable vibration
        /// Other code using this service should disable vibration if this is true.
        /// Its the same as the sound service DisableSound flag (also set at the same time, so listening to one of both should be fine)
        /// </summary>
        public bool DisableVibration { get; private set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="VibrationService"/> class.
        /// Just for debugging purposes
        /// </summary>
        public VibrationService()
        {
            Debug.WriteLine("Vibration service created");
        }

        /// <summary>
        /// Disable vibration.
        /// Does nothing, only sets the DisableVibration flag.
        /// </summary>
        public void Disable()
        {
            DisableVibration = true;
            Debug.WriteLine("Vibration disabled");
        }

        /// <summary>
        /// Enable vibration.
        /// Does nothing, only sets the DisableVibration flag.
        /// </summary>
        public void Enable()
        {
            DisableVibration = false;
            Debug.WriteLine("Vibration enabled");
        }

        /// <summary>
        /// Vibrate the device for a specified duration.
        /// </summary>
        /// <param name="duration">The duration to vibrate the device.</param>
        public void Vibrate(TimeSpan duration)
        {
            if (!DisableVibration)
            {
                Vibration.Default.Vibrate(duration);
            }
        }

    }
}

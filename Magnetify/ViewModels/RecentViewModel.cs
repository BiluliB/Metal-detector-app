using Magnetify.Common;
using Magnetify.Data;
using Magnetify.Interfaces;
using System.Collections.ObjectModel;

namespace Magnetify.ViewModels
{
    public class RecentViewModel : BaseNotifyHandler
    {
        /// <summary>
        /// local link to the magnetometer service
        /// </summary>
        private readonly IMagnetometerService _magnetometerService;

        /// <summary>
        /// Relay the recent items from the magnetometer service
        /// </summary>
        public BetterCollection<RecentItem> Items => _magnetometerService.RecentItems;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentViewModel"/> class.
        /// </summary>
        /// <param name="magnetometerService"></param>
        public RecentViewModel(IMagnetometerService magnetometerService) {
            _magnetometerService = magnetometerService;
        }
    }
}

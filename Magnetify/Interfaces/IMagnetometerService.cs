
using Magnetify.Common;
using Magnetify.Data;
using System.Collections.ObjectModel;

namespace Magnetify.Interfaces
{
    public interface IMagnetometerService : IBaseNotifyHandler
    {
        double CurrentAverage { get; }
        double CurrentMax { get; }
        double CurrentMin { get; }
        double CurrentValue { get; }
        double DetectionThreshold { get; set; }
        double MaxMagnetometerValue { get; set; }
        double MinMagnetometerValue { get; set; }
        double NormalizedValue { get; }
        Queue<double> Values { get; }
        ObservableCollection<RecentItem> RecentItems { get; }
        public void Start();
        public void Stop();

        public Task InitializeAsync();

        void AddValue(double value);
    }
}
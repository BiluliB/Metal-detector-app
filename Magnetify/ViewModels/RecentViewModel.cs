using Magnetify.Common;
using Magnetify.Data;
using Magnetify.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnetify.ViewModels
{
    public class RecentViewModel : BaseNotifyHandler
    {

        private readonly IMagnetometerService _magnetometerService;

        public ObservableCollection<RecentItem> Items { get; set; } = new ObservableCollection<RecentItem>();

        private int _debounceCount = 0;

        public RecentViewModel(IMagnetometerService magnetometerService) {
            _magnetometerService = magnetometerService;
            _magnetometerService.PropertyChanged += HandleMagnetometerServicePropertyChanged;
        }

        private void HandleMagnetometerServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_magnetometerService.CurrentValue))
            {
                _debounceCount++;
                if (_debounceCount < 20)
                {
                    return;
                }

                Items.Insert(0, new RecentItem
                {
                    Name = $"{_magnetometerService.CurrentValue:F2} µT",
                    Description = $"{_magnetometerService.CurrentValue:F9}"
                });

                // Cleanup the list
                // TODO: improve by disabling the animations for this
                if (Items.Count > 20)
                {
                    Items.RemoveAt(Items.Count - 1);
                }
                _debounceCount = 0;
            }
        }
    }
}

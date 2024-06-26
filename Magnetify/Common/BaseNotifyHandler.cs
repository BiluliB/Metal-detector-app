﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Magnetify.Interfaces;

namespace Magnetify.Common
{
    /// <summary>
    /// Not yet fully sure how this will be done, since we use Fody.PropertyChanged
    /// For now, this works as is for each ViewModel as a base class
    /// 
    /// It is used to notify the UI that a property has changed and will be automatically applied to all public get/set Properties of the class
    /// </summary>
    public class BaseNotifyHandler : INotifyPropertyChanged, IBaseNotifyHandler
    {
#pragma warning disable CS8612 
#pragma warning disable CS8618 
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS8618 
#pragma warning restore CS8612 

        protected void SetProperty<T>(ref T backingStore, T value, string propertyName = "", Action onChanged = null!)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}

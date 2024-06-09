using System.ComponentModel;

namespace Magnetify.Interfaces
{
    public interface IBaseNotifyHandler
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}
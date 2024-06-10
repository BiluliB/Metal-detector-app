using Magnetify.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Magnetify.ViewModels
{
    public class AboutViewModel : BaseNotifyHandler
    {
        public ICommand OpenUrlCommand { get; }

        public AboutViewModel()
        {
            OpenUrlCommand = new Command<string>(async (url) => await OpenUrl(url));
        }

        private async Task OpenUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                return;
            }

            await Launcher.OpenAsync(uri);
        }
    }
}

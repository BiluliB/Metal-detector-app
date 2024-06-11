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
        /// <summary>
        /// Command to open a URL.
        /// </summary>
        public ICommand OpenUrlCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        public AboutViewModel()
        {
            OpenUrlCommand = new Command<string>(async (url) => await OpenUrl(url));
        }

        /// <summary>
        /// Opens a URL in the default browser.
        /// </summary>
        /// <param name="url">The URL to open.</param>
        /// <returns></returns>
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

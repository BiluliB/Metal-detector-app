using System.Diagnostics.CodeAnalysis;

namespace Magnetify
{
    public partial class App : Application
    {
        /// <summary>
        /// Global service provider.
        /// </summary>
        [AllowNull, NotNull]
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Global flag to determine if the app is on the home page.
        /// </summary>
        public static bool IsHome { get; set; } = false;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            ServiceProvider = serviceProvider;
            MainPage = ServiceProvider.GetRequiredService<AppShell>();
        }
    }
}

namespace Magnetify
{
    public partial class App : Application
    {
        /// <summary>
        /// Global service provider.
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            ServiceProvider = serviceProvider;
            MainPage = ServiceProvider.GetRequiredService<AppShell>();
        }
    }
}

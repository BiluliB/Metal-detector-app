namespace MetalDetectorApp
{
    public partial class App : Application
    {
        public static bool IsAppInBackground = false;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            IsAppInBackground = true;
        }

        protected override void OnResume()
        {
            base.OnResume();
            IsAppInBackground = false;
        }
    }
}

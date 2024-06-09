using Magnetify.Interfaces;
using Magnetify.Services;
using Magnetify.ViewModels;
using Magnetify.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using System.Diagnostics;

namespace Magnetify
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<IVibrationService, VibrationService>();
            builder.Services.AddSingleton<IMagnetometerService, MagnetometerService>();
            builder.Services.AddSingleton<ISoundService, SoundService>();

            builder.Services.AddSingleton<AppShell>();

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<HomeViewModel>();

            builder.Services.AddSingleton<AboutPage>();
            builder.Services.AddSingleton<AboutViewModel>();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif


            var serviceProvider = builder.Services.BuildServiceProvider();
            builder.ConfigureLifecycleEvents(lifecycle =>
            {

#if ANDROID
                lifecycle.AddAndroid(android =>
                {
                    android.OnCreate((activity, bundle) => {
                        Debug.WriteLine("Lifecycle: OnCreate ------");
                        var magnetometerService = App.ServiceProvider.GetService<IMagnetometerService>();
                        var soundService = App.ServiceProvider.GetService<ISoundService>();
                        var vibrationService = App.ServiceProvider.GetService<IVibrationService>();
                        magnetometerService?.InitializeAsync().Wait();
                        soundService?.InitializeAsync().Wait();
                    });

                    android.OnResume(activity => {
                        Debug.WriteLine("Lifecycle: OnResume ------");
                        var magnetometerService = App.ServiceProvider.GetService<IMagnetometerService>();
                        var soundService = App.ServiceProvider.GetService<ISoundService>();
                        var vibrationService = App.ServiceProvider.GetService<IVibrationService>();
                        magnetometerService?.Start();
                        soundService?.Enable();
                        vibrationService?.Enable();
                    });

                    android.OnStop(activity => {
                        Debug.WriteLine("Lifecycle: OnStop ------");
                        var magnetometerService = App.ServiceProvider.GetService<IMagnetometerService>();
                        var soundService = App.ServiceProvider.GetService<ISoundService>();
                        var vibrationService = App.ServiceProvider.GetService<IVibrationService>();
                        magnetometerService?.Stop();
                        soundService?.Disable();
                        vibrationService?.Disable();
                    });
                });
                
#endif

#if IOS
                lifecycle.AddiOS(ios =>
                {
                    ios.FinishedLaunching((app, options) => {
                        Debug.WriteLine("Lifecycle: FinishedLaunching ------");
                        var service = builder.Services.BuildServiceProvider().GetService<IMagnetometerService>();
                        service?.InitializeAsync().Wait();
                        service?.Start();
                        return true;
                    });

                    ios.WillEnterForeground(app => {
                        Debug.WriteLine("Lifecycle: WillEnterForeground ------");
                        var service = builder.Services.BuildServiceProvider().GetService<IMagnetometerService>();
                        service?.Start();
                    });

                    ios.DidEnterBackground(app => {
                        Debug.WriteLine("Lifecycle: DidEnterBackground ------");
                        var service = builder.Services.BuildServiceProvider().GetService<IMagnetometerService>();
                        service?.Stop();
                    });
                });
#endif
            });

            return builder.Build();
        }
    }
}

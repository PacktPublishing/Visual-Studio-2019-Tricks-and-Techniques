using CitySelector.WPF.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace CitySelector.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // private const int SW_SHOW = 5;
        private const string CONFIG_BASEURL = "BaseUrl";

        private const string CONFIG_DATAPROVIDER = "DataProvider";

        private const int SW_HIDE = 0;

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string msgBadDataProviderConfig = $"Please specify a DATAPROVIDER configuration value of 'API', 'JSON', or 'MOCK'";
            var dataProvider = ConfigurationManager.AppSettings[CONFIG_DATAPROVIDER];
            if (string.IsNullOrWhiteSpace(dataProvider))
            {
                throw new ApplicationException(msgBadDataProviderConfig);
            }

            HideConsoleWindow();
            IServiceCollection services = Program.ServiceCollection;
            Log.Logger.Information($"Using {dataProvider} as a data provider.");
            switch (dataProvider?.ToUpperInvariant())
            {
                case "API":
                    var baseUrl = ConfigurationManager.AppSettings[CONFIG_BASEURL];
                    IServiceProvider serviceProvider = Program.ServiceProvider;
                    var logger = serviceProvider.GetService<ILogger<CitySelectorApiService>>();
                    var citySelectorApiService = new CitySelectorApiService(logger, baseUrl);
                    services.Replace(ServiceDescriptor.Singleton<ICitySelectorService>(_ => citySelectorApiService));
                    break;

                case "JSON":
                    services.Replace(ServiceDescriptor.Singleton<ICitySelectorService, CitySelectorJsonService>());
                    break;

                case "MOCK":
                    services.Replace(ServiceDescriptor.Singleton<ICitySelectorService, CitySelectorMockService>());
                    break;

                default:
                    throw new ApplicationException(msgBadDataProviderConfig);
                    // break;
            }

            Program.ServiceProvider = services.BuildServiceProvider();
            var myWindow = Program.ServiceProvider.GetRequiredService<MainWindow>();
            myWindow.Show();
        }

        private void HideConsoleWindow()
        {
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);

            // Show
            // ShowWindow(handle, SW_SHOW);
        }
    }
}
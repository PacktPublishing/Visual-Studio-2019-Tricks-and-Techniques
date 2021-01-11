using CitySelector.WPF.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace CitySelector.WPF
{
    public class Program
    {
        public static IServiceProvider ServiceProvider { get; internal set; }
        public static IServiceCollection ServiceCollection { get; private set; }

        [STAThread]
#pragma warning disable IDE0060 // Remove unused parameter
        public static void Main(string[] args)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            var app = new App();
            app.InitializeComponent();

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   ServiceCollection = services;
                   services.AddSingleton<ICitySelectorService,
                       CitySelectorMockService>();
                   services.AddSingleton<MainWindow>();

                   //Add Serilog
                   var serilogLogger = new LoggerConfiguration()
                           .WriteTo.RollingFile("CitySelector.WPF.log")
                           .CreateLogger();

                   services.AddLogging(x =>
                   {
                       x.SetMinimumLevel(LogLevel.Information);
                       x.AddSerilog(logger: serilogLogger, dispose: true);
                   });
               });

            var host = builder.Build();
            using var serviceScope = host.Services.CreateScope();
            ServiceProvider = serviceScope.ServiceProvider;

            try
            {
                app.Run();
                Console.WriteLine("Success Starting");
            }
            catch (Exception ex)
            {
                string msg = $"Error Occured";
                Console.WriteLine(msg);
                var logger = ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, msg);
            }
        }
    }
}
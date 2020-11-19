using Microsoft.Extensions.Logging;
using Serilog;
using System.Web;

namespace CGHClientServer1.WebApi
{
    public static class LogConfig
    {
        public static LoggerFactory LoggerFactory = new LoggerFactory();
        public static Serilog.ILogger Logger { get; set; }

        public static void RegisterLogger()
        {
            var path = HttpContext.Current.Server.MapPath("~/logs/log-.txt"); // "log.txt"
            var serilogLogger = new LoggerConfiguration()
                .WriteTo.File(
                    path: path,
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Logger = serilogLogger;
            LoggerFactory.AddSerilog(logger: serilogLogger, dispose: true);
            Logger = Log.Logger;
        }
    }
}
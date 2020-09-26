using System;
using System.Diagnostics;
using System.Reflection;

namespace versioning.core
{
    class Program
    {
        static void Main(string[] args)
        {
            var assyVersion = Assembly.GetEntryAssembly().GetName().Version;
            var fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            string fileVersion = fvi.FileVersion;
            Console.WriteLine($"Assembly Version: {assyVersion}");
            Console.WriteLine($"File Version: {fileVersion}");
        }
    }
}

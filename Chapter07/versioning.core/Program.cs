using System;
using System.Diagnostics;
using System.Reflection;

namespace versioning.core
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var assyVersion = Assembly.GetEntryAssembly().GetName().Version;
            var fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            string fileVersion = fvi.FileVersion;
            Console.WriteLine($"Assembly Version: {assyVersion}");
            Console.WriteLine($"File Version: {fileVersion}");

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey(); // Pause so the console output can be read.
        }
    }
}
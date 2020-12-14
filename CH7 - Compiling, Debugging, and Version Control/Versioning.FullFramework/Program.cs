using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var assyVersion = Assembly.GetEntryAssembly()
  .GetName().Version;
            var fvi = FileVersionInfo.GetVersionInfo(Assembly
              .GetExecutingAssembly().Location);
            string fileVersion = fvi.FileVersion;
            Console.WriteLine($"Assembly Version: {assyVersion}");
            Console.WriteLine($"File Version: {fileVersion}");
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
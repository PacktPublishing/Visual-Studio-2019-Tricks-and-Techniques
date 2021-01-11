namespace WritingExample
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        private static void Main(string[] args)
        {
            List<object> writingInstruments = new List<object>();
            writingInstruments.Add(new Crayon(ConsoleColor.Red));
            writingInstruments.Add(new Pen(ConsoleColor.Green));
            writingInstruments.Add(new Pencil());
            writingInstruments.Add(new Crayon(ConsoleColor.Blue));

#if DEBUG
            Console.WriteLine("This is a debug message");
#endif

            string message = "My sample message.";
            foreach (var writingInstrument in writingInstruments)
            {
                switch (writingInstrument.GetType().Name)
                {
                    case "Crayon":
                        ((Crayon)writingInstrument).Write(message);
                        break;

                    case "Marker":
                        ((Marker)writingInstrument).Write(message);
                        break;

                    case "Pen":
                        ((Pen)writingInstrument).Write(message);
                        break;

                    case "Pencil":
                        ((Pencil)writingInstrument).Write(message);
                        break;

                    default:
                        break;
                }
            }

            Console.ReadKey();
        }
    }
}
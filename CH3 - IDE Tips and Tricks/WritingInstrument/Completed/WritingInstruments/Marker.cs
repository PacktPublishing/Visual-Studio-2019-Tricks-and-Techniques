using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WritingExample
{
    internal class Marker
    {
        private int _fluidLevel = 2;
        private bool _isOpen = false;

        public Marker(ConsoleColor color)
        {
            Color = color;
        }

        private Marker()
        {
        }

        public bool CanWrite
        {
            get { return _isOpen && _fluidLevel > 0; }
        }

        public ConsoleColor Color { get; set; }

        public int FluidLevel
        {
            get { return _fluidLevel; }
        }

        public bool Close()
        {
            _isOpen = false;
            Console.WriteLine($"{GetType().Name} is closed: ");

            return _isOpen;
        }

        public bool Open()
        {
            _isOpen = true;
            Console.WriteLine($"{GetType().Name} is open: ");

            return _isOpen;
        }

        public void Write(string text)
        {
            if (CanWrite)
            {
                Console.ForegroundColor = Color;
                Console.WriteLine($"{GetType().Name} is writing: " + text);
                Console.ResetColor();
                _fluidLevel--;
            }
            else
            {
                Console.WriteLine($"{GetType().Name} cannot write.");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace WritingExample
{
    internal class Pen
    {
        private const int MAX_TIP_SIZE = 3;

        private int _tipSize = MAX_TIP_SIZE;

        public Pen(ConsoleColor color)
        {
            Color = color;
        }

        private Pen()
        {
        }

        public bool CanWrite
        {
            get { return _tipSize >= 1; }
        }

        public ConsoleColor Color { get; set; }

        public int TipSize
        {
            get { return _tipSize; }
        }

        public void Sharpen()
        {
            _tipSize = MAX_TIP_SIZE;
        }

        public void Write(string text)
        {
            if (CanWrite)
            {
                Console.ForegroundColor = Color;
                Console.WriteLine($"{GetType().Name} is writing: " + text);
                Console.ResetColor();
                _tipSize -= 1;
            }
            else
            {
                Console.WriteLine($"{GetType().Name} cannot write, please sharpen!");
            }
        }
    }
}
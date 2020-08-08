using System;
using System.Collections.Generic;
using System.Text;

namespace WritingExample
{
    internal class Pencil
    {
        private const int MAX_TIP_SIZE = 3;

        private int _tipSize = MAX_TIP_SIZE;

        public bool CanWrite
        {
            get { return _tipSize >= 1; }
        }

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
                Console.WriteLine($"{GetType().Name} is writing: " + text);
                _tipSize -= 1;
            }
            else
            {
                Console.WriteLine($"{GetType().Name} cannot write.");
            }
        }
    }
}
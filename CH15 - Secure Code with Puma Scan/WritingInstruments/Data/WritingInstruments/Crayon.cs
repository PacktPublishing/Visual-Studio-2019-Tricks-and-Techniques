using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;
using WritingInstruments.Utils;

namespace WritingExample.Data
{
    public class Crayon : BaseEntity
    {
        private const int MAX_TIP_SIZE = 3;

        private int _tipSize = MAX_TIP_SIZE;

        public Crayon() { }

        public Crayon(Color color)
        {
            Color = color;
        }

        public bool CanWrite
        {
            get { return _tipSize >= 1; }
        }

        public string HTMLColor { get; set; }

        [NotMapped]
        public Color Color
        {
            get { return System.Drawing.ColorTranslator.FromHtml(HTMLColor); }
            set { HTMLColor = System.Drawing.ColorTranslator.ToHtml(value); }
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
                Console.ForegroundColor = ConsoleColorUtil.ClosestConsoleColor(Color);
                Console.WriteLine($"{GetType().Name} is writing: " + text);
                Console.ResetColor();
                _tipSize -= 1;
            }
            else
            {
                Console.WriteLine($"{GetType().Name} cannot write.");
            }
        }
    }
}
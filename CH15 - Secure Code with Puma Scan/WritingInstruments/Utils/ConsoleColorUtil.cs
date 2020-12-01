using System;
using System.Drawing;

namespace WritingInstruments.Utils
{
    public class ConsoleColorUtil
    {
        public static ConsoleColor ClosestConsoleColor(Color MatchColor)
        {
            ConsoleColor ret = 0;
            double rr = MatchColor.R, gg = MatchColor.G, bb = MatchColor.B, delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
                var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    public static class Constants
    {
        static string digitSeparators = ",";
        static string spaces = " ";
        public static bool IsDigitSeparator(char c) => digitSeparators.Contains(c);
        public static bool IsSpace(char c) => spaces.Contains(c);
    }
}

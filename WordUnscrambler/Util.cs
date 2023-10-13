using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordUnscrambler
{
    static class Util
    {
        public static string Prepare(this string s) // Extension Method
        {
            return s.Trim().ToLower();
        }
    }
}
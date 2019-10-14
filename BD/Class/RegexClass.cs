using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BD.Class
{
    class RegexClass
    {
        public static bool RegexCheck(string textBox)
        {
            string s = textBox;
            Regex regex = new Regex(@"\S");
            MatchCollection matches = regex.Matches(s);
            if (matches.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

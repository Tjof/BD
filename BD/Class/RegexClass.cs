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
        public static bool RegexDrugstore(string textBoxName, string time1, string time2)
        {
            string s = textBoxName;
            string t1 = time1;
            string t2 = time2;
            Regex regexDrugstore = new Regex(@"^[(А-ЯЁ)|0-9|\""][а-яёА-ЯЁ\s0-9\-\/.\""№]{2,}$");
            Regex regexTime = new Regex(@"(2[0-3]|[0-1]\d):[0-5]\d");
            MatchCollection matches = regexDrugstore.Matches(s);
            MatchCollection matchesTime1 = regexTime.Matches(t1);
            MatchCollection matchesTime2 = regexTime.Matches(t2);
            if (matches.Count > 0 && matchesTime1.Count > 0 && matchesTime2.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RegexDistrict(string districtName)
        {
            string s = districtName;
            Regex regexDistrict = new Regex(@"^[А-ЯЁ][а-яёА-ЯЁ\s0-9\-]{2,}$");
            MatchCollection matches = regexDistrict.Matches(s);
            if (matches.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RegexRoute(string RouteName)
        {
            string s = RouteName;
            Regex regexRoute = new Regex(@"^[0-9][а-яёА-ЯЁ0-9]{2,}$");
            MatchCollection matches = regexRoute.Matches(s);
            if (matches.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RegexAssortment(string Count, string Price)
        {
            string c = Count;
            string p = Price;
            Regex regexCount = new Regex(@"^[0-9]+$");
            Regex regexPrice = new Regex(@"^[0-9]{1,5}.[0-9]{2}$");
            MatchCollection matches1 = regexCount.Matches(c);
            MatchCollection matches2 = regexPrice.Matches(p);
            if (matches1.Count > 0 && matches2.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RegexStreet(string streetName)
        {
            string s = streetName;
            Regex regexStreet = new Regex(@"^[А-ЯЁ|0-9][а-яёА-ЯЁ\s0-9\-.\/(),]{2,}$");
            MatchCollection matches = regexStreet.Matches(s);
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

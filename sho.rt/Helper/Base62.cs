using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sho.rt.Helper
{
    public class Base62
    {
        private const int CODE_LENTH = 62;
        private static readonly char[] CODE62 = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static readonly Dictionary<char, int> EDOC = new Dictionary<char, int>
        {
            {'0',0},
            {'1',1},
            {'2',2},
            {'3',3},
            {'4',4},
            {'5',5},
            {'6',6},
            {'7',7},
            {'8',8},
            {'9',9},
            {'a',10},
            {'b',11},
            {'c',12},
            {'d',13},
            {'e',14},
            {'f',15},
            {'g',16},
            {'h',17},
            {'i',18},
            {'j',19},
            {'k',20},
            {'l',21},
            {'m',22},
            {'n',23},
            {'o',24},
            {'p',25},
            {'q',26},
            {'r',27},
            {'s',28},
            {'t',29},
            {'u',30},
            {'v',31},
            {'w',32},
            {'x',33},
            {'y',34},
            {'z',35},
            {'A',36},
            {'B',37},
            {'C',38},
            {'D',39},
            {'E',40},
            {'F',41},
            {'G',42},
            {'H',43},
            {'I',44},
            {'J',45},
            {'K',46},
            {'L',47},
            {'M',48},
            {'N',49},
            {'O',50},
            {'P',51},
            {'Q',52},
            {'R',53},
            {'S',54},
            {'T',55},
            {'U',56},
            {'V',57},
            {'W',58},
            {'X',59},
            {'Y',60},
            {'Z',61},
        };

        /// <summary>
        /// Encode
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Encode(Int64 number)
        {
            if (number == 0) return "0";

            List<char> result = new List<char>();

            while (number > 0)
            {
                var round = number / CODE_LENTH;
                var remain = number % CODE_LENTH;
                result.Add(CODE62[remain]);
                number = round;
            }
            result.Reverse();
            return new string(result.ToArray());
        }
        /// <summary>
        /// Decode
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Int64 Decode(string str)
        {
            str = str.Trim();
            int strLen = str.Length;
            long result = 0;
            for (int i = 0; i < str.Length; i++)
            {
                long power = strLen - (i + 1);
                result += (long)(EDOC[str[i]] * Math.Pow(CODE_LENTH, power));
            }
            return result;
        }
    }
}

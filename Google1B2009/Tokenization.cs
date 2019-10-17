using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Google1B2009
{
    public static class Tokenization
    {
        public static Queue<string> TokenizeString(string input)
        {
            //Go to https://regex101.com/ and check the Regular Expression
            Regex lineSplitter = new Regex(@"\s+|(?<=\()(?!\s)|(?<!\s)(?=\))");
            string[] TokenArray = lineSplitter.Split(input);

            return new Queue<string>(TokenArray);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TddKata1
{
    public class CalculationExpressionParser
    {
        // match an expression like [x] or this [xxx]
        static Regex delimiter = new Regex(@"\[([\x20-\x2F\x3A-\x7E])\1*\]");

        public bool ContainsDelimiterExpression(string calcProgram)
        {
            // expression like //[%]\n... or //[%][*]\n... or //[***]\n...
            return Regex.IsMatch(calcProgram, @"//(?:(?:\[([\x20-\x2F\x3A-\x7E])\1*\])+|[\x20-\x2F\x3A-\x7E])\n.*");
        }

        public bool IsMultiDelimiter(string delimiters)
        {
            // match   [%][*] or [***][%%%] 
            // but not [%]    or [***]
            return Regex.IsMatch(delimiters, @"(?:\[([\x20-\x2F\x3A-\x7E])\1*\]){2,}");
        }

        public string[] Delimiters(string calcProgram)
        {
            bool containsDelimiterExpression = ContainsDelimiterExpression(calcProgram);
            if (containsDelimiterExpression)
            {
                // Delimiter is everything past first two comment characters up to newline, less [] if multi-character delimiter
                string allDelimiters = calcProgram.Substring(2, calcProgram.IndexOf('\n') - 2);
                if (IsMultiDelimiter(allDelimiters))
                {
                    MatchCollection matches = delimiter.Matches(allDelimiters);
                    return matches.Cast<Match>().Select(m => TrimDelimiter(m.Value)).ToArray();
                }
                return new[] { TrimDelimiter(allDelimiters) };
            }
            return new[] { "," };
        }

        private static string TrimDelimiter(string allDelimiters)
        {
            return allDelimiters.Trim(new[] { '[', ']' });
        }

        public string InputInstructions(string calcProgram)
        {
            if (ContainsDelimiterExpression(calcProgram))
            {
                int indexFirstNewline = calcProgram.IndexOf('\n');
                return calcProgram.Substring(indexFirstNewline + 1);
            }
            else
            {
                return calcProgram;
            }
        }
    }
}
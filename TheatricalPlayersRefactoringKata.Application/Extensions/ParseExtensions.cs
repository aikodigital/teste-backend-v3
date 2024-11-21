using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Application.Extensions
{
    public static class ParseExtensions
    {
        /// <summary>
        /// Safely parses a string to an integer with a default value if parsing fails.
        /// </summary>
        public static int SafeParseInt(this string input, int defaultValue = 0)
        {
            return int.TryParse(input, out int result) ? result : defaultValue;
        }

        /// <summary>
        /// Safely parses a string to a decimal with a default value if parsing fails.
        /// </summary>
        public static decimal SafeParseDecimal(this string input, decimal defaultValue = 0m)
        {
            return decimal.TryParse(input, out decimal result) ? result : defaultValue;
        }

        /// <summary>
        /// Safely parses a string to a double with a default value if parsing fails.
        /// </summary>
        public static double SafeParseDouble(this string input, double defaultValue = 0.0)
        {
            return double.TryParse(input, out double result) ? result : defaultValue;
        }

        /// <summary>
        /// Safely parses a string to a float with a default value if parsing fails.
        /// </summary>
        public static float SafeParseFloat(this string input, float defaultValue = 0f)
        {
            return float.TryParse(input, out float result) ? result : defaultValue;
        }

        /// <summary>
        /// Ensures a string is safe to use by providing a default value if it is null, empty, or whitespace.
        /// </summary>
        public static string SafeParseString(this string input, string defaultValue = "")
        {
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }
    }
}

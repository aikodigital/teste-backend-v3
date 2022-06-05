using System.Globalization;

namespace TheatricalPlayersRefactoringKata.CrossCutting.Extension
{
    public static class NumberExtension
    {
        public static string FormatCentsHigherThan10(this decimal value)
        {
            if (value % 1 > 0.10m) return value.ToString("F1", new CultureInfo("en-US"));
            else return value.ToString("F0", new CultureInfo("en-US"));
        }
    }
}
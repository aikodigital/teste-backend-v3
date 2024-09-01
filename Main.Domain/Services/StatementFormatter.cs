using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Domain.Services
{
    public class StatementFormatter
    {
        public string EntryStatement(string customer)
        {
            string entry = string.Format("Statement for {0}\n", customer);
            return entry;
        }

        public string FormatStatement(string name, int amount, int audience, CultureInfo cultureInfo)
        {
            string result = String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", name, Convert.ToDecimal(amount / 100), audience);
            return result;
        }

        public string FinalStatement(int amount, int volumeCredits, CultureInfo cultureInfo)
        {
            string result = string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(amount / 100));
            result += string.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }
    }
}

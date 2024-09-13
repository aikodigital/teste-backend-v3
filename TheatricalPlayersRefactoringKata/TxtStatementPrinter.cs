﻿using System.Collections.Generic;
using System.Globalization;
using System;

namespace TheatricalPlayersRefactoringKata
{
    public class TxtStatementPrinter : IStatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var thisAmount = play.CalculateBaseAmount();

                IPlayAmountCalculator calculator = PlayAmountCalculatorFactory.GetCalculator(play.Type);
                thisAmount = calculator.CalculateAmount(perf, thisAmount);

                volumeCredits += calculator.CalculateEarnedCredits(perf.Audience);

                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100m), perf.Audience);
                totalAmount += thisAmount;
            }
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100m));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }
    }
}

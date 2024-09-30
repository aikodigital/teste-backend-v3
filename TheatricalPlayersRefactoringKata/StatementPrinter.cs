using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            decimal totalAmount = 0;
            int volumeCredits = 0;
            var statementData = new List<string>();

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var thisAmount = CalculateAmount(play, perf);
                volumeCredits += CalculateVolumeCredits(play, perf);
                statementData.Add(FormatLine(play, perf, thisAmount));
                totalAmount += thisAmount;

                Console.WriteLine($"Play: {play.Name}, Lines: {play.Lines}, Audience: {perf.Audience}, ThisAmount: {thisAmount}");
            }

            return FormatStatement(invoice.Customer, totalAmount, volumeCredits, statementData);
        }

        private int CalculateAmount(Play play, Performance perf)
        {
            // Calcular a linha base
            int baseLines = Math.Clamp(play.Lines, 1000, 4000);
            int baseAmount = (baseLines / 10) * 100; // Convertendo para centavos
            int thisAmount = 0;

            switch (play.Type)
            {
                case "tragedy":
                    thisAmount = baseAmount;
                    if (perf.Audience > 30)
                    {
                        thisAmount += 1000 * (perf.Audience - 30); // $10.00 por espectador acima de 30
                    }
                    break;

                case "comedy":
                    thisAmount = baseAmount;
                    thisAmount += 300 * perf.Audience; // $3.00 por espectador
                    if (perf.Audience > 20)
                    {
                        thisAmount += 10000 + 500 * (perf.Audience - 20); // $100.00 + $5.00 por espectador acima de 20
                    }
                    break;

                case "history":
                    
                    int tragedyAmount = baseAmount; // Base para tragédia
                    if (perf.Audience > 30)
                    {
                        tragedyAmount += 1000 * (perf.Audience - 30); // Adiciona se houver mais de 30
                    }

                    int comedyAmount = 0; // Inicializa o valor de comédia
                    comedyAmount = baseAmount; // Adiciona a base para comédia
                    comedyAmount += 300 * perf.Audience; // Adiciona $3.00 por espectador
                    if (perf.Audience > 20)
                    {
                        comedyAmount += 10000 + 500 * (perf.Audience - 20); // Adiciona $100.00 + $5.00 por espectador acima de 20
                    }

                    thisAmount = tragedyAmount + comedyAmount; // Soma os valores
                    break;

                default:
                    throw new Exception("Unknown type: " + play.Type);
            }

            return thisAmount;
        }





        private decimal CalculateHistoryAmount(int baseLines, Performance perf)
        {
            // Calcula o valor para tragédia
            decimal tragedyAmount = (baseLines / 10) * 100m;
            if (perf.Audience > 30)
            {
                tragedyAmount += 1000m * (perf.Audience - 30); // $10.00 por espectador acima de 30
            }

            // Calcula o valor para comédia
            decimal comedyAmount = (baseLines / 10) * 100m;
            comedyAmount += 300m * perf.Audience; // $3.00 por espectador
            if (perf.Audience > 20)
            {
                comedyAmount += 10000m + 500m * (perf.Audience - 20); // $100.00 + $5.00 por espectador acima de 20
            }

            // Soma os valores de tragédia e comédia
            return tragedyAmount + comedyAmount;
        }

        private int CalculateVolumeCredits(Play play, Performance perf)
        {
            int credits = Math.Max(perf.Audience - 30, 0);

            if (play.Type == "comedy")
            {
                credits += (int)Math.Floor((decimal)perf.Audience / 5);
            }

            return credits;
        }

        private string FormatLine(Play play, Performance perf, decimal amount)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            return String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, amount / 100, perf.Audience);
        }

        private string FormatStatement(string customer, decimal totalAmount, int volumeCredits, List<string> statementData)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            var result = new StringBuilder();
            result.AppendFormat("Statement for {0}\n", customer);

            foreach (var line in statementData)
            {
                result.Append(line);
            }

            result.AppendFormat(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100);
            result.AppendFormat("You earned {0} credits\n", volumeCredits);
            return result.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.TheatricalGenre;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementLine
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Seats { get; set; }
        public int Credits { get; set; }
        /// <summary>
        /// Represents a line in a statement
        /// </summary>
        /// <param name="name">Name of the play</param>
        /// <param name="amount">Amount of the play</param>
        /// <param name="seats">Number of occupied seats</param>
        public StatementLine(string name, decimal amount, int credits, int seats)
        {
            Name = name;
            Amount = amount;
            Seats = seats;
            Credits = credits;
        }
    }

    public class StatementPrinter
    {
        private readonly string _costumer;
        private readonly decimal _totalAmount;
        private readonly int _volumeCredits;
        private List<StatementLine> _lines;

        /// <summary>
        /// Represents a statement printer for a costumer
        /// </summary>
        /// <param name="invoice">Invoice of the costumer</param>
        /// <param name="plays">Plays of the costumer</param>
        public StatementPrinter(Invoice invoice, Dictionary<string, Play> plays)
        {
            _costumer = invoice.Customer;
            _lines = new List<StatementLine>();
            _totalAmount = 0;
            _volumeCredits = 0;
            foreach (var perf in invoice.Performances)
            {
                Play play = plays[perf.PlayId];
                int lines = play.Lines;
                Genre genre = play.Type switch
                {
                    TheatricalType.Tragedy => new Tragedy(),
                    TheatricalType.Comedy => new Comedy(),
                    TheatricalType.History => new History(),
                    _ => throw new Exception($"Unknown theatrical genre {play.Type}")
                };
                // calculate this amount in dollar
                decimal thisAmount = Convert.ToDecimal(genre.CalculateAmount(perf.Audience, lines) / 100m);
                // calculate volume credits
                int credits = genre.CalculateVolumeCredits(perf.Audience);
                // save line for this order
                _lines.Add(new StatementLine(play.Name, thisAmount, credits, perf.Audience));
                // increase total amount and credits
                _volumeCredits += credits;
                _totalAmount += thisAmount;
            }
        }

        /// <summary>
        /// Print the statement in text format
        /// </summary>
        /// <returns> Text string </returns>
        public string TxtPrint()
        {
            var result = new StringBuilder();
            CultureInfo cultureInfo = new CultureInfo("en-US");
            result.AppendFormat("Statement for {0}\n", _costumer);
            foreach (StatementLine line in _lines)
            {
                result.AppendFormat(cultureInfo, "  {0}: {1:C} ({2} seats)\n", line.Name, line.Amount, line.Seats);
            }
            result.AppendFormat(cultureInfo, "Amount owed is {0:C}\n", _totalAmount);
            result.AppendFormat("You earned {0} credits\n", _volumeCredits);
            return result.ToString();
        }

        /// <summary>
        /// Print the statement in XML format
        /// </summary>
        /// <returns> XML string </returns>
        public string XmlPrint()
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };
            // fix UTF-8 encoding
            using var memoryStream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(memoryStream, settings))
            {
                // create root element
                writer.WriteStartElement("Statement");
                // add xmlns:xsi
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                // add xmlns:xsd
                writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
                // add customer element
                writer.WriteElementString("Customer", _costumer);
                // add elements for each line
                writer.WriteStartElement("Items");
                foreach (var line in _lines)
                {
                    writer.WriteStartElement("Item");
                    writer.WriteElementString("AmountOwed", line.Amount.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("EarnedCredits", line.Credits.ToString());
                    writer.WriteElementString("Seats", line.Seats.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                // add AmountOwed element
                writer.WriteElementString("AmountOwed", _totalAmount.ToString(CultureInfo.InvariantCulture));
                // add EarnedCredits element
                writer.WriteElementString("EarnedCredits", _volumeCredits.ToString());
                // close
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            // Convert the MemoryStream to a UTF-8 encoded string
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
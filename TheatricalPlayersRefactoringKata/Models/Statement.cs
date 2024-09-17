﻿using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Statement
    {
        public string Customer { get; set; }
        public List<Item> Items { get; set; }
        public double AmountOwed { get; set; }
        public int EarnedCredits { get; set; }

        public string ToTXT(CultureInfo cultureInfo)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(cultureInfo, $"Statement for {Customer}");

            Items.ForEach(item => stringBuilder.AppendLine(cultureInfo,
                $"  {item.PlayName}: " +
                $"{item.AmountOwed:C} " +
                $"({item.Seats} seats)"));

            stringBuilder.AppendLine(cultureInfo, $"Amount owed is {AmountOwed:C}");
            stringBuilder.AppendLine($"You earned {EarnedCredits} credits");

            return stringBuilder.ToString();
        }

        public string ToXML(CultureInfo cultureInfo)
        {
            using var stringWriter = new Utf8StringWriter(cultureInfo);
            var xmlSerializer = new XmlSerializer(GetType());
            xmlSerializer.Serialize(stringWriter, this);
            return stringWriter.ToString();
        }
    }

    public class Item
    {
        [XmlIgnore]
        public string PlayName { get; set; }
        public double AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
        public int Seats { get; set; }
    }
}
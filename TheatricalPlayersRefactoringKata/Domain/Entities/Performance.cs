using System;
using System.Text.Json.Serialization;
using TheatricalPlayersRefactoringKata.Application.Converters;
using TheatricalPlayersRefactoringKata.Domain.Entities.Gender;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Performance
    {
        #region Properties
        private const int ValueBaseLines = 10;
        private const int AudienceLimit = 30;
        private const int CreditPerAdditionalPerson = 1;
        private const int ComedyBonusDivision = 5;


        public string Name { get; private set; } = string.Empty;
        public int Lines { get; private set; } = int.MinValue;
        public int Audience { get; private set; } = int.MinValue;

        [JsonConverter(typeof(GenderConverter))]
        public IGender Gender { get; private set; }
        public int Credits => CalculateCredits();

        #endregion

        #region Constructors
        public Performance(
            string name,
            int lines,
            int audience,
            IGender gender)
        {
            Name = name;
            Lines = lines;
            Audience = audience;
            Gender = gender;
        }
        #endregion

        #region Methods Public

        public decimal CalculateTotalCost()
        {
            // Ajusta o valor de Lines para estar entre 1000 e 4000
            decimal adjustedLines = Math.Max(1000, Math.Min(Lines, 4000));

            decimal basePrice = adjustedLines / ValueBaseLines;

            decimal genderCost = Gender.Calculate(basePrice, Audience);

            return genderCost;
        }

        public int CalculateCredits()
        {
            int credits = CalculateBaseCredits();

            if (Gender is Comedy)
                credits += CalculateComedyBonus();

            return credits;
        }

        #endregion

        #region Methods Private

        private int CalculateBaseCredits()
        {
            int extraAudience = Audience - AudienceLimit;

            return Audience > AudienceLimit
                ? extraAudience * CreditPerAdditionalPerson
                : 0;
        }

        private int CalculateComedyBonus()
        {
            return (int)Math.Floor((decimal)Audience / ComedyBonusDivision);
        }

        #endregion
    }
}

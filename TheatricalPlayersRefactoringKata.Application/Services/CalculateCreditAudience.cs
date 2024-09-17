using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Constants;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class CalculateCreditAudience : ICalculateCreditAudience
    {
        public decimal CalculateCredit(int audience, InvoiceCreditSettings invoiceCreditSettings)
        {
            var volumeCredits = Math.Max(audience - invoiceCreditSettings.MinimumAudience, 0);
            if (invoiceCreditSettings.BonusCreditPerAttendees > 0) volumeCredits += (int)Math.Floor(audience / invoiceCreditSettings.BonusCreditPerAttendees);

            return volumeCredits;
        }
    }
}

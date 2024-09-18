using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Formatters.Interface
{
    public interface IExtratoFormatter
    {
        public string GenerateStatement(string customer, string performace, string totalAmount, string totalCredits, CultureInfo cultureInfo);
        public string FormatCustomer(string customer);
        public string FormatPerformance(Play play, Performance performance, decimal performanceAmount,decimal performaceCredits, CultureInfo cultureInfo);

        public string FormatTotalAmount(decimal totalAmount, CultureInfo cultureInfo);

        public string FormatTotalCredits(int totalCredits);

    }
}

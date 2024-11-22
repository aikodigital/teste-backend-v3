using Aplication.DTO;
using Aplication.Interfaces;
using System.Globalization;

namespace Aplication.Services.Formatters
{
    public class TextoInvoiceFormater : IInvoiceFormatter
    {
        public string Format(InvoiceDto invoice, int valorTotal, int valorCreditos,
            IEnumerable<PerformanceResult> performances)
        {
            CultureInfo cultura = new CultureInfo("en-US");
            var resultado = string.Format("Statement for {0}\n", invoice.Customer);

            foreach (var perf in performances)
                resultado += string.Format(cultura, "  {0}: {1:C} ({2} seats)\n",
                    perf.PlayName, Convert.ToDecimal(perf.ValorPorPerformance) / 100, perf.Audience);

            resultado += string.Format(cultura, "Amount owed is {0:C}\n", Convert.ToDecimal(valorTotal) / 100);
            resultado += string.Format("You earned {0} credits\n", valorCreditos);
            return resultado;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Application.Extensions;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Strategy
{
    public class InvoicePrintXML : IInvoicePrint
    {
        public string Print(InvoicePrint.Statement invoicePrint)
        {
            return $"\uFEFF{invoicePrint.GenerateXmlAsString()}";
        }
    }
}

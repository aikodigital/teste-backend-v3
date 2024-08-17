using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Formatters;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class StatementPrinter
    {
        private readonly Dictionary<string, IStatementFormatter> _formatters;

        public StatementPrinter()
        {
            _formatters = new Dictionary<string, IStatementFormatter>
        {
            { "text", new TextStatementFormatter() },
            { "xml", new XmlStatementFormatter() }
        };
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays, string format)
        {
            if (_formatters.TryGetValue(format, out var formatter))
            {
                return formatter.Format(invoice, plays);
            }
            throw new ArgumentException($"Unsupported format: {format}");
        }
    }
}

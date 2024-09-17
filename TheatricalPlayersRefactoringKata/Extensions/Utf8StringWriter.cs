using System;
using System.IO;
using System.Text;

namespace TheatricalPlayersRefactoringKata.Extensions
{
    public class Utf8StringWriter : StringWriter
    {
        private readonly IFormatProvider _formatProvider;

        public Utf8StringWriter(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public override Encoding Encoding => Encoding.UTF8;
        public override IFormatProvider FormatProvider => _formatProvider;
    }
}

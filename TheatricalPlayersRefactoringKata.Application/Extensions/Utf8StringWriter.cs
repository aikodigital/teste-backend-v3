using System.Text;

namespace TheatricalPlayersRefactoringKata.Application.Extensions
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

using System.Text;

namespace TheatricalPlayersRefactoringKata.CrossCutting.Extension
{
    public sealed class StringWriterWithEncoding : StringWriter
    {
        private Encoding encoding;
        public override Encoding Encoding
        {
            get { return encoding; }
        }

        public StringWriterWithEncoding(Encoding encoding, StringBuilder sb) : base (sb)
        {
            this.encoding = encoding;
        }
    }
}
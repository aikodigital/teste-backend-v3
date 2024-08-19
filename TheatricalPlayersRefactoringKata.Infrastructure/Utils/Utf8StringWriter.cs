using System.Text;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Utils
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}

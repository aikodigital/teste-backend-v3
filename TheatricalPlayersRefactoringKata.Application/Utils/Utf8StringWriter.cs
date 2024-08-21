using System.Text;

namespace TheatricalPlayersRefactoringKata.Application.Utils
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}

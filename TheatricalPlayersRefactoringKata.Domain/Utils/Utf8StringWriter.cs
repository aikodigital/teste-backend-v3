using System.IO;
using System.Text;

public sealed class Utf8StringWriter : StringWriter {
    public override Encoding Encoding => new UTF8Encoding(false);
}
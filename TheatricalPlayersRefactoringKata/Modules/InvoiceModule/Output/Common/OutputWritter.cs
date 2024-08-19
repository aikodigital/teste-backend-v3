using System.Globalization;

namespace TheatricalPlayersRefactoringKata.Modules;

public interface OutputWritter
{
    string FileType { get; }
    byte[] GenerateOutput(Invoice invoice, CultureInfo cultureInfo);
}

public abstract class AbstractOutputWritter : OutputWritter
{
    public static AbstractOutputWritter? FromString(string type)
    {
        return type switch
        {
            "txt" => new TextOutputWritter(),
            "xml" => new XMLOutputWritter(),
            _ => null
        };
    }

    virtual public string FileType { get => "txt"; }

    virtual public byte[] GenerateOutput(Invoice invoice, CultureInfo cultureInfo) { return []; }
}
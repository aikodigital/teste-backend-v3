using System.Globalization;

namespace TheatricalPlayersRefactoringKata.Modules;

public interface OutputWritter
{
    string FileType { get; }
    byte[] GenerateOutput(Invoice invoice, CultureInfo cultureInfo);
}

public abstract class AbstractOutputWritter : OutputWritter
{
    virtual public string FileType { get => "txt"; }

    virtual public byte[] GenerateOutput(Invoice invoice, CultureInfo cultureInfo) { return []; }
}
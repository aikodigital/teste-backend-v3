using System.ComponentModel;

namespace TheatricalPlayersRefactoringKata.Domain.enums
{
    public enum PrintType
    {
        [Description("none")]
        none,
        [Description("text")]
        text,
        [Description("xml")]
        xml,
 
    }
}
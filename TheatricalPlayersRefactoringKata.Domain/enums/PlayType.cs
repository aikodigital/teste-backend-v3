using System.ComponentModel;

namespace TheatricalPlayersRefactoringKata.Domain.enums
{
    public enum PlayType
    {
        [Description("none")]
        none,
        [Description("tragedy")]
        tragedy,
        [Description("comedy")]
        comedy,
        [Description("history")]
        history
    }
}
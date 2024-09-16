using System.ComponentModel;

namespace TheatricalPlayers.Core.Enums;

public enum PlayTypeEnum
{
    [Description("tragedy")]
    Tragedy,
    [Description("comedy")]
    Comedy,
    [Description("historical")]
    Historical
}
using System.ComponentModel;

namespace TS.Domain.Enums
{
    public enum ETypePLay
    {
        [Description("Comédia")]
        Comedy = 1,
        [Description("Tragédia")]
        Tragedy = 2,
        [Description("Histórico")]
        History = 3
    }
}
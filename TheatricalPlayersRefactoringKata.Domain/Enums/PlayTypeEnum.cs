
namespace TheatricalPlayersRefactoringKata.Domain.Enums
{
    public enum PlayTypeEnum
    {
        [PlayTypeEnumAttributes("Comedy")]
        Comedy = 0,

        [PlayTypeEnumAttributes("Tragedy")]
        Tragedy = 1,

        [PlayTypeEnumAttributes("History")]
        History = 2
    }

    public class PlayTypeEnumAttributes : Attribute
    {
        public string Name { get; private set; }

        internal PlayTypeEnumAttributes(string name)
        {
            this.Name = name;
        }
    }
}

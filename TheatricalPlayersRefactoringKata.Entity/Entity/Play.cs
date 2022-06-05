using TheatricalPlayersRefactoringKata.Domain.Model.Enum;

namespace TheatricalPlayersRefactoringKata.Domain.Model.Entity
{
    public class Play : BaseEntity
    {
        public string Name { get; set; }
        public int Lines { get; set; }
        public PlayTypeEnum PlayType { get; set; }

        public virtual List<Performance> Performances { get; set; }
    }
}
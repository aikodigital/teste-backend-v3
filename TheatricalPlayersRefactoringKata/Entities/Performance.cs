using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public  class Performance
    {
        [Key]
        public int Id { get; set; }
        public string PlayName {  get; set; }

        public int Audience {  get; set; }

        public int PlayId {  get; set; }

        [ForeignKey(nameof(PlayId))]
        public Play Play { get; set; }
    }
}

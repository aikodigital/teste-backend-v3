
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public abstract class Entity
    {
        [XmlIgnore]
        public Guid Id { get; protected set; }
    }
}

using System;
using System.Text.Json.Serialization;

namespace TheatricalPlayersRefactoringKata.Core.Entities
{
    public class Play
    {
        public Guid PlayId { get; private set; }
        public string Name { get; private set; }
        public Genre Type { get; private set; }
        public decimal Price { get; private set; }
        public int Audience { get; private set; }

        [JsonConstructor]
        public Play(Guid playId, string name, Genre type, decimal price, int audience)
        {
            PlayId = playId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Price = price >= 0 ? price : throw new ArgumentOutOfRangeException(nameof(price));
            Audience = audience;
        }

        public Play(string name, Genre type, decimal price) : this(Guid.NewGuid(), name, type, price, 0) { }

        public void UpdateName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void UpdateType(Genre type)
        {
            Type = type;
        }

        public void UpdatePrice(decimal price)
        {
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
            Price = price;
        }

        public void UpdateAudience(int audience)
        {
            if (audience < 0) throw new ArgumentOutOfRangeException(nameof(audience));
            Audience = audience;
        }
    }
}
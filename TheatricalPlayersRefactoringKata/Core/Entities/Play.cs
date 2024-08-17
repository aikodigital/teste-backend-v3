using System;

namespace TheatricalPlayersRefactoringKata.Core.Entities
{
    public class Play
    {
        public Guid PlayId { get; private set; }
        public string Name { get; private set; }
        public Genre Type { get; private set; }
        public decimal Price { get; private set; }

        public Play(string name, Genre type, decimal price, Guid? playId = null)
        {
            PlayId = playId ?? Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Price = price >= 0 ? price : throw new ArgumentOutOfRangeException(nameof(price));
        }

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
    }
}
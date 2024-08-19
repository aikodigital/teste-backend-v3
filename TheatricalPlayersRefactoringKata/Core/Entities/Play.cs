using System;
using System.Text.Json.Serialization;

namespace TheatricalPlayersRefactoringKata.Core.Entities
{
    public class Play
    {
        public Guid PlayId { get; set; }
        public string Name { get; private set; }
        public Genre Genre { get; private set; }
        public decimal Price { get; private set; }
        public int Audience { get; private set; }

        [JsonConstructor]
        public Play(Guid playId, string name, Genre Genre, decimal price, int audience)
        {
            PlayId = playId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Genre = Genre;
            Price = price >= 0 ? price : throw new ArgumentOutOfRangeException(nameof(price));
            Audience = audience;
        }

        public Play(string name, Genre Genre, decimal price) : this(Guid.NewGuid(), name, Genre, price, 0) { }

        public void UpdateName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void UpdateType(Genre Genre)
        {
            Genre = Genre;
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

        private decimal CalculatePrice(Play play, Performance performance)
        {
            return play.Genre switch
            {
                Genre.Tragedy => performance.Audience > 30 ? 40000 + 1000 * (performance.Audience - 30) : 40000,
                Genre.Comedy => 30000 + 300 * performance.Audience + 100 * Math.Max(0, performance.Audience - 20),
                _ => throw new InvalidOperationException($"Unknown play genre: {play.Genre}")
            };
        }
    }
}
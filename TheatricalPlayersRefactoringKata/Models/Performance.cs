using System;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Performance
    {
        public int Id { get; set; } // Performance ID
        public string PlayId { get; set; } // ID of the play (e.g "as-like")
        public int Audience { get; set;} // Number of audience members

        public Performance() { }

        /// <summary>
        /// Represents a performance of a play.
        /// </summary>
        /// <param name="playID">ID of the play</param>
        /// <param name="audience">Number of audience members</param>
        /// <exception cref="ArgumentException">Thrown when playId is null, empty, or whitespace, or when audience is negative.</exception>
        public Performance(string playID, int audience)
        {
            if (string.IsNullOrWhiteSpace(playID))
                throw new ArgumentException("PlayId cannot be null, empty, or whitespace.", nameof(playID));
            else if (audience < 0)
                throw new ArgumentException("Audience must be greater than or equal to zero.", nameof(audience));
            PlayId = playID;
            Audience = audience;
        }
    }
}
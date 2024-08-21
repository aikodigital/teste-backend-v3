using System;
using System.ComponentModel.DataAnnotations;
using TheatricalPlayersRefactoringKata.Enums;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Play
    {
        public int Id { get; set; } // Play ID
        public string Name { get; set; } // Play name (e.g "As You Like It")
        public int Lines { get; set; } // Number of lines in the play
        public TheatricalType Type { get; set; } // Type of the play (e.g Tragedy, Comedy)

        public Play() { }

        /// <summary>
        /// Represents a play in a theatrical performance.
        /// </summary>
        /// <param name="name">Name of the play</param>
        /// <param name="lines">Number of lines in the play</param>
        /// <param name="type">Type of the play</param>
        /// <exception cref="ArgumentException">Thrown when name is null, empty, or whitespace, or when lines is less than or equal to zero.</exception>
        public Play(string name, int lines, TheatricalType type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null, empty, or whitespace.", nameof(name));
            else if (lines <= 0)
                throw new ArgumentException("Lines must be greater than zero.", nameof(lines));
            Name = name;
            Lines = lines;
            Type = type;
        }
    }
}

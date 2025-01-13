using TheatricalPlayersRefactoringKata.Application.Services.CalculatorGenre;
using TheatricalPlayersRefactoringKata.Application.Services.TextFileGenerator;

namespace TheatricalPlayersRefactoringKata.Application.Services.Mapping
{
    public static class MappingService
    {
        // mapping of the genre to class
        public static readonly Dictionary<string, Type> GenreMapping = new Dictionary<string, Type>
        {
            { "comedy", typeof(CalculatorGenreComedy) },
            { "tragedy", typeof(CalculatorGenreTragedy) },
            { "history", typeof(CalculatorGenreHistory) }
        };

        // mapping of the format file to class
        public static readonly Dictionary<string, Type> FormatFileMapping = new Dictionary<string, Type>
        {
            { "txt", typeof(TextFileGeneratorText) },
            { "xml", typeof(TextFileGeneratorXml) },
        };
    }
}

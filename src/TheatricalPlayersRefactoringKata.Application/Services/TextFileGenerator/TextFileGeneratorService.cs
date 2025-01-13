using TheatricalPlayersRefactoringKata.Application.Services.Mapping;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Exception.ExceptionsBase;
using TheatricalPlayersRefactoringKata.Exception;

namespace TheatricalPlayersRefactoringKata.Application.Services.TextFileGenerator
{
    public static class TextFileGeneratorService
    {
        public static async Task<string> TextFileAsync(Invoice invoice, List<Statement> statements, string formatFile)
        {
            var textFile = string.Empty;

            if (MappingService.FormatFileMapping.TryGetValue(formatFile, out var Type))
            {
                // creates an object of class of type of format file
                var instanceTextFileContext = Activator.CreateInstance(Type) as ITextFileGenerator;

                // creates an object of class of context that redirect to a class specific to make of the calcule of the values
                var textFileContext = new TextFileContext(instanceTextFileContext!, invoice, statements);

                // generates the text file of stataments
                textFile = await Task.Run(() => textFileContext.TextFile());
            }
            else
            {
                throw new ErrorOnValidationException(new List<string>() { string.Format(ResourceErrorMessages.UNKNOWN_TYPE, formatFile) });
            }

            return textFile;
        }
    }
}

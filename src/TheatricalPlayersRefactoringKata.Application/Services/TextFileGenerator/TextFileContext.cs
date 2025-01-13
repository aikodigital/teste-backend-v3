using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.TextFileGenerator
{
    public class TextFileContext
    {
        private ITextFileGenerator _textFileGenerator;
        private Invoice _invoice;
        private List<Statement> _statements;

        public TextFileContext(ITextFileGenerator textFileGenerator, Invoice invoice, List<Statement> statements)
        {
            _textFileGenerator = textFileGenerator;
            _invoice = invoice;
            _statements = statements;
        }

        public string TextFile()
        {
            return _textFileGenerator.TextFile(_invoice, _statements);
        }
    }
}

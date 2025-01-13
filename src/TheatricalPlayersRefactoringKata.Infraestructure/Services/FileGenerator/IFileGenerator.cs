namespace TheatricalPlayersRefactoringKata.Infraestructure.Services.FileGenerator
{
    public interface IFileGenerator
    {
        Task FileGeneratorAsync(string textFile, string formatFile);
    }
}

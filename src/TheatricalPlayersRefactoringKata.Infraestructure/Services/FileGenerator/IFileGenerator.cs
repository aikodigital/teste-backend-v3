namespace TheatricalPlayersRefactoringKata.Infraestructure.Services.FileGenerator
{
    public interface IFileGenerator
    {
        Task<bool> FileGeneratorAsync(string textFile, string formatFile);
    }
}

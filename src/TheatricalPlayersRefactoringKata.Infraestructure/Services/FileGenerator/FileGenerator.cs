namespace TheatricalPlayersRefactoringKata.Infraestructure.Services.FileGenerator
{
    public class FileGenerator : IFileGenerator
    {
        public async Task<bool> FileGeneratorAsync(string textFile, string formatFile)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory; 
            
            string file = baseDirectory + @"\statement." + formatFile; 
            
            await WriteToFileAsync(textFile, file);

            if (File.Exists(file))
                return true;
            else
                return false;
        }

        public async Task WriteToFileAsync(string conteudo, string caminhoArquivo) { 
            using (StreamWriter writer = new StreamWriter(caminhoArquivo)) 
            { 
                await writer.WriteAsync(conteudo); 
            } 
        }
    }
}

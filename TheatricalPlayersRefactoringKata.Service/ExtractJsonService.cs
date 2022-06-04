using Newtonsoft.Json;
using TheatricalPlayersRefactoringKata.Domain.Interface.Services;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.Domain.Service
{
    public class ExtractJsonService : ExtractService, IExtractJsonService
    {
        public ExtractJsonService(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public override string GenerateExtract(Invoice invoice)
        {
            string json = JsonConvert.SerializeObject(invoice, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
        }
    }
}
using TheatricalPlayersRefactoringKata.Domain.Interface.Services;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;
using TheatricalPlayersRefactoringKata.Domain.Model.Enum;

namespace TheatricalPlayersRefactoringKata.Domain.Service
{
    public class ExtractService : IExtractService
    {
        private readonly IServiceProvider _serviceProvider;

        public ExtractService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual string GenerateExtract(Invoice invoice) => throw new Exception("Need set a subclass constructor");
        
        public virtual string GenerateExtract(Invoice invoice, ExtractTypeEnum extractType)
        {
            IExtractService instance = Factory(extractType);
            return instance.GenerateExtract(invoice);
        }

        private IExtractService Factory(ExtractTypeEnum extractType)
        {
            IExtractService extractService = null;

            if (extractType == ExtractTypeEnum.XML)
                extractService = (IExtractXMLService)_serviceProvider.GetService(typeof(IExtractXMLService));

            if (extractType == ExtractTypeEnum.Text)
                extractService = (IExtractTextService)_serviceProvider.GetService(typeof(IExtractTextService));

            if (extractType == ExtractTypeEnum.Json)
                extractService = (IExtractJsonService)_serviceProvider.GetService(typeof(IExtractJsonService));

            return extractService;
        }
    }
}
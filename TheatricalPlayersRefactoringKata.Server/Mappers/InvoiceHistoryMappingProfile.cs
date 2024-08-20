using AutoMapper;

using TheatricalPlayersRefactoringKata.Modules;
using TheatricalPlayersRefactoringKata.Server.Database.DTOs.InvoiceHistory;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.InvoiceHistory;

namespace TheatricalPlayersRefactoringKata.Server.Mappers
{
    public class InvoiceHistoryMappingProfile : Profile
    {
        public InvoiceHistoryMappingProfile()
        {
            // <InvoiceHistoryEntity to InvoiceHistoryDTO>
            CreateMap<InvoiceHistoryEntity, InvoiceHistoryDTO>()
                .ForMember(destination => destination.PerformancesHistories, options => options.MapFrom(source => source.PerformancesHistories));

            // <Invoice to InvoiceHistoryEntity>
            CreateMap<Invoice, InvoiceHistoryEntity>()
                .ConstructUsing(source => new InvoiceHistoryEntity
                {
                    Customer = source.Customer,
                    TotalAmountOwed = source.Results != null ? source.Results.TotalAmountOwed : 0,
                    TotalEarnedCredits = source.Results != null ? source.Results.TotalEarnedCredits : 0
                });
        }
    }
}
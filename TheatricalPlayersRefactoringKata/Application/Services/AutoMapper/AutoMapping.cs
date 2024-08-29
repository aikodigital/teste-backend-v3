using AutoMapper;
using TheatricalPlayersRefactoringKata.Communication.Request;
using TheatricalPlayersRefactoringKata.Communication.Response;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();

    }
    private void RequestToDomain()
    {
        // Mapping from Request to Domain
        CreateMap<InvoiceRequest, Invoice>();
    }
    private void DomainToResponse()
    {
        // Mapping from Domain to Response
        CreateMap<Invoice, InvoiceResponse>();
    }
}
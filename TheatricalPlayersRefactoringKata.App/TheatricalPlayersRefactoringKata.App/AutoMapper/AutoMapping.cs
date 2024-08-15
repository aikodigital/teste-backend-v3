using AutoMapper;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Domain.Entities;


namespace TheatricalPlayersRefactoringKata.App.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestInvoice, Invoice>();
    }

    private void EntityToResponse()
    {
        CreateMap<Invoice, ResponseInvoice>();
        CreateMap<Invoice, ResponseInvoiceRegistered>();
    }
}

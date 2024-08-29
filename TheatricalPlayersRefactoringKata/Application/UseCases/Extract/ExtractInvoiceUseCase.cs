using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Request;
using TheatricalPlayersRefactoringKata.Communication.Response;
using TheatricalPlayersRefactoringKata.Core.Interfaces.Repositories.Invoice;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Extract;

public class ExtractInvoiceUseCase : IExtractInvoiceUseCase
{
    private readonly IInvoiceWriteOnlyRepository _writeOnlyRepository;
    private readonly IInvoiceReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;

    public ExtractInvoiceUseCase(IInvoiceWriteOnlyRepository writeOnlyRepository, IInvoiceReadOnlyRepository readOnlyRepository, IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    public async Task<ExtractResponse> ExecuteAsync(ExtractRequest request)
    {
        var invoices = await _readOnlyRepository.GetByDateRangeAsync(request.StartDate, request.DataFim, request.InvoiceType);
        var response = new ExtractResponse
        {
            TotalInvoices = invoices.Count,
            Invoices = _mapper.Map<List<InvoiceResponse>>(invoices)
        };
        return response;
    }
}


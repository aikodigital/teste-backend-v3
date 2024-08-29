using AutoMapper;
using System;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Request;
using TheatricalPlayersRefactoringKata.Communication.Response;
using TheatricalPlayersRefactoringKata.Core.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Core.Interfaces.Repositories.Invoice;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Invoice;

public class ProcessInvoiceUseCase : IProcessInvoiceUseCase
{
    private readonly IInvoiceWriteOnlyRepository _writeOnlyRepository;
    private readonly IInvoiceReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;


    public ProcessInvoiceUseCase(IInvoiceWriteOnlyRepository writeOnlyRepository, IInvoiceReadOnlyRepository readOnlyRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<InvoiceResponse> ExecuteAsync(InvoiceRequest request)
    {
        await Validate(request);

        var invoice = _mapper.Map<Core.Entities.Invoice>(request);
        await _writeOnlyRepository.AddAsync(invoice);
        return _mapper.Map<InvoiceResponse>(invoice);
    }
    private async Task Validate(InvoiceRequest request)
    {
        var existingInvoice = await _readOnlyRepository.GetByDateRangeAsync(request.DateProcessing, request.DateProcessing, null);
        if (existingInvoice != null)
        {
            throw new InvalidOperationException("Invoice with the same number already exists.");
        }
    }
}

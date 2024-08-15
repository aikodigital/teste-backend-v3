using AutoMapper;
using System.Threading.Tasks;
using TheatherPlayersInfra;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repos;
using TheatricalPlayersRefactoringKata.Validations.Invoices.Register;
namespace TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;
public class RegisterInvoiceValidation : IRegisterInvoiceValidation
{
    private readonly IInvoices _repo;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;
    public RegisterInvoiceValidation(
        IInvoice repo,
        IUnityOfWork unityOfWork,
        IMapper mapper
        )
    {
        _repo = repo;
        _unityOfWork = unityOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseInvoice> Execute(RequestInvoices request)
    {
        Validate(request);

        var entity = _mapper.Map<Invoice>(request);
        await _repo.Add(entity);

        await _unityOfWork.Commit();
        return _mapper.Map<ResponseInvoice>(entity);
    }

    private void Validate(RequestInvoices request)
    {
        var validator = new RegisterInvoiceValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidation(errorMessages);
        }
    }
}
using AutoMapper;
using System.Text;
using TheatricalPlayersRefactoringKata.App.Interfaces;
using TheatricalPlayersRefactoringKata.App.Model.Request;
using TheatricalPlayersRefactoringKata.App.Model.Response;
using TheatricalPlayersRefactoringKata.App.ViewModel;
using TheatricalPlayersRefactoringKata.Domain.Interface.Services;
using TheatricalPlayersRefactoringKata.Domain.Interface.UoW;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.App
{
    public class InvoiceApp : IInvoiceApp
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IInvoiceService _invoiceService;

        public InvoiceApp(IInvoiceService invoiceService,
            IMapper mapper,
            IUnitOfWork uow)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<Response<NewInvoiceResponse>> Invoice(Request<NewInvoiceRequest> request)
        {
            Response<NewInvoiceResponse> response = new Response<NewInvoiceResponse>(_uow);

            using (_uow.OpenTransation())
            {
                bool isValidRequest = ValidateRequest(request, out StringBuilder errors, out Customer customer, out List<Play> lstPlays);
                if (!isValidRequest)
                {
                    response.AddErrorMessage(errors); 
                    return response;
                }

                List<Performance> performances = FilterPerformance(request.Value.PerformanceViewModels, lstPlays);

                Invoice invoice = await _invoiceService.Invoice(request.Value.CustomerId, performances, lstPlays);

                NewInvoiceResponse newInvoiceResponse = new NewInvoiceResponse();
                newInvoiceResponse.Id = invoice.Id;
                newInvoiceResponse.CreationDate = invoice.CreationDate;
                newInvoiceResponse.TotalAmout = invoice.Amount;
                newInvoiceResponse.TotalCredits = invoice.Credits;
                newInvoiceResponse.CustomerId = customer.Id;
                newInvoiceResponse.CustomerName = customer.Name;
                response.Value = newInvoiceResponse;
            }

            return response;
        }

        private List<Performance> FilterPerformance(List<PerformanceViewModel> performanceViewModels, List<Play> plays)
        {
            List<Performance> performances = performanceViewModels.Select(x => new Performance()
            {
                Id = 0,
                Active = true,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Audience = x.Audience,
                PlayId = x.PlayId                
            }).ToList();

            return performances;
        }

        private bool ValidateRequest(Request<NewInvoiceRequest> request, out StringBuilder errors, out Customer customer, out List<Play> lstPlays)
        {
            errors = new StringBuilder();
            lstPlays = new List<Play>();
            customer = new Customer();

            if (request == null || request.Value == null)
            {
                errors.Append("Request nula");
                return false;
            }

            if (request.Value.CustomerId <= 0)
            {
                errors.Append("Cliente Inválido");
                return false;
            }

            if (request.Value.PerformanceViewModels == null || (request.Value.PerformanceViewModels.Count == 0))
            {
                errors.Append("Necessário informar Performance");
                return false;
            }
                        
            customer = _uow.CustomerRepository.Get(request.Value.CustomerId);
            if (customer == null)
            {
                errors.Append("Cliente inválido.");
                return false;
            }

            List<long> lstPlayIds = request.Value.PerformanceViewModels.Select(x => x.PlayId).Distinct().ToList();
            List<Play> lstPlaysExisting = _uow.PlayRepository.Get(lstPlayIds);

            if (lstPlaysExisting == null || lstPlaysExisting.Count == 0)
            {
                errors.Append("Contém Peças inválidas");
                return false;
            }

            if (!lstPlayIds.TrueForAll(id => lstPlaysExisting.Any(play => play.Id == id)))
            {
                errors.Append("Contém Peças inválidas");
                return false;
            }

            lstPlays = lstPlaysExisting;

            return true;
        }
    }
}
using AutoMapper;
using TheatricalPlayersRefactoringKata.App.ViewModel;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.CrossCutting.Mapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Invoice, InvoiceViewModel>();
        }
    }
}
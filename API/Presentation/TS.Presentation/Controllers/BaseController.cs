using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TS.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator? Mediator => _mediator ??= (IMediator?)HttpContext.RequestServices.GetService(typeof(IMediator));
    }
}
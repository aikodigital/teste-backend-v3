using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TS.Presentation.Controllers
{
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator? Mediator => _mediator ??= (IMediator?)HttpContext.RequestServices.GetService(typeof(IMediator));

    }
}
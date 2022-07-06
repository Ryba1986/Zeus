using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeus.Models.Base;
using Zeus.Utilities.Extensions;

namespace Zeus.Api.Client.Controllers
{
   [Authorize]
   public abstract class BaseController : ControllerBase
   {
      private readonly IMediator _mediator;

      public BaseController(IMediator mediator)
      {
         _mediator = mediator;
      }

      protected async Task<IActionResult> SendAsync<T>(IRequest<T?> request) where T : class
      {
         return Ok(await _mediator.Send(request));
      }

      protected async Task<IActionResult> SendAsync(IRequest<Result> request)
      {
         return ModelState.IsValid
            ? Ok(await _mediator.Send(request))
            : BadRequest(ModelState);
      }

      protected int GetLocationId()
      {
         return User.Claims.GetNameValue();
      }
   }
}

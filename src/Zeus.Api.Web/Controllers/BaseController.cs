using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Api.Web.Attributes;
using Zeus.Enums.Users;
using Zeus.Models.Base;
using Zeus.Models.Reports.Dto;
using Zeus.Utilities.Extensions;

namespace Zeus.Api.Web.Controllers
{
   [Authorization(UserRole.User)]
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

      protected async Task<IActionResult> SendAsync(IRequest<ReportFileDto> request)
      {
         ReportFileDto result = await _mediator.Send(request);
         return File(result.Content, result.Type, result.Name);
      }

      protected async Task<IActionResult> SendAsync(IRequest<Result> request)
      {
         return ModelState.IsValid
            ? Ok(await _mediator.Send(request))
            : BadRequest(ModelState);
      }

      protected int GetUserId()
      {
         return User.Claims.GetNameValue();
      }
   }
}

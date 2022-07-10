using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Models.Plcs.Climatixs.Commands;

namespace Zeus.Api.Client.Controllers
{
   public sealed class ClimatixController : BaseController
   {
      public ClimatixController(IMediator mediator) : base(mediator)
      {
      }

      [HttpPost]
      public async Task<IActionResult> CreateClimatix([FromBody] CreateClimatixCommand request)
      {
         request.Update(GetLocationId());
         return await SendAsync(request);
      }
   }
}

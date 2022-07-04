using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Models.Plcs.Meters.Commands;

namespace Zeus.Api.Client.Controllers
{
   public sealed class MeterController : BaseController
   {
      public MeterController(IMediator mediator) : base(mediator)
      {
      }

      [HttpPost]
      public async Task<IActionResult> CreateMeter([FromBody] CreateMeterCommand request)
      {
         request.Update(GetLocationId());
         return await SendAsync(request);
      }
   }
}

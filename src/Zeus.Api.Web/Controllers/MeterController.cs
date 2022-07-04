using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Models.Plcs.Meters.Queries;

namespace Zeus.Api.Web.Controllers
{
   public sealed class MeterController : BaseController
   {
      public MeterController(IMediator mediator) : base(mediator)
      {
      }

      [HttpGet]
      public async Task<IActionResult> GetMeterChart([FromQuery] GetMeterChartQuery request)
      {
         return await SendAsync(request);
      }

      [HttpGet]
      public async Task<IActionResult> GetMeterLast([FromQuery] GetMeterLastQuery request)
      {
         return await SendAsync(request);
      }
   }
}

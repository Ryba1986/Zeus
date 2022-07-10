using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Models.Plcs.Climatixs.Queries;

namespace Zeus.Api.Web.Controllers
{
   public sealed class ClimatixController : BaseController
   {
      public ClimatixController(IMediator mediator) : base(mediator)
      {
      }

      [HttpGet]
      public async Task<IActionResult> GetClimatixChart([FromQuery] GetClimatixChartQuery request)
      {
         return await SendAsync(request);
      }

      [HttpGet]
      public async Task<IActionResult> GetClimatixLast([FromQuery] GetClimatixLastQuery request)
      {
         return await SendAsync(request);
      }
   }
}

using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Models.Plcs.Rvds.Queries;

namespace Zeus.Api.Web.Controllers
{
   public sealed class Rvd145Controller : BaseController
   {
      public Rvd145Controller(IMediator mediator) : base(mediator)
      {
      }

      [HttpGet]
      public async Task<IActionResult> GetRvd145Chart([FromQuery] GetRvd145ChartQuery request)
      {
         return await SendAsync(request);
      }

      [HttpGet]
      public async Task<IActionResult> GetRvd145Last([FromQuery] GetRvd145LastQuery request)
      {
         return await SendAsync(request);
      }
   }
}

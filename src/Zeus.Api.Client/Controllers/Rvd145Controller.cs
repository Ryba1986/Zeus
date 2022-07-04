using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Models.Plcs.Rvds.Commands;

namespace Zeus.Api.Client.Controllers
{
   public sealed class Rvd145Controller : BaseController
   {
      public Rvd145Controller(IMediator mediator) : base(mediator)
      {
      }

      [HttpPost]
      public async Task<IActionResult> CreateRvd145([FromBody] CreateRvd145Command request)
      {
         request.Update(GetLocationId());
         return await SendAsync(request);
      }
   }
}

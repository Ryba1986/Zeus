using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeus.Models.Locations.Queries;

namespace Zeus.Api.Client.Controllers
{
   public sealed class LocationController : BaseController
   {
      public LocationController(IMediator mediator) : base(mediator)
      {
      }

      [AllowAnonymous]
      [HttpPost]
      public async Task<IActionResult> GetLocationToken([FromBody] GetLocationTokenQuery request)
      {
         return await SendAsync(request);
      }

      [HttpPost]
      public async Task<IActionResult> GetLocationTokenRefresh()
      {
         return await SendAsync(new GetLocationTokenRefreshQuery()
         {
            LocationId = GetLocationId()
         });
      }
   }
}

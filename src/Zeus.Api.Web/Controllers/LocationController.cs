using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Api.Web.Attributes;
using Zeus.Enums.Users;
using Zeus.Models.Locations.Commands;
using Zeus.Models.Locations.Queries;

namespace Zeus.Api.Web.Controllers
{
   public sealed class LocationController : BaseController
   {
      public LocationController(IMediator mediator) : base(mediator)
      {
      }

      [HttpGet]
      public async Task<IActionResult> GetLocations()
      {
         return await SendAsync(new GetLocationsQuery());
      }

      [HttpGet]
      public async Task<IActionResult> GetLocationHistory([FromQuery] GetLocationHistoryQuery request)
      {
         return await SendAsync(request);
      }

      [HttpGet]
      public async Task<IActionResult> GetLocationsDictionary()
      {
         return await SendAsync(new GetLocationsDictionaryQuery());
      }

      [Authorization(UserRole.PowerUser)]
      [HttpPost]
      public async Task<IActionResult> CreateLocation([FromBody] CreateLocationCommand request)
      {
         request.Update(GetUserId());
         return await SendAsync(request);
      }

      [Authorization(UserRole.PowerUser)]
      [HttpPost]
      public async Task<IActionResult> UpdateLocation([FromBody] UpdateLocationCommand request)
      {
         request.Update(GetUserId());
         return await SendAsync(request);
      }
   }
}

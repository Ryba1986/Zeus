using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Models.Devices.Queries;

namespace Zeus.Api.Client.Controllers
{
   public sealed class DeviceController : BaseController
   {
      public DeviceController(IMediator mediator) : base(mediator)
      {
      }

      [HttpGet]
      public async Task<IActionResult> GetDevicesFromLocation()
      {
         return await SendAsync(new GetDevicesFromLocationQuery()
         {
            LocationId = GetLocationId()
         });
      }
   }
}

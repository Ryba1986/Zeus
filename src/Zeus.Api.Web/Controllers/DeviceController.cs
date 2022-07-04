using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Api.Web.Attributes;
using Zeus.Enums.Users;
using Zeus.Models.Devices.Commands;
using Zeus.Models.Devices.Queries;

namespace Zeus.Api.Web.Controllers
{
   public sealed class DeviceController : BaseController
   {
      public DeviceController(IMediator mediator) : base(mediator)
      {
      }

      [HttpGet]
      public async Task<IActionResult> GetDevices()
      {
         return await SendAsync(new GetDevicesQuery());
      }

      [HttpGet]
      public async Task<IActionResult> GetDevicesFromLocation([FromQuery] GetDevicesFromLocationQuery request)
      {
         return await SendAsync(request);
      }

      [HttpGet]
      public async Task<IActionResult> GetDeviceHistory([FromQuery] GetDeviceHistoryQuery request)
      {
         return await SendAsync(request);
      }

      [HttpGet]
      public async Task<IActionResult> GetDeviceTypeDictionary()
      {
         return await SendAsync(new GetDeviceTypeDictionaryQuery());
      }

      [HttpGet]
      public async Task<IActionResult> GetDeviceBoundRateDictionary()
      {
         return await SendAsync(new GetDeviceBoundRateDictionaryQuery());
      }

      [HttpGet]
      public async Task<IActionResult> GetDeviceDataBitDictionary()
      {
         return await SendAsync(new GetDeviceDataBitDictionaryQuery());
      }

      [HttpGet]
      public async Task<IActionResult> GetDeviceParityDictionary()
      {
         return await SendAsync(new GetDeviceParityDictionaryQuery());
      }

      [HttpGet]
      public async Task<IActionResult> GetDeviceStopBitDictionary()
      {
         return await SendAsync(new GetDeviceStopBitDictionaryQuery());
      }

      [Authorization(UserRole.PowerUser)]
      [HttpPost]
      public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceCommand request)
      {
         request.Update(GetUserId());
         return await SendAsync(request);
      }

      [Authorization(UserRole.PowerUser)]
      [HttpPost]
      public async Task<IActionResult> UpdateDevice([FromBody] UpdateDeviceCommand request)
      {
         request.Update(GetUserId());
         return await SendAsync(request);
      }
   }
}

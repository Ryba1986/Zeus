using System.Collections.Generic;
using MediatR;
using Zeus.Models.Devices.Dto;

namespace Zeus.Models.Devices.Queries
{
   public sealed class GetDevicesFromLocationQuery : IRequest<IEnumerable<DeviceDto>>
   {
      public int LocationId { get; init; }
   }
}

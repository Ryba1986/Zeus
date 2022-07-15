using System.Collections.Generic;
using MediatR;
using Zeus.Models.Devices.Dto;

namespace Zeus.Models.Devices.Queries
{
   public sealed class GetDevicesFromLocationQuery : IRequest<IReadOnlyCollection<DeviceDto>>
   {
      public int LocationId { get; init; }
   }
}

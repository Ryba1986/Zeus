using System.Collections.Generic;
using MediatR;
using Zeus.Models.Devices.Dto;

namespace Zeus.Models.Devices.Queries
{
   public sealed class GetDeviceHistoryQuery : IRequest<IReadOnlyCollection<DeviceHistoryDto>>
   {
      public int DeviceId { get; init; }
   }
}

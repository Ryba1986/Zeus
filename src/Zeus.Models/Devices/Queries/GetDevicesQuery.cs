using System.Collections.Generic;
using MediatR;
using Zeus.Models.Devices.Dto;

namespace Zeus.Models.Devices.Queries
{
   public sealed class GetDevicesQuery : IRequest<IReadOnlyCollection<DeviceDto>>
   {
   }
}

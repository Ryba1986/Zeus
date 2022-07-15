using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zeus.Enums.Devices;
using Zeus.Models.Devices.Queries;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Devices.Queries
{
   internal sealed class GetDeviceTypeDictionaryHandler : IRequestHandler<GetDeviceTypeDictionaryQuery, IReadOnlyCollection<KeyValuePair<int, string>>>
   {
      public Task<IReadOnlyCollection<KeyValuePair<int, string>>> Handle(GetDeviceTypeDictionaryQuery request, CancellationToken cancellationToken)
      {
         return Task.FromResult(EnumExtensions.GetValues<DeviceType>());
      }
   }
}

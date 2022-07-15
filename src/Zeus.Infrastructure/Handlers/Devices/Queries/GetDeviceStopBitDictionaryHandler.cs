using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zeus.Enums.SerialPorts;
using Zeus.Models.Devices.Queries;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Devices.Queries
{
   internal sealed class GetDeviceStopBitDictionaryHandler : IRequestHandler<GetDeviceStopBitDictionaryQuery, IReadOnlyCollection<KeyValuePair<int, string>>>
   {
      public Task<IReadOnlyCollection<KeyValuePair<int, string>>> Handle(GetDeviceStopBitDictionaryQuery request, CancellationToken cancellationToken)
      {
         return Task.FromResult(EnumExtensions.GetValues<StopBits>());
      }
   }
}

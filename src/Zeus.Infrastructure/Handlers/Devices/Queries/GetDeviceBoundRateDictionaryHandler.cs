using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zeus.Enums.SerialPorts;
using Zeus.Models.Devices.Queries;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Devices.Queries
{
   internal sealed class GetDeviceBoundRateDictionaryHandler : IRequestHandler<GetDeviceBoundRateDictionaryQuery, IEnumerable<KeyValuePair<int, string>>>
   {
      public Task<IEnumerable<KeyValuePair<int, string>>> Handle(GetDeviceBoundRateDictionaryQuery request, CancellationToken cancellationToken)
      {
         return Task.FromResult(EnumExtensions.GetValues<BoundRate>());
      }
   }
}

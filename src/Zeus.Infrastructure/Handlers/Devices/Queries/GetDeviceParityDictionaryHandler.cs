using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zeus.Enums.SerialPorts;
using Zeus.Models.Devices.Queries;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Devices.Queries
{
   internal sealed class GetDeviceParityDictionaryHandler : IRequestHandler<GetDeviceParityDictionaryQuery, IEnumerable<KeyValuePair<int, string>>>
   {
      public Task<IEnumerable<KeyValuePair<int, string>>> Handle(GetDeviceParityDictionaryQuery request, CancellationToken cancellationToken)
      {
         return Task.FromResult(EnumExtensions.GetValues<Parity>());
      }
   }
}

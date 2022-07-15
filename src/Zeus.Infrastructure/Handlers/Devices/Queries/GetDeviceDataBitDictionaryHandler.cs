using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zeus.Enums.SerialPorts;
using Zeus.Models.Devices.Queries;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Devices.Queries
{
   internal sealed class GetDeviceDataBitDictionaryHandler : IRequestHandler<GetDeviceDataBitDictionaryQuery, IReadOnlyCollection<KeyValuePair<int, string>>>
   {
      public Task<IReadOnlyCollection<KeyValuePair<int, string>>> Handle(GetDeviceDataBitDictionaryQuery request, CancellationToken cancellationToken)
      {
         return Task.FromResult(EnumExtensions.GetValues<DataBits>());
      }
   }
}

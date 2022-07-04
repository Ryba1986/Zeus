using System.Collections.Generic;
using MediatR;

namespace Zeus.Models.Devices.Queries
{
   public sealed class GetDeviceBoundRateDictionaryQuery : IRequest<IEnumerable<KeyValuePair<int, string>>>
   {
   }
}

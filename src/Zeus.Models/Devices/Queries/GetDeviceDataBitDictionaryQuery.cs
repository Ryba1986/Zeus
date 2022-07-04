using System.Collections.Generic;
using MediatR;

namespace Zeus.Models.Devices.Queries
{
   public sealed class GetDeviceDataBitDictionaryQuery : IRequest<IEnumerable<KeyValuePair<int, string>>>
   {
   }
}

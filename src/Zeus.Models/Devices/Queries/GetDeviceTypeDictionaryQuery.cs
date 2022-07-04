using System.Collections.Generic;
using MediatR;

namespace Zeus.Models.Devices.Queries
{
   public sealed class GetDeviceTypeDictionaryQuery : IRequest<IEnumerable<KeyValuePair<int, string>>>
   {
   }
}

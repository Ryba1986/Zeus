using System.Collections.Generic;
using MediatR;

namespace Zeus.Models.Locations.Queries
{
   public sealed class GetLocationsDictionaryQuery : IRequest<IEnumerable<KeyValuePair<int, string>>>
   {
   }
}

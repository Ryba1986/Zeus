using System.Collections.Generic;
using MediatR;

namespace Zeus.Models.Locations.Queries
{
   public sealed class GetLocationsDictionaryQuery : IRequest<IReadOnlyCollection<KeyValuePair<int, string>>>
   {
   }
}

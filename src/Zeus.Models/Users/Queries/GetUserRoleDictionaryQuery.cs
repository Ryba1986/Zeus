using System.Collections.Generic;
using MediatR;

namespace Zeus.Models.Users.Queries
{
   public sealed class GetUserRoleDictionaryQuery : IRequest<IEnumerable<KeyValuePair<int, string>>>
   {
   }
}

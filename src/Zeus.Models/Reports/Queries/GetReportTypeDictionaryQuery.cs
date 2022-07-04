using System.Collections.Generic;
using MediatR;

namespace Zeus.Models.Reports.Queries
{
   public sealed class GetReportTypeDictionaryQuery : IRequest<IEnumerable<KeyValuePair<int, string>>>
   {
   }
}

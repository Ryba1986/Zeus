using System.Collections.Generic;
using MediatR;

namespace Zeus.Models.Reports.Queries
{
   public sealed class GetReportTypeDictionaryQuery : IRequest<IReadOnlyCollection<KeyValuePair<int, string>>>
   {
   }
}

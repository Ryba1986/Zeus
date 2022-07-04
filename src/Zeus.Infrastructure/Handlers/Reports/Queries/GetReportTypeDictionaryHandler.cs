using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zeus.Enums.Reports;
using Zeus.Models.Reports.Queries;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Reports.Queries
{
   internal sealed class GetReportTypeDictionaryHandler : IRequestHandler<GetReportTypeDictionaryQuery, IEnumerable<KeyValuePair<int, string>>>
   {
      public Task<IEnumerable<KeyValuePair<int, string>>> Handle(GetReportTypeDictionaryQuery request, CancellationToken cancellationToken)
      {
         return Task.FromResult(EnumExtensions.GetValues<ReportType>());
      }
   }
}

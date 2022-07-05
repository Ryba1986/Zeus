using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Domain.Base;
using Zeus.Infrastructure.Reports.Types.Base;
using Zeus.Models.Base.Dto;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Utilities;

namespace Zeus.Infrastructure.Reports.Plcs.Base
{
   internal abstract class BasePlcProcessor
   {
      public static async Task<IReadOnlyCollection<D>> GetPlcDataAsync<S, D>(IMongoQueryable<S> plc, DateOnly date, DeviceReportDto device, IReportProcessor reportProcessor, Expression<Func<IGrouping<int, S>, D>> selector, CancellationToken cancellationToken) where S : BasePlc where D : BasePlcReportDto
      {
         DateRange range = reportProcessor.GetRange(date);

         return await plc
            .Where(x =>
               x.DeviceId == device.Id &&
               x.Date >= range.Start &&
               x.Date < range.End
            )
            .GroupBy(reportProcessor.GetPlcGroup<S>())
            .Select(selector)
            .ToListAsync(cancellationToken);
      }
   }
}

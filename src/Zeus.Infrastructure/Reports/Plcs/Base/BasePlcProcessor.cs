using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Base;
using Zeus.Infrastructure.Reports.Types.Base;
using Zeus.Models.Base.Dto;
using Zeus.Models.Utilities;

namespace Zeus.Infrastructure.Reports.Plcs.Base
{
   internal abstract class BasePlcProcessor
   {
      public static async Task<IReadOnlyDictionary<int, TResult[]>> GetPlcDataAsync<TSource, TResult>(IQueryable<TSource> plc, DateOnly date, IReportProcessor reportProcessor, TypeAdapterConfig mapper, CancellationToken cancellationToken) where TSource : BasePlc where TResult : BasePlcReportDto
      {
         DateRange range = reportProcessor.GetRange(date);

         IReadOnlyCollection<TResult> result = await plc
            .AsNoTracking()
            .Where(x =>
               x.Date >= range.Start &&
               x.Date < range.End
            )
            .GroupBy(reportProcessor.GetPlcGroup<TSource>())
            .ProjectToType<TResult>(mapper)
            .ToArrayAsync(cancellationToken);

         return result
            .GroupBy(x => x.DeviceId)
            .ToDictionary(x => x.Key, x => x.ToArray());
      }
   }
}

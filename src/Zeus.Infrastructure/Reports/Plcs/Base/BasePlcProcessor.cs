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
      public static async Task<IReadOnlyDictionary<int, D[]>> GetPlcDataAsync<S, D>(IQueryable<S> plc, DateOnly date, IReportProcessor reportProcessor, TypeAdapterConfig mapper, CancellationToken cancellationToken) where S : BasePlc where D : BasePlcReportDto
      {
         DateRange range = reportProcessor.GetRange(date);

         IReadOnlyCollection<D> result = await plc
            .AsNoTracking()
            .Where(x =>
               x.Date >= range.Start &&
               x.Date < range.End
            )
            .GroupBy(reportProcessor.GetPlcGroup<S>())
            .ProjectToType<D>(mapper)
            .ToArrayAsync(cancellationToken);

         return result
            .GroupBy(x => x.DeviceId)
            .ToDictionary(x => x.Key, x => x.ToArray());
      }
   }
}

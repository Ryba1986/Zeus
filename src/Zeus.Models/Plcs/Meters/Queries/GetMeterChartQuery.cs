using System;
using System.Collections.Generic;
using MediatR;
using Zeus.Models.Plcs.Meters.Dto;

namespace Zeus.Models.Plcs.Meters.Queries
{
   public sealed class GetMeterChartQuery : IRequest<IReadOnlyCollection<MeterChartDto>>
   {
      public int DeviceId { get; init; }
      public DateOnly Date { get; init; }

      public GetMeterChartQuery()
      {
         Date = DateOnly.FromDateTime(DateTime.Now);
      }
   }
}

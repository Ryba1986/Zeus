using System;
using System.Collections.Generic;
using MediatR;
using Zeus.Models.Plcs.Climatixs.Dto;

namespace Zeus.Models.Plcs.Climatixs.Queries
{
   public sealed class GetClimatixChartQuery : IRequest<IReadOnlyCollection<ClimatixChartDto>>
   {
      public int DeviceId { get; set; }
      public DateOnly Date { get; init; }

      public GetClimatixChartQuery()
      {
         Date = DateOnly.FromDateTime(DateTime.Now);
      }
   }
}

using System;
using System.Collections.Generic;
using MediatR;
using Zeus.Models.Plcs.Rvds.Dto;

namespace Zeus.Models.Plcs.Rvds.Queries
{
   public sealed class GetRvd145ChartQuery : IRequest<IReadOnlyCollection<Rvd145ChartDto>>
   {
      public int DeviceId { get; set; }
      public DateOnly Date { get; init; }

      public GetRvd145ChartQuery()
      {
         Date = DateOnly.FromDateTime(DateTime.Now);
      }
   }
}

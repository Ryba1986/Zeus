using System.Collections.Generic;
using MediatR;
using Zeus.Models.Locations.Dto;

namespace Zeus.Models.Locations.Queries
{
   public sealed class GetLocationHistoryQuery : IRequest<IReadOnlyCollection<LocationHistoryDto>>
   {
      public int LocationId { get; init; }
   }
}

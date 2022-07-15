using System.Collections.Generic;
using MediatR;
using Zeus.Models.Locations.Dto;

namespace Zeus.Models.Locations.Queries
{
   public sealed class GetLocationsQuery : IRequest<IReadOnlyCollection<LocationDto>>
   {
   }
}

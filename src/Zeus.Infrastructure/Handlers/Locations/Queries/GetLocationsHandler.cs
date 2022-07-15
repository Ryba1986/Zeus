using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Locations.Queries;

namespace Zeus.Infrastructure.Handlers.Locations.Queries
{
   internal sealed class GetLocationsHandler : BaseRequestQueryHandler, IRequestHandler<GetLocationsQuery, IReadOnlyCollection<LocationDto>>
   {
      public GetLocationsHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public async Task<IReadOnlyCollection<LocationDto>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
      {
         return await _uow.Location
            .AsNoTracking()
            .OrderByDescending(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ProjectToType<LocationDto>(_mapper)
            .ToArrayAsync(cancellationToken);
      }
   }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Mongo;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Locations.Queries;

namespace Zeus.Infrastructure.Handlers.Locations.Queries
{
   internal sealed class GetLocationsHandler : BaseRequestQueryHandler, IRequestHandler<GetLocationsQuery, IEnumerable<LocationDto>>
   {
      public GetLocationsHandler(UnitOfWork uow, IMapper mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<LocationDto>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
      {
         return await _uow.Location
            .AsQueryable()
            .OrderByDescending(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ProjectTo<LocationDto>(_mapper)
            .ToListAsync(cancellationToken);
      }
   }
}

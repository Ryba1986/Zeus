using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Locations;
using Zeus.Domain.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Locations.Queries;

namespace Zeus.Infrastructure.Handlers.Locations.Queries
{
   internal sealed class GetLocationHistoryHandler : BaseRequestQueryHandler, IRequestHandler<GetLocationHistoryQuery, IEnumerable<LocationHistoryDto>>
   {
      public GetLocationHistoryHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<LocationHistoryDto>> Handle(GetLocationHistoryQuery request, CancellationToken cancellationToken)
      {
         IQueryable<Tuple<LocationHistory, User>> query =
            from locationHistory in _uow.LocationHistory
            join user in _uow.User on locationHistory.CreatedById equals user.Id
            where locationHistory.LocationId == request.LocationId
            orderby locationHistory.CreateDate descending
            select new Tuple<LocationHistory, User>(locationHistory, user);

         return await query
            .AsNoTracking()
            .ProjectToType<LocationHistoryDto>(_mapper)
            .ToArrayAsync(cancellationToken);
      }
   }
}

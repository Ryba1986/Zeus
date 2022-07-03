using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Domain.Locations;
using Zeus.Domain.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Mongo;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Locations.Queries;

namespace Zeus.Infrastructure.Handlers.Locations.Queries
{
   internal sealed class GetLocationHistoryHandler : BaseRequestQueryHandler, IRequestHandler<GetLocationHistoryQuery, IEnumerable<LocationHistoryDto>>
   {
      public GetLocationHistoryHandler(UnitOfWork uow, IMapper mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<LocationHistoryDto>> Handle(GetLocationHistoryQuery request, CancellationToken cancellationToken)
      {
         IMongoQueryable<Tuple<LocationHistory, User>> query =
            from locationHistory in _uow.LocationHistory.AsQueryable()
            join user in _uow.User.AsQueryable() on locationHistory.CreatedById equals user.Id
            where locationHistory.LocationId == request.LocationId
            orderby locationHistory.CreateDate descending
            select new Tuple<LocationHistory, User>(locationHistory, user);

         return await query
            .ProjectTo<LocationHistoryDto>(_mapper)
            .ToListAsync(cancellationToken);
      }
   }
}

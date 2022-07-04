using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Domain.Devices;
using Zeus.Domain.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Mongo;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Devices.Queries;

namespace Zeus.Infrastructure.Handlers.Devices.Queries
{
   internal sealed class GetDeviceHistoryHandler : BaseRequestQueryHandler, IRequestHandler<GetDeviceHistoryQuery, IEnumerable<DeviceHistoryDto>>
   {
      public GetDeviceHistoryHandler(UnitOfWork uow, IMapper mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<DeviceHistoryDto>> Handle(GetDeviceHistoryQuery request, CancellationToken cancellationToken)
      {
         IMongoQueryable<Tuple<DeviceHistory, User>> query =
            from deviceHistory in _uow.DeviceHistory.AsQueryable()
            join user in _uow.User.AsQueryable() on deviceHistory.CreatedById equals user.Id
            where deviceHistory.DeviceId == request.DeviceId
            orderby deviceHistory.CreateDate descending
            select new Tuple<DeviceHistory, User>(deviceHistory, user);

         return await query
            .ProjectTo<DeviceHistoryDto>(_mapper)
            .ToListAsync(cancellationToken);
      }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Devices;
using Zeus.Domain.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Devices.Queries;

namespace Zeus.Infrastructure.Handlers.Devices.Queries
{
   internal sealed class GetDeviceHistoryHandler : BaseRequestQueryHandler, IRequestHandler<GetDeviceHistoryQuery, IEnumerable<DeviceHistoryDto>>
   {
      public GetDeviceHistoryHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<DeviceHistoryDto>> Handle(GetDeviceHistoryQuery request, CancellationToken cancellationToken)
      {
         IQueryable<Tuple<DeviceHistory, User>> query =
            from deviceHistory in _uow.DeviceHistory
            join user in _uow.User on deviceHistory.CreatedById equals user.Id
            where deviceHistory.DeviceId == request.DeviceId
            orderby deviceHistory.CreateDate descending
            select new Tuple<DeviceHistory, User>(deviceHistory, user);

         return await query
            .AsNoTracking()
            .ProjectToType<DeviceHistoryDto>(_mapper)
            .ToArrayAsync(cancellationToken);
      }
   }
}

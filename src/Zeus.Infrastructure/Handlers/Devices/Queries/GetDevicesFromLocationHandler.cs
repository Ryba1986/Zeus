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
using Zeus.Models.Devices.Dto;
using Zeus.Models.Devices.Queries;

namespace Zeus.Infrastructure.Handlers.Devices.Queries
{
   internal sealed class GetDevicesFromLocationHandler : BaseRequestQueryHandler, IRequestHandler<GetDevicesFromLocationQuery, IEnumerable<DeviceDto>>
   {
      public GetDevicesFromLocationHandler(UnitOfWork uow, IMapper mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<DeviceDto>> Handle(GetDevicesFromLocationQuery request, CancellationToken cancellationToken)
      {
         return await _uow.Device
            .AsQueryable()
            .Where(x =>
               x.LocationId == request.LocationId &&
               x.IsActive
            )
            .OrderBy(x => x.Name)
            .ProjectTo<DeviceDto>(_mapper)
            .ToListAsync(cancellationToken);
      }
   }
}

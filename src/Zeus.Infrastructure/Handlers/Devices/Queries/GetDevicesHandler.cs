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
   internal sealed class GetDevicesHandler : BaseRequestQueryHandler, IRequestHandler<GetDevicesQuery, IEnumerable<DeviceDto>>
   {
      public GetDevicesHandler(UnitOfWork uow, IMapper mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<DeviceDto>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
      {
         return await _uow.Device
            .AsQueryable()
            .OrderByDescending(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ProjectTo<DeviceDto>(_mapper)
            .ToListAsync(cancellationToken);
      }
   }
}

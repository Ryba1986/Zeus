using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Devices.Queries;

namespace Zeus.Infrastructure.Handlers.Devices.Queries
{
   internal sealed class GetDevicesHandler : BaseRequestQueryHandler, IRequestHandler<GetDevicesQuery, IReadOnlyCollection<DeviceDto>>
   {
      public GetDevicesHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public async Task<IReadOnlyCollection<DeviceDto>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
      {
         return await _uow.Device
            .AsNoTracking()
            .OrderByDescending(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ProjectToType<DeviceDto>(_mapper)
            .ToArrayAsync(cancellationToken);
      }
   }
}

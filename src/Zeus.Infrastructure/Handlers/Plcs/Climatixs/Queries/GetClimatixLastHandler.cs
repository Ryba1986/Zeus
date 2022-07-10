using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Climatixs;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Plcs.Climatixs.Dto;
using Zeus.Models.Plcs.Climatixs.Queries;

namespace Zeus.Infrastructure.Handlers.Plcs.Climatixs.Queries
{
   internal sealed class GetClimatixLastHandler : BaseRequestQueryHandler, IRequestHandler<GetClimatixLastQuery, ClimatixDto?>
   {
      public GetClimatixLastHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public Task<ClimatixDto?> Handle(GetClimatixLastQuery request, CancellationToken cancellationToken)
      {
         IQueryable<Tuple<Climatix, Device>> query =
            from clim in _uow.Climatix
            join device in _uow.Device on clim.DeviceId equals device.Id
            where clim.DeviceId == request.DeviceId
            orderby clim.Date descending
            select new Tuple<Climatix, Device>(clim, device);

         return query
            .AsNoTracking()
            .ProjectToType<ClimatixDto>(_mapper)
            .FirstOrDefaultAsync(cancellationToken);
      }
   }
}

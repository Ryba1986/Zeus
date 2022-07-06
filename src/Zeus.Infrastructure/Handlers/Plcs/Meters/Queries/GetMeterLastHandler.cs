using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Meters;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Plcs.Meters.Dto;
using Zeus.Models.Plcs.Meters.Queries;

namespace Zeus.Infrastructure.Handlers.Plcs.Meters.Queries
{
   internal sealed class GetMeterLastHandler : BaseRequestQueryHandler, IRequestHandler<GetMeterLastQuery, MeterDto?>
   {
      public GetMeterLastHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public Task<MeterDto?> Handle(GetMeterLastQuery request, CancellationToken cancellationToken)
      {
         IQueryable<Tuple<Meter, Device>> query =
            from meter in _uow.Meter
            join device in _uow.Device on meter.DeviceId equals device.Id
            where meter.DeviceId == request.DeviceId
            orderby meter.Date descending
            select new Tuple<Meter, Device>(meter, device);

         return query
            .AsNoTracking()
            .ProjectToType<MeterDto>(_mapper)
            .FirstOrDefaultAsync(cancellationToken);
      }
   }
}

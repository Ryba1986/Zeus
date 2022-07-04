using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Meters;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Mongo;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Plcs.Meters.Dto;
using Zeus.Models.Plcs.Meters.Queries;

namespace Zeus.Infrastructure.Handlers.Plcs.Meters.Queries
{
   internal sealed class GetMeterLastHandler : BaseRequestQueryHandler, IRequestHandler<GetMeterLastQuery, MeterDto>
   {
      public GetMeterLastHandler(UnitOfWork uow, IMapper mapper) : base(uow, mapper)
      {
      }

      public Task<MeterDto> Handle(GetMeterLastQuery request, CancellationToken cancellationToken)
      {
         IMongoQueryable<Tuple<Meter, Device>> query =
            from meter in _uow.Meter.AsQueryable()
            join device in _uow.Device.AsQueryable() on meter.DeviceId equals device.Id
            where meter.DeviceId == request.DeviceId
            orderby meter.Date descending
            select new Tuple<Meter, Device>(meter, device);

         return query
            .ProjectTo<MeterDto>(_mapper)
            .FirstOrDefaultAsync(cancellationToken);
      }
   }
}

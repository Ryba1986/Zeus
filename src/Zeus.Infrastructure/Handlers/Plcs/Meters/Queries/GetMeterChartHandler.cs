using System;
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
using Zeus.Models.Plcs.Meters.Dto;
using Zeus.Models.Plcs.Meters.Queries;

namespace Zeus.Infrastructure.Handlers.Plcs.Meters.Queries
{
   internal sealed class GetMeterChartHandler : BaseRequestQueryHandler, IRequestHandler<GetMeterChartQuery, IEnumerable<MeterChartDto>>
   {
      public GetMeterChartHandler(UnitOfWork uow, IMapper mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<MeterChartDto>> Handle(GetMeterChartQuery request, CancellationToken cancellationToken)
      {
         DateTime dateTime = request.Date.ToDateTime(TimeOnly.MinValue);

         return await _uow.Meter
            .AsQueryable()
            .Where(x =>
               x.DeviceId == request.DeviceId &&
               x.Date >= dateTime &&
               x.Date < dateTime.AddDays(1)
            )
            .OrderBy(x => x.Date)
            .ProjectTo<MeterChartDto>(_mapper)
            .ToListAsync(cancellationToken);
      }
   }
}

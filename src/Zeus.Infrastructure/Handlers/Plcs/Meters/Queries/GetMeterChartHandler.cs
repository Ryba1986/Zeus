using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Plcs.Meters.Dto;
using Zeus.Models.Plcs.Meters.Queries;

namespace Zeus.Infrastructure.Handlers.Plcs.Meters.Queries
{
   internal sealed class GetMeterChartHandler : BaseRequestQueryHandler, IRequestHandler<GetMeterChartQuery, IEnumerable<MeterChartDto>>
   {
      public GetMeterChartHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<MeterChartDto>> Handle(GetMeterChartQuery request, CancellationToken cancellationToken)
      {
         DateTime dateTime = request.Date.ToDateTime(TimeOnly.MinValue);

         return await _uow.Meter
            .AsNoTracking()
            .Where(x =>
               x.Date >= dateTime &&
               x.Date < dateTime.AddDays(1) &&
               x.DeviceId == request.DeviceId
            )
            .OrderBy(x => x.Date)
            .ProjectToType<MeterChartDto>(_mapper)
            .ToArrayAsync(cancellationToken);
      }
   }
}

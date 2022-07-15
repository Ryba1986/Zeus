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
using Zeus.Models.Plcs.Climatixs.Dto;
using Zeus.Models.Plcs.Climatixs.Queries;

namespace Zeus.Infrastructure.Handlers.Plcs.Climatixs.Queries
{
   internal sealed class GetClimatixChartHandler : BaseRequestQueryHandler, IRequestHandler<GetClimatixChartQuery, IReadOnlyCollection<ClimatixChartDto>>
   {
      public GetClimatixChartHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public async Task<IReadOnlyCollection<ClimatixChartDto>> Handle(GetClimatixChartQuery request, CancellationToken cancellationToken)
      {
         DateTime date = request.Date.ToDateTime(TimeOnly.MinValue);

         return await _uow.Climatix
            .AsNoTracking()
            .Where(x =>
               x.Date >= date &&
               x.Date < date.AddDays(1) &&
               x.DeviceId == request.DeviceId
            )
            .OrderBy(x => x.Date)
            .ProjectToType<ClimatixChartDto>(_mapper)
            .ToArrayAsync(cancellationToken);
      }
   }
}

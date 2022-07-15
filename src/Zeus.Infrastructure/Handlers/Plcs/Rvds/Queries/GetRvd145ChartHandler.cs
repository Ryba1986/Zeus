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
using Zeus.Models.Plcs.Rvds.Dto;
using Zeus.Models.Plcs.Rvds.Queries;

namespace Zeus.Infrastructure.Handlers.Plcs.Rvds.Queries
{
   internal sealed class GetRvd145ChartHandler : BaseRequestQueryHandler, IRequestHandler<GetRvd145ChartQuery, IReadOnlyCollection<Rvd145ChartDto>>
   {
      public GetRvd145ChartHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public async Task<IReadOnlyCollection<Rvd145ChartDto>> Handle(GetRvd145ChartQuery request, CancellationToken cancellationToken)
      {
         DateTime date = request.Date.ToDateTime(TimeOnly.MinValue);

         return await _uow.Rvd145
            .AsNoTracking()
            .Where(x =>
               x.Date >= date &&
               x.Date < date.AddDays(1) &&
               x.DeviceId == request.DeviceId
            )
            .OrderBy(x => x.Date)
            .ProjectToType<Rvd145ChartDto>(_mapper)
            .ToArrayAsync(cancellationToken);
      }
   }
}

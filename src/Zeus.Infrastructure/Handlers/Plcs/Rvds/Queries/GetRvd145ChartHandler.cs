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
using Zeus.Models.Plcs.Rvds.Dto;
using Zeus.Models.Plcs.Rvds.Queries;

namespace Zeus.Infrastructure.Handlers.Plcs.Rvds.Queries
{
   internal sealed class GetRvd145ChartHandler : BaseRequestQueryHandler, IRequestHandler<GetRvd145ChartQuery, IEnumerable<Rvd145ChartDto>>
   {
      public GetRvd145ChartHandler(UnitOfWork uow, IMapper mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<Rvd145ChartDto>> Handle(GetRvd145ChartQuery request, CancellationToken cancellationToken)
      {
         DateTime date = request.Date.ToDateTime(TimeOnly.MinValue);

         return await _uow.Rvd145
            .AsQueryable()
            .Where(x =>
               x.DeviceId == request.DeviceId &&
               x.Date >= date &&
               x.Date < date.AddDays(1)
            )
            .OrderBy(x => x.Date)
            .ProjectTo<Rvd145ChartDto>(_mapper)
            .ToListAsync(cancellationToken);
      }
   }
}

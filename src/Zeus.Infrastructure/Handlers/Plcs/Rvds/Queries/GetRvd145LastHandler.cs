using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Rvds;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Plcs.Rvds.Dto;
using Zeus.Models.Plcs.Rvds.Queries;

namespace Zeus.Infrastructure.Handlers.Plcs.Rvds.Queries
{
   internal sealed class GetRvd145LastHandler : BaseRequestQueryHandler, IRequestHandler<GetRvd145LastQuery, Rvd145Dto?>
   {
      public GetRvd145LastHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public Task<Rvd145Dto?> Handle(GetRvd145LastQuery request, CancellationToken cancellationToken)
      {
         IQueryable<Tuple<Rvd145, Device>> query =
            from rvd in _uow.Rvd145
            join device in _uow.Device on rvd.DeviceId equals device.Id
            where rvd.DeviceId == request.DeviceId
            orderby rvd.Date descending
            select new Tuple<Rvd145, Device>(rvd, device);

         return query
            .AsNoTracking()
            .ProjectToType<Rvd145Dto>(_mapper)
            .FirstOrDefaultAsync(cancellationToken);
      }
   }
}

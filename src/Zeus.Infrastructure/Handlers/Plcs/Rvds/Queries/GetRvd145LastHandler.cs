using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Rvds;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Mongo;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Plcs.Rvds.Dto;
using Zeus.Models.Plcs.Rvds.Queries;

namespace Zeus.Infrastructure.Handlers.Plcs.Rvds.Queries
{
   internal sealed class GetRvd145LastHandler : BaseRequestQueryHandler, IRequestHandler<GetRvd145LastQuery, Rvd145Dto>
   {
      public GetRvd145LastHandler(UnitOfWork uow, IMapper mapper) : base(uow, mapper)
      {
      }

      public Task<Rvd145Dto> Handle(GetRvd145LastQuery request, CancellationToken cancellationToken)
      {
         IMongoQueryable<Tuple<Rvd145, Device>> query =
            from rvd in _uow.Rvd145.AsQueryable()
            join device in _uow.Device.AsQueryable() on rvd.DeviceId equals device.Id
            where rvd.DeviceId == request.DeviceId
            orderby rvd.Date descending
            select new Tuple<Rvd145, Device>(rvd, device);

         return query
            .ProjectTo<Rvd145Dto>(_mapper)
            .FirstOrDefaultAsync(cancellationToken);
      }
   }
}

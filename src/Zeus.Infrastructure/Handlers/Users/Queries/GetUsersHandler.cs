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
using Zeus.Models.Users.Dto;
using Zeus.Models.Users.Queries;

namespace Zeus.Infrastructure.Handlers.Users.Queries
{
   internal sealed class GetUsersHandler : BaseRequestQueryHandler, IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
   {
      public GetUsersHandler(UnitOfWork uow, IMapper mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
      {
         return await _uow.User
            .AsQueryable()
            .OrderByDescending(x => x.IsActive)
            .ThenBy(x => x.Name)
            .ProjectTo<UserDto>(_mapper)
            .ToListAsync(cancellationToken);
      }
   }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Users.Dto;
using Zeus.Models.Users.Queries;

namespace Zeus.Infrastructure.Handlers.Users.Queries
{
   internal sealed class GetUsersHandler : BaseRequestQueryHandler, IRequestHandler<GetUsersQuery, IReadOnlyCollection<UserDto>>
   {
      public GetUsersHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public async Task<IReadOnlyCollection<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
      {
         return await _uow.User
            .AsNoTracking()
            .OrderByDescending(x => x.IsActive)
            .ThenBy(x => x.Name)
            .ProjectToType<UserDto>(_mapper)
            .ToArrayAsync(cancellationToken);
      }
   }
}

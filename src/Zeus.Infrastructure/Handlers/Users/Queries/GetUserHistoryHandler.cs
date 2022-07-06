using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Users.Dto;
using Zeus.Models.Users.Queries;

namespace Zeus.Infrastructure.Handlers.Users.Queries
{
   internal sealed class GetUserHistoryHandler : BaseRequestQueryHandler, IRequestHandler<GetUserHistoryQuery, IEnumerable<UserHistoryDto>>
   {
      public GetUserHistoryHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<UserHistoryDto>> Handle(GetUserHistoryQuery request, CancellationToken cancellationToken)
      {
         IQueryable<Tuple<UserHistory, User>> query =
            from userHistory in _uow.UserHistory
            join user in _uow.User on userHistory.CreatedById equals user.Id
            where userHistory.UserId == request.UserId
            orderby userHistory.CreateDate descending
            select new Tuple<UserHistory, User>(userHistory, user);

         return await query
            .AsNoTracking()
            .ProjectToType<UserHistoryDto>(_mapper)
            .ToArrayAsync(cancellationToken);
      }
   }
}

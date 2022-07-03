using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Domain.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Mongo;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Users.Dto;
using Zeus.Models.Users.Queries;

namespace Zeus.Infrastructure.Handlers.Users.Queries
{
   internal sealed class GetUserHistoryHandler : BaseRequestQueryHandler, IRequestHandler<GetUserHistoryQuery, IEnumerable<UserHistoryDto>>
   {
      public GetUserHistoryHandler(UnitOfWork uow, IMapper mapper) : base(uow, mapper)
      {
      }

      public async Task<IEnumerable<UserHistoryDto>> Handle(GetUserHistoryQuery request, CancellationToken cancellationToken)
      {
         IMongoQueryable<Tuple<UserHistory, User>> query =
            from userHistory in _uow.UserHistory.AsQueryable()
            join user in _uow.User.AsQueryable() on userHistory.CreatedById equals user.Id
            where userHistory.UserId == request.UserId
            orderby userHistory.CreateDate descending
            select new Tuple<UserHistory, User>(userHistory, user);

         return await query
            .ProjectTo<UserHistoryDto>(_mapper)
            .ToListAsync(cancellationToken);
      }
   }
}

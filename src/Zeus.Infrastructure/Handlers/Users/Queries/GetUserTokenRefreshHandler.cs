using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Domain.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Helpers;
using Zeus.Infrastructure.Repositories;
using Zeus.Infrastructure.Settings;
using Zeus.Models.Base;
using Zeus.Models.Users.Queries;

namespace Zeus.Infrastructure.Handlers.Users.Queries
{
   internal sealed class GetUserTokenRefreshHandler : BaseRequestHandler, IRequestHandler<GetUserTokenRefreshQuery, Result>
   {
      private readonly JwtSettings _settings;

      public GetUserTokenRefreshHandler(UnitOfWork uow, JwtSettings settings) : base(uow)
      {
         _settings = settings;
      }

      public async Task<Result> Handle(GetUserTokenRefreshQuery request, CancellationToken cancellationToken)
      {
         User? existingUser = await _uow.User
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               x.Id == request.UserId &&
               x.IsActive
            , cancellationToken);

         if (existingUser is null)
         {
            return Result.Error("User not found.");
         }

         return JwtHandlerHelper.CreateWeb(existingUser.Id, existingUser.Name, existingUser.Role, _settings);
      }
   }
}

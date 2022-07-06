using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Helpers;
using Zeus.Infrastructure.Repositories;
using Zeus.Infrastructure.Settings;
using Zeus.Models.Base;
using Zeus.Models.Users.Queries;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Users.Queries
{
   internal sealed class GetUserTokenHandler : BaseRequestHandler, IRequestHandler<GetUserTokenQuery, Result>
   {
      private readonly JwtSettings _settings;

      public GetUserTokenHandler(UnitOfWork uow, JwtSettings settings) : base(uow)
      {
         _settings = settings;
      }

      public async Task<Result> Handle(GetUserTokenQuery request, CancellationToken cancellationToken)
      {
         User? existingUser = await _uow.User
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Email == request.Email &&
               x.Password == request.Password.CreatePassword() &&
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

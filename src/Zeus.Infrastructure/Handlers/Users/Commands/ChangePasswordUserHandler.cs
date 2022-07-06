using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Base;
using Zeus.Models.Users.Commands;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Users.Commands
{
   internal sealed class ChangePasswordUserHandler : BaseRequestHandler, IRequestHandler<ChangePasswordUserCommand, Result>
   {
      public ChangePasswordUserHandler(UnitOfWork uow) : base(uow)
      {
      }

      public async Task<Result> Handle(ChangePasswordUserCommand request, CancellationToken cancellationToken)
      {
         User? existingUser = await _uow.User
            .FirstOrDefaultAsync(x =>
               x.Email == request.Email &&
               x.Password == request.OldPassword.CreatePassword() &&
               x.IsActive
            , cancellationToken);

         if (existingUser is null)
         {
            return Result.Error("User not found.");
         }

         existingUser.Update(request.NewPassword);

         await _uow.SaveChangesAsync(cancellationToken);
         return Result.Success();
      }
   }
}

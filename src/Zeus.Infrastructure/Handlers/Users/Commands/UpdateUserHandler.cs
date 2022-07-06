using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Users;
using Zeus.Enums.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Base;
using Zeus.Models.Users.Commands;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Users.Commands
{
   internal sealed class UpdateUserHandler : BaseRequestHandler, IRequestHandler<UpdateUserCommand, Result>
   {
      public UpdateUserHandler(UnitOfWork uow) : base(uow)
      {
      }

      public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
      {
         User? modifiedBy = await _uow.User
               .AsNoTracking()
               .FirstOrDefaultAsync(x =>
                  x.Id == request.ModifiedById &&
                  x.IsActive
               , cancellationToken);

         if (modifiedBy == null)
         {
            return Result.Error($"Cannot find an active user with id {request.ModifiedById}.");
         }

         User? otherUser = await _uow.User
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Email == request.Email &&
               x.Id != request.Id
            , cancellationToken);

         if (otherUser?.Email == request.Email)
         {
            return Result.Error($"User with email address {request.Email} allready exist.");
         }

         User? existingUser = await _uow.User
            .FirstOrDefaultAsync(x =>
               x.Id == request.Id
            , cancellationToken);

         if (existingUser?.Id != request.Id)
         {
            return Result.Error($"User with id: {request.Id} not found.");
         }
         if (!existingUser.Version.SequenceEqual(request.Version))
         {
            return Result.Error("User has been changed.");
         }

         if (existingUser.Role == UserRole.SysAdmin)
         {
            if (modifiedBy.Id != existingUser.Id)
            {
               return Result.Error($"Cannot update the user with role {existingUser.Role.GetDescription()}.");
            }
            if (request.Role != UserRole.SysAdmin)
            {
               return Result.Error("Cannot change the role of system admin user.");
            }
         }
         else
         {
            if (request.Role == UserRole.SysAdmin)
            {
               return Result.Error($"Cannot set the user role {request.Role.GetDescription()}.");
            }
         }

         CreateHistory(existingUser, request, modifiedBy);
         existingUser.Update(request.Name, request.Email, request.Role, request.IsActive);

         await _uow.SaveChangesAsync(cancellationToken);
         return Result.Success();
      }

      private void CreateHistory(User user, UpdateUserCommand request, User createdBy)
      {
         if
         (
            user.Name == request.Name &&
            user.Email == request.Email &&
            user.Role == request.Role &&
            user.IsActive == request.IsActive
         )
         {
            return;
         }

         _uow.UserHistory.Add(new(
            user.Id,
            user.Name != request.Name ? request.Name : string.Empty,
            user.Email != request.Email ? request.Email : string.Empty,
            user.Role != request.Role ? request.Role.GetDescription() : string.Empty,
            request.IsActive,
            createdBy.Id
         ));
      }
   }
}

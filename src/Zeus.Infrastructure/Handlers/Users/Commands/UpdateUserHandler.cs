using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               x.Id == request.ModifiedById &&
               x.IsActive
            , cancellationToken);

         if (modifiedBy is null)
         {
            return Result.Error($"Cannot find an active user with id {request.ModifiedById}.");
         }

         User? otherUser = await _uow.User
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               x.Email == request.Email &&
               x.Id != request.Id
            , cancellationToken);

         if (otherUser?.Email == request.Email)
         {
            return Result.Error($"User with email address {request.Email} allready exist.");
         }

         User? existingUser = await _uow.User
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               x.Id == request.Id
            , cancellationToken);

         if (existingUser?.Id != request.Id)
         {
            return Result.Error($"User with id: {request.Id} not found.");
         }
         if (existingUser.Version != request.Version)
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

         return await _uow.ExecuteTransactionAsync(
            async (session, token) =>
            {
               await CreateHistory(session, existingUser, request, modifiedBy, token);

               existingUser.Update(request.Name, request.Email, request.Role, request.IsActive);
               await _uow.User.ReplaceOneAsync(session, x => x.Id == existingUser.Id, existingUser, cancellationToken: token);
            }
            , cancellationToken);
      }

      private async Task CreateHistory(IClientSessionHandle session, User user, UpdateUserCommand request, User createdBy, CancellationToken cancellationToken)
      {
         if (
               user.Name == request.Name &&
               user.Email == request.Email &&
               user.Role == request.Role &&
               user.IsActive == request.IsActive
            )
         {
            return;
         }

         await _uow.UserHistory.InsertOneAsync(
            session,
            new(
               user.Id,
               user.Name != request.Name ? request.Name : string.Empty,
               user.Email != request.Email ? request.Email : string.Empty,
               user.Role != request.Role ? request.Role.GetDescription() : string.Empty,
               request.IsActive,
               createdBy.Id
            ),
            cancellationToken: cancellationToken);
      }
   }
}

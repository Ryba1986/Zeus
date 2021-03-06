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
   internal sealed class CreateUserHandler : BaseRequestHandler, IRequestHandler<CreateUserCommand, Result>
   {
      public CreateUserHandler(UnitOfWork uow) : base(uow)
      {
      }

      public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
      {
         if (request.Role == UserRole.SysAdmin)
         {
            return Result.Error($"Cannot add the user with role {request.Role.GetDescription()}.");
         }

         User? createdBy = await _uow.User
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Id == request.CreatedById &&
               x.IsActive
            , cancellationToken);

         if (createdBy is null)
         {
            return Result.Error($"Cannot find an active user with id {request.CreatedById}.");
         }

         User? existingUser = await _uow.User
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Email == request.Email
            , cancellationToken);

         if (existingUser?.Email == request.Email)
         {
            return Result.Error($"User with email address {request.Email} allready exist.");
         }

         User newUser = new(request.Name, request.Email, request.Email, request.Role, request.IsActive);

         return await _uow.ExecuteTransactionAsync(
            async token =>
            {
               _uow.User.Add(newUser);
               await _uow.SaveChangesAsync(token);

               _uow.UserHistory.Add(new(newUser.Id, newUser.Name, newUser.Email, newUser.Role.GetDescription(), newUser.IsActive, createdBy.Id));
               await _uow.SaveChangesAsync(token);
            },
            cancellationToken
         );
      }
   }
}

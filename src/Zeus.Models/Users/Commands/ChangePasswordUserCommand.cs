using MediatR;
using Zeus.Models.Base;

namespace Zeus.Models.Users.Commands
{
   public sealed class ChangePasswordUserCommand : IRequest<Result>
   {
      public string Email { get; init; }
      public string OldPassword { get; init; }
      public string NewPassword { get; init; }

      public ChangePasswordUserCommand()
      {
         Email = string.Empty;
         OldPassword = string.Empty;
         NewPassword = string.Empty;
      }
   }
}

using Zeus.Enums.Users;
using Zeus.Models.Base.Commands;

namespace Zeus.Models.Users.Commands
{
   public sealed class CreateUserCommand : BaseCreateCommand
   {
      public string Name { get; init; }
      public string Email { get; init; }
      public UserRole Role { get; init; }

      public CreateUserCommand()
      {
         Name = string.Empty;
         Email = string.Empty;
      }
   }
}

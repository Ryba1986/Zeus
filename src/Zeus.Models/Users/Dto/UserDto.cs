using Zeus.Enums.Users;
using Zeus.Models.Base.Dto;

namespace Zeus.Models.Users.Dto
{
   public sealed class UserDto : BaseDto
   {
      public string Name { get; init; }
      public string Email { get; init; }
      public UserRole Role { get; init; }

      public UserDto()
      {
         Name = string.Empty;
         Email = string.Empty;
      }
   }
}

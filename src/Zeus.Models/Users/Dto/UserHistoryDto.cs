using Zeus.Models.Base.Dto;

namespace Zeus.Models.Users.Dto
{
   public sealed class UserHistoryDto : BaseHistoryDto
   {
      public string Name { get; init; }
      public string Email { get; init; }
      public string Role { get; init; }

      public UserHistoryDto()
      {
         Name = string.Empty;
         Email = string.Empty;
         Role = string.Empty;
      }
   }
}

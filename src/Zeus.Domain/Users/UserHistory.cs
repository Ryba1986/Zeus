using Zeus.Domain.Base;

namespace Zeus.Domain.Users
{
   public sealed class UserHistory : BaseHistory
   {
      public int UserId { get; init; }
      public string Name { get; init; }
      public string Email { get; init; }
      public string Role { get; init; }

      public UserHistory(int userId, string name, string email, string role, bool isActive, int createdById) : base(isActive, createdById)
      {
         UserId = userId;
         Name = name;
         Email = email;
         Role = role;
      }
   }
}

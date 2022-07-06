using Zeus.Domain.Base;
using Zeus.Enums.Users;
using Zeus.Utilities.Extensions;

namespace Zeus.Domain.Users
{
   public sealed class User : BaseDomain
   {
      public string Name { get; private set; }
      public string Email { get; private set; }
      public string Password { get; private set; }
      public UserRole Role { get; private set; }

      public User(string name, string email, string password, UserRole role, bool isActive) : base(isActive)
      {
         Name = name;
         Email = email;
         Password = password.CreatePassword();
         Role = role;
      }

      public void Update(string name, string email, UserRole role, bool isActive)
      {
         Name = name;
         Email = email;
         Role = role;
         IsActive = isActive;
      }

      public void Update(string newPassword)
      {
         Password = newPassword.CreatePassword();
      }
   }
}

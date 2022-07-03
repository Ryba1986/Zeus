using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Zeus.Enums.Users;
using Zeus.Utilities.Extensions;

namespace Zeus.Api.Web.Attributes
{
   internal sealed class AuthorizationAttribute : AuthorizeAttribute
   {
      private static readonly IReadOnlyCollection<int> _roles = EnumExtensions.GetValues<UserRole>()
         .Select(x => x.Key)
         .ToArray();

      public AuthorizationAttribute(UserRole role)
      {
         IReadOnlyCollection<int> values = _roles
            .Where(x => x >= (byte)role)
            .ToArray();

         Roles = string.Join(",", values);
      }
   }
}

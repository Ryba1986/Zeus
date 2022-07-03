using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Zeus.Utilities.Extensions
{
   public static class JwtExtensions
   {
      private static readonly string _name = nameof(ClaimTypes.Name).ToLower();
      private static readonly string _role = nameof(ClaimTypes.Role).ToLower();

      public static int GetNameValue(this IEnumerable<Claim> claims)
      {
         Claim? nameClaim = claims.FirstOrDefault(x => x.Type == _name);
         return Convert.ToInt32(nameClaim?.Value);
      }

      public static Claim CreateNameClaim(this int value)
      {
         return new Claim(_name, value.ToString(), ClaimValueTypes.Integer);
      }

      public static Claim CreateRoleClaim(this byte value)
      {
         return new Claim(_role, value.ToString(), ClaimValueTypes.Integer);
      }
   }
}

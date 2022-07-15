using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Zeus.Enums.Tokens;
using Zeus.Enums.Users;
using Zeus.Infrastructure.Settings;
using Zeus.Models.Base;
using Zeus.Models.Utilities;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Helpers
{
   internal static class JwtHandlerHelper
   {
      public static Result CreateClient(int locationId, JwtSettings settings)
      {
         DateRange range = GetTokenRange(settings.ExpireMinutes);
         Claim[] claims = new[]
         {
            locationId.CreateNameClaim(),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String),
            new Claim(JwtRegisteredClaimNames.Iat, range.Start.ToTimestamp().ToString(), ClaimValueTypes.Integer64)
         };

         return CreateToken(range, claims, JwtIssuer.Client, settings.Key);
      }

      public static Result CreateWeb(int userId, string userName, UserRole role, JwtSettings settings)
      {
         DateRange range = GetTokenRange(settings.ExpireMinutes);
         Claim[] claims = new[]
         {
            new Claim(JwtRegisteredClaimNames.FamilyName, userName, ClaimValueTypes.String),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String),
            new Claim(JwtRegisteredClaimNames.Iat, range.Start.ToTimestamp().ToString(), ClaimValueTypes.Integer64),
            userId.CreateNameClaim(),
            ((byte)role).CreateRoleClaim()
         };

         return CreateToken(range, claims, JwtIssuer.Web, settings.Key);
      }

      private static DateRange GetTokenRange(int expireMinutes)
      {
         DateTime createDate = DateTime.UtcNow;
         DateTime expiresDate = createDate.AddMinutes(expireMinutes);

         return new()
         {
            Start = createDate,
            End = expiresDate
         };
      }

      private static Result CreateToken(DateRange range, IReadOnlyCollection<Claim> claims, JwtIssuer issuer, string tokenKey)
      {
         SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(tokenKey));
         SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

         JwtSecurityToken jwt = new(
            claims: claims,
            notBefore: range.Start,
            expires: range.End,
            signingCredentials: credentials,
            issuer: issuer.GetDescription()
         );

         string result = new JwtSecurityTokenHandler().WriteToken(jwt);
         return Result.Success(result);
      }
   }
}

using System;
using System.Security.Cryptography;
using System.Text;

namespace Zeus.Utilities.Extensions
{
   public static class StringExtensions
   {
      public static string CreatePassword(this string value)
      {
         byte[] byteArray = Encoding.UTF8.GetBytes(value ?? string.Empty);
         byte[] hash = SHA256.HashData(byteArray);

         return Convert.ToHexString(hash);
      }
   }
}

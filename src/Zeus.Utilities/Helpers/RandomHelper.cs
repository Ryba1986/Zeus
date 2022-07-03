using System;
using System.Security.Cryptography;

namespace Zeus.Utilities.Helpers
{
   public static class RandomHelper
   {
      public static int CreateInt()
      {
         byte[] buffer = new byte[4];
         int result;

         do
         {
            RandomNumberGenerator.Fill(buffer);
            result = BitConverter.ToInt32(buffer);
         } while (result == default);

         return Math.Abs(result);
      }

      public static long CreateLong()
      {
         byte[] buffer = new byte[8];
         long result;

         do
         {
            RandomNumberGenerator.Fill(buffer);
            result = BitConverter.ToInt64(buffer);
         } while (result == default);

         return Math.Abs(result);
      }

      public static short CreateShort()
      {
         byte[] buffer = new byte[2];
         short result;

         do
         {
            RandomNumberGenerator.Fill(buffer);
            result = BitConverter.ToInt16(buffer);
         } while (result == default);

         return Math.Abs(result);
      }
   }
}

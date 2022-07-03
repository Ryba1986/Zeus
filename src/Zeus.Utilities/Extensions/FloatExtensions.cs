using System;

namespace Zeus.Utilities.Extensions
{
   public static class FloatExtensions
   {
      public static float Round(this float value)
      {
         return MathF.Round(value, 2);
      }
   }
}

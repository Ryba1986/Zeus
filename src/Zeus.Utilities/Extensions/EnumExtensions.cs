using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Zeus.Utilities.Extensions
{
   public static class EnumExtensions
   {
      public static string GetDescription(this Enum value)
      {
         return value
            .GetType()
            .GetField(value.ToString())?
            .GetCustomAttributes<DescriptionAttribute>(false)
            .FirstOrDefault()?
            .Description ?? string.Empty;
      }

      public static IReadOnlyCollection<KeyValuePair<int, string>> GetValues<T>() where T : Enum
      {
         return Enum
            .GetValues(typeof(T))
            .Cast<T>()
            .Select(x => new KeyValuePair<int, string>(Convert.ToInt32(x), x.GetDescription()))
            .ToArray();
      }
   }
}

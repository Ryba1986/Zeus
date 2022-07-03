using Microsoft.Extensions.Configuration;

namespace Zeus.Utilities.Extensions
{
   public static class ConfigurationExtensions
   {
      public static T GetSettings<T>(this IConfiguration configuration) where T : new()
      {
         T value = new();

         string section = typeof(T)
            .Name
            .Replace("Settings", string.Empty);

         configuration
            .GetSection(section)
            .Bind(value);

         return value;
      }
   }
}

using System.Reflection;
using AutoMapper;

namespace Zeus.Infrastructure.Configuration.Mappers
{
   internal static class AutoMapperConfig
   {
      public static IMapper Initialize(Assembly assembly)
      {
         MapperConfiguration config = new(cfg => cfg.AddMaps(assembly));
         config.CompileMappings();

         return config.CreateMapper();
      }
   }
}

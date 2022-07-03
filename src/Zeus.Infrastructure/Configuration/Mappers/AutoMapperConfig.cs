using System.Reflection;
using AutoMapper;

namespace Zeus.Infrastructure.Configuration.Mappers
{
   internal static class AutoMapperConfig
   {
      public static IMapper Initialize(Assembly assembly)
      {
         return new MapperConfiguration(cfg =>
            cfg.AddMaps(assembly)
         )
         .CreateMapper();
      }
   }
}

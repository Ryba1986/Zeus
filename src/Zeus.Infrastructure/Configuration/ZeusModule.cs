using Autofac;
using Microsoft.Extensions.Configuration;
using Zeus.Infrastructure.Configuration.Mappers;
using Zeus.Infrastructure.Configuration.Modules;

namespace Zeus.Infrastructure.Configuration
{
   public sealed class ZeusModule : Module
   {
      private readonly IConfiguration _configuration;

      public ZeusModule(IConfiguration configuration)
      {
         _configuration = configuration;
      }

      protected override void Load(ContainerBuilder builder)
      {
         RegisterMappers(builder);
         RegisterModules(builder);
      }

      private void RegisterMappers(ContainerBuilder builder)
      {
         builder
            .RegisterInstance(AutoMapperConfig.Initialize(ThisAssembly))
            .SingleInstance();
      }

      private void RegisterModules(ContainerBuilder builder)
      {
         builder.RegisterModule<MediatorModule>();
         builder.RegisterModule<MongoDbModule>();
         builder.RegisterModule(new SettingsModule(_configuration));
      }
   }
}

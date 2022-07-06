using Autofac;
using Microsoft.Extensions.Configuration;
using Zeus.Infrastructure.Settings;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Configuration.Modules
{
   internal sealed class SettingsModule : Module
   {
      private readonly IConfiguration _configuration;

      public SettingsModule(IConfiguration configuration)
      {
         _configuration = configuration;
      }

      protected override void Load(ContainerBuilder builder)
      {
         builder
            .RegisterInstance(_configuration.GetSettings<JwtSettings>())
            .SingleInstance();

         builder
            .RegisterInstance(_configuration.GetSettings<SqlSettings>())
            .SingleInstance();

         builder
            .RegisterInstance(_configuration.GetSettings<ZeusSettings>())
            .SingleInstance();
      }
   }
}

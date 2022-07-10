using Autofac;
using Microsoft.Extensions.Configuration;
using Zeus.Client.Settings;
using Zeus.Utilities.Extensions;

namespace Zeus.Client.Configuration.Modules
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
            .RegisterInstance(_configuration.GetSettings<ZeusSettings>())
            .SingleInstance();
      }
   }
}

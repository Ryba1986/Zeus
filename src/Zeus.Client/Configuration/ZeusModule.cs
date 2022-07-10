using Autofac;
using Microsoft.Extensions.Configuration;
using Zeus.Client.Configuration.Modules;

namespace Zeus.Client.Configuration
{
   internal sealed class ZeusModule : Module
   {
      private readonly IConfiguration _configuration;

      public ZeusModule(IConfiguration configuration)
      {
         _configuration = configuration;
      }

      protected override void Load(ContainerBuilder builder)
      {
         builder.RegisterModule(new SettingsModule(_configuration));
      }
   }
}

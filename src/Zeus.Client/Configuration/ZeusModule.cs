using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Zeus.Client.Modbus.Base;
using Zeus.Client.Modbus.Climatixs;
using Zeus.Client.Modbus.Meters;
using Zeus.Client.Modbus.Rvds;
using Zeus.Client.Settings;
using Zeus.Enums.Plcs;
using Zeus.Utilities.Extensions;

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
         RegisterMediator(builder);
         RegisterModbus(builder);
         RegisterRestClient(builder);
         RegisterSettings(builder);
      }

      private void RegisterMediator(ContainerBuilder builder)
      {
         builder.RegisterMediatR(ThisAssembly);
      }

      private static void RegisterModbus(ContainerBuilder builder)
      {
         builder
            .RegisterType<MeterProcessor>()
            .Keyed<IModbusProcessor>(PlcType.Meter);

         builder
            .RegisterType<ClimatixProcessor>()
            .Keyed<IModbusProcessor>(PlcType.Climatix);

         builder
            .RegisterType<Rvd145Processor>()
            .Keyed<IModbusProcessor>(PlcType.Rvd145);
      }

      private static void RegisterRestClient(ContainerBuilder builder)
      {
         builder.Register((ZeusSettings settings) =>
         {
            RestClientOptions options = new()
            {
               BaseUrl = new(settings.ApiBaseUrl),
               ThrowOnAnyError = false,
            };

            return new RestClient(options);
         })
         .AsSelf()
         .SingleInstance();
      }

      private void RegisterSettings(ContainerBuilder builder)
      {
         builder
            .RegisterInstance(_configuration.GetSettings<ZeusSettings>())
            .SingleInstance();
      }
   }
}

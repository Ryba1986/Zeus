using System.IO;
using Autofac;
using LiteDB;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
         RegisterLiteDb(builder);
         RegisterMediator(builder);
         RegisterModbus(builder);
         RegisterRestClient(builder);
         RegisterSettings(builder);
      }

      private static void RegisterLiteDb(ContainerBuilder builder)
      {
         builder.Register((IHostEnvironment environment) =>
         {
            return new LiteDatabase($"{environment.ContentRootPath}{Path.DirectorySeparatorChar}ZeusClientDb.db")
            {
               CheckpointSize = 1,
               UtcDate = false
            };
         })
         .AsSelf()
         .SingleInstance();
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

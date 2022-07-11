using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using RestSharp;
using Zeus.Client.Settings;

namespace Zeus.Client.Configuration.Modules
{
   internal sealed class ServiceModule : Module
   {
      protected override void Load(ContainerBuilder builder)
      {
         RegisterMediator(builder);
         RegisterRestClient(builder);
      }

      private void RegisterMediator(ContainerBuilder builder)
      {
         builder.RegisterMediatR(ThisAssembly);
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
   }
}

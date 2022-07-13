using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zeus.Client.Configuration;
using Zeus.Client.Workers;

namespace Zeus.Client
{
   internal sealed class Program
   {
      public static async Task Main()
      {
         await CreateHostBuilder()
            .Build()
            .RunAsync();
      }

      private static IHostBuilder CreateHostBuilder()
      {
         return Host
            .CreateDefaultBuilder()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseSystemd()
            .ConfigureServices(services =>
            {
               services.AddHostedService<AuthWorker>();
            })
            .ConfigureContainer<ContainerBuilder>((ctx, builder) =>
            {
               builder.RegisterModule(new ZeusModule(ctx.Configuration));
            });
      }
   }
}

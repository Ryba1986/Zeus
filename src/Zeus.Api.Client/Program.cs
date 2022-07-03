using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Zeus.Infrastructure.Configuration;

namespace Zeus.Api.Client
{
   internal sealed class Program
   {
      public static Task Main(string[] args)
      {
         return CreateHostBuilder(args)
            .Build()
            .RunAsync();
      }

      private static IHostBuilder CreateHostBuilder(string[] args)
      {
         return Host
            .CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseSystemd()
            .ConfigureWebHostDefaults(webBuilder =>
            {
               webBuilder
                  .UseStartup<Startup>()
                  .UseKestrel(options =>
                  {
                     options.AddServerHeader = false;
                  });
            })
            .ConfigureContainer<ContainerBuilder>((context, builder) =>
            {
               builder.RegisterModule(new ZeusModule(context.Configuration));
            });
      }
   }
}

using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace Zeus.Infrastructure.Configuration.Modules
{
   internal sealed class MediatorModule : Module
   {
      protected override void Load(ContainerBuilder builder)
      {
         builder.RegisterMediatR(ThisAssembly);
      }
   }
}

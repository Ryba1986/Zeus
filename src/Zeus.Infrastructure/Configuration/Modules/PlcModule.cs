using Autofac;
using Zeus.Enums.Plcs;
using Zeus.Infrastructure.Plc;
using Zeus.Infrastructure.Plc.Base;

namespace Zeus.Infrastructure.Configuration.Modules
{
   internal sealed class PlcModule : Module
   {
      protected override void Load(ContainerBuilder builder)
      {
         builder
            .RegisterType<MeterPlcProcessor>()
            .Keyed<IPlcProcessor>(PlcType.Meter);

         builder
            .RegisterType<Rvd145PlcProcessor>()
            .Keyed<IPlcProcessor>(PlcType.Rvd145);
      }
   }
}

using Autofac;
using Zeus.Client.Modbus.Meters;
using Zeus.Client.Modbus.Base;
using Zeus.Enums.Plcs;
using Zeus.Client.Modbus.Climatixs;
using Zeus.Client.Modbus.Rvds;

namespace Zeus.Client.Configuration.Modules
{
   internal sealed class ModbusModule : Module
   {
      protected override void Load(ContainerBuilder builder)
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
   }
}

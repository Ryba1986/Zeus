using Zeus.Domain.Base;
using Zeus.Enums.Devices;
using Zeus.Enums.Plcs;
using Zeus.Enums.SerialPorts;

namespace Zeus.Domain.Devices
{
   public sealed class Device : BaseDomain
   {
      public int LocationId { get; private set; }
      public string Name { get; private set; }
      public string SerialNumber { get; private set; }
      public DeviceType Type { get; private set; }
      public byte ModbusId { get; private set; }
      public BoundRate RsBoundRate { get; private set; }
      public DataBits RsDataBits { get; private set; }
      public Parity RsParity { get; private set; }
      public StopBits RsStopBits { get; private set; }
      public bool IncludeReport { get; private set; }

      public PlcType PlcType => Type switch
      {
         DeviceType.Kamstrup or DeviceType.KamstrupRs500 => PlcType.Meter,
         DeviceType.ClimatixCo or DeviceType.ClimatixCoCo or DeviceType.ClimatixCoCwu => PlcType.Climatix,
         DeviceType.Rvd145Co or DeviceType.Rvd145CoCo or DeviceType.Rvd145CoCwu => PlcType.Rvd145,
         _ => PlcType.None
      };

      public bool IsPlc => Type switch
      {
         DeviceType.Kamstrup or DeviceType.KamstrupRs500 => false,
         _ => true
      };

      public bool IsCo1 => IsPlc;

      public bool IsCo2 => Type switch
      {
         DeviceType.ClimatixCoCo or DeviceType.Rvd145CoCo => true,
         _ => false
      };

      public bool IsCwu => Type switch
      {
         DeviceType.ClimatixCoCwu or DeviceType.Rvd145CoCwu => true,
         _ => false
      };

      public Device(int locationId, string name, DeviceType type, byte modbusId, BoundRate rsBoundRate, DataBits rsDataBits, Parity rsParity, StopBits rsStopBits, bool includeReport, bool isActive) : base(isActive)
      {
         SerialNumber = string.Empty;

         LocationId = locationId;
         Name = name;
         Type = type;
         ModbusId = modbusId;
         RsBoundRate = rsBoundRate;
         RsDataBits = rsDataBits;
         RsParity = rsParity;
         RsStopBits = rsStopBits;
         IncludeReport = includeReport;
      }

      public void Update(int locationId, string name, DeviceType type, byte modbusId, BoundRate rsBoundRate, DataBits rsDataBits, Parity rsParity, StopBits rsStopBits, bool includeReport, bool isActive)
      {
         LocationId = locationId;
         Name = name;
         Type = type;
         ModbusId = modbusId;
         RsBoundRate = rsBoundRate;
         RsDataBits = rsDataBits;
         RsParity = rsParity;
         RsStopBits = rsStopBits;
         IncludeReport = includeReport;
         IsActive = isActive;
      }

      public void Update(string serialNumber)
      {
         SerialNumber = serialNumber;
      }
   }
}

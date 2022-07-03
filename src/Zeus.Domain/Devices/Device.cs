using Zeus.Domain.Base;
using Zeus.Enums.Devices;
using Zeus.Enums.SerialPorts;
using Zeus.Utilities.Helpers;

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
         Version = RandomHelper.CreateShort();

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
         Version = RandomHelper.CreateShort();

         SerialNumber = serialNumber;
      }
   }
}

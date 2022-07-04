using Zeus.Enums.Devices;
using Zeus.Enums.SerialPorts;
using Zeus.Models.Base.Commands;

namespace Zeus.Models.Devices.Commands
{
   public sealed class CreateDeviceCommand : BaseCreateCommand
   {
      public int LocationId { get; init; }
      public string Name { get; init; }
      public DeviceType Type { get; init; }
      public byte ModbusId { get; init; }
      public BoundRate RsBoundRate { get; init; }
      public DataBits RsDataBits { get; init; }
      public Parity RsParity { get; init; }
      public StopBits RsStopBits { get; init; }
      public bool IncludeReport { get; init; }

      public CreateDeviceCommand()
      {
         Name = string.Empty;
      }
   }
}

using Zeus.Enums.Devices;
using Zeus.Enums.Plcs;
using Zeus.Enums.SerialPorts;
using Zeus.Models.Base.Dto;

namespace Zeus.Models.Devices.Dto
{
   public sealed class DeviceDto : BaseDto
   {
      public int LocationId { get; init; }
      public string Name { get; init; }
      public string SerialNumber { get; init; }
      public DeviceType Type { get; init; }
      public PlcType PlcType { get; init; }
      public byte ModbusId { get; init; }
      public BoundRate RsBoundRate { get; init; }
      public DataBits RsDataBits { get; init; }
      public Parity RsParity { get; init; }
      public StopBits RsStopBits { get; init; }
      public bool IncludeReport { get; init; }

      public DeviceDto()
      {
         Name = string.Empty;
         SerialNumber = string.Empty;
      }
   }
}

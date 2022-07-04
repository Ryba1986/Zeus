using Zeus.Models.Base.Dto;

namespace Zeus.Models.Devices.Dto
{
   public sealed class DeviceHistoryDto : BaseHistoryDto
   {
      public string Name { get; init; }
      public string LocationName { get; init; }
      public string Type { get; init; }
      public string ModbusId { get; init; }
      public string RsBoundRate { get; init; }
      public string RsDataBits { get; init; }
      public string RsParity { get; init; }
      public string RsStopBits { get; init; }
      public bool IncludeReport { get; init; }

      public DeviceHistoryDto()
      {
         Name = string.Empty;
         LocationName = string.Empty;
         Type = string.Empty;
         ModbusId = string.Empty;
         RsBoundRate = string.Empty;
         RsDataBits = string.Empty;
         RsParity = string.Empty;
         RsStopBits = string.Empty;
      }
   }
}

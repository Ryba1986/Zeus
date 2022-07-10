using Zeus.Enums.Plcs;
using Zeus.Models.Base.Dto;

namespace Zeus.Models.Devices.Dto
{
   public sealed class DeviceReportDto : BaseReportDto
   {
      public int LocationId { get; init; }
      public string Name { get; init; }
      public PlcType PlcType { get; init; }
      public bool IsCo1 { get; init; }
      public bool IsCo2 { get; init; }
      public bool IsCwu { get; init; }

      public DeviceReportDto()
      {
         Name = string.Empty;
      }
   }
}

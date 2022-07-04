using Zeus.Enums.Devices;
using Zeus.Enums.Plcs;
using Zeus.Models.Base.Dto;

namespace Zeus.Models.Devices.Dto
{
   public sealed class DeviceReportDto : BaseReportDto
   {
      public int LocationId { get; init; }
      public string Name { get; init; }
      public DeviceType Type { get; init; }

      public PlcType PlcType =>
         Type switch
         {
            DeviceType.Kamstrup or DeviceType.KamstrupRs500 => PlcType.Meter,
            DeviceType.Rvd145Co or DeviceType.Rvd145CoCwu => PlcType.Rvd145,
            _ => PlcType.None
         };

      public bool IsPlc =>
         Type != DeviceType.Kamstrup &&
         Type != DeviceType.KamstrupRs500;

      public bool IsCo =>
         Type == DeviceType.ClimatixCo ||
         Type == DeviceType.ClimatixCoCwu ||
         Type == DeviceType.Rvd145Co ||
         Type == DeviceType.Rvd145CoCwu;

      public bool IsCwu =>
         Type == DeviceType.ClimatixCoCwu ||
         Type == DeviceType.Rvd145CoCwu;

      public DeviceReportDto()
      {
         Name = string.Empty;
      }
   }
}

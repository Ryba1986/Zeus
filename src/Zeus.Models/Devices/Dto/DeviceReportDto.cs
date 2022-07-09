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
         Type switch
         {
            DeviceType.Kamstrup or DeviceType.KamstrupRs500 => false,
            _ => true
         };

      public bool IsCo1 => IsPlc;

      public bool IsCo2 =>
         Type switch
         {
            DeviceType.ClimatixCoCo or DeviceType.Rvd145CoCo => true,
            _ => false
         };

      public bool IsCwu =>
         Type switch
         {
            DeviceType.ClimatixCoCwu or DeviceType.Rvd145CoCwu => true,
            _ => false
         };

      public DeviceReportDto()
      {
         Name = string.Empty;
      }
   }
}

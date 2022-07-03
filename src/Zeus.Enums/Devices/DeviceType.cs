using System.ComponentModel;

namespace Zeus.Enums.Devices
{
   public enum DeviceType : short
   {
      [Description("Kamstrup")]
      Kamstrup = 1,

      [Description("KamstrupRS500")]
      KamstrupRs500 = 2,


      [Description("ClimatixCo")]
      ClimatixCo = 1000,

      [Description("ClimatixCoCo")]
      ClimatixCoCo = 1010,

      [Description("ClimatixCoCwu")]
      ClimatixCoCwu = 10020,


      [Description("Rvd145Co")]
      Rvd145Co = 1030,

      [Description("Rvd145CoCo")]
      Rvd145CoCo = 1040,

      [Description("Rvd145CoCwu")]
      Rvd145CoCwu = 1050,
   }
}

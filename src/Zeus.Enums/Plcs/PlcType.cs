using System.ComponentModel;

namespace Zeus.Enums.Plcs
{
   public enum PlcType
   {
      [Description("None")]
      None = 0,

      [Description("Meter")]
      Meter = 1,

      [Description("Rvd145")]
      Rvd145 = 2,
   }
}

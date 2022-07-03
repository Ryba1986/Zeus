using System.ComponentModel;

namespace Zeus.Enums.SerialPorts
{
   public enum BoundRate : int
   {
      [Description("2400")]
      B2400 = 2400,

      [Description("4800")]
      B4800 = 4800,

      [Description("9600")]
      B9600 = 9600,

      [Description("19200")]
      B19200 = 19200,

      [Description("38400")]
      B38400 = 38400,
   }
}

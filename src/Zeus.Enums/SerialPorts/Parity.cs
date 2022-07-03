using System.ComponentModel;

namespace Zeus.Enums.SerialPorts
{
   public enum Parity : byte
   {
      [Description("None")]
      None = 0,

      [Description("Odd")]
      Odd = 1,

      [Description("Even")]
      Even = 2
   }
}

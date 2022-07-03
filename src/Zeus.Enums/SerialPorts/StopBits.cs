using System.ComponentModel;

namespace Zeus.Enums.SerialPorts
{
   public enum StopBits : byte
   {
      [Description("Zero")]
      Zero = 0,

      [Description("One")]
      One = 1,

      [Description("Two")]
      Two = 2
   }
}

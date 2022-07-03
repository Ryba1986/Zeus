using System.ComponentModel;

namespace Zeus.Enums.Users
{
   public enum UserRole : byte
   {
      [Description("None")]
      None = 0,

      [Description("User")]
      User = 1,

      [Description("PowerUser")]
      PowerUser = 100,

      [Description("Admin")]
      Admin = 200,

      [Description("SysAdmin")]
      SysAdmin = byte.MaxValue
   }
}

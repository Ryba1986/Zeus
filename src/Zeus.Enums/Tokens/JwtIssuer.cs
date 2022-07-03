using System.ComponentModel;

namespace Zeus.Enums.Tokens
{
   public enum JwtIssuer : byte
   {
      [Description("Client")]
      Client = 1,

      [Description("Web")]
      Web = 2
   }
}

namespace Zeus.Infrastructure.Settings
{
   internal sealed class JwtSettings
   {
      public string Key { get; init; }
      public ushort ExpireMinutes { get; init; }

      public JwtSettings()
      {
         Key = string.Empty;
      }
   }
}

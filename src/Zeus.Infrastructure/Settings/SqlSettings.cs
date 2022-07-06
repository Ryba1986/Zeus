namespace Zeus.Infrastructure.Settings
{
   internal sealed class SqlSettings
   {
      public string Server { get; init; }
      public ushort Port { get; init; }
      public string Database { get; init; }
      public string Username { get; init; }
      public string Password { get; init; }
      public bool Encrypt { get; init; }
      public ushort CommandTimeout { get; init; }

      public SqlSettings()
      {
         Server = string.Empty;
         Database = string.Empty;
         Username = string.Empty;
         Password = string.Empty;
      }
   }
}

namespace Zeus.Infrastructure.Settings
{
   internal sealed class MongoSettings
   {
      public string Server { get; init; }
      public ushort Port { get; init; }
      public string ReplicaSetName { get; init; }
      public string Database { get; init; }
      public string Username { get; init; }
      public string Password { get; init; }

      public MongoSettings()
      {
         Server = string.Empty;
         ReplicaSetName = string.Empty;
         Database = string.Empty;
         Username = string.Empty;
         Password = string.Empty;
      }
   }
}

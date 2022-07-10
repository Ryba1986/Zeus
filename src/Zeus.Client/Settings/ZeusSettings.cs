namespace Zeus.Client.Settings
{
   internal sealed class ZeusSettings
   {
      public string ApiBaseUrl { get; init; }
      public string TorHostPath { get; init; }
      public int PlcRefreshInterval { get; init; }
      public string SerialPortName { get; init; }
      public short SerialPortTimeout { get; init; }

      public ZeusSettings()
      {
         ApiBaseUrl = string.Empty;
         TorHostPath = string.Empty;
         SerialPortName = string.Empty;
      }
   }
}

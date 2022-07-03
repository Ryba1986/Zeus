using Zeus.Domain.Base;

namespace Zeus.Domain.Devices
{
   public sealed class DeviceHistory : BaseHistory
   {
      public int DeviceId { get; init; }
      public string Name { get; init; }
      public string LocationName { get; init; }
      public string Type { get; init; }
      public string ModbusId { get; init; }
      public string RsBoundRate { get; init; }
      public string RsDataBits { get; init; }
      public string RsParity { get; init; }
      public string RsStopBits { get; init; }
      public bool IncludeReport { get; init; }

      public DeviceHistory(int deviceId, string name, string locationName, string type, string modbusId, string rsBoundRate, string rsDataBits, string rsParity, string rsStopBits, bool includeReport, bool isActive, int createdById) : base(isActive, createdById)
      {
         DeviceId = deviceId;
         Name = name;
         LocationName = locationName;
         Type = type;
         ModbusId = modbusId;
         RsBoundRate = rsBoundRate;
         RsDataBits = rsDataBits;
         RsParity = rsParity;
         RsStopBits = rsStopBits;
         IncludeReport = includeReport;
      }
   }
}

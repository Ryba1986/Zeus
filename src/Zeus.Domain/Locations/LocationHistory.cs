using Zeus.Domain.Base;

namespace Zeus.Domain.Locations
{
   public sealed class LocationHistory : BaseHistory
   {
      public int LocationId { get; init; }
      public string Name { get; init; }
      public string MacAddress { get; init; }
      public bool IncludeReport { get; init; }

      public LocationHistory(int locationId, string name, string macAddress, bool includeReport, bool isActive, int createdById) : base(isActive, createdById)
      {
         LocationId = locationId;
         Name = name;
         MacAddress = macAddress;
         IncludeReport = includeReport;
      }
   }
}

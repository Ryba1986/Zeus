using Zeus.Models.Base.Dto;

namespace Zeus.Models.Locations.Dto
{
   public sealed class LocationDto : BaseDto
   {
      public string Name { get; init; }
      public string MacAddress { get; init; }
      public string Hostname { get; init; }
      public string ClientVersion { get; init; }
      public bool IncludeReport { get; init; }

      public LocationDto()
      {
         Name = string.Empty;
         MacAddress = string.Empty;
         Hostname = string.Empty;
         ClientVersion = string.Empty;
      }
   }
}

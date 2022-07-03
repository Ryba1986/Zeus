using Zeus.Models.Base.Dto;

namespace Zeus.Models.Locations.Dto
{
   public sealed class LocationHistoryDto : BaseHistoryDto
   {
      public string Name { get; init; }
      public string MacAddress { get; init; }
      public bool IncludeReport { get; init; }

      public LocationHistoryDto()
      {
         Name = string.Empty;
         MacAddress = string.Empty;
      }
   }
}

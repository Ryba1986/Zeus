using Zeus.Models.Base.Dto;

namespace Zeus.Models.Locations.Dto
{
   public sealed class LocationReportDto : BaseReportDto
   {
      public string Name { get; init; }

      public LocationReportDto()
      {
         Name = string.Empty;
      }
   }
}

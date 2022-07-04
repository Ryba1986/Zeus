using Zeus.Models.Base.Dto;

namespace Zeus.Models.Plcs.Meters.Dto
{
   public sealed class MeterChartDto : BaseChartDto
   {
      public float InletTemp { get; init; }
      public float OutletTemp { get; init; }
      public float Power { get; init; }
      public float Volume { get; init; }
   }
}

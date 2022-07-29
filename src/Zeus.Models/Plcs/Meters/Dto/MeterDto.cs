using Zeus.Models.Base.Dto;

namespace Zeus.Models.Plcs.Meters.Dto
{
   public sealed class MeterDto : BasePlcDto
   {
      public float InletTemp { get; init; }
      public float OutletTemp { get; init; }
      public float Power { get; init; }
      public float Volume { get; init; }
      public float VolumeSummary { get; init; }
      public float EnergySummary { get; init; }
      public int HourCount { get; init; }
      public short ErrorCode { get; init; }
   }
}

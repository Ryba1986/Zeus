using Zeus.Models.Base.Dto;

namespace Zeus.Models.Plcs.Rvds.Dto
{
   public sealed class Rvd145ChartDto : BaseChartDto
   {
      public float OutsideTemp { get; init; }
      public float CoHighInletPresure { get; init; }
      public float Co1HighOutletTemp { get; init; }
      public float Co1LowInletTemp { get; init; }
      public float Co1LowOutletPresure { get; init; }
      public float Co1HeatCurveTemp { get; init; }
      public float CwuTemp { get; init; }
      public float CwuTempSet { get; init; }
   }
}

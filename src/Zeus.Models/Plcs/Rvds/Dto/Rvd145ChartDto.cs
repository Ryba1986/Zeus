using Zeus.Models.Base.Dto;

namespace Zeus.Models.Plcs.Rvds.Dto
{
   public sealed class Rvd145ChartDto : BaseChartDto
   {
      public float OutsideTemp { get; init; }
      public float CoHighInletPresure { get; init; }
      public float CoHighOutletTemp { get; init; }
      public float CoLowInletTemp { get; init; }
      public float CoLowOutletPresure { get; init; }
      public float CoHeatCurveTemp { get; init; }
      public float CwuTemp { get; init; }
      public float CwuTempSet { get; init; }
   }
}

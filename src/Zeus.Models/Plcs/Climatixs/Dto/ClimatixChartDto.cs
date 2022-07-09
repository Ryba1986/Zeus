using Zeus.Models.Base.Dto;

namespace Zeus.Models.Plcs.Climatixs.Dto
{
   public sealed class ClimatixChartDto : BaseChartDto
   {
      public float OutsideTemp { get; init; }
      public float CoHighInletPresure { get; init; }
      public float CoHighOutletPresure { get; init; }

      public float Co1LowInletTemp { get; init; }
      public float Co1LowOutletTemp { get; init; }
      public float Co1LowOutletPresure { get; init; }
      public float Co1HeatCurveTemp { get; init; }

      public float Co2LowInletTemp { get; init; }
      public float Co2LowOutletTemp { get; init; }
      public float Co2LowOutletPresure { get; init; }
      public float Co2HeatCurveTemp { get; init; }

      public float CwuTemp { get; init; }
      public float CwuTempSet { get; init; }
   }
}

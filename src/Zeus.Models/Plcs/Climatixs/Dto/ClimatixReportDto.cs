using Zeus.Models.Base.Dto;

namespace Zeus.Models.Plcs.Climatixs.Dto
{
   public sealed class ClimatixReportDto : BasePlcReportDto
   {
      public float OutsideTempAvg { get; init; }
      public float OutsideTempMin { get; init; }
      public float OutsideTempMax { get; init; }

      public float CoHighInletPresureAvg { get; init; }
      public float CoHighInletPresureMin { get; init; }
      public float CoHighInletPresureMax { get; init; }

      public float CoHighOutletPresureAvg { get; init; }
      public float CoHighOutletPresureMin { get; init; }
      public float CoHighOutletPresureMax { get; init; }

      public float Co1LowInletTempAvg { get; init; }
      public float Co1LowInletTempMin { get; init; }
      public float Co1LowInletTempMax { get; init; }

      public float Co1LowOutletTempAvg { get; init; }
      public float Co1LowOutletTempMin { get; init; }
      public float Co1LowOutletTempMax { get; init; }

      public float Co1LowOutletPresureAvg { get; init; }
      public float Co1LowOutletPresureMin { get; init; }
      public float Co1LowOutletPresureMax { get; init; }

      public float Co1HeatCurveTempAvg { get; init; }
      public float Co1HeatCurveTempMin { get; init; }
      public float Co1HeatCurveTempMax { get; init; }

      public float Co2LowInletTempAvg { get; init; }
      public float Co2LowInletTempMin { get; init; }
      public float Co2LowInletTempMax { get; init; }

      public float Co2LowOutletTempAvg { get; init; }
      public float Co2LowOutletTempMin { get; init; }
      public float Co2LowOutletTempMax { get; init; }

      public float Co2LowOutletPresureAvg { get; init; }
      public float Co2LowOutletPresureMin { get; init; }
      public float Co2LowOutletPresureMax { get; init; }

      public float Co2HeatCurveTempAvg { get; init; }
      public float Co2HeatCurveTempMin { get; init; }
      public float Co2HeatCurveTempMax { get; init; }

      public float CwuTempAvg { get; init; }
      public float CwuTempMin { get; init; }
      public float CwuTempMax { get; init; }
   }
}

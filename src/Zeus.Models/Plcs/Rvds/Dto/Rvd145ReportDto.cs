using Zeus.Models.Base.Dto;

namespace Zeus.Models.Plcs.Rvds.Dto
{
   public sealed class Rvd145ReportDto : BasePlcReportDto
   {
      public float OutsideTempAvg { get; init; }
      public float OutsideTempMin { get; init; }
      public float OutsideTempMax { get; init; }

      public float CoHighInletPresureAvg { get; init; }
      public float CoHighInletPresureMin { get; init; }
      public float CoHighInletPresureMax { get; init; }

      public float Co1LowInletTempAvg { get; init; }
      public float Co1LowInletTempMin { get; init; }
      public float Co1LowInletTempMax { get; init; }

      public float Co1LowOutletPresureAvg { get; init; }
      public float Co1LowOutletPresureMin { get; init; }
      public float Co1LowOutletPresureMax { get; init; }

      public float CwuTempAvg { get; init; }
      public float CwuTempMin { get; init; }
      public float CwuTempMax { get; init; }

      public float CwuCirculationTempAvg { get; init; }
      public float CwuCirculationTempMin { get; init; }
      public float CwuCirculationTempMax { get; init; }
   }
}

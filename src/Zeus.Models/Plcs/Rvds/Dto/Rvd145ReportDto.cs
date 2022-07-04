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
      public float CoLowInletTempAvg { get; init; }
      public float CoLowInletTempMin { get; init; }
      public float CoLowInletTempMax { get; init; }
      public float CoLowOutletPresureAvg { get; init; }
      public float CoLowOutletPresureMin { get; init; }
      public float CoLowOutletPresureMax { get; init; }
      public float CwuTempAvg { get; init; }
      public float CwuTempMin { get; init; }
      public float CwuTempMax { get; init; }
      public float CwuCirculationTempAvg { get; init; }
      public float CwuCirculationTempMin { get; init; }
      public float CwuCirculationTempMax { get; init; }
   }
}

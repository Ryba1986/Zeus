using Zeus.Models.Base.Dto;

namespace Zeus.Models.Plcs.Meters.Dto
{
   public sealed class MeterReportDto : BasePlcReportDto
   {
      public float InletTempAvg { get; init; }
      public float InletTempMin { get; init; }
      public float InletTempMax { get; init; }
      public float OutletTempAvg { get; init; }
      public float OutletTempMin { get; init; }
      public float OutletTempMax { get; init; }
      public float PowerAvg { get; init; }
      public float PowerMin { get; init; }
      public float PowerMax { get; init; }
      public float VolumeAvg { get; init; }
      public float VolumeMin { get; init; }
      public float VolumeMax { get; init; }
      public int VolumeSummary { get; init; }
      public int EnergySummary { get; init; }
   }
}

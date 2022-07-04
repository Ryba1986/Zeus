using Zeus.Models.Base.Commands;

namespace Zeus.Models.Plcs.Meters.Commands
{
   public sealed class CreateMeterCommand : BaseCreatePlcCommand
   {
      public float InletTemp { get; init; }
      public float OutletTemp { get; init; }
      public float Power { get; init; }
      public float Volume { get; init; }
      public int VolumeSummary { get; init; }
      public int EnergySummary { get; init; }
      public int HourCount { get; init; }
      public int SerialNumber { get; init; }
      public short ErrorCode { get; init; }
   }
}

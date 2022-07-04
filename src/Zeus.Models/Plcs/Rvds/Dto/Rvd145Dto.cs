using Zeus.Models.Base.Dto;

namespace Zeus.Models.Plcs.Rvds.Dto
{
   public sealed class Rvd145Dto : BasePlcDto
   {
      public float OutsideTemp { get; init; }
      public float CoHighInletPresure { get; init; }
      public short Alarm { get; init; }

      public float CoHighOutletTemp { get; init; }
      public float CoLowInletTemp { get; init; }
      public float CoLowOutletPresure { get; init; }
      public float CoHeatCurveTemp { get; init; }
      public bool CoPumpStatus { get; init; }
      public bool CoStatus { get; init; }

      public float CwuTemp { get; init; }
      public float CwuTempSet { get; init; }
      public float CwuCirculationTemp { get; init; }
      public bool CwuPumpStatus { get; init; }
      public bool CwuStatus { get; init; }
   }
}

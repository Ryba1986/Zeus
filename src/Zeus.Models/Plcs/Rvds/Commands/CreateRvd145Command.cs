using Zeus.Models.Base.Commands;

namespace Zeus.Models.Plcs.Rvds.Commands
{
   public sealed class CreateRvd145Command : BaseCreatePlcCommand
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

using Zeus.Models.Base.Commands;

namespace Zeus.Models.Plcs.Rvds.Commands
{
   public sealed class CreateRvd145Command : BaseCreatePlcCommand
   {
      public float OutsideTemp { get; init; }
      public float CoHighInletPresure { get; init; }
      public short Alarm { get; init; }

      public float Co1HighOutletTemp { get; init; }
      public float Co1LowInletTemp { get; init; }
      public float Co1LowOutletPresure { get; init; }
      public float Co1HeatCurveTemp { get; init; }
      public bool Co1PumpStatus { get; init; }
      public bool Co1Status { get; init; }

      public float CwuTemp { get; init; }
      public float CwuTempSet { get; init; }
      public float CwuCirculationTemp { get; init; }
      public bool CwuPumpStatus { get; init; }
      public bool CwuStatus { get; init; }
   }
}

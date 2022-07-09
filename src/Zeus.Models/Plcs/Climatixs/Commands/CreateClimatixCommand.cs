using Zeus.Models.Base.Commands;

namespace Zeus.Models.Plcs.Climatixs.Commands
{
   public sealed class CreateClimatixCommand : BaseCreatePlcCommand
   {
      public float OutsideTemp { get; init; }
      public float CoHighInletPresure { get; init; }
      public float CoHighOutletPresure { get; init; }
      public bool Alarm { get; init; }

      public float Co1LowInletTemp { get; init; }
      public float Co1LowOutletTemp { get; init; }
      public float Co1LowOutletPresure { get; init; }
      public float Co1HeatCurveTemp { get; init; }
      public bool Co1PumpAlarm { get; init; }
      public bool Co1PumpStatus { get; init; }
      public byte Co1ValvePosition { get; init; }
      public bool Co1Status { get; init; }

      public float Co2LowInletTemp { get; init; }
      public float Co2LowOutletTemp { get; init; }
      public float Co2LowOutletPresure { get; init; }
      public float Co2HeatCurveTemp { get; init; }
      public bool Co2PumpAlarm { get; init; }
      public bool Co2PumpStatus { get; init; }
      public byte Co2ValvePosition { get; init; }
      public bool Co2Status { get; init; }

      public float CwuTemp { get; init; }
      public float CwuTempSet { get; init; }
      public bool CwuPumpAlarm { get; init; }
      public bool CwuPumpStatus { get; init; }
      public byte CwuValvePosition { get; init; }
      public bool CwuStatus { get; init; }
   }
}

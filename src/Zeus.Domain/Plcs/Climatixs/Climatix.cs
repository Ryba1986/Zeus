using System;
using Zeus.Domain.Base;
using Zeus.Utilities.Extensions;

namespace Zeus.Domain.Plcs.Climatixs
{
   public sealed class Climatix : BasePlc
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

      public Climatix(float outsideTemp, float coHighInletPresure, float coHighOutletPresure, bool alarm, float co1LowInletTemp, float co1LowOutletTemp, float co1LowOutletPresure, float co1HeatCurveTemp, bool co1PumpAlarm, bool co1PumpStatus, byte co1ValvePosition, bool co1Status, float co2LowInletTemp, float co2LowOutletTemp, float co2LowOutletPresure, float co2HeatCurveTemp, bool co2PumpAlarm, bool co2PumpStatus, byte co2ValvePosition, bool co2Status, float cwuTemp, float cwuTempSet, bool cwuPumpAlarm, bool cwuPumpStatus, byte cwuValvePosition, bool cwuStatus, DateTime date, int deviceId) : base(date, deviceId)
      {
         OutsideTemp = outsideTemp.Round();
         CoHighInletPresure = coHighInletPresure.Round();
         CoHighOutletPresure = coHighOutletPresure.Round();
         Alarm = alarm;

         Co1LowInletTemp = co1LowInletTemp.Round();
         Co1LowOutletTemp = co1LowOutletTemp.Round();
         Co1LowOutletPresure = co1LowOutletPresure.Round();
         Co1HeatCurveTemp = co1HeatCurveTemp.Round();
         Co1PumpAlarm = co1PumpAlarm;
         Co1PumpStatus = co1PumpStatus;
         Co1ValvePosition = co1ValvePosition;
         Co1Status = co1Status;

         Co2LowInletTemp = co2LowInletTemp.Round();
         Co2LowOutletTemp = co2LowOutletTemp.Round();
         Co2LowOutletPresure = co2LowOutletPresure.Round();
         Co2HeatCurveTemp = co2HeatCurveTemp.Round();
         Co2PumpAlarm = co2PumpAlarm;
         Co2PumpStatus = co2PumpStatus;
         Co2ValvePosition = co2ValvePosition;
         Co2Status = co2Status;

         CwuTemp = cwuTemp.Round();
         CwuTempSet = cwuTempSet.Round();
         CwuPumpAlarm = cwuPumpAlarm;
         CwuPumpStatus = cwuPumpStatus;
         CwuValvePosition = cwuValvePosition;
         CwuStatus = cwuStatus;
      }
   }
}

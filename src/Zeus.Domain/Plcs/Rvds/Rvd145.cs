using System;
using Zeus.Domain.Base;
using Zeus.Utilities.Extensions;

namespace Zeus.Domain.Plcs.Rvds
{
   public sealed class Rvd145 : BasePlc
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

      public Rvd145(float outsideTemp, float coHighInletPresure, short alarm, float co1HighOutletTemp, float co1LowInletTemp, float co1LowOutletPresure, float co1HeatCurveTemp, bool co1PumpStatus, bool co1Status, float cwuTemp, float cwuTempSet, float cwuCirculationTemp, bool cwuPumpStatus, bool cwuStatus, DateTime date, int deviceId) : base(date, deviceId)
      {
         OutsideTemp = outsideTemp.Round();
         CoHighInletPresure = coHighInletPresure.Round();
         Alarm = alarm;

         Co1HighOutletTemp = co1HighOutletTemp.Round();
         Co1LowInletTemp = co1LowInletTemp.Round();
         Co1LowOutletPresure = co1LowOutletPresure.Round();
         Co1HeatCurveTemp = co1HeatCurveTemp.Round();
         Co1PumpStatus = co1PumpStatus;
         Co1Status = co1Status;

         CwuTemp = cwuTemp.Round();
         CwuTempSet = cwuTempSet.Round();
         CwuCirculationTemp = cwuCirculationTemp.Round();
         CwuPumpStatus = cwuPumpStatus;
         CwuStatus = cwuStatus;
      }
   }
}

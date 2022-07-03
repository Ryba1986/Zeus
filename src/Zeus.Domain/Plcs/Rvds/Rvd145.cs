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

      public Rvd145(float outsideTemp, float coHighInletPresure, short alarm, float coHighOutletTemp, float coLowInletTemp, float coLowOutletPresure, float coHeatCurveTemp, bool coPumpStatus, bool coStatus, float cwuTemp, float cwuTempSet, float cwuCirculationTemp, bool cwuPumpStatus, bool cwuStatus, int deviceId, DateTime date) : base(deviceId, date)
      {
         OutsideTemp = outsideTemp.Round();
         CoHighInletPresure = coHighInletPresure.Round();
         Alarm = alarm;

         CoHighOutletTemp = coHighOutletTemp.Round();
         CoLowInletTemp = coLowInletTemp.Round();
         CoLowOutletPresure = coLowOutletPresure.Round();
         CoHeatCurveTemp = coHeatCurveTemp.Round();
         CoPumpStatus = coPumpStatus;
         CoStatus = coStatus;

         CwuTemp = cwuTemp.Round();
         CwuTempSet = cwuTempSet.Round();
         CwuCirculationTemp = cwuCirculationTemp.Round();
         CwuPumpStatus = cwuPumpStatus;
         CwuStatus = cwuStatus;
      }
   }
}

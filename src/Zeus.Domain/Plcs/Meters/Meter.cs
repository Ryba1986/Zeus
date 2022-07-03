using System;
using Zeus.Domain.Base;
using Zeus.Utilities.Extensions;

namespace Zeus.Domain.Plcs.Meters
{
   public sealed class Meter : BasePlc
   {
      public float InletTemp { get; init; }
      public float OutletTemp { get; init; }
      public float Power { get; init; }
      public float Volume { get; init; }
      public int VolumeSummary { get; init; }
      public int EnergySummary { get; init; }
      public int HourCount { get; init; }
      public short ErrorCode { get; init; }

      public Meter(float inletTemp, float outletTemp, float power, float volume, int volumeSummary, int energySummary, int hourCount, short errorCode, int deviceId, DateTime date) : base(deviceId, date)
      {
         InletTemp = inletTemp.Round();
         OutletTemp = outletTemp.Round();
         Power = power.Round();
         Volume = volume.Round();
         VolumeSummary = volumeSummary;
         EnergySummary = energySummary;
         HourCount = hourCount;
         ErrorCode = errorCode;
      }
   }
}

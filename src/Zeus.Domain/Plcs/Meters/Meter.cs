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
      public float VolumeSummary { get; init; }
      public float EnergySummary { get; init; }
      public int HourCount { get; init; }
      public short ErrorCode { get; init; }

      public Meter(float inletTemp, float outletTemp, float power, float volume, float volumeSummary, float energySummary, int hourCount, short errorCode, DateTime date, int deviceId) : base(date, deviceId)
      {
         InletTemp = inletTemp.Round();
         OutletTemp = outletTemp.Round();
         Power = power.Round();
         Volume = volume.Round();
         VolumeSummary = volumeSummary.Round();
         EnergySummary = energySummary.Round();
         HourCount = hourCount;
         ErrorCode = errorCode;
      }
   }
}

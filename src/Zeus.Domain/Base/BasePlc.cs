using System;
using Zeus.Utilities.Extensions;

namespace Zeus.Domain.Base
{
   public abstract class BasePlc
   {
      public DateTime Date { get; init; }
      public int DeviceId { get; init; }

      public BasePlc(DateTime date, int deviceId)
      {
         Date = date.RoundToSecond();
         DeviceId = deviceId;
      }
   }
}

using System;
using Zeus.Utilities.Extensions;
using Zeus.Utilities.Helpers;

namespace Zeus.Domain.Base
{
   public abstract class BasePlc
   {
      public long Id { get; init; }
      public int DeviceId { get; init; }
      public DateTime Date { get; init; }


      public BasePlc(int deviceId, DateTime date)
      {
         Id = RandomHelper.CreateLong();

         DeviceId = deviceId;
         Date = date.RoundToSecond();
      }
   }
}

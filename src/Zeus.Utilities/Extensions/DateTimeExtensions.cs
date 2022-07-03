using System;

namespace Zeus.Utilities.Extensions
{
   public static class DateTimeExtensions
   {
      public static long ToTimestamp(this DateTime dateTime)
      {
         DateTime epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
         DateTime time = dateTime.Subtract(new TimeSpan(epoch.Ticks));

         return time.Ticks / 10000;
      }

      public static DateTime RoundToSecond(this DateTime date)
      {
         return new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
      }
   }
}

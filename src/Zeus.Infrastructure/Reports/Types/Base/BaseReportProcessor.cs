using System;
using Zeus.Models.Utilities;

namespace Zeus.Infrastructure.Reports.Types.Base
{
   internal abstract class BaseReportProcessor
   {
      public StartingPoints StartingPoints { get; init; }

      public BaseReportProcessor()
      {
         StartingPoints = new()
         {
            MeterColumn = 33,
            PlcColumn = 2,
            Row = 8
         };
      }

      protected static string GetHeader(string value, string locationName, DateOnly date, string dateFormat)
      {
         return value
            .Replace("{{LOCATION}}", locationName)
            .Replace("{{DATE}}", date.ToString(dateFormat));
      }
   }
}

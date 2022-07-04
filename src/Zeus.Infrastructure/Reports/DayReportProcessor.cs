using System;
using System.Linq.Expressions;
using Zeus.Domain.Base;
using Zeus.Enums.Reports;
using Zeus.Infrastructure.Reports.Base;
using Zeus.Models.Utilities;

namespace Zeus.Infrastructure.Reports
{
   internal sealed class DayReportProcessor : BaseReportProcessor, IReportProcessor
   {
      public ReportType ReportType { get; init; }
      public short SummaryRowOffset { get; init; }

      public DayReportProcessor()
      {
         ReportType = ReportType.Day;
         SummaryRowOffset = 24;
      }

      public int GetDatePart(DateTime date)
      {
         return date.Hour;
      }

      public string GetFileName(DateOnly date)
      {
         return $"Report_{date:yyyy-MM-dd}.xlsx";
      }

      public string GetHeader(string value, string locationName, DateOnly date)
      {
         return GetHeader(value, locationName, date, "yyyy-MM-dd");
      }

      public Expression<Func<T, int>> GetPlcGroup<T>() where T : BasePlc
      {
         return x => x.Date.Hour;
      }

      public DateRange GetRange(DateOnly date)
      {
         DateTime dateTime = date.ToDateTime(TimeOnly.MinValue);

         return new()
         {
            Start = dateTime,
            End = dateTime.AddDays(1)
         };
      }
   }
}

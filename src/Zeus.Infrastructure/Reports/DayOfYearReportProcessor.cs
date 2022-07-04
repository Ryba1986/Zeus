using System;
using System.Linq.Expressions;
using Zeus.Domain.Base;
using Zeus.Enums.Reports;
using Zeus.Infrastructure.Reports.Base;
using Zeus.Models.Utilities;

namespace Zeus.Infrastructure.Reports
{
   internal sealed class DayOfYearReportProcessor : BaseReportProcessor, IReportProcessor
   {
      public ReportType ReportType { get; init; }
      public short SummaryRowOffset { get; init; }

      public DayOfYearReportProcessor()
      {
         ReportType = ReportType.DayOfYear;
         SummaryRowOffset = 366;
      }

      public int GetDatePart(DateTime date)
      {
         return date.DayOfYear - 1;
      }

      public string GetFileName(DateOnly date)
      {
         return $"Report_{date:yyyy}_365.xlsx";
      }

      public string GetHeader(string value, string locationName, DateOnly date)
      {
         return GetHeader(value, locationName, date, "yyyy");
      }

      public Expression<Func<T, int>> GetPlcGroup<T>() where T : BasePlc
      {
         return x => x.Date.DayOfYear;
      }

      public DateRange GetRange(DateOnly date)
      {
         DateTime dateTime = new(date.Year, 1, 1);

         return new()
         {
            Start = dateTime,
            End = dateTime.AddYears(1)
         };
      }
   }
}

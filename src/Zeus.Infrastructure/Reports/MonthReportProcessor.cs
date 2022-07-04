using System;
using System.Linq.Expressions;
using Zeus.Domain.Base;
using Zeus.Enums.Reports;
using Zeus.Infrastructure.Reports.Base;
using Zeus.Models.Utilities;

namespace Zeus.Infrastructure.Reports
{
   internal sealed class MonthReportProcessor : BaseReportProcessor, IReportProcessor
   {
      public ReportType ReportType { get; init; }
      public short SummaryRowOffset { get; init; }

      public MonthReportProcessor()
      {
         ReportType = ReportType.Month;
         SummaryRowOffset = 31;
      }

      public int GetDatePart(DateTime date)
      {
         return date.Day - 1;
      }

      public string GetFileName(DateOnly date)
      {
         return $"Report_{date:yyyy-MM}.xlsx";
      }

      public string GetHeader(string value, string locationName, DateOnly date)
      {
         return GetHeader(value, locationName, date, "yyyy-MM");
      }

      public Expression<Func<T, int>> GetPlcGroup<T>() where T : BasePlc
      {
         return x => x.Date.Day;
      }

      public DateRange GetRange(DateOnly date)
      {
         DateTime dateTime = new(date.Year, date.Month, 1);

         return new()
         {
            Start = dateTime,
            End = dateTime.AddMonths(1)
         };
      }
   }
}

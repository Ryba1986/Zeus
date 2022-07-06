using System;
using System.Linq.Expressions;
using Zeus.Domain.Base;
using Zeus.Enums.Reports;
using Zeus.Models.Utilities;

namespace Zeus.Infrastructure.Reports.Types.Base
{
   internal interface IReportProcessor
   {
      ReportType ReportType { get; init; }
      StartingPoints StartingPoints { get; init; }
      short SummaryRowOffset { get; init; }

      int GetDatePart(DateTime date);
      string GetFileName(DateOnly date);
      string GetHeader(string value, string locationName, DateOnly date);
      Expression<Func<T, PlcGroupBy>> GetPlcGroup<T>() where T : BasePlc;
      DateRange GetRange(DateOnly date);
   }
}

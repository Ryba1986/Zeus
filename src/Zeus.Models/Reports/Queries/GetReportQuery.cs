using System;
using MediatR;
using Zeus.Enums.Reports;
using Zeus.Enums.System;
using Zeus.Models.Reports.Dto;

namespace Zeus.Models.Reports.Queries
{
   public sealed class GetReportQuery : IRequest<ReportFileDto>
   {
      public DateOnly Date { get; init; }
      public Language Language { get; init; }
      public ReportType Type { get; init; }

      public GetReportQuery()
      {
         Date = DateOnly.FromDateTime(DateTime.Now);
         Language = Language.English;
         Type = ReportType.Day;
      }
   }
}

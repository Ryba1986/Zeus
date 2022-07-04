using Autofac;
using Zeus.Enums.Reports;
using Zeus.Infrastructure.Reports;
using Zeus.Infrastructure.Reports.Base;

namespace Zeus.Infrastructure.Configuration.Modules
{
   internal sealed class ReportModule : Module
   {
      protected override void Load(ContainerBuilder builder)
      {
         builder
            .RegisterType<DayReportProcessor>()
            .Keyed<IReportProcessor>(ReportType.Day);

         builder
            .RegisterType<MonthReportProcessor>()
            .Keyed<IReportProcessor>(ReportType.Month);

         builder
            .RegisterType<YearReportProcessor>()
            .Keyed<IReportProcessor>(ReportType.Year);

         builder
            .RegisterType<DayOfYearReportProcessor>()
            .Keyed<IReportProcessor>(ReportType.DayOfYear);
      }
   }
}

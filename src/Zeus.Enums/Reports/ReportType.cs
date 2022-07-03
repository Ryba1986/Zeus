using System.ComponentModel;

namespace Zeus.Enums.Reports
{
   public enum ReportType : byte
   {
      [Description("Day")]
      Day = 1,

      [Description("Month")]
      Month = 2,

      [Description("Year")]
      Year = 3,

      [Description("DayOfYear")]
      DayOfYear = 4
   }
}

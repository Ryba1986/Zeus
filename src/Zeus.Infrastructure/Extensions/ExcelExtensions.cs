using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using Zeus.Enums.System;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Reports.Queries;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Extensions
{
   internal static class ExcelExtensions
   {
      public static ExcelPackage GetReportTemplate(this Language language)
      {
         return new(new($"{AppContext.BaseDirectory}Templates{Path.DirectorySeparatorChar}Reports_{language.GetDescription()}.xlsx"), true);
      }

      public static IReadOnlyCollection<string> GetSheetNames(this ExcelWorksheets sheets)
      {
         return sheets
            .Select(x => x.Name)
            .ToArray();
      }

      public static ExcelWorksheet CloneSheet(this ExcelWorksheets sheets, GetReportQuery request, LocationReportDto location)
      {
         return sheets.Copy($"{request.Type.GetDescription()}_{request.Language.GetDescription()}", location.Name);
      }

      public static void RemoveSheets(this ExcelWorksheets sheets, IReadOnlyCollection<string> sheetNames)
      {
         foreach (string name in sheetNames)
         {
            sheets.Delete(name);
         }
         if (sheets.Count == 0)
         {
            sheets.Add("NO DATA");
         }

         sheets
            .First()
            .Select();
      }
   }
}

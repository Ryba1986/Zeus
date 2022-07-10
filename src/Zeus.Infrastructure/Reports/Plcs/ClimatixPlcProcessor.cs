using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using OfficeOpenXml;
using Zeus.Domain.Plcs.Climatixs;
using Zeus.Infrastructure.Reports.Plcs.Base;
using Zeus.Infrastructure.Reports.Types.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Plcs.Climatixs.Dto;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Reports.Plcs
{
   internal sealed class ClimatixPlcProcessor : BasePlcProcessor, IPlcProcessor
   {
      public async Task FillDataAsync(UnitOfWork uow, ExcelWorksheets sheets, DateOnly date, IReadOnlyCollection<LocationReportDto> locations, IReadOnlyCollection<DeviceReportDto> devices, IReportProcessor reportProcessor, TypeAdapterConfig mapper, CancellationToken cancellationToken)
      {
         IReadOnlyDictionary<int, ClimatixReportDto[]> plcData = await GetPlcDataAsync<Climatix, ClimatixReportDto>(uow.Climatix, date, reportProcessor, mapper, cancellationToken);
         if (plcData.Count == 0)
         {
            return;
         }

         foreach (LocationReportDto location in locations)
         {
            foreach (DeviceReportDto device in devices.Where(x => x.LocationId == location.Id))
            {
               if (!plcData.ContainsKey(device.Id))
               {
                  continue;
               }

               FillSheet(sheets[location.Name], device, plcData[device.Id], reportProcessor);
            }
         }
      }

      private static void FillSheet(ExcelWorksheet sheet, DeviceReportDto device, IReadOnlyCollection<ClimatixReportDto> data, IReportProcessor reportProcessor)
      {
         foreach (ClimatixReportDto climatix in data)
         {
            int rowIndex = reportProcessor.StartingPoints.Row + reportProcessor.GetDatePart(climatix.Date);

            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 0].Value = climatix.OutsideTempAvg.Round();
            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 1].Value = climatix.OutsideTempMin.Round();
            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 2].Value = climatix.OutsideTempMax.Round();

            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 3].Value = climatix.CoHighInletPresureAvg.Round();
            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 4].Value = climatix.CoHighInletPresureMin.Round();
            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 5].Value = climatix.CoHighInletPresureMax.Round();

            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 6].Value = climatix.CoHighOutletPresureAvg.Round();
            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 7].Value = climatix.CoHighOutletPresureMin.Round();
            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 8].Value = climatix.CoHighOutletPresureMax.Round();

            if (device.IsCo1)
            {
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 9].Value = climatix.Co1LowInletTempAvg.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 10].Value = climatix.Co1LowInletTempMin.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 11].Value = climatix.Co1LowInletTempMax.Round();

               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 12].Value = climatix.Co1LowOutletTempAvg.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 13].Value = climatix.Co1LowOutletTempMin.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 14].Value = climatix.Co1LowOutletTempMax.Round();

               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 15].Value = climatix.Co1LowOutletPresureAvg.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 16].Value = climatix.Co1LowOutletPresureMin.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 17].Value = climatix.Co1LowOutletPresureMax.Round();
            }

            if (device.IsCo2)
            {
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 18].Value = climatix.Co2LowInletTempAvg.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 19].Value = climatix.Co2LowInletTempMin.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 20].Value = climatix.Co2LowInletTempMax.Round();

               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 21].Value = climatix.Co2LowOutletTempAvg.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 22].Value = climatix.Co2LowOutletTempMin.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 23].Value = climatix.Co2LowOutletTempMax.Round();

               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 24].Value = climatix.Co2LowOutletPresureAvg.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 25].Value = climatix.Co2LowOutletPresureMin.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 26].Value = climatix.Co2LowOutletPresureMax.Round();
            }

            if (device.IsCwu)
            {
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 27].Value = climatix.CwuTempAvg.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 28].Value = climatix.CwuTempMin.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + 29].Value = climatix.CwuTempMax.Round();
            }
         }

         int summaryRowIndex = reportProcessor.StartingPoints.Row + reportProcessor.SummaryRowOffset;

         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 0].Value = data.Average(x => x.OutsideTempAvg).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 1].Value = data.Min(x => x.OutsideTempMin).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 2].Value = data.Max(x => x.OutsideTempMax).Round();

         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 3].Value = data.Average(x => x.CoHighInletPresureAvg).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 4].Value = data.Min(x => x.CoHighInletPresureMin).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 5].Value = data.Max(x => x.CoHighInletPresureMax).Round();

         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 6].Value = data.Average(x => x.CoHighOutletPresureAvg).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 7].Value = data.Min(x => x.CoHighOutletPresureMin).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 8].Value = data.Max(x => x.CoHighOutletPresureMax).Round();

         if (device.IsCo1)
         {
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 9].Value = data.Average(x => x.Co1LowInletTempAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 10].Value = data.Min(x => x.Co1LowInletTempMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 11].Value = data.Max(x => x.Co1LowInletTempMax).Round();

            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 12].Value = data.Average(x => x.Co1LowOutletTempAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 13].Value = data.Min(x => x.Co1LowOutletTempMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 14].Value = data.Max(x => x.Co1LowOutletTempMax).Round();

            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 15].Value = data.Average(x => x.Co1LowOutletPresureAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 16].Value = data.Min(x => x.Co1LowOutletPresureMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 17].Value = data.Max(x => x.Co1LowOutletPresureMax).Round();
         }

         if (device.IsCo2)
         {
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 18].Value = data.Average(x => x.Co2LowInletTempAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 19].Value = data.Min(x => x.Co2LowInletTempMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 20].Value = data.Max(x => x.Co2LowInletTempMax).Round();

            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 21].Value = data.Average(x => x.Co2LowOutletTempAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 22].Value = data.Min(x => x.Co2LowOutletTempMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 23].Value = data.Max(x => x.Co2LowOutletTempMax).Round();

            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 24].Value = data.Average(x => x.Co2LowOutletPresureAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 25].Value = data.Min(x => x.Co2LowOutletPresureMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 26].Value = data.Max(x => x.Co2LowOutletPresureMax).Round();
         }

         if (device.IsCwu)
         {
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 27].Value = data.Average(x => x.CwuTempAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 28].Value = data.Min(x => x.CwuTempMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + 29].Value = data.Max(x => x.CwuTempMax).Round();
         }
      }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using OfficeOpenXml;
using Zeus.Domain.Plcs.Rvds;
using Zeus.Infrastructure.Reports.Plcs.Base;
using Zeus.Infrastructure.Reports.Types.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Plcs.Rvds.Dto;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Reports.Plcs
{
   internal sealed class Rvd145PlcProcessor : BasePlcProcessor, IPlcProcessor
   {
      public async Task FillDataAsync(UnitOfWork uow, ExcelWorksheets sheets, DateOnly date, IReadOnlyCollection<LocationReportDto> locations, IReadOnlyCollection<DeviceReportDto> devices, IReportProcessor reportProcessor, TypeAdapterConfig mapper, CancellationToken cancellationToken)
      {
         IReadOnlyDictionary<int, Rvd145ReportDto[]> plcData = await GetPlcDataAsync<Rvd145, Rvd145ReportDto>(uow.Rvd145, date, reportProcessor, mapper, cancellationToken);
         if (plcData.Count == 0)
         {
            return;
         }

         foreach (LocationReportDto location in locations)
         {
            ExcelWorksheet sheet = sheets[location.Name];

            foreach (DeviceReportDto device in devices.Where(x => x.LocationId == location.Id))
            {
               if (!plcData.ContainsKey(device.Id) || plcData[device.Id].Length == 0)
               {
                  continue;
               }

               FillSheet(sheet, device, plcData[device.Id], reportProcessor);
            }
         }
      }

      private static void FillSheet(ExcelWorksheet sheet, DeviceReportDto device, IReadOnlyCollection<Rvd145ReportDto> data, IReportProcessor reportProcessor)
      {
         foreach (Rvd145ReportDto rvd in data)
         {
            int rowIndex = reportProcessor.StartingPoints.Row + reportProcessor.GetDatePart(rvd.Date);
            int colIndex = 0;

            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.OutsideTempAvg.Round();
            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.OutsideTempMin.Round();
            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.OutsideTempMax.Round();

            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CoHighInletPresureAvg.Round();
            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CoHighInletPresureMin.Round();
            sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CoHighInletPresureMax.Round();

            if (device.IsCo)
            {
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CoLowInletTempAvg.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CoLowInletTempMin.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CoLowInletTempMax.Round();

               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CoLowOutletPresureAvg.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CoLowOutletPresureMin.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CoLowOutletPresureMax.Round();
            }

            if (device.IsCwu)
            {
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CwuTempAvg.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CwuTempMin.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CwuTempMax.Round();

               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CwuCirculationTempAvg.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CwuCirculationTempMin.Round();
               sheet.Cells[rowIndex, reportProcessor.StartingPoints.PlcColumn + colIndex++].Value = rvd.CwuCirculationTempMax.Round();
            }
         }

         int summaryRowIndex = reportProcessor.StartingPoints.Row + reportProcessor.SummaryRowOffset;
         int summaryColIndex = 0;

         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Average(x => x.OutsideTempAvg).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Min(x => x.OutsideTempMin).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Max(x => x.OutsideTempMax).Round();

         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Average(x => x.CoHighInletPresureAvg).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Min(x => x.CoHighInletPresureMin).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Max(x => x.CoHighInletPresureMax).Round();

         if (device.IsCo)
         {
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Average(x => x.CoLowInletTempAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Min(x => x.CoLowInletTempMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Max(x => x.CoLowInletTempMax).Round();

            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Average(x => x.CoLowOutletPresureAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Min(x => x.CoLowOutletPresureMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Max(x => x.CoLowOutletPresureMax).Round();
         }

         if (device.IsCwu)
         {
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Average(x => x.CwuTempAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Min(x => x.CwuTempMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Max(x => x.CwuTempMax).Round();

            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Average(x => x.CwuCirculationTempAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Min(x => x.CwuCirculationTempMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = data.Max(x => x.CwuCirculationTempMax).Round();
         }
      }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using OfficeOpenXml;
using Zeus.Domain.Plcs.Rvds;
using Zeus.Infrastructure.Reports.Plcs.Base;
using Zeus.Infrastructure.Reports.Types.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Plcs.Rvds.Dto;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Reports.Plcs
{
   internal sealed class Rvd145PlcProcessor : BasePlcProcessor, IPlcProcessor
   {
      private readonly Expression<Func<IGrouping<int, Rvd145>, Rvd145ReportDto>> _selector;

      public Rvd145PlcProcessor()
      {
         _selector = x => new()
         {
            Date = x.Min(g => g.Date),
            OutsideTempAvg = x.Average(g => g.OutsideTemp),
            OutsideTempMin = x.Min(g => g.OutsideTemp),
            OutsideTempMax = x.Max(g => g.OutsideTemp),
            CoHighInletPresureAvg = x.Average(g => g.CoHighInletPresure),
            CoHighInletPresureMin = x.Min(g => g.CoHighInletPresure),
            CoHighInletPresureMax = x.Max(g => g.CoHighInletPresure),
            CoLowInletTempAvg = x.Average(g => g.CoLowInletTemp),
            CoLowInletTempMin = x.Min(g => g.CoLowInletTemp),
            CoLowInletTempMax = x.Max(g => g.CoLowInletTemp),
            CoLowOutletPresureAvg = x.Average(g => g.CoLowOutletPresure),
            CoLowOutletPresureMin = x.Min(g => g.CoLowOutletPresure),
            CoLowOutletPresureMax = x.Max(g => g.CoLowOutletPresure),
            CwuTempAvg = x.Average(g => g.CwuTemp),
            CwuTempMin = x.Min(g => g.CwuTemp),
            CwuTempMax = x.Max(g => g.CwuTemp),
            CwuCirculationTempAvg = x.Average(g => g.CwuCirculationTemp),
            CwuCirculationTempMin = x.Min(g => g.CwuCirculationTemp),
            CwuCirculationTempMax = x.Max(g => g.CwuCirculationTemp),
         };
      }

      public async Task FillDataAsync(UnitOfWork uow, ExcelWorksheet sheet, DateOnly date, IReadOnlyCollection<DeviceReportDto> devices, IReportProcessor reportProcessor, IMapper mapper, CancellationToken cancellationToken)
      {
         foreach (DeviceReportDto device in devices)
         {
            IReadOnlyCollection<Rvd145ReportDto> plcData = await GetPlcDataAsync(uow.Rvd145.AsQueryable(), date, device, reportProcessor, _selector, cancellationToken);
            if (plcData.Count == 0)
            {
               return;
            }

            FillSheet(sheet, device, plcData, reportProcessor);
         }
      }

      private static void FillSheet(ExcelWorksheet sheet, DeviceReportDto device, IReadOnlyCollection<Rvd145ReportDto> locationData, IReportProcessor reportProcessor)
      {
         foreach (Rvd145ReportDto rvd in locationData)
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

         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Average(x => x.OutsideTempAvg).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Min(x => x.OutsideTempMin).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Max(x => x.OutsideTempMax).Round();

         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Average(x => x.CoHighInletPresureAvg).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Min(x => x.CoHighInletPresureMin).Round();
         sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Max(x => x.CoHighInletPresureMax).Round();

         if (device.IsCo)
         {
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Average(x => x.CoLowInletTempAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Min(x => x.CoLowInletTempMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Max(x => x.CoLowInletTempMax).Round();

            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Average(x => x.CoLowOutletPresureAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Min(x => x.CoLowOutletPresureMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Max(x => x.CoLowOutletPresureMax).Round();
         }

         if (device.IsCwu)
         {
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Average(x => x.CwuTempAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Min(x => x.CwuTempMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Max(x => x.CwuTempMax).Round();

            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Average(x => x.CwuCirculationTempAvg).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Min(x => x.CwuCirculationTempMin).Round();
            sheet.Cells[summaryRowIndex, reportProcessor.StartingPoints.PlcColumn + summaryColIndex++].Value = locationData.Max(x => x.CwuCirculationTempMax).Round();
         }
      }
   }
}

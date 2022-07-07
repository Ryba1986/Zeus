using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Zeus.Domain.Plcs.Meters;
using Zeus.Infrastructure.Reports.Plcs.Base;
using Zeus.Infrastructure.Reports.Types.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Plcs.Meters.Dto;
using Zeus.Models.Utilities;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Reports.Plcs
{
   internal sealed class MeterPlcProcessor : BasePlcProcessor, IPlcProcessor
   {
      public async Task FillDataAsync(UnitOfWork uow, ExcelWorksheets sheets, DateOnly date, IReadOnlyCollection<LocationReportDto> locations, IReadOnlyCollection<DeviceReportDto> devices, IReportProcessor reportProcessor, TypeAdapterConfig mapper, CancellationToken cancellationToken)
      {
         IReadOnlyDictionary<int, MeterReportDto[]> plcData = await GetPlcDataAsync<Meter, MeterReportDto>(uow.Meter, date, reportProcessor, mapper, cancellationToken);
         if (plcData.Count == 0)
         {
            return;
         }

         IReadOnlyDictionary<int, MeterDto> beforeData = await GetBeforeDataAsync(uow.Meter, date, devices, reportProcessor, mapper, cancellationToken);
         if (beforeData.Count == 0)
         {
            return;
         }

         foreach (LocationReportDto location in locations)
         {
            ExcelWorksheet sheet = sheets[location.Name];
            int startColumn = reportProcessor.StartingPoints.MeterColumn;

            foreach (DeviceReportDto device in devices.Where(x => x.LocationId == location.Id))
            {
               if (!plcData.ContainsKey(device.Id) || plcData[device.Id].Length == 0)
               {
                  continue;
               }

               startColumn += FillSheet(sheet, device, beforeData[device.Id], plcData[device.Id], startColumn, reportProcessor);
            }
         }
      }

      private static int FillSheet(ExcelWorksheet sheet, DeviceReportDto device, MeterDto beforeMeter, IReadOnlyCollection<MeterReportDto> currentData, int startColumn, IReportProcessor reportProcessor)
      {
         float beforeVolumeSummary = beforeMeter.VolumeSummary;
         float beforeEnergySummary = beforeMeter.EnergySummary;

         foreach (MeterReportDto meter in currentData)
         {
            int rowIndex = reportProcessor.StartingPoints.Row + reportProcessor.GetDatePart(meter.Date);
            int colIndex = 0;

            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.InletTempAvg.Round();
            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.InletTempMin.Round();
            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.InletTempMax.Round();

            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.OutletTempAvg.Round();
            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.OutletTempMin.Round();
            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.OutletTempMax.Round();

            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.PowerAvg.Round();
            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.PowerMin.Round();
            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.PowerMax.Round();

            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.VolumeAvg.Round();
            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.VolumeMin.Round();
            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.VolumeMax.Round();

            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.VolumeSummary - beforeVolumeSummary;
            beforeVolumeSummary = meter.VolumeSummary;

            sheet.Cells[rowIndex, startColumn + colIndex++].Value = meter.EnergySummary - beforeEnergySummary;
            beforeEnergySummary = meter.EnergySummary;
         }

         int summaryRowIndex = reportProcessor.StartingPoints.Row + reportProcessor.SummaryRowOffset;
         int summaryColIndex = 0;

         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Average(x => x.InletTempAvg).Round();
         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Min(x => x.InletTempMin).Round();
         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Max(x => x.InletTempMax).Round();

         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Average(x => x.OutletTempAvg).Round();
         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Min(x => x.OutletTempMin).Round();
         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Max(x => x.OutletTempMax).Round();

         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Average(x => x.PowerAvg).Round();
         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Min(x => x.PowerMin).Round();
         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Max(x => x.PowerMax).Round();

         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Average(x => x.VolumeAvg).Round();
         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Min(x => x.VolumeMin).Round();
         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Max(x => x.VolumeMax).Round();

         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Max(x => x.VolumeSummary) - beforeMeter.VolumeSummary;
         sheet.Cells[summaryRowIndex, startColumn + summaryColIndex++].Value = currentData.Max(x => x.EnergySummary) - beforeMeter.EnergySummary;

         sheet.Cells[reportProcessor.StartingPoints.Row - 4, startColumn].Value = device.Name;

         return summaryColIndex;
      }

      private static async Task<IReadOnlyDictionary<int, MeterDto>> GetBeforeDataAsync(IQueryable<Meter> plc, DateOnly date, IReadOnlyCollection<DeviceReportDto> devices, IReportProcessor reportProcessor, TypeAdapterConfig mapper, CancellationToken cancellationToken)
      {
         DateRange range = reportProcessor.GetRange(date);

         Dictionary<int, MeterDto> result = new();

         foreach (DeviceReportDto device in devices)
         {
            MeterDto? before = await plc
               .AsNoTracking()
               .OrderByDescending(x => x.Date)
               .ProjectToType<MeterDto>(mapper)
               .FirstOrDefaultAsync(x =>
                  x.Date < range.Start &&
                  x.DeviceId == device.Id
               , cancellationToken);

            if (before is not null)
            {
               result.Add(device.Id, before);
               continue;
            }

            before = await plc
               .AsNoTracking()
               .OrderBy(x => x.Date)
               .ProjectToType<MeterDto>(mapper)
               .FirstAsync(x =>
                  x.Date < range.End &&
                  x.DeviceId == device.Id
               , cancellationToken);

            result.Add(device.Id, before);
         }

         return result;
      }
   }
}

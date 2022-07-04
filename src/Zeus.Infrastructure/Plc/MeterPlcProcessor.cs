using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OfficeOpenXml;
using Zeus.Domain.Plcs.Meters;
using Zeus.Infrastructure.Mongo;
using Zeus.Infrastructure.Plc.Base;
using Zeus.Infrastructure.Reports.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Plcs.Meters.Dto;
using Zeus.Models.Utilities;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Plc
{
   internal sealed class MeterPlcProcessor : BasePlcProcessor, IPlcProcessor
   {
      private readonly Expression<Func<IGrouping<int, Meter>, MeterReportDto>> _selector;

      public MeterPlcProcessor()
      {
         _selector = x => new()
         {
            Date = x.Min(g => g.Date),
            InletTempAvg = x.Average(g => g.InletTemp),
            InletTempMin = x.Min(g => g.InletTemp),
            InletTempMax = x.Max(g => g.InletTemp),
            OutletTempAvg = x.Average(g => g.OutletTemp),
            OutletTempMin = x.Min(g => g.OutletTemp),
            OutletTempMax = x.Max(g => g.OutletTemp),
            PowerAvg = x.Average(g => g.Power),
            PowerMin = x.Min(g => g.Power),
            PowerMax = x.Max(g => g.Power),
            VolumeAvg = x.Average(g => g.Volume),
            VolumeMin = x.Min(g => g.Volume),
            VolumeMax = x.Max(g => g.Volume),
            VolumeSummary = x.Max(g => g.VolumeSummary),
            EnergySummary = x.Max(g => g.EnergySummary),
         };
      }

      public async Task FillDataAsync(UnitOfWork uow, ExcelWorksheet sheet, DateOnly date, IReadOnlyCollection<DeviceReportDto> devices, IReportProcessor reportProcessor, IMapper mapper, CancellationToken cancellationToken)
      {
         int startColumn = reportProcessor.StartingPoints.MeterColumn;

         foreach (DeviceReportDto device in devices)
         {
            IReadOnlyCollection<MeterReportDto> currentPlcData = await GetPlcDataAsync(uow.Meter.AsQueryable(), date, device, reportProcessor, _selector, cancellationToken);
            if (currentPlcData.Count == 0)
            {
               return;
            }

            MeterReportDto beforePlc = await GetBeforeDataAsync(uow.Meter.AsQueryable(), date, device, reportProcessor, mapper, cancellationToken);
            startColumn += FillSheet(sheet, device, beforePlc, currentPlcData, startColumn, reportProcessor);
         }
      }

      private static int FillSheet(ExcelWorksheet sheet, DeviceReportDto device, MeterReportDto beforeMeter, IReadOnlyCollection<MeterReportDto> currentData, int startColumn, IReportProcessor reportProcessor)
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

      private static async Task<MeterReportDto> GetBeforeDataAsync(IMongoQueryable<Meter> plc, DateOnly date, DeviceReportDto device, IReportProcessor reportProcessor, IMapper mapper, CancellationToken cancellationToken)
      {
         DateRange range = reportProcessor.GetRange(date);

         MeterReportDto? before = await plc
            .Where(x =>
               x.DeviceId == device.Id &&
               x.Date < range.Start
            )
            .OrderByDescending(x => x.Date)
            .ProjectTo<MeterReportDto>(mapper)
            .FirstOrDefaultAsync(cancellationToken);

         if (before != null)
         {
            return before;
         }

         before = await plc
            .Where(x =>
               x.DeviceId == device.Id &&
               x.Date < range.End
            )
            .OrderBy(x => x.Date)
            .ProjectTo<MeterReportDto>(mapper)
            .FirstAsync(cancellationToken);

         return before;
      }
   }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OfficeOpenXml;
using Zeus.Enums.Plcs;
using Zeus.Enums.Reports;
using Zeus.Infrastructure.Extensions;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Mongo;
using Zeus.Infrastructure.Plc.Base;
using Zeus.Infrastructure.Reports.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Reports.Dto;
using Zeus.Models.Reports.Queries;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Reports.Queries
{
   internal sealed class GetReportHandler : BaseRequestQueryHandler, IRequestHandler<GetReportQuery, ReportFileDto>
   {
      private readonly IIndex<PlcType, IPlcProcessor> _plcProcessors;
      private readonly IIndex<ReportType, IReportProcessor> _reportProcessors;

      public GetReportHandler(UnitOfWork uow, IMapper mapper, IIndex<PlcType, IPlcProcessor> plcProcessors, IIndex<ReportType, IReportProcessor> reportProcessors) : base(uow, mapper)
      {
         ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
         _plcProcessors = plcProcessors;
         _reportProcessors = reportProcessors;
      }

      public async Task<ReportFileDto> Handle(GetReportQuery request, CancellationToken cancellationToken)
      {
         using ExcelPackage ep = request.Lang.GetReportTemplate();
         IReadOnlyCollection<string> templateSheetNames = ep.Workbook.Worksheets.GetSheetNames();

         IReportProcessor reportProcessor = _reportProcessors[request.Type];

         await CreateSheetsAsync(ep.Workbook.Worksheets, request, reportProcessor, cancellationToken);
         ep.Workbook.Worksheets.RemoveSheets(templateSheetNames);

         return new()
         {
            Name = reportProcessor.GetFileName(request.Date),
            Type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            Content = ep.GetAsByteArray()
         };
      }

      private async Task CreateSheetsAsync(ExcelWorksheets sheets, GetReportQuery request, IReportProcessor reportProcessor, CancellationToken cancellationToken)
      {
         IReadOnlyCollection<LocationReportDto> locations = await _uow.Location
            .AsQueryable()
            .Where(x => x.IncludeReport)
            .OrderBy(x => x.Name)
            .ProjectTo<LocationReportDto>(_mapper)
            .ToListAsync(cancellationToken);

         if (locations.Count == 0)
         {
            return;
         }

         IReadOnlyCollection<DeviceReportDto> devices = await _uow.Device
            .AsQueryable()
            .Where(x => x.IncludeReport)
            .OrderBy(x => x.Name)
            .ProjectTo<DeviceReportDto>(_mapper)
            .ToListAsync(cancellationToken);

         if (devices.Count == 0)
         {
            return;
         }

         List<Task> tasks = new();
         foreach (LocationReportDto location in locations)
         {
            IReadOnlyDictionary<PlcType, DeviceReportDto[]> deviceGroups = devices
               .Where(x => x.LocationId == location.Id)
               .GroupBy(x => x.PlcType)
               .ToDictionary(x => x.Key, x => x.ToArray());

            tasks.Add(CreateSheetAsync(sheets, request, location, deviceGroups, reportProcessor, cancellationToken));
         }

         await Task.WhenAll(tasks);
      }

      private async Task CreateSheetAsync(ExcelWorksheets sheets, GetReportQuery request, LocationReportDto location, IReadOnlyDictionary<PlcType, DeviceReportDto[]> deviceGroups, IReportProcessor reportProcessor, CancellationToken cancellationToken)
      {
         ExcelWorksheet sheet = sheets.Copy($"{request.Type.GetDescription()}_{request.Lang.GetDescription()}", location.Name);
         sheet.View.ZoomScale = 70;

         ExcelRange headerCell = sheet.Cells[1, 1];
         headerCell.Value = reportProcessor.GetHeader(headerCell.GetCellValue<string>(), location.Name, request.Date);

         foreach (var deviceGroup in deviceGroups)
         {
            await _plcProcessors[deviceGroup.Key].FillDataAsync(_uow, sheet, request.Date, deviceGroup.Value, reportProcessor, _mapper, cancellationToken);
         }
      }
   }
}

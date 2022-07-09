using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Zeus.Enums.Plcs;
using Zeus.Enums.Reports;
using Zeus.Enums.System;
using Zeus.Infrastructure.Extensions;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Reports.Language.Base;
using Zeus.Infrastructure.Reports.Plcs.Base;
using Zeus.Infrastructure.Reports.Types.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Reports.Dto;
using Zeus.Models.Reports.Queries;

namespace Zeus.Infrastructure.Handlers.Reports.Queries
{
   internal sealed class GetReportHandler : BaseRequestQueryHandler, IRequestHandler<GetReportQuery, ReportFileDto>
   {
      private readonly IIndex<Language, ILanguageProcessor> _languageProcessors;
      private readonly IIndex<PlcType, IPlcProcessor> _plcProcessors;
      private readonly IIndex<ReportType, IReportProcessor> _reportProcessors;

      public GetReportHandler(UnitOfWork uow, TypeAdapterConfig mapper, IIndex<Language, ILanguageProcessor> languageProcessors, IIndex<PlcType, IPlcProcessor> plcProcessors, IIndex<ReportType, IReportProcessor> reportProcessors) : base(uow, mapper)
      {
         ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

         _languageProcessors = languageProcessors;
         _plcProcessors = plcProcessors;
         _reportProcessors = reportProcessors;
      }

      public async Task<ReportFileDto> Handle(GetReportQuery request, CancellationToken cancellationToken)
      {
         using ExcelPackage ep = request.Language.GetReportTemplate();
         IReadOnlyCollection<string> templateSheetNames = ep.Workbook.Worksheets.GetSheetNames();

         IReportProcessor reportProcessor = _reportProcessors[request.Type];

         await CreateSheetsAsync(ep.Workbook.Worksheets, request, reportProcessor, cancellationToken);
         ep.Workbook.Worksheets.RemoveSheets(templateSheetNames);

         return new()
         {
            Name = reportProcessor.GetFileName(_languageProcessors[request.Language].FIleName, request.Date),
            Type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            Content = ep.GetAsByteArray()
         };
      }

      private async Task CreateSheetsAsync(ExcelWorksheets sheets, GetReportQuery request, IReportProcessor reportProcessor, CancellationToken cancellationToken)
      {
         IReadOnlyCollection<LocationReportDto> locations = await _uow.Location
            .AsNoTracking()
            .Where(x => x.IncludeReport)
            .OrderBy(x => x.Name)
            .ProjectToType<LocationReportDto>(_mapper)
            .ToArrayAsync(cancellationToken);

         if (locations.Count == 0)
         {
            return;
         }

         IReadOnlyCollection<DeviceReportDto> devices = await _uow.Device
            .AsNoTracking()
            .Where(x => x.IncludeReport)
            .OrderBy(x => x.Name)
            .ProjectToType<DeviceReportDto>(_mapper)
            .ToArrayAsync(cancellationToken);

         if (devices.Count == 0)
         {
            return;
         }

         foreach (LocationReportDto location in locations)
         {
            ExcelWorksheet sheet = sheets.CloneSheet(request, location);
            sheet.View.ZoomScale = 70;

            ExcelRange headerCell = sheet.Cells[1, 1];
            headerCell.Value = reportProcessor.GetHeader(headerCell.GetCellValue<string>(), location.Name, request.Date);
         }

         IReadOnlyDictionary<PlcType, DeviceReportDto[]> deviceGroups = devices
            .GroupBy(x => x.PlcType)
            .ToDictionary(x => x.Key, x => x.ToArray());

         foreach (var deviceGroup in deviceGroups)
         {
            await _plcProcessors[deviceGroup.Key].FillDataAsync(_uow, sheets, request.Date, locations, deviceGroup.Value, reportProcessor, _mapper, cancellationToken);
         }
      }
   }
}

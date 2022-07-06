using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Meters;
using Zeus.Enums.Devices;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Base;
using Zeus.Models.Plcs.Meters.Commands;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Plcs.Meters.Commands
{
   internal sealed class CreateMeterHandler : BaseRequestHandler, IRequestHandler<CreateMeterCommand, Result>
   {
      public CreateMeterHandler(UnitOfWork uow) : base(uow)
      {
      }

      public async Task<Result> Handle(CreateMeterCommand request, CancellationToken cancellationToken)
      {
         Meter? existingMeter = await _uow.Meter
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Date == request.Date.RoundToSecond() &&
               x.DeviceId == request.DeviceId
            , cancellationToken);

         if (existingMeter is not null)
         {
            return Result.Success();
         }

         Device? existingDevice = await _uow.Device
            .FirstOrDefaultAsync(x =>
               x.Id == request.DeviceId
            , cancellationToken);

         if (existingDevice?.Id != request.DeviceId)
         {
            return Result.Error("Device not found.");
         }
         if (existingDevice.Type != DeviceType.Kamstrup && existingDevice.Type != DeviceType.KamstrupRs500)
         {
            return Result.Error("Device type is incorrect.");
         }
         if (existingDevice.LocationId != request.LocationId)
         {
            return Result.Error("Device location is incorrect.");
         }

         if (!string.IsNullOrWhiteSpace(request.SerialNumber) && existingDevice.SerialNumber != request.SerialNumber)
         {
            existingDevice.Update(request.SerialNumber);
         }

         _uow.Meter.Add(new(request.InletTemp, request.OutletTemp, request.Power, request.Volume, request.VolumeSummary, request.EnergySummary, request.HourCount, request.ErrorCode, request.Date, existingDevice.Id));

         await _uow.SaveChangesAsync(cancellationToken);
         return Result.Success();
      }
   }
}

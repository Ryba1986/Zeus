using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Climatixs;
using Zeus.Enums.Plcs;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Base;
using Zeus.Models.Plcs.Climatixs.Commands;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Plcs.Climatixs.Commands
{
   internal sealed class CreateClimatixHandler : BaseRequestHandler, IRequestHandler<CreateClimatixCommand, Result>
   {
      public CreateClimatixHandler(UnitOfWork uow) : base(uow)
      {
      }

      public async Task<Result> Handle(CreateClimatixCommand request, CancellationToken cancellationToken)
      {
         Device? existingDevice = await _uow.Device
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Id == request.DeviceId
            , cancellationToken);

         if (existingDevice?.Id != request.DeviceId)
         {
            return Result.Error("Device not found.");
         }
         if (existingDevice.PlcType != PlcType.Climatix)
         {
            return Result.Error("Device type is incorrect.");
         }
         if (existingDevice.LocationId != request.LocationId)
         {
            return Result.Error("Device location is incorrect.");
         }

         Climatix? existingClimatix = await _uow.Climatix
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Date == request.Date.RoundToSecond() &&
               x.DeviceId == request.DeviceId
            , cancellationToken);

         if (existingClimatix is not null)
         {
            return Result.Success();
         }

         _uow.Climatix.Add(new(request.OutsideTemp, request.CoHighInletPresure, request.CoHighOutletPresure, request.Alarm, request.Co1LowInletTemp, request.Co1LowOutletTemp, request.Co1LowOutletPresure, request.Co1HeatCurveTemp, request.Co1PumpAlarm, request.Co1PumpStatus, request.Co1ValvePosition, request.Co1Status, request.Co2LowInletTemp, request.Co2LowOutletTemp, request.Co2LowOutletPresure, request.Co2HeatCurveTemp, request.Co2PumpAlarm, request.Co2PumpStatus, request.Co2ValvePosition, request.Co2Status, request.CwuTemp, request.CwuTempSet, request.CwuPumpAlarm, request.CwuPumpStatus, request.CwuValvePosition, request.CwuStatus, request.Date, request.DeviceId));

         await _uow.SaveChangesAsync(cancellationToken);
         return Result.Success();
      }
   }
}

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               x.DeviceId == request.DeviceId &&
               x.Date == request.Date.RoundToSecond()
            , cancellationToken);

         if (existingMeter is not null)
         {
            return Result.Success();
         }

         Device? existingDevice = await _uow.Device
            .AsQueryable()
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

         return await _uow.ExecuteTransactionAsync(
            async (session, token) =>
            {
               // TODO: move to event
               if (!string.IsNullOrWhiteSpace(request.SerialNumber) && existingDevice.SerialNumber != request.SerialNumber)
               {
                  existingDevice.Update(request.SerialNumber);
                  await _uow.Device.ReplaceOneAsync(session, x => x.Id == existingDevice.Id, existingDevice, cancellationToken: token);
               }

               await _uow.Meter.InsertOneAsync(session, new(request.InletTemp, request.OutletTemp, request.Power, request.Volume, request.VolumeSummary, request.EnergySummary, request.HourCount, request.ErrorCode, existingDevice.Id, request.Date), cancellationToken: token);
            },
            cancellationToken
         );
      }
   }
}

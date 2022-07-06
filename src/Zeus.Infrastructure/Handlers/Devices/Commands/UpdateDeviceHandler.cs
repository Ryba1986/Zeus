using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Devices;
using Zeus.Domain.Locations;
using Zeus.Domain.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Base;
using Zeus.Models.Devices.Commands;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Devices.Commands
{
   internal sealed class UpdateDeviceHandler : BaseRequestHandler, IRequestHandler<UpdateDeviceCommand, Result>
   {
      public UpdateDeviceHandler(UnitOfWork uow) : base(uow)
      {
      }

      public async Task<Result> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
      {
         User? modifiedBy = await _uow.User
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Id == request.ModifiedById &&
               x.IsActive
            , cancellationToken);

         if (modifiedBy is null)
         {
            return Result.Error($"Cannot find an active user with id {request.ModifiedById}.");
         }

         Location? existingLocation = await _uow.Location
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Id == request.LocationId
            , cancellationToken);

         if (existingLocation is null)
         {
            return Result.Error("Location not found.");
         }

         Device? otherDevice = await _uow.Device
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.LocationId == request.LocationId &&
               x.Id != request.Id &&
               (x.Name == request.Name || x.ModbusId == request.ModbusId)
            , cancellationToken);

         if (otherDevice?.Name == request.Name)
         {
            return Result.Error($"Device with name {request.Name} exist.");
         }
         if (otherDevice?.ModbusId == request.ModbusId)
         {
            return Result.Error($"Device with modbus id {request.ModbusId} exist.");
         }

         Device? existingDevice = await _uow.Device
            .FirstOrDefaultAsync(x =>
               x.Id == request.Id
            , cancellationToken);

         if (existingDevice?.Id != request.Id)
         {
            return Result.Error("Device not exist.");
         }
         if (!existingDevice.Version.SequenceEqual(request.Version))
         {
            return Result.Error("Device has been changed.");
         }

         CreateHistory(existingDevice, existingLocation, request, modifiedBy);
         existingDevice.Update(existingLocation.Id, request.Name, request.Type, request.ModbusId, request.RsBoundRate, request.RsDataBits, request.RsParity, request.RsStopBits, request.IncludeReport, request.IsActive);

         await _uow.SaveChangesAsync(cancellationToken);
         return Result.Success();
      }

      private void CreateHistory(Device device, Location location, UpdateDeviceCommand request, User createdBy)
      {
         if
         (
            device.LocationId == location.Id &&
            device.Name == request.Name &&
            device.Type == request.Type &&
            device.ModbusId == request.ModbusId &&
            device.RsBoundRate == request.RsBoundRate &&
            device.RsDataBits == request.RsDataBits &&
            device.RsParity == request.RsParity &&
            device.RsStopBits == request.RsStopBits &&
            device.IncludeReport == request.IncludeReport &&
            device.IsActive == request.IsActive
         )
         {
            return;
         }

         _uow.DeviceHistory.Add(new(
            device.Id,
            device.Name != request.Name ? request.Name : string.Empty,
            device.LocationId != location.Id ? location.Name : string.Empty,
            device.Type != request.Type ? request.Type.GetDescription() : string.Empty,
            device.ModbusId != request.ModbusId ? request.ModbusId.ToString() : string.Empty,
            device.RsBoundRate != request.RsBoundRate ? request.RsBoundRate.GetDescription() : string.Empty,
            device.RsDataBits != request.RsDataBits ? request.RsDataBits.GetDescription() : string.Empty,
            device.RsParity != request.RsParity ? request.RsParity.GetDescription() : string.Empty,
            device.RsStopBits != request.RsStopBits ? request.RsStopBits.GetDescription() : string.Empty,
            request.IncludeReport,
            request.IsActive,
            createdBy.Id
         ));
      }
   }
}

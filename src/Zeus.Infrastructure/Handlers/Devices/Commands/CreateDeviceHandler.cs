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
   internal sealed class CreateDeviceHandler : BaseRequestHandler, IRequestHandler<CreateDeviceCommand, Result>
   {
      public CreateDeviceHandler(UnitOfWork uow) : base(uow)
      {
      }

      public async Task<Result> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
      {
         User? createdBy = await _uow.User
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Id == request.CreatedById &&
               x.IsActive
            , cancellationToken);

         if (createdBy is null)
         {
            return Result.Error($"Cannot find an active user with id {request.CreatedById}.");
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

         Device? existingDevice = await _uow.Device
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.LocationId == request.LocationId &&
               (x.Name == request.Name || x.ModbusId == request.ModbusId)
            , cancellationToken);

         if (existingDevice?.Name == request.Name)
         {
            return Result.Error($"Device with name {request.Name} exist.");
         }
         if (existingDevice?.ModbusId == request.ModbusId)
         {
            return Result.Error($"Device with modbus id {request.ModbusId} exist.");
         }

         Device newDevice = new(existingLocation.Id, request.Name, request.Type, request.ModbusId, request.RsBoundRate, request.RsDataBits, request.RsParity, request.RsStopBits, request.IncludeReport, request.IsActive);

         return await _uow.ExecuteTransactionAsync(
            async token =>
            {
               _uow.Device.Add(newDevice);
               await _uow.SaveChangesAsync(token);

               _uow.DeviceHistory.Add(new(newDevice.Id, newDevice.Name, existingLocation.Name, newDevice.Type.GetDescription(), newDevice.ModbusId.ToString(), newDevice.RsBoundRate.GetDescription(), newDevice.RsDataBits.GetDescription(), newDevice.RsParity.GetDescription(), newDevice.RsStopBits.GetDescription(), newDevice.IncludeReport, newDevice.IsActive, createdBy.Id));
               await _uow.SaveChangesAsync(token);
            },
            cancellationToken
         );
      }
   }
}

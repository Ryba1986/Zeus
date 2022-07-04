using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Rvds;
using Zeus.Enums.Devices;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Base;
using Zeus.Models.Plcs.Rvds.Commands;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Handlers.Plcs.Rvds.Commands
{
   internal sealed class CreateRvd145Handler : BaseRequestHandler, IRequestHandler<CreateRvd145Command, Result>
   {
      public CreateRvd145Handler(UnitOfWork uow) : base(uow)
      {
      }

      public async Task<Result> Handle(CreateRvd145Command request, CancellationToken cancellationToken)
      {
         Rvd145? existingRvd = await _uow.Rvd145
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               x.DeviceId == request.DeviceId &&
               x.Date == request.Date.RoundToSecond()
            , cancellationToken);

         if (existingRvd is not null)
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
         if (existingDevice.Type != DeviceType.Rvd145Co && existingDevice.Type != DeviceType.Rvd145CoCwu)
         {
            return Result.Error("Device type is incorrect.");
         }
         if (existingDevice.LocationId != request.LocationId)
         {
            return Result.Error("Device location is incorrect.");
         }

         await _uow.Rvd145.InsertOneAsync(new(request.OutsideTemp, request.CoHighInletPresure, request.Alarm, request.CoHighOutletTemp, request.CoLowInletTemp, request.CoLowOutletPresure, request.CoHeatCurveTemp, request.CoPumpStatus, request.CoStatus, request.CwuTemp, request.CwuTempSet, request.CwuCirculationTemp, request.CwuPumpStatus, request.CwuStatus, existingDevice.Id, request.Date), cancellationToken: cancellationToken);
         return Result.Success();
      }
   }
}

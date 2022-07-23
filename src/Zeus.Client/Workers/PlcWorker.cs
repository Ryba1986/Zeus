using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using MediatR;
using Zeus.Client.Modbus.Base;
using Zeus.Client.Settings;
using Zeus.Client.Workers.Base;
using Zeus.Enums.Plcs;
using Zeus.Models.Base.Commands;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Devices.Queries;

namespace Zeus.Client.Workers
{
   internal sealed class PlcWorker : BaseWorker
   {
      private readonly IIndex<PlcType, IModbusProcessor> _modbusProcessors;

      public PlcWorker(IMediator mediator, ZeusSettings settings, IIndex<PlcType, IModbusProcessor> modbusProcessors) : base(mediator, settings)
      {
         _modbusProcessors = modbusProcessors;
      }

      protected override async Task ExecuteAsync(CancellationToken cancellationToken)
      {
         while (!cancellationToken.IsCancellationRequested)
         {
            Stopwatch sw = Stopwatch.StartNew();

            IReadOnlyCollection<DeviceDto> devices = await _mediator.Send(new GetDevicesFromLocationQuery(), cancellationToken);
            foreach (DeviceDto device in devices)
            {
               try
               {
                  BaseCreatePlcCommand command = await _modbusProcessors[device.PlcType].GetPlcAsync(device, cancellationToken);
                  await _mediator.Send(command, cancellationToken);
               }
               catch (Exception ex)
               {
                  // TODO: move to other logic
                  Console.WriteLine(ex.Message);
               }
            }

            sw.Stop();
            await GetPlcIntervalAsync(sw.Elapsed, cancellationToken);
         }
      }
   }
}

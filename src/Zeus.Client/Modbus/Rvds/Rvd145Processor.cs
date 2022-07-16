using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using NModbus;
using NModbus.Serial;
using Zeus.Client.Modbus.Base;
using Zeus.Client.Settings;
using Zeus.Models.Base.Commands;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Plcs.Rvds.Commands;

namespace Zeus.Client.Modbus.Rvds
{
   internal sealed class Rvd145Processor : BaseModbusProcessor, IModbusProcessor
   {
      public Rvd145Processor(ZeusSettings settings) : base(settings)
      {
      }

      public async Task<BaseCreatePlcCommand> GetPlcAsync(DeviceDto device, CancellationToken cancellationToken)
      {
         using SerialPort port = GetSerialPort(device);
         port.Open();

         using IModbusSerialMaster master = _factory.CreateRtuMaster(port);
         IReadOnlyList<ushort> table1 = await master.ReadHoldingRegistersAsync(device.ModbusId, 1000, 70);
         IReadOnlyList<ushort> table2 = await master.ReadHoldingRegistersAsync(device.ModbusId, 200, 45);

         return new CreateRvd145Command()
         {
            Date = DateTime.Now,
            DeviceId = device.Id,

            OutsideTemp = (short)table1[44] / 64f,
            CoHighInletPresure = (short)table1[53] / 50f,
            Alarm = (short)table1[31],

            Co1HighOutletTemp = (short)table1[48] / 64f,
            Co1LowInletTemp = (short)table1[45] / 64f,
            Co1LowOutletPresure = (short)table1[52] / 50f,
            Co1HeatCurveTemp = (short)table1[66] / 64f,
            Co1PumpStatus = (short)table1[36] != 0,
            Co1Status = (short)table2[10] == 1,

            CwuTemp = (short)table1[46] / 64f,
            CwuTempSet = (short)table1[58] / 64f,
            CwuCirculationTemp = (short)table1[49] / 64f,
            CwuPumpStatus = (short)table1[40] != 0,
            CwuStatus = (short)table2[41] == 1,
         };
      }
   }
}

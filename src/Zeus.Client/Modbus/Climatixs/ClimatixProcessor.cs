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
using Zeus.Models.Plcs.Climatixs.Commands;

namespace Zeus.Client.Modbus.Climatixs
{
   internal sealed class ClimatixProcessor : BaseModbusProcessor, IModbusProcessor
   {
      public ClimatixProcessor(ZeusSettings settings) : base(settings)
      {
      }

      public async Task<BaseCreatePlcCommand> GetPlcAsync(DeviceDto device, CancellationToken cancellationToken)
      {
         using SerialPort port = GetSerialPort(device);
         port.Open();

         using IModbusSerialMaster master = _factory.CreateRtuMaster(port);

         IReadOnlyList<bool> inputStates = await master.ReadInputsAsync(device.ModbusId, 0, 900);

         IReadOnlyList<ushort> inputRegisters0 = await master.ReadInputRegistersAsync(device.ModbusId, 0, 80);
         IReadOnlyList<ushort> inputRegisters4 = await master.ReadInputRegistersAsync(device.ModbusId, 400, 90);
         IReadOnlyList<ushort> inputRegisters5 = await master.ReadInputRegistersAsync(device.ModbusId, 500, 90);
         IReadOnlyList<ushort> inputRegisters8 = await master.ReadInputRegistersAsync(device.ModbusId, 800, 90);

         IReadOnlyList<ushort> holdingRegisters = await master.ReadHoldingRegistersAsync(device.ModbusId, 800, 20);

         return new CreateClimatixCommand()
         {
            Alarm = inputStates[2 - 1] || inputStates[1 - 1],
            Co1PumpAlarm = inputStates[430 - 1],
            Co1PumpStatus = inputStates[492 - 1],
            Co2PumpAlarm = inputStates[530 - 1],
            Co2PumpStatus = inputStates[592 - 1],
            CwuPumpAlarm = inputStates[830 - 1],
            CwuPumpStatus = inputStates[891 - 1],

            OutsideTemp = (short)inputRegisters0[50 - 1] / 10f,
            CoHighInletPresure = (short)inputRegisters0[77 - 1] / 100f,
            CoHighOutletPresure = (short)inputRegisters0[76 - 1] / 100f,

            Co1LowInletTemp = (short)inputRegisters4[50 - 1] / 10f,
            Co1LowOutletTemp = (short)inputRegisters4[52 - 1] / 10f,
            Co1LowOutletPresure = (short)inputRegisters4[74 - 1] / 100f,
            Co1HeatCurveTemp = (short)inputRegisters4[20 - 1] / 10f,
            Co1ValvePosition = (byte)inputRegisters4[90 - 1],
            Co1Status = (short)inputRegisters4[1 - 1] != 0,

            Co2LowInletTemp = (short)inputRegisters5[50 - 1] / 10f,
            Co2LowOutletTemp = (short)inputRegisters5[52 - 1] / 10f,
            Co2LowOutletPresure = (short)inputRegisters5[74 - 1] / 100f,
            Co2HeatCurveTemp = (short)inputRegisters5[20 - 1] / 10f,
            Co2ValvePosition = (byte)inputRegisters5[90 - 1],
            Co2Status = (short)inputRegisters5[1 - 1] != 0,

            CwuTemp = (short)inputRegisters8[50 - 1] / 10f,
            CwuValvePosition = (byte)inputRegisters8[90 - 1],
            CwuStatus = inputRegisters8[1 - 1] != 0,

            CwuTempSet = (short)holdingRegisters[20 - 1] / 10,
         };
      }
   }
}

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using NModbus;
using NModbus.Serial;
using NModbus.Utility;
using Zeus.Client.Modbus.Base;
using Zeus.Client.Settings;
using Zeus.Models.Base.Commands;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Plcs.Meters.Commands;

namespace Zeus.Client.Modbus.Meters
{
   internal sealed class MeterProcessor : BaseModbusProcessor, IModbusProcessor
   {
      public MeterProcessor(ZeusSettings settings) : base(settings)
      {
      }

      public async Task<BaseCreatePlcCommand> GetPlcAsync(DeviceDto device, CancellationToken cancellationToken)
      {
         using SerialPort port = GetSerialPort(device);
         port.Open();

         using IModbusSerialMaster master = _factory.CreateRtuMaster(port);
         IReadOnlyList<ushort> table = await master.ReadInputRegistersAsync(device.ModbusId, 0, 86);

         float power = ModbusUtility.GetSingle(table[7], table[6]);
         float volume = ModbusUtility.GetSingle(table[3], table[2]);

         return new CreateMeterCommand()
         {
            Date = DateTime.Now,
            DeviceId = device.Id,

            InletTemp = ModbusUtility.GetSingle(table[9], table[8]),
            OutletTemp = ModbusUtility.GetSingle(table[11], table[10]),
            Power = table[19] == 2 ? power * 1000f : power,
            Volume = table[18] == 49 ? volume / 1000f : volume,
            VolumeSummary = ModbusUtility.GetSingle(table[5], table[4]),
            EnergySummary = ModbusUtility.GetSingle(table[1], table[0]),

            HourCount = (int)ModbusUtility.GetUInt32(table[84], table[85]),
            SerialNumber = ModbusUtility.GetUInt32(table[76], table[77]).ToString(),
            ErrorCode = (short)table[43],
         };
      }
   }
}

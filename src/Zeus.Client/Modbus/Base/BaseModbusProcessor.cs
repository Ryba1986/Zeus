using System.IO.Ports;
using NModbus;
using Zeus.Client.Settings;
using Zeus.Models.Devices.Dto;

namespace Zeus.Client.Modbus.Base
{
   internal abstract class BaseModbusProcessor
   {
      protected readonly ModbusFactory _factory;
      private readonly ZeusSettings _settings;

      public BaseModbusProcessor(ZeusSettings settings)
      {
         _factory = new();
         _settings = settings;
      }

      public SerialPort GetSerialPort(DeviceDto device)
      {
         return new()
         {
            PortName = _settings.SerialPortName,
            BaudRate = (int)device.RsBoundRate,
            Parity = (Parity)device.RsParity,
            DataBits = (int)device.RsDataBits,
            StopBits = (StopBits)device.RsStopBits,

            ReadTimeout = _settings.SerialPortTimeout,
            WriteTimeout = _settings.SerialPortTimeout
         };
      }
   }
}

using System.Threading;
using System.Threading.Tasks;
using Zeus.Models.Base.Commands;
using Zeus.Models.Devices.Dto;

namespace Zeus.Client.Modbus.Base
{
   internal interface IModbusProcessor
   {
      Task<BaseCreatePlcCommand> GetPlcAsync(DeviceDto device, CancellationToken cancellationToken);
   }
}

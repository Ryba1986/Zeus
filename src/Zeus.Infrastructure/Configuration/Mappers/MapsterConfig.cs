using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using Zeus.Domain.Devices;
using Zeus.Domain.Locations;
using Zeus.Domain.Plcs.Meters;
using Zeus.Domain.Plcs.Climatixs;
using Zeus.Domain.Plcs.Rvds;
using Zeus.Domain.Users;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Plcs.Meters.Dto;
using Zeus.Models.Plcs.Rvds.Dto;
using Zeus.Models.Users.Dto;
using Zeus.Models.Plcs.Climatixs.Dto;

namespace Zeus.Infrastructure.Configuration.Mappers
{
   internal static class MapsterConfig
   {
      public static TypeAdapterConfig Initialize()
      {
         return new TypeAdapterConfig()
            .GetLocationConfig()
            .GetDeviceConfig()
            .GetUserConfig()
            .GetMeterConfig()
            .GetClimatixConfig()
            .GetRvd145Config();
      }

      private static TypeAdapterConfig GetLocationConfig(this TypeAdapterConfig cfg)
      {
         cfg.NewConfig<Location, LocationDto>();

         cfg.NewConfig<Tuple<LocationHistory, User>, LocationHistoryDto>()
            .Map(d => d.Name, s => s.Item1.Name)
            .Map(d => d.MacAddress, s => s.Item1.MacAddress)
            .Map(d => d.IncludeReport, s => s.Item1.IncludeReport)
            .Map(d => d.IsActive, s => s.Item1.IsActive)
            .Map(d => d.CreatedByName, s => s.Item2.Name)
            .Map(d => d.CreateDate, s => s.Item1.CreateDate);

         cfg.NewConfig<Location, LocationReportDto>();

         return cfg;
      }

      private static TypeAdapterConfig GetDeviceConfig(this TypeAdapterConfig cfg)
      {
         cfg.NewConfig<Device, DeviceDto>();

         cfg.NewConfig<Tuple<DeviceHistory, User>, DeviceHistoryDto>()
            .Map(d => d.Name, s => s.Item1.Name)
            .Map(d => d.LocationName, s => s.Item1.LocationName)
            .Map(d => d.ModbusId, s => s.Item1.ModbusId)
            .Map(d => d.Type, s => s.Item1.Type)
            .Map(d => d.RsBoundRate, s => s.Item1.RsBoundRate)
            .Map(d => d.RsDataBits, s => s.Item1.RsDataBits)
            .Map(d => d.RsParity, s => s.Item1.RsParity)
            .Map(d => d.RsStopBits, s => s.Item1.RsStopBits)
            .Map(d => d.IncludeReport, s => s.Item1.IncludeReport)
            .Map(d => d.IsActive, s => s.Item1.IsActive)
            .Map(d => d.CreatedByName, s => s.Item2.Name)
            .Map(d => d.CreateDate, s => s.Item1.CreateDate);

         cfg.NewConfig<Device, DeviceReportDto>();

         return cfg;
      }

      private static TypeAdapterConfig GetUserConfig(this TypeAdapterConfig cfg)
      {
         cfg.NewConfig<User, UserDto>();

         cfg.NewConfig<Tuple<UserHistory, User>, UserHistoryDto>()
            .Map(d => d.Name, s => s.Item1.Name)
            .Map(d => d.Email, s => s.Item1.Email)
            .Map(d => d.Role, s => s.Item1.Role)
            .Map(d => d.IsActive, s => s.Item1.IsActive)
            .Map(d => d.CreatedByName, s => s.Item2.Name)
            .Map(d => d.CreateDate, s => s.Item1.CreateDate);

         return cfg;
      }

      private static TypeAdapterConfig GetMeterConfig(this TypeAdapterConfig cfg)
      {
         cfg.NewConfig<Tuple<Meter, Device>, MeterDto>()
            .Map(d => d.Date, s => s.Item1.Date)
            .Map(d => d.DeviceId, s => s.Item1.DeviceId)
            .Map(d => d.InletTemp, s => s.Item1.InletTemp)
            .Map(d => d.OutletTemp, s => s.Item1.OutletTemp)
            .Map(d => d.Power, s => s.Item1.Power)
            .Map(d => d.Volume, s => s.Item1.Volume)
            .Map(d => d.VolumeSummary, s => s.Item1.VolumeSummary)
            .Map(d => d.EnergySummary, s => s.Item1.EnergySummary)
            .Map(d => d.Name, s => s.Item2.Name)
            .Ignore(x => x.IsDelayed);

         cfg.NewConfig<Meter, MeterChartDto>();

         cfg.NewConfig<IEnumerable<Meter>, MeterReportDto>()
            .Map(d => d.Date, s => s.Min(x => x.Date))
            .Map(d => d.DeviceId, s => s.Min(x => x.DeviceId))
            .Map(d => d.InletTempAvg, s => s.Average(x => x.InletTemp))
            .Map(d => d.InletTempMin, s => s.Min(x => x.InletTemp))
            .Map(d => d.InletTempMax, s => s.Max(x => x.InletTemp))
            .Map(d => d.OutletTempAvg, s => s.Average(x => x.OutletTemp))
            .Map(d => d.OutletTempMin, s => s.Min(x => x.OutletTemp))
            .Map(d => d.OutletTempMax, s => s.Max(x => x.OutletTemp))
            .Map(d => d.PowerAvg, s => s.Average(x => x.Power))
            .Map(d => d.PowerMin, s => s.Min(x => x.Power))
            .Map(d => d.PowerMax, s => s.Max(x => x.Power))
            .Map(d => d.VolumeAvg, s => s.Average(x => x.Volume))
            .Map(d => d.VolumeMin, s => s.Min(x => x.Volume))
            .Map(d => d.VolumeMax, s => s.Max(x => x.Volume))
            .Map(d => d.VolumeSummaryMax, s => s.Max(x => x.VolumeSummary))
            .Map(d => d.EnergySummaryMax, s => s.Max(x => x.EnergySummary));

         return cfg;
      }

      private static TypeAdapterConfig GetClimatixConfig(this TypeAdapterConfig cfg)
      {
         cfg.NewConfig<Tuple<Climatix, Device>, ClimatixDto>()
            .Map(d => d.Date, s => s.Item1.Date)
            .Map(d => d.DeviceId, s => s.Item1.DeviceId)
            .Map(d => d.OutsideTemp, s => s.Item1.OutsideTemp)
            .Map(d => d.CoHighInletPresure, s => s.Item1.CoHighInletPresure)
            .Map(d => d.CoHighOutletPresure, s => s.Item1.CoHighOutletPresure)
            .Map(d => d.Alarm, s => s.Item1.Alarm)
            .Map(d => d.Co1LowInletTemp, s => s.Item1.Co1LowInletTemp)
            .Map(d => d.Co1LowOutletTemp, s => s.Item1.Co1LowOutletTemp)
            .Map(d => d.Co1LowOutletPresure, s => s.Item1.Co1LowOutletPresure)
            .Map(d => d.Co1HeatCurveTemp, s => s.Item1.Co1HeatCurveTemp)
            .Map(d => d.Co1PumpAlarm, s => s.Item1.Co1PumpAlarm)
            .Map(d => d.Co1PumpStatus, s => s.Item1.Co1PumpStatus)
            .Map(d => d.Co1ValvePosition, s => s.Item1.Co1ValvePosition)
            .Map(d => d.Co1Status, s => s.Item1.Co1Status)
            .Map(d => d.Co2LowInletTemp, s => s.Item1.Co2LowInletTemp)
            .Map(d => d.Co2LowOutletTemp, s => s.Item1.Co2LowOutletTemp)
            .Map(d => d.Co2LowOutletPresure, s => s.Item1.Co2LowOutletPresure)
            .Map(d => d.Co2HeatCurveTemp, s => s.Item1.Co2HeatCurveTemp)
            .Map(d => d.Co2PumpAlarm, s => s.Item1.Co2PumpAlarm)
            .Map(d => d.Co2PumpStatus, s => s.Item1.Co2PumpStatus)
            .Map(d => d.Co2ValvePosition, s => s.Item1.Co2ValvePosition)
            .Map(d => d.Co2Status, s => s.Item1.Co2Status)
            .Map(d => d.CwuTemp, s => s.Item1.CwuTemp)
            .Map(d => d.CwuTempSet, s => s.Item1.CwuTempSet)
            .Map(d => d.CwuPumpAlarm, s => s.Item1.CwuPumpAlarm)
            .Map(d => d.CwuPumpStatus, s => s.Item1.CwuPumpStatus)
            .Map(d => d.CwuValvePosition, s => s.Item1.CwuValvePosition)
            .Map(d => d.CwuStatus, s => s.Item1.CwuStatus)
            .Map(d => d.Name, s => s.Item2.Name)
            .Ignore(x => x.IsDelayed);

         cfg.NewConfig<Climatix, ClimatixChartDto>();

         cfg.NewConfig<IEnumerable<Climatix>, ClimatixReportDto>()
            .Map(d => d.Date, s => s.Min(x => x.Date))
            .Map(d => d.DeviceId, s => s.Min(x => x.DeviceId))
            .Map(d => d.OutsideTempAvg, s => s.Average(x => x.OutsideTemp))
            .Map(d => d.OutsideTempMin, s => s.Min(x => x.OutsideTemp))
            .Map(d => d.OutsideTempMax, s => s.Max(x => x.OutsideTemp))
            .Map(d => d.CoHighInletPresureAvg, s => s.Average(x => x.CoHighInletPresure))
            .Map(d => d.CoHighInletPresureMin, s => s.Min(x => x.CoHighInletPresure))
            .Map(d => d.CoHighInletPresureMax, s => s.Max(x => x.CoHighInletPresure))
            .Map(d => d.CoHighOutletPresureAvg, s => s.Average(x => x.CoHighOutletPresure))
            .Map(d => d.CoHighOutletPresureMin, s => s.Min(x => x.CoHighOutletPresure))
            .Map(d => d.CoHighOutletPresureMax, s => s.Max(x => x.CoHighOutletPresure))
            .Map(d => d.Co1LowInletTempAvg, s => s.Average(x => x.Co1LowInletTemp))
            .Map(d => d.Co1LowInletTempMin, s => s.Min(x => x.Co1LowInletTemp))
            .Map(d => d.Co1LowInletTempMax, s => s.Max(x => x.Co1LowInletTemp))
            .Map(d => d.Co1LowOutletTempAvg, s => s.Average(x => x.Co1LowOutletTemp))
            .Map(d => d.Co1LowOutletTempMin, s => s.Min(x => x.Co1LowOutletTemp))
            .Map(d => d.Co1LowOutletTempMax, s => s.Max(x => x.Co1LowOutletTemp))
            .Map(d => d.Co1LowOutletPresureAvg, s => s.Average(x => x.Co1LowOutletPresure))
            .Map(d => d.Co1LowOutletPresureMin, s => s.Min(x => x.Co1LowOutletPresure))
            .Map(d => d.Co1LowOutletPresureMax, s => s.Max(x => x.Co1LowOutletPresure))
            .Map(d => d.Co2LowInletTempAvg, s => s.Average(x => x.Co2LowInletTemp))
            .Map(d => d.Co2LowInletTempMin, s => s.Min(x => x.Co2LowInletTemp))
            .Map(d => d.Co2LowInletTempMax, s => s.Max(x => x.Co2LowInletTemp))
            .Map(d => d.Co2LowOutletTempAvg, s => s.Average(x => x.Co2LowOutletTemp))
            .Map(d => d.Co2LowOutletTempMin, s => s.Min(x => x.Co2LowOutletTemp))
            .Map(d => d.Co2LowOutletTempMax, s => s.Max(x => x.Co2LowOutletTemp))
            .Map(d => d.Co2LowOutletPresureAvg, s => s.Average(x => x.Co2LowOutletPresure))
            .Map(d => d.Co2LowOutletPresureMin, s => s.Min(x => x.Co2LowOutletPresure))
            .Map(d => d.Co2LowOutletPresureMax, s => s.Max(x => x.Co2LowOutletPresure))
            .Map(d => d.CwuTempAvg, s => s.Average(x => x.CwuTemp))
            .Map(d => d.CwuTempMin, s => s.Min(x => x.CwuTemp))
            .Map(d => d.CwuTempMax, s => s.Max(x => x.CwuTemp));

         return cfg;
      }

      private static TypeAdapterConfig GetRvd145Config(this TypeAdapterConfig cfg)
      {
         cfg.NewConfig<Tuple<Rvd145, Device>, Rvd145Dto>()
            .Map(d => d.Date, s => s.Item1.Date)
            .Map(d => d.DeviceId, s => s.Item1.DeviceId)
            .Map(d => d.OutsideTemp, s => s.Item1.OutsideTemp)
            .Map(d => d.CoHighInletPresure, s => s.Item1.CoHighInletPresure)
            .Map(d => d.Alarm, s => s.Item1.Alarm)
            .Map(d => d.Co1HighOutletTemp, s => s.Item1.Co1HighOutletTemp)
            .Map(d => d.Co1LowInletTemp, s => s.Item1.Co1LowInletTemp)
            .Map(d => d.Co1LowOutletPresure, s => s.Item1.Co1LowOutletPresure)
            .Map(d => d.Co1HeatCurveTemp, s => s.Item1.Co1HeatCurveTemp)
            .Map(d => d.Co1PumpStatus, s => s.Item1.Co1PumpStatus)
            .Map(d => d.Co1Status, s => s.Item1.Co1Status)
            .Map(d => d.CwuTemp, s => s.Item1.CwuTemp)
            .Map(d => d.CwuTempSet, s => s.Item1.CwuTempSet)
            .Map(d => d.CwuCirculationTemp, s => s.Item1.CwuCirculationTemp)
            .Map(d => d.CwuPumpStatus, s => s.Item1.CwuPumpStatus)
            .Map(d => d.CwuStatus, s => s.Item1.CwuStatus)
            .Map(d => d.Name, s => s.Item2.Name)
            .Ignore(x => x.IsDelayed);

         cfg.NewConfig<Rvd145, Rvd145ChartDto>();

         cfg.NewConfig<IEnumerable<Rvd145>, Rvd145ReportDto>()
            .Map(d => d.Date, s => s.Min(x => x.Date))
            .Map(d => d.DeviceId, s => s.Min(x => x.DeviceId))
            .Map(d => d.OutsideTempAvg, s => s.Average(x => x.OutsideTemp))
            .Map(d => d.OutsideTempMin, s => s.Min(x => x.OutsideTemp))
            .Map(d => d.OutsideTempMax, s => s.Max(x => x.OutsideTemp))
            .Map(d => d.CoHighInletPresureAvg, s => s.Average(x => x.CoHighInletPresure))
            .Map(d => d.CoHighInletPresureMin, s => s.Min(x => x.CoHighInletPresure))
            .Map(d => d.CoHighInletPresureMax, s => s.Max(x => x.CoHighInletPresure))
            .Map(d => d.Co1LowInletTempAvg, s => s.Average(x => x.Co1LowInletTemp))
            .Map(d => d.Co1LowInletTempMin, s => s.Min(x => x.Co1LowInletTemp))
            .Map(d => d.Co1LowInletTempMax, s => s.Max(x => x.Co1LowInletTemp))
            .Map(d => d.Co1LowOutletPresureAvg, s => s.Average(x => x.Co1LowOutletPresure))
            .Map(d => d.Co1LowOutletPresureMin, s => s.Min(x => x.Co1LowOutletPresure))
            .Map(d => d.Co1LowOutletPresureMax, s => s.Max(x => x.Co1LowOutletPresure))
            .Map(d => d.CwuTempAvg, s => s.Average(x => x.CwuTemp))
            .Map(d => d.CwuTempMin, s => s.Min(x => x.CwuTemp))
            .Map(d => d.CwuTempMax, s => s.Max(x => x.CwuTemp))
            .Map(d => d.CwuCirculationTempAvg, s => s.Average(x => x.CwuCirculationTemp))
            .Map(d => d.CwuCirculationTempMin, s => s.Min(x => x.CwuCirculationTemp))
            .Map(d => d.CwuCirculationTempMax, s => s.Max(x => x.CwuCirculationTemp));

         return cfg;
      }
   }
}

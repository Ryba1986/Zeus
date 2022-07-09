using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using Zeus.Domain.Devices;
using Zeus.Domain.Locations;
using Zeus.Domain.Plcs.Meters;
using Zeus.Domain.Plcs.Rvds;
using Zeus.Domain.Users;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Locations.Dto;
using Zeus.Models.Plcs.Meters.Dto;
using Zeus.Models.Plcs.Rvds.Dto;
using Zeus.Models.Users.Dto;

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

         cfg.NewConfig<Device, DeviceReportDto>()
            .Ignore(x => x.IsPlc)
            .Ignore(x => x.IsCo1)
            .Ignore(x => x.IsCo2)
            .Ignore(x => x.IsCwu)
            .Ignore(x => x.PlcType);

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

         cfg.NewConfig<Meter, MeterReportDto>();

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

         cfg.NewConfig<Rvd145, Rvd145ReportDto>();

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

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Rvds;
using Zeus.Models.Plcs.Rvds.Dto;

namespace Zeus.Infrastructure.Configuration.Mappers.Profiles
{
   internal sealed class RvdProfile : Profile
   {
      public RvdProfile()
      {
         CreateMap<Tuple<Rvd145, Device>, Rvd145Dto>()
            .ForMember(dst => dst.DeviceId, src => src.MapFrom((s, d) => s.Item1.DeviceId))
            .ForMember(dst => dst.Date, src => src.MapFrom((s, d) => s.Item1.Date))
            .ForMember(dst => dst.CoHighInletPresure, src => src.MapFrom((s, d) => s.Item1.CoHighInletPresure))
            .ForMember(dst => dst.Alarm, src => src.MapFrom((s, d) => s.Item1.Alarm))
            .ForMember(dst => dst.CoHighOutletTemp, src => src.MapFrom((s, d) => s.Item1.CoHighOutletTemp))
            .ForMember(dst => dst.CoLowInletTemp, src => src.MapFrom((s, d) => s.Item1.CoLowInletTemp))
            .ForMember(dst => dst.CoLowOutletPresure, src => src.MapFrom((s, d) => s.Item1.CoLowOutletPresure))
            .ForMember(dst => dst.CoHeatCurveTemp, src => src.MapFrom((s, d) => s.Item1.CoHeatCurveTemp))
            .ForMember(dst => dst.CoPumpStatus, src => src.MapFrom((s, d) => s.Item1.CoPumpStatus))
            .ForMember(dst => dst.CoStatus, src => src.MapFrom((s, d) => s.Item1.CoStatus))
            .ForMember(dst => dst.CwuTemp, src => src.MapFrom((s, d) => s.Item1.CwuTemp))
            .ForMember(dst => dst.CwuTempSet, src => src.MapFrom((s, d) => s.Item1.CwuTempSet))
            .ForMember(dst => dst.CwuCirculationTemp, src => src.MapFrom((s, d) => s.Item1.CwuCirculationTemp))
            .ForMember(dst => dst.CwuPumpStatus, src => src.MapFrom((s, d) => s.Item1.CwuPumpStatus))
            .ForMember(dst => dst.CwuStatus, src => src.MapFrom((s, d) => s.Item1.CwuStatus))
            .ForMember(dst => dst.Name, src => src.MapFrom((s, d) => s.Item2.Name))
            .ForMember(dst => dst.IsDelayed, src => src.Ignore());

         CreateMap<Rvd145, Rvd145ChartDto>();

         CreateMap<IEnumerable<Rvd145>, Rvd145ReportDto>()
            .ForMember(dst => dst.Date, src => src.MapFrom((s, d) => s.Min(x => x.Date)))
            .ForMember(dst => dst.OutsideTempAvg, src => src.MapFrom((s, d) => s.Average(x => x.OutsideTemp)))
            .ForMember(dst => dst.OutsideTempMin, src => src.MapFrom((s, d) => s.Min(x => x.OutsideTemp)))
            .ForMember(dst => dst.OutsideTempMax, src => src.MapFrom((s, d) => s.Max(x => x.OutsideTemp)))
            .ForMember(dst => dst.CoHighInletPresureAvg, src => src.MapFrom((s, d) => s.Average(x => x.CoHighInletPresure)))
            .ForMember(dst => dst.CoHighInletPresureMin, src => src.MapFrom((s, d) => s.Min(x => x.CoHighInletPresure)))
            .ForMember(dst => dst.CoHighInletPresureMax, src => src.MapFrom((s, d) => s.Max(x => x.CoHighInletPresure)))
            .ForMember(dst => dst.CoLowInletTempAvg, src => src.MapFrom((s, d) => s.Average(x => x.CoLowInletTemp)))
            .ForMember(dst => dst.CoLowInletTempMin, src => src.MapFrom((s, d) => s.Min(x => x.CoLowInletTemp)))
            .ForMember(dst => dst.CoLowInletTempMax, src => src.MapFrom((s, d) => s.Max(x => x.CoLowInletTemp)))
            .ForMember(dst => dst.CoLowOutletPresureAvg, src => src.MapFrom((s, d) => s.Average(x => x.CoLowOutletPresure)))
            .ForMember(dst => dst.CoLowOutletPresureMin, src => src.MapFrom((s, d) => s.Min(x => x.CoLowOutletPresure)))
            .ForMember(dst => dst.CoLowOutletPresureMax, src => src.MapFrom((s, d) => s.Max(x => x.CoLowOutletPresure)))
            .ForMember(dst => dst.CwuTempAvg, src => src.MapFrom((s, d) => s.Average(x => x.CwuTemp)))
            .ForMember(dst => dst.CwuTempMin, src => src.MapFrom((s, d) => s.Min(x => x.CwuTemp)))
            .ForMember(dst => dst.CwuTempMax, src => src.MapFrom((s, d) => s.Max(x => x.CwuTemp)))
            .ForMember(dst => dst.CwuCirculationTempAvg, src => src.MapFrom((s, d) => s.Average(x => x.CwuCirculationTemp)))
            .ForMember(dst => dst.CwuCirculationTempMin, src => src.MapFrom((s, d) => s.Min(x => x.CwuCirculationTemp)))
            .ForMember(dst => dst.CwuCirculationTempMax, src => src.MapFrom((s, d) => s.Max(x => x.CwuCirculationTemp)));
      }
   }
}

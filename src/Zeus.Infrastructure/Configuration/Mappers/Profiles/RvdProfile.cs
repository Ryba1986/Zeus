using System;
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
         CreateProjection<Tuple<Rvd145, Device>, Rvd145Dto>()
            .ForMember(dst => dst.DeviceId, opt => opt.MapFrom(src => src.Item1.DeviceId))
            .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Item1.Date))
            .ForMember(dst => dst.CoHighInletPresure, opt => opt.MapFrom(src => src.Item1.CoHighInletPresure))
            .ForMember(dst => dst.Alarm, opt => opt.MapFrom(src => src.Item1.Alarm))
            .ForMember(dst => dst.CoHighOutletTemp, opt => opt.MapFrom(src => src.Item1.CoHighOutletTemp))
            .ForMember(dst => dst.CoLowInletTemp, opt => opt.MapFrom(src => src.Item1.CoLowInletTemp))
            .ForMember(dst => dst.CoLowOutletPresure, opt => opt.MapFrom(src => src.Item1.CoLowOutletPresure))
            .ForMember(dst => dst.CoHeatCurveTemp, opt => opt.MapFrom(src => src.Item1.CoHeatCurveTemp))
            .ForMember(dst => dst.CoPumpStatus, opt => opt.MapFrom(src => src.Item1.CoPumpStatus))
            .ForMember(dst => dst.CoStatus, opt => opt.MapFrom(src => src.Item1.CoStatus))
            .ForMember(dst => dst.CwuTemp, opt => opt.MapFrom(src => src.Item1.CwuTemp))
            .ForMember(dst => dst.CwuTempSet, opt => opt.MapFrom(src => src.Item1.CwuTempSet))
            .ForMember(dst => dst.CwuCirculationTemp, opt => opt.MapFrom(src => src.Item1.CwuCirculationTemp))
            .ForMember(dst => dst.CwuPumpStatus, opt => opt.MapFrom(src => src.Item1.CwuPumpStatus))
            .ForMember(dst => dst.CwuStatus, opt => opt.MapFrom(src => src.Item1.CwuStatus))
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item2.Name))
            .ForMember(dst => dst.IsDelayed, opt => opt.Ignore());

         CreateProjection<Rvd145, Rvd145ChartDto>();
      }
   }
}

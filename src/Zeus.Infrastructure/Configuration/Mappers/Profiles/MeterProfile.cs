using System;
using AutoMapper;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Meters;
using Zeus.Models.Plcs.Meters.Dto;

namespace Zeus.Infrastructure.Configuration.Mappers.Profiles
{
   internal sealed class MeterProfile : Profile
   {
      public MeterProfile()
      {
         CreateProjection<Tuple<Meter, Device>, MeterDto>()
            .ForMember(dst => dst.DeviceId, opt => opt.MapFrom(src => src.Item1.DeviceId))
            .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Item1.Date))
            .ForMember(dst => dst.InletTemp, opt => opt.MapFrom(src => src.Item1.InletTemp))
            .ForMember(dst => dst.OutletTemp, opt => opt.MapFrom(src => src.Item1.OutletTemp))
            .ForMember(dst => dst.Power, opt => opt.MapFrom(src => src.Item1.Power))
            .ForMember(dst => dst.Volume, opt => opt.MapFrom(src => src.Item1.Volume))
            .ForMember(dst => dst.VolumeSummary, opt => opt.MapFrom(src => src.Item1.VolumeSummary))
            .ForMember(dst => dst.EnergySummary, opt => opt.MapFrom(src => src.Item1.EnergySummary))
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item2.Name))
            .ForMember(dst => dst.IsDelayed, opt => opt.Ignore());

         CreateProjection<Meter, MeterChartDto>();
      }
   }
}

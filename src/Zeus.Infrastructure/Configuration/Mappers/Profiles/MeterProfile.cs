using System;
using System.Collections.Generic;
using System.Linq;
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
         CreateMap<Tuple<Meter, Device>, MeterDto>()
            .ForMember(dst => dst.DeviceId, src => src.MapFrom((s, d) => s.Item1.DeviceId))
            .ForMember(dst => dst.Date, src => src.MapFrom((s, d) => s.Item1.Date))
            .ForMember(dst => dst.InletTemp, src => src.MapFrom((s, d) => s.Item1.InletTemp))
            .ForMember(dst => dst.OutletTemp, src => src.MapFrom((s, d) => s.Item1.OutletTemp))
            .ForMember(dst => dst.Power, src => src.MapFrom((s, d) => s.Item1.Power))
            .ForMember(dst => dst.Volume, src => src.MapFrom((s, d) => s.Item1.Volume))
            .ForMember(dst => dst.VolumeSummary, src => src.MapFrom((s, d) => s.Item1.VolumeSummary))
            .ForMember(dst => dst.EnergySummary, src => src.MapFrom((s, d) => s.Item1.EnergySummary))
            .ForMember(dst => dst.Name, src => src.MapFrom((s, d) => s.Item2.Name))
            .ForMember(dst => dst.IsDelayed, src => src.Ignore());

         CreateMap<Meter, MeterChartDto>();

         CreateMap<IEnumerable<Meter>, MeterReportDto>()
            .ForMember(dst => dst.Date, src => src.MapFrom((s, d) => s.Min(x => x.Date)))
            .ForMember(dst => dst.InletTempAvg, src => src.MapFrom((s, d) => s.Average(x => x.InletTemp)))
            .ForMember(dst => dst.InletTempMin, src => src.MapFrom((s, d) => s.Min(x => x.InletTemp)))
            .ForMember(dst => dst.InletTempMax, src => src.MapFrom((s, d) => s.Max(x => x.InletTemp)))
            .ForMember(dst => dst.OutletTempAvg, src => src.MapFrom((s, d) => s.Average(x => x.OutletTemp)))
            .ForMember(dst => dst.OutletTempMin, src => src.MapFrom((s, d) => s.Min(x => x.OutletTemp)))
            .ForMember(dst => dst.OutletTempMax, src => src.MapFrom((s, d) => s.Max(x => x.OutletTemp)))
            .ForMember(dst => dst.PowerAvg, src => src.MapFrom((s, d) => s.Average(x => x.Power)))
            .ForMember(dst => dst.PowerMin, src => src.MapFrom((s, d) => s.Min(x => x.Power)))
            .ForMember(dst => dst.PowerMax, src => src.MapFrom((s, d) => s.Max(x => x.Power)))
            .ForMember(dst => dst.VolumeAvg, src => src.MapFrom((s, d) => s.Average(x => x.Volume)))
            .ForMember(dst => dst.VolumeMin, src => src.MapFrom((s, d) => s.Min(x => x.Volume)))
            .ForMember(dst => dst.VolumeMax, src => src.MapFrom((s, d) => s.Max(x => x.Volume)))
            .ForMember(dst => dst.VolumeSummary, src => src.MapFrom((s, d) => s.Max(x => x.VolumeSummary)))
            .ForMember(dst => dst.EnergySummary, src => src.MapFrom((s, d) => s.Max(x => x.EnergySummary)));
      }
   }
}

using System;
using AutoMapper;
using Zeus.Domain.Devices;
using Zeus.Domain.Users;
using Zeus.Models.Devices.Dto;

namespace Zeus.Infrastructure.Configuration.Mappers.Profiles
{
   internal sealed class DeviceProfile : Profile
   {
      public DeviceProfile()
      {
         CreateMap<Device, DeviceDto>();

         CreateMap<Tuple<DeviceHistory, User>, DeviceHistoryDto>()
            .ForMember(dst => dst.Name, src => src.MapFrom((s, d) => s.Item1.Name))
            .ForMember(dst => dst.LocationName, src => src.MapFrom((s, d) => s.Item1.LocationName))
            .ForMember(dst => dst.ModbusId, src => src.MapFrom((s, d) => s.Item1.ModbusId))
            .ForMember(dst => dst.Type, src => src.MapFrom((s, d) => s.Item1.Type))
            .ForMember(dst => dst.RsBoundRate, src => src.MapFrom((s, d) => s.Item1.RsBoundRate))
            .ForMember(dst => dst.RsDataBits, src => src.MapFrom((s, d) => s.Item1.RsDataBits))
            .ForMember(dst => dst.RsParity, src => src.MapFrom((s, d) => s.Item1.RsParity))
            .ForMember(dst => dst.RsStopBits, src => src.MapFrom((s, d) => s.Item1.RsStopBits))
            .ForMember(dst => dst.IncludeReport, src => src.MapFrom((s, d) => s.Item1.IncludeReport))
            .ForMember(dst => dst.IsActive, src => src.MapFrom((s, d) => s.Item1.IsActive))
            .ForMember(dst => dst.CreatedByName, src => src.MapFrom((s, d) => s.Item2.Name))
            .ForMember(dst => dst.CreateDate, src => src.MapFrom((s, d) => s.Item1.CreateDate));

         CreateMap<Device, DeviceReportDto>()
            .ForMember(dst => dst.IsPlc, src => src.Ignore())
            .ForMember(dst => dst.IsCo, src => src.Ignore())
            .ForMember(dst => dst.IsCwu, src => src.Ignore())
            .ForMember(dst => dst.PlcType, src => src.Ignore());
      }
   }
}

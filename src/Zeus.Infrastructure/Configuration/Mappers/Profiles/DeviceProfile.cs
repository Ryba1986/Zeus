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
         CreateProjection<Device, DeviceDto>();

         CreateProjection<Tuple<DeviceHistory, User>, DeviceHistoryDto>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.Name))
            .ForMember(dst => dst.LocationName, opt => opt.MapFrom(src => src.Item1.LocationName))
            .ForMember(dst => dst.ModbusId, opt => opt.MapFrom(src => src.Item1.ModbusId))
            .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Item1.Type))
            .ForMember(dst => dst.RsBoundRate, opt => opt.MapFrom(src => src.Item1.RsBoundRate))
            .ForMember(dst => dst.RsDataBits, opt => opt.MapFrom(src => src.Item1.RsDataBits))
            .ForMember(dst => dst.RsParity, opt => opt.MapFrom(src => src.Item1.RsParity))
            .ForMember(dst => dst.RsStopBits, opt => opt.MapFrom(src => src.Item1.RsStopBits))
            .ForMember(dst => dst.IncludeReport, opt => opt.MapFrom(src => src.Item1.IncludeReport))
            .ForMember(dst => dst.IsActive, opt => opt.MapFrom(src => src.Item1.IsActive))
            .ForMember(dst => dst.CreatedByName, opt => opt.MapFrom(src => src.Item2.Name))
            .ForMember(dst => dst.CreateDate, opt => opt.MapFrom(src => src.Item1.CreateDate));

         CreateProjection<Device, DeviceReportDto>()
            .ForMember(dst => dst.IsPlc, opt => opt.Ignore())
            .ForMember(dst => dst.IsCo, opt => opt.Ignore())
            .ForMember(dst => dst.IsCwu, opt => opt.Ignore())
            .ForMember(dst => dst.PlcType, opt => opt.Ignore());
      }
   }
}

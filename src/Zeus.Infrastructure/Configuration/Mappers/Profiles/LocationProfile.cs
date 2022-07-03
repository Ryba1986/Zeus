using System;
using AutoMapper;
using Zeus.Domain.Locations;
using Zeus.Domain.Users;
using Zeus.Models.Locations.Dto;

namespace Zeus.Infrastructure.Configuration.Mappers.Profiles
{
   internal sealed class LocationProfile : Profile
   {
      public LocationProfile()
      {
         CreateMap<Location, LocationDto>();

         CreateMap<Tuple<LocationHistory, User>, LocationHistoryDto>()
            .ForMember(dst => dst.Name, src => src.MapFrom((s, d) => s.Item1.Name))
            .ForMember(dst => dst.MacAddress, src => src.MapFrom((s, d) => s.Item1.MacAddress))
            .ForMember(dst => dst.IncludeReport, src => src.MapFrom((s, d) => s.Item1.IncludeReport))
            .ForMember(dst => dst.IsActive, src => src.MapFrom((s, d) => s.Item1.IsActive))
            .ForMember(dst => dst.CreatedByName, src => src.MapFrom((s, d) => s.Item2.Name))
            .ForMember(dst => dst.CreateDate, src => src.MapFrom((s, d) => s.Item1.CreateDate));

         CreateMap<Location, LocationReportDto>();
      }
   }
}

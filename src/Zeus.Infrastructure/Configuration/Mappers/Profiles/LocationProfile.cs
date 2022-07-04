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
         CreateProjection<Location, LocationDto>();

         CreateProjection<Tuple<LocationHistory, User>, LocationHistoryDto>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.Name))
            .ForMember(dst => dst.MacAddress, opt => opt.MapFrom(src => src.Item1.MacAddress))
            .ForMember(dst => dst.IncludeReport, opt => opt.MapFrom(src => src.Item1.IncludeReport))
            .ForMember(dst => dst.IsActive, opt => opt.MapFrom(src => src.Item1.IsActive))
            .ForMember(dst => dst.CreatedByName, opt => opt.MapFrom(src => src.Item2.Name))
            .ForMember(dst => dst.CreateDate, opt => opt.MapFrom(src => src.Item1.CreateDate));

         CreateProjection<Location, LocationReportDto>();
      }
   }
}

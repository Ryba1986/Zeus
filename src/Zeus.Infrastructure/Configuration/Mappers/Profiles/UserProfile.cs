using System;
using AutoMapper;
using Zeus.Domain.Users;
using Zeus.Models.Users.Dto;

namespace Zeus.Infrastructure.Configuration.Mappers.Profiles
{
   internal sealed class UserProfile : Profile
   {
      public UserProfile()
      {
         CreateProjection<User, UserDto>();

         CreateProjection<Tuple<UserHistory, User>, UserHistoryDto>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.Name))
            .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Item1.Email))
            .ForMember(dst => dst.Role, opt => opt.MapFrom(src => src.Item1.Role))
            .ForMember(dst => dst.IsActive, opt => opt.MapFrom(src => src.Item1.IsActive))
            .ForMember(dst => dst.CreatedByName, opt => opt.MapFrom(src => src.Item2.Name))
            .ForMember(dst => dst.CreateDate, opt => opt.MapFrom(src => src.Item1.CreateDate));
      }
   }
}

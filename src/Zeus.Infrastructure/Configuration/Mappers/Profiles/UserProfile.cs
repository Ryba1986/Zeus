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
         CreateMap<User, UserDto>();

         CreateMap<Tuple<UserHistory, User>, UserHistoryDto>()
            .ForMember(dst => dst.Name, src => src.MapFrom((s, d) => s.Item1.Name))
            .ForMember(dst => dst.Email, src => src.MapFrom((s, d) => s.Item1.Email))
            .ForMember(dst => dst.Role, src => src.MapFrom((s, d) => s.Item1.Role))
            .ForMember(dst => dst.IsActive, src => src.MapFrom((s, d) => s.Item1.IsActive))
            .ForMember(dst => dst.CreatedByName, src => src.MapFrom((s, d) => s.Item2.Name))
            .ForMember(dst => dst.CreateDate, src => src.MapFrom((s, d) => s.Item1.CreateDate));
      }
   }
}

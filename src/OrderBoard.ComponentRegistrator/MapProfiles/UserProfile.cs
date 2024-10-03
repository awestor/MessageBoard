using AutoMapper;
using OrderBoard.Contracts.UserDto;
using OrderBoard.Domain.Entities;

namespace OrderBoard.ComponentRegistrator.MapProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<EntUser, UserInfoModel>()
                .ForMember(s => s.Id, map => map.MapFrom(s => s.Id))
                .ForMember(s => s.Login, map => map.MapFrom(s => s.Login))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => s.CreatedAt))
                .ForMember(s => s.Description, map => map.MapFrom(s => s.Description));

            CreateMap<UserCreateModel, EntUser>(MemberList.None)
                .ForMember(s => s.Email, map => map.MapFrom(s => s.Email))
                .ForMember(s => s.Login, map => map.MapFrom(s => s.Login))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
                .ForMember(s => s.Password, map => map.MapFrom(s => s.Password));
        }
    }
}
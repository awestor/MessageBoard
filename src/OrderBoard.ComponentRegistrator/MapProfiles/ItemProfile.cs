using AutoMapper;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.UserDto;
using OrderBoard.Domain;
using OrderBoard.Domain.Entities;

namespace OrderBoard.ComponentRegistrator.MapProfiles
{
    public class ItemProfile: Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemInfoModel>()
                .ForMember(s => s.UserId, map => map.MapFrom(s => s.UserId))
                .ForMember(s => s.Name, map => map.MapFrom(s => s.Name))
                .ForMember(s => s.Count, map => map.MapFrom(s => s.Count))
                .ForMember(s => s.Price, map => map.MapFrom(s => s.Price))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => s.CreatedAt))
                .ForMember(s => s.Description, map => map.MapFrom(s => s.Description))
                .ForMember(s => s.Comment, map => map.MapFrom(s => s.Comment));

            CreateMap<ItemCreateModel, Item>(MemberList.None)
                .ForMember(s => s.Name, map => map.MapFrom(s => s.Name))
                .ForMember(s => s.Count, map => map.MapFrom(s => s.Count))
                .ForMember(s => s.Price, map => map.MapFrom(s => s.Price))
                .ForMember(s => s.UserId, map => map.MapFrom(s => s.UserId))
                .ForMember(s => s.CategoryId, map => map.MapFrom(s => s.CategoryId))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

            CreateMap<Item, ItemDataModel>();
        }
    }
}

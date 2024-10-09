using AutoMapper;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Domain.Entities;

namespace OrderBoard.ComponentRegistrator.MapProfiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemInfoModel>()
                .ForMember(s => s.ItemId, map => map.MapFrom(s => s.ItemId))
                .ForMember(s => s.Count, map => map.MapFrom(s => s.Count))
                .ForMember(s => s.OrderPrice, map => map.MapFrom(s => s.OrderPrice));

            CreateMap<OrderItemCreateModel, OrderItem>(MemberList.None)
                .ForMember(s => s.ItemId, map => map.MapFrom(s => s.ItemId))
                .ForMember(s => s.Count, map => map.MapFrom(s => s.Count))
                .ForMember(s => s.OrderId, map => map.MapFrom(s => s.OrderId));


            CreateMap<OrderItemDataModel, OrderItem>(MemberList.None)
                .ForMember(s => s.Id, map => map.MapFrom(s => s.Id));
            CreateMap<OrderItem, OrderItemDataModel>();
        }
    }
}

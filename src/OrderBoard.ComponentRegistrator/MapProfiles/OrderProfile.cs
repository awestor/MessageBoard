using AutoMapper;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;

namespace OrderBoard.ComponentRegistrator.MapProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDataModel>()
                .ForMember(s => s.TotalPrice, map => map.MapFrom(s => s.TotalPrice))
                .ForMember(s => s.TotalCount, map => map.MapFrom(s => s.TotalCount));

            CreateMap<Order, OrderInfoModel>()
                .ForMember(s => s.UserId, map => map.MapFrom(s => s.UserId));


            CreateMap<OrderCreateModel, Order>(MemberList.None)
                .ForMember(s => s.UserId, map => map.MapFrom(s => s.UserId))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
                .ForMember(s => s.PaidAt, map => map.MapFrom(s => DateTime.UtcNow));
        }
    }
}

using AutoMapper;
using OrderBoard.Contracts.Orders;
using OrderBoard.Contracts.Orders.Requests;
using OrderBoard.Domain.Entities;

namespace OrderBoard.ComponentRegistrator.MapProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDataModel>();

            CreateMap<OrderDataModel, Order>(MemberList.None)
                .ForMember(s => s.Id, map => map.MapFrom(s => s.Id));

            CreateMap<Order, OrderInfoModel>(MemberList.None)
                .ForMember(s => s.UserId, map => map.MapFrom(s => s.UserId));


            CreateMap<OrderCreateModel, Order>(MemberList.None)
                .ForMember(s => s.UserId, map => map.MapFrom(s => s.UserId))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

            CreateMap<SearchOrderAuthRequest, SearchOrderRequest>(MemberList.None);
        }
    }
}

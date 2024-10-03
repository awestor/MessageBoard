using AutoMapper;
using OrderBoard.Contracts.Categories;
using OrderBoard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.ComponentRegistrator.MapProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryInfoModel>()
                .ForMember(s => s.Id, map => map.MapFrom(s => s.Id))
                .ForMember(s => s.Created, map => map.MapFrom(s => s.Created))
                .ForMember(s => s.Name, map => map.MapFrom(s => s.Name))
                .ForMember(s => s.Description, map => map.MapFrom(s => s.Description));

            CreateMap<CategoryCreateModel, Category>(MemberList.None)
                .ForMember(s => s.Name, map => map.MapFrom(s => s.Title))
                .ForMember(s => s.Description, map => map.MapFrom(s => s.Description))
                .ForMember(s => s.Created, map => map.MapFrom(s => DateTime.UtcNow));
        }
    }
}
using AutoMapper;
using OrderBoard.Contracts.Files;
using OrderBoard.Contracts.Items;
using OrderBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.ComponentRegistrator.MapProfiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<FileContent, FileInfoModel>();

            CreateMap<FileCreateModel, FileContent>(MemberList.None)
                .ForMember(s => s.Length, map => map.MapFrom(s => s.Content.Length))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

            CreateMap<FileContent, FileDataModel>();

            CreateMap<FileDataModel, FileContent>(MemberList.None)
                .ForMember(s => s.Id, map => map.MapFrom(s => s.Id));
        }
    }
}

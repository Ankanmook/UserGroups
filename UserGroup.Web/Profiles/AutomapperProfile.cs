using AutoMapper;
using UseGroup.DataModel.Models;

namespace UserGroup.Web.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Person, Common.DTO.PersonDto>().ReverseMap();
            CreateMap<Person, Common.DTO.PersonCreationDto>().ReverseMap();
            CreateMap<Person, Common.DTO.PersonUpdateDto>().ReverseMap();

            CreateMap<Group, Common.DTO.GroupDto>().ReverseMap();
            CreateMap<Group, Common.DTO.GroupCreationDto>().ReverseMap();
            CreateMap<Group, Common.DTO.GroupUpdateDto>().ReverseMap();//redundant
        }
    }
}
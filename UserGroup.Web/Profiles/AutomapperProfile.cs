using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UseGroup.DataModel;
using UseGroup.DataModel.Models;

namespace UserGroup.Web.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Person, Common.DTO.PersonDto>();
            CreateMap<Person, Common.DTO.PersonForCreationDto>();
            CreateMap<Person, Common.DTO.PersonForUpdateDto>();

            CreateMap<Group, Common.DTO.GroupDto>();
            //CreateMap<Group, Common.DTO.GroupForCreationDto>();
            //CreateMap<Group, Common.DTO.GroupForUpdateDto>();


        }
    }
}

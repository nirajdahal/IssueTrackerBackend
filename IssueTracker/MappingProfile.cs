using AutoMapper;
using Library.Entities.DTO;
using Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap< RegisterModel, RegisterModelDto>();
            CreateMap<ApplicationUser, LoginModelDto>();
        }
    }

}

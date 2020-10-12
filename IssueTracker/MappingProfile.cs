using AutoMapper;
using Library.Entities.DTO;
using Library.Entities.DTO.ProjectDto;
using Library.Entities.Models;
using Library.Entities.Models.Projects;

namespace IssueTracker
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterModelDto, RegisterModel>();
            CreateMap<LoginModelDto, LoginModel>();
            CreateMap<ProjectForCreation, Project>();
        }
    }

}

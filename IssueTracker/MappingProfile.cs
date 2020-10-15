using AutoMapper;
using Library.Entities.DTO;
using Library.Entities.DTO.ProjectDto;
using Library.Entities.DTO.TicketDto;
using Library.Entities.Models;
using Library.Entities.Models.Projects;
using Library.Entities.Models.Tickets;

namespace IssueTracker
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterModelDto, RegisterModel>();
            CreateMap<LoginModelDto, LoginModel>();
            CreateMap<ProjectForCreation, Project>();
            CreateMap<TicketForCreationDto, Ticket>()
                .ForMember(dest => dest.TPriorityId, opt => opt.MapFrom(src => src.TPriorityId))
                .ForMember(dest => dest.TTypeId, opt => opt.MapFrom(src => src.TTypeId))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.TStatusId, opt => opt.MapFrom(src => src.TStatusId));
            CreateMap<TicketForUpdateDto, Ticket>();
        }
    }
}
using AutoMapper;
using Library.Entities.DTO;
using Library.Entities.DTO.ProjectDto;
using Library.Entities.DTO.TicketDto;
using Library.Entities.DTO.UserDto;
using Library.Entities.DTO.UserProjectsDto;
using Library.Entities.DTO.UsersTicketsDto;
using Library.Entities.Models;
using Library.Entities.Models.Projects;
using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersProjects;
using Library.Entities.Models.UsersTickets;

namespace IssueTracker
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterModelDto, RegisterModel>();
            CreateMap<LoginModelDto, LoginModel>();
            CreateMap<ApplicationUser, ApplicationUserVm>()
                .ForMember(dest => dest.userEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.Name));
            CreateMap<ApplicationUser, UserVm>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<UserTicket, TicketsOfUsersDto>()
               .ForMember(dest => dest.TicketVm, opt => opt.MapFrom(src => src.Ticket));

            //Ticket Dtos
            CreateMap<TicketForCreationDto, Ticket>()
                .ForMember(dest => dest.TPriorityId, opt => opt.MapFrom(src => src.TPriorityId))
                .ForMember(dest => dest.TTypeId, opt => opt.MapFrom(src => src.TTypeId))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.TStatusId, opt => opt.MapFrom(src => src.TStatusId)).ReverseMap();
            CreateMap<TicketForUpdateDto, Ticket>();
            CreateMap<TicketPriority, TicketPriorityVmDto>().ReverseMap();
            CreateMap<TicketComment, TicketCommentVmDto>().ReverseMap();
            CreateMap<TicketStatus, TicketStatusVmDto>().ReverseMap();
            CreateMap<TicketType, TicketTypeVmDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<UserTicket, UserTicketVmDto>().ReverseMap();
            CreateMap<Project, ProjectForTicketDto>();

            CreateMap<Ticket, UserTicketVmDto>();
            CreateMap<Ticket, GetAllTicketVmDto>()
                .ForMember(dest => dest.TicketPriorityVm, opt => opt.MapFrom(src => src.TicketPriority))
                .ForMember(dest => dest.TicketStatusVm, opt => opt.MapFrom(src => src.TicketStatus))
                .ForMember(dest => dest.TicketTypeVm, opt => opt.MapFrom(src => src.TicketType))
                .ForMember(dest => dest.ProjectVm, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.UsersTicketsVm, opt => opt.MapFrom(src => src.UsersTickets));

            //Project Dto
            CreateMap<ProjectForCreation, Project>();
            CreateMap<ProjectForUpdateDto, Project>();
            CreateMap<Ticket, TicketForProjectDto>();
            CreateMap<UserProject, UserProjectVmDto>().ReverseMap();
            CreateMap<ProjectManager, ProjectManagerVmDto>();
            CreateMap<Project, ProjectVmDto>()
                .ForMember(dest => dest.TicketVm, opt => opt.MapFrom(src => src.Ticket))
                .ForMember(dest => dest.UsersProjects, opt => opt.MapFrom(src => src.UsersProjects))
                .ForMember(dest => dest.ProjectManagers, opt => opt.MapFrom(src => src.ProjectManagers))
                ;

            CreateMap<Project, ProjectIdNameVm>();
        }
    }
}
﻿using AutoMapper;
using Library.Entities.DTO;
using Library.Entities.DTO.ProjectDto;
using Library.Entities.DTO.TicketDto;
using Library.Entities.DTO.UserDto;
using Library.Entities.DTO.UsersTicketsDto;
using Library.Entities.Models;
using Library.Entities.Models.Projects;
using Library.Entities.Models.Tickets;
using Library.Entities.Models.UsersTickets;

namespace IssueTracker
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterModelDto, RegisterModel>();
            CreateMap<LoginModelDto, LoginModel>();
            CreateMap<ApplicationUser, ApplicationUserVm>();

            //Ticket Dtos
            CreateMap<TicketForCreationDto, Ticket>()
                .ForMember(dest => dest.TPriorityId, opt => opt.MapFrom(src => src.TPriorityId))
                .ForMember(dest => dest.TTypeId, opt => opt.MapFrom(src => src.TTypeId))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.TStatusId, opt => opt.MapFrom(src => src.TStatusId));
            CreateMap<TicketForUpdateDto, Ticket>();
            CreateMap<TicketPriority, TicketPriorityVmDto>();
            CreateMap<TicketStatus, TicketStatusVmDto>();
            CreateMap<TicketType, TicketTypeVmDto>();
            CreateMap<UserTicket, UserTicketVmDto>();
            CreateMap<Project, ProjectForTicketDto>();

            CreateMap<Ticket, GetAllTicketVmDto>()
                .ForMember(dest => dest.TicketPriorityVm, opt => opt.MapFrom(src => src.TicketPriority))
                .ForMember(dest => dest.TicketStatusVm, opt => opt.MapFrom(src => src.TicketStatus))
                .ForMember(dest => dest.TicketTypeVm, opt => opt.MapFrom(src => src.TicketType))
                .ForMember(dest => dest.ProjectVm, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.UsersTicketsVm, opt => opt.MapFrom(src => src.UsersTickets));

            CreateMap<ProjectForCreation, Project>();
        }
    }
}
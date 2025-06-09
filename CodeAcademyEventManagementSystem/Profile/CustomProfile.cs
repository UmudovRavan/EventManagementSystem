using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.ViewModels.Event;
using CodeAcademyEventManagementSystem.ViewModels.EventType;
using CodeAcademyEventManagementSystem.ViewModels.Feedback;
using CodeAcademyEventManagementSystem.ViewModels.Invitation;
using CodeAcademyEventManagementSystem.ViewModels.Location;
using CodeAcademyEventManagementSystem.ViewModels.Notification;
using CodeAcademyEventManagementSystem.ViewModels.Organizer;
using CodeAcademyEventManagementSystem.ViewModels.Participation;
using CodeAcademyEventManagementSystem.ViewModels.Person;

namespace CodeAcademyEventManagementSystem.Profile
{
    public class CustomProfile : AutoMapper.Profile
    {
        public CustomProfile()
        {
            CreateMap<EventVM,Event>().ReverseMap();
            CreateMap<EventCreateVM,Event>().ReverseMap();
            CreateMap<EventEditVM, Event>().ReverseMap();
            CreateMap<EventTypeVM, EventType>().ReverseMap();
            CreateMap<EventTypeCreateVM, EventType>().ReverseMap();
            CreateMap<EventTypeEditVM, EventType>().ReverseMap();
            CreateMap<FeedbackVM, Feedback>().ReverseMap();
            CreateMap<FeedbackCreateVM, Feedback>().ReverseMap();
            CreateMap<InvitationVM, Invitation>().ReverseMap();
            CreateMap<InvitationCreateVM, Invitation>().ReverseMap();
            CreateMap<LocationVM, Location>().ReverseMap();
            CreateMap<LocationCreateVM, Location>().ReverseMap();
            CreateMap<LocationEditVM, Location>().ReverseMap();
            CreateMap<NotificationVM, Notification>().ReverseMap();
            CreateMap<NotificationCreateVM, Notification>().ReverseMap();
            CreateMap<OrganizerVM, Organizer>().ReverseMap();
            CreateMap<OrganizerCreateVM, Organizer>().ReverseMap();
            CreateMap<OrganizerEditVM, Organizer>().ReverseMap();
            CreateMap<ParticipationVM, Participation>().ReverseMap();
            CreateMap<ParticipationCreateVM, Participation>().ReverseMap();
            CreateMap<PersonVM, Person>().ReverseMap();
            CreateMap<PersonCreateVM, Person>().ReverseMap();
            CreateMap<PersonEditVM, Person>().ReverseMap();
        }
    }
}
